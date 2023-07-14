#import "UnityAppController.h"
#import "UnityAppController+ViewHandling.h"
#import "UnityAppController+Rendering.h"
#import "iPhone_Sensors.h"

#import <CoreGraphics/CoreGraphics.h>
#import <QuartzCore/QuartzCore.h>
#import <QuartzCore/CADisplayLink.h>
#import <Availability.h>

#import <OpenGLES/EAGL.h>
#import <OpenGLES/EAGLDrawable.h>
#import <OpenGLES/ES2/gl.h>
#import <OpenGLES/ES2/glext.h>

#include <mach/mach_time.h>

// MSAA_DEFAULT_SAMPLE_COUNT was moved to iPhone_GlesSupport.h
// ENABLE_INTERNAL_PROFILER and related defines were moved to iPhone_Profiler.h
// kFPS define for removed: you can use Application.targetFrameRate (30 fps by default)
// DisplayLink is the only run loop mode now - all others were removed

#include "CrashReporter.h"

#include "UI/OrientationSupport.h"
#include "UI/UnityView.h"
#include "UI/Keyboard.h"
#include "UI/SplashScreen.h"
#include "Unity/InternalProfiler.h"
#include "Unity/DisplayManager.h"
#include "Unity/EAGLContextHelper.h"
#include "Unity/GlesHelper.h"
#include "Unity/ObjCRuntime.h"
#include "PluginBase/AppDelegateListener.h"

#include <assert.h>
#include <stdbool.h>
#include <sys/types.h>
#include <unistd.h>
#include <sys/sysctl.h>

#include <sys/param.h>
#include <sys/mount.h>
#import <AVFoundation/AVCaptureDevice.h>
//#import <Photos/PHPhotoLibrary.h>
#import "Utils/UtilsCommon/UtilsCommon.h"
#import <FBSDKCoreKit/FBSDKCoreKit.h>
#import "Macro.h"
//#import "Utils/AssetManager/AssetManager.h"
#import "Utils/payment/IAPHelper.h"
#import "Utils/Statistic/Statistic.h"
//#import <iAd/ADClient.h> //量江湖sdk接入
//#import "Utils/IronSourceManager/AdManager.h"
//#import "Utils/WebView/WebLoadViewController.h"
#import <Bolts.h>
//#import <Tapjoy/Tapjoy.h>
//#import <VKSdk.h>
#import <Firebase.h>
#import <AVFoundation/AVFoundation.h>
//idfa
#import <AppTrackingTransparency/AppTrackingTransparency.h>
#import <AdSupport/ASIdentifierManager.h>
#import <StoreKit/SKAdNetwork.h>
//AppsFlyer
#import <AppsFlyerLib/AppsFlyerLib.h>
#import "AppsFlyerStat.h"
//#import <AppLovinSDK/AppLovinSDK.h>
//#import <AppsFlyerAdRevenue/AppsFlyerAdRevenue.h>
// we assume that app delegate is never changed and we can cache it, instead of re-query UIApplication every time
UnityAppController* _UnityAppController = nil;
static NSString* afConversionData = @"";
static NSNumber* afConversionFlag = @0;

static NSString* afAttributionData = @"";
static NSNumber* afAttributionFlag = @0;

// Standard Gesture Recognizers enabled on all iOS apps absorb touches close to the top and bottom of the screen.
// This sometimes causes an ~1 second delay before the touch is handled when clicking very close to the edge.
// You should enable this if you want to avoid that delay. Enabling it should not have any effect on default iOS gestures.
#define DISABLE_TOUCH_DELAYS 1

// we keep old bools around to support "old" code that might have used them
bool _ios81orNewer = false, _ios82orNewer = false, _ios83orNewer = false, _ios90orNewer = false, _ios91orNewer = false;
bool _ios100orNewer = false, _ios101orNewer = false, _ios102orNewer = false, _ios103orNewer = false;
bool _ios110orNewer = false, _ios111orNewer = false, _ios112orNewer = false;
bool _ios130orNewer = false;

// was unity rendering already inited: we should not touch rendering while this is false
bool    _renderingInited        = false;
// was unity inited: we should not touch unity api while this is false
bool    _unityAppReady          = false;
// see if there's a need to do internal player pause/resume handling
//
// Typically the trampoline code should manage this internally, but
// there are use cases, videoplayer, plugin code, etc where the player
// is paused before the internal handling comes relevant. Avoid
// overriding externally managed player pause/resume handling by
// caching the state
bool    _wasPausedExternal      = false;
// should we skip present on next draw: used in corner cases (like rotation) to fill both draw-buffers with some content
bool    _skipPresent            = false;
// was app "resigned active": some operations do not make sense while app is in background
bool    _didResignActive        = false;

// was startUnity scheduled: used to make startup robust in case of locking device
static bool _startUnityScheduled    = false;

bool    _supportsMSAA           = false;

//缓存deeplink字符串
static NSString* deeplinkStr= @"";
//缓存从推送通知启动的 Json 字符串
static NSString* launchPushDataJsonStr = nil;
//缓存的点击通知列表数据
static NSMutableArray* launchPushDataArray = [[NSMutableArray alloc] init];
//是否已经启动lua端的推送流程
static Boolean isLaunchedFromLua = false;
//网络状态
static NSString* lastStatus = @"1";
#if UNITY_SUPPORT_ROTATION
// Required to enable specific orientation for some presentation controllers: see supportedInterfaceOrientationsForWindow below for details
NSInteger _forceInterfaceOrientationMask = 0;
#endif

@implementation UnityAppController

@synthesize unityView               = _unityView;
@synthesize unityDisplayLink        = _displayLink;

@synthesize rootView                = _rootView;
@synthesize rootViewController      = _rootController;
@synthesize mainDisplay             = _mainDisplay;
@synthesize renderDelegate          = _renderDelegate;
@synthesize quitHandler             = _quitHandler;

#if UNITY_SUPPORT_ROTATION
@synthesize interfaceOrientation    = _curOrientation;
#endif

extern "C"
{
    void FacebookLogin()
    {
        [UtilsCommon doFacebookLogin];
    }
    
    void FaceBookLogOut()
    {
        [UtilsCommon doFacebookLogOut];
    }
    
    void GoogleLogin()
    {
        [UtilsCommon doGoogleLogin];
    }
    
    void GoogleLogOut()
    {
        [UtilsCommon doGoogleLogOut];
    }
    
    void AppleLogin()
    {
        UnityAppController *vc = GetAppController();
        [vc doAppleLogin];
    }
    
    void AppleLogOut()
    {
        
    }
    
    //埋点常量

    static NSString *  KEY_APP_ORDERID   =  @"_app_orderid";
    //=====================================================================
    //应用订单id(应用订单流水号)
    //static NSString*  KEY_APP_ORDERID      = @"_app_orderid";
    //平台订单id (第三方支付平台生成)
    static NSString*  KEY_CHANNEL_ORDERID  = @"_channel_orderid";
    //应用商品ID
    static NSString*  KEY_APP_SKU          = @"_app_sku";
    //平台商品ID
    static NSString*  KEY_CHANNEL_SKU      = @"_channel_sku";
    //实际支付订单价格 (单位:分) int
    static NSString*  KEY_ORDER_AMOUNT     = @"_order_amount";
    // 订单支付货币类型（USD）
    static NSString*  KEY_ORDER_CURRENCY_TYPE  = @"_order_currency_type";
    //错误码
    static NSString*  KEY_ERROR_CODE       = @"_err_code";
    //错误信息
    static NSString*  KEY_ERROR_INFO       = @"_err_info";
    //物品ID  (详见物品ID列表)
    static NSString*  KEY_ITEM_ID          = @"_item_id";
    //int 变化数量
    static NSString*  KEY_CHANGE_AMOUNT    = @"_change_amount";
    // int 变化后数量
    static NSString*  KEY_LATTER_AMOUNT    = @"_latter_amount";
    //变化原因
    static NSString*  KEY_CHANGE_REASON    = @"_change_reason";


    //应用唯一标识
    static NSString*  KEY_APP_ID           = @"_app_id";
    //APP包名
    static NSString*  Key_PackageName = @"_package_name";
    //渠道id
    static NSString*  KEY_APP_CHANNEL_ID   = @"_app_channel_id";
    //APP的应用版本(包体内置版本号 versionname)
    static NSString*  KEY_APP_VERSION      = @"_app_version";
    //应用游戏版本号
    static NSString*  KEY_APP_GAME_VERSION = @"_app_game_version";
    //应用资源版本号
    static NSString*  KEY_APP_RES_VERSION  = @"_app_res_version";
    //用安装后第一次启动随机生成 随机取值范围[10000000,99999999]  缓存在磁盘中
    static NSString*  KEY_APP_INSTALL_ID   = @"_app_install_id";
    //应用每次冷启动启动随机生成 随机取值范围[10000000,99999999] int  缓存在内存中
    static NSString*  KEY_APP_ACTIVATE_ID  = @"_app_activate_id";
    //应用语言代码
    static NSString*  KEY_APP_LANG         = @"_app_lang";
    //账户类型  游客=vistor  facebook登录=fb  google登录=gp  苹果登录=apple
    static NSString*  KEY_APP_ACCOUNT_BINDTYPE  = @"_app_account_bindtype";
    //用户id
    static NSString*  KEY_APP_USER_ID      = @"_app_user_id";
    //SDK版本
    //static NSString*  KEY_LIB_VERSION      = @"_app_sdk_version";

    //设备ID
    static NSString*  KEY_DEVICE_ID        = @"_device_id";
    //广告id
    static NSString*  KEY_AD_ID            = @"_ad_id";
    //android id
    static NSString*  KEY_ANDROID_ID       = @"_androidid";
    //ios idfv
    static NSString*  KEY_IDFV             = @"_idfv";
    //设备制造商
    static NSString*  KEY_MANUFACTURER     = @"_device_manufacturer";
    //设备品牌
    static NSString*  KEY_BRAND            = @"_device_brand";
    //设备型号
    static NSString*  KEY_MODEL            = @"_device_model";
    //设备等级
    static NSString*  KEY_DEVICE_LVL       = @"_device_lvl";
    //屏幕高度
    static NSString*  KEY_SCREEN_HEIGHT    = @"_device_screen_h";
    //屏幕宽度
    static NSString*  KEY_SCREEN_WIDTH     = @"_device_screen_w";
    //运营商名称
    static NSString*  KEY_CARRIER          = @"_device_carrier";
    //网络类型
    static NSString*  KEY_NETWORK_TYPE     = @"_device_network_type";
    //设备语言代码
    static NSString*  KEY_DEVICE_LANG      = @"_device_lang";
    //操作系统
    static NSString*  KEY_OS               = @"_os";
    //操作系统类型 （android/ios/macos/windows/...）
    static NSString*  KEY_OS_TYPE          = @"_os_type";
    //操作系统版本
    static NSString*  KEY_OS_VERSION       = @"_os_version";
    //时间戳(秒)
    static NSString*  KEY_OS_TIMESTAMP     = @"_os_timestamp";
    //时区偏移量
    static NSString*  KEY_TIMEZONE_OFFSET  = @"_os_timezone_offset";

    //其他数据 json
    static NSString*  KEY_PROPERTIES       = @"properties";

    static NSString*  KEY_SUB_EVENT_NAME       = @"_sub_event_name";

    //归因数据
    static NSString*   KEY_CONVERSION_RAW   = @"_af_conversion_raw";
    
    char* GetStatBaseData()
    {
        NSMutableDictionary *baseDic = [NSMutableDictionary dictionary];
        NSString *baseJsonStr  = @"";
//        baseDic[@"data"] = @"";
//        baseDic[@"error_code"] = 0;
//        baseDic[@"error_info"] = @"ios pay success";
        
//        baseDic[KEY_APP_ORDERID] = @"";
        baseDic[KEY_OS_TYPE] = @"ios";
        baseDic[KEY_OS_VERSION] = [[UIDevice currentDevice] systemVersion];
        
        baseDic[KEY_APP_ID] = @"cm1007";
        baseDic[KEY_APP_CHANNEL_ID] = @"AVG20001";
        NSDictionary *infoDictionary = [[NSBundle mainBundle] infoDictionary];
        NSString *app_Version = [infoDictionary objectForKey:@"CFBundleShortVersionString"];
        baseDic[KEY_APP_VERSION] = app_Version;
//
        baseDic[Key_PackageName] = [[NSBundle mainBundle] bundleIdentifier];
//
        baseDic[KEY_DEVICE_LANG] = [[NSLocale currentLocale] objectForKey:NSLocaleLanguageCode];
        
//        baseDic[KEY_CARRIER, _deviceInfoMap.get(StatConstant.KEY_CARRIER));
        
        NSString *uuid = [UtilsCommon idfvString];
        NSLog(@"获取UUID=%@",uuid);
        baseDic[KEY_DEVICE_ID] = uuid;
        NSTimeZone *timeZone = [NSTimeZone localTimeZone];
        NSInteger offset = [timeZone secondsFromGMT] / 3600.0;
//        NSTimeZone * interval = [NSTimeZone localTimeZone];
//        NSLog(@"timezone=%ld",offset);
        baseDic[KEY_TIMEZONE_OFFSET] = [NSString stringWithFormat:@"%ld" , offset];
//
//
//        //需要实时获取的参数
//        //baseDic[KEY_OS_TIMESTAMP, TimeTool.getNowTimestamp());lua重写了
//        //baseDic[KEY_NETWORK_TYPE, NetworkStateHelper.getNetworkState());//unity直接获取
//        baseDic[KEY_APP_LANG, AppInfoTool.getAppLanguage());//unity直接获取
//        //baseDic[KEY_APP_GAME_VERSION, AppInfoTool.getCodeVersion());//尽量在lua获取
//        //baseDic[KEY_APP_RES_VERSION, AppInfoTool.getResVersion());//尽量在lua获取
//
//        baseDic[KEY_APP_INSTALL_ID] = GetUUID();
//        baseDic[KEY_APP_ACTIVATE_ID, ""+AppInfoTool.getRunId());
        baseDic[KEY_AD_ID] = @"";
        baseDic[KEY_ANDROID_ID] = @"ios";
        baseDic[KEY_IDFV] = @"";
//        baseDic[KEY_APP_USER_ID, AppInfoTool.getUuid());
        if ([NSJSONSerialization isValidJSONObject:baseDic]) {
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:baseDic options:0 error:NULL];
            baseJsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            const char * str = [baseJsonStr UTF8String];
            return strdup(str);
        }
        return strdup("{}");
    }
    
    bool IsSupportAppleLogin()
    {
        if (IS_IOS13LATER)
            return true;
        return false;
    }

    bool IsNotchScreen()
    {
        return kIsBangsScreen;
    }
    
    void GetUUID()
    {
        NSString *uuid = [UtilsCommon idfvString];
        NSLog(@"获取UUID=%@",uuid);
        UnitySendMessage("CommonManager", "GetUUID", [uuid UTF8String]);
    }
    
    const char* GetDeviceId()
    {
        NSString *uuid = [UtilsCommon idfvString];
        const char * str = [uuid UTF8String];
        return strdup(str);
        
    }
    
//    const char* GetIdfa()
//    {
//        NSString *uuid = [UtilsCommon getIDFA];
//        const char * str = [uuid UTF8String];
//        return strdup(str);
//    }
    
//    const char* GetIdfv()
//    {
//        NSString *uuid = [[[UIDevice currentDevice] identifierForVendor] UUIDString];
//        const char * str = [uuid UTF8String];
//        return strdup(str);
//    }
    
    const int64_t GetFreeStorageSpace()
    {
        struct statfs buf;
        long long freespace = 100;
        if(statfs("/var", &buf) >= 0)
        {
            freespace = (long long)(buf.f_bsize * buf.f_bfree)/1024/1024;
        }
        return freespace;
    }
    
    void share(int32_t type, char* text)
    {
        NSLog(@"分析---%s",text);
        [UtilsCommon sharType:type url:[NSString stringWithFormat:@"%s",text]];
    }
    
    void share2(char* filepath, char* title, char* shareUrl)
    {
        [UtilsCommon sharType2:([NSString stringWithFormat:@"%s", filepath]) :([NSString stringWithUTF8String:title]) :([NSString stringWithFormat:@"%s", shareUrl])];
    }
    
    void initFyber()
    {
        //[[FyberManager shareInstance] initFybe];
    }
    
    void playAdsVideo()
    {
        //[[FyberManager shareInstance] Player];
    }
    
    void requestFyberVideo()
    {
        //[[FyberManager shareInstance] requestVide];
    }
    
    int32_t isFyberVideoisAvailable()
    {
        //return [[//FyberManager shareInstance] canPlay];
        return 0;
    }
    
    //return the sdk version
//    char* getIronSourceSdkVersiont()
//    {
//        const char * conent = [[[AdManager shareInstance] getIronSourceSdkVersion] UTF8String];
//        return strdup(conent);
//    }
    //initalize the ironsource sdk from unity
//    void initIronSource(char* uuid)
//    {
//        if (uuid != NULL)
//        {
//            NSString* userid=[NSString stringWithFormat:@"%s",uuid];
//            [[AdManager shareInstance] initIronSource:userid];
//        }else{
//            [[AdManager shareInstance] initIronSource:@""];
//        }
//    }
    
//    //show reward video from unity
//    void showRewardVideo(char* placementName)
//    {
//        if (placementName != NULL)
//        {
//            NSString* placeName=[NSString stringWithFormat:@"%s",placementName];
//            if (TEST_ADS) {
//                placeName = [UtilsCommon getTestAdunit:YES];
//            }
//            [[AdManager shareInstance] showRewardVideo:[UIApplication sharedApplication].delegate.window.rootViewController placeName:placeName];
//        }
//    }
    
    //check 指定的广告位是否用完
//    bool isVideoCappedForPlacement(char* placement)
//    {
//        if (placement != NULL)
//        {
//            NSString* placeName=[NSString stringWithFormat:@"%s",placement];
//            if (TEST_ADS) {
//                placeName = [UtilsCommon getTestAdunit:YES];
//            }
//            return [[AdManager shareInstance] isVideoCappedForPlacement:placeName];
//        }else{
//            return true;
//        }
//    }
    
    //判断广告是否可以播放
//    bool isVideoAvailable()
//    {
//        return [[AdManager shareInstance] isVideoAvailable];
//    }

    //判断Mopub激励视频广告是否可以播放 by wjp 2020/06/13
//    bool isRewardVideoAvailable(char* placementName)
//    {
//        if (placementName !=NULL)
//        {
//            NSString* placeName=[NSString stringWithFormat:@"%s",placementName];
//            if (TEST_ADS) {
//                placeName = [UtilsCommon getTestAdunit:YES];
//            }
//            return [[AdManager shareInstance] isRewardVideoAvailable:placeName];
//        }else
//        {
//            return false;
//        }
//    }
    
    //show reward video from unity
//    void showInterstitialVideo(char* placementName)
//    {
//        if (placementName != NULL)
//        {
//            NSString* placeName=[NSString stringWithFormat:@"%s",placementName];
//            if (TEST_ADS) {
//                placeName = [UtilsCommon getTestAdunit:NO];
//            }
//            [[AdManager shareInstance] showInterstitialVideo:[UIApplication sharedApplication].delegate.window.rootViewController placeName:placeName];
//        }
//    }
    
    //check 指定的广告位是否用完
//    bool isInterstitialCapped(char* placement)
//    {
//        if (placement != NULL)
//        {
//            NSString* placeName=[NSString stringWithFormat:@"%s",placement];
//            if (TEST_ADS) {
//                placeName = [UtilsCommon getTestAdunit:NO];
//            }
//            return [[AdManager shareInstance] isInterstitialCapped:placeName];
//        }else{
//            return true;
//        }
//    }
    
    //判断广告是否可以播放
//    bool isInterstitialReady()
//    {
//        return [[AdManager shareInstance] isInterstitialReady];
//    }
    
//    void loadInterstitialVideo()
//    {
//        [[AdManager shareInstance] loadInterstitialVideo];
//    }

    //will not called from unity by wjp 2022/02/15
//    void loadRewardVideo(char* placementName)
//    {
//
//    }
//
    char*  getInputAreaConent()
    {
        UnityAppController *vc = GetAppController();
        const char * conent = [vc.inputView == nil? @"0" : [vc.inputView getInputAreaConent] UTF8String];
        return strdup(conent);
    };
    
    char* getInputAreaHeight()
    {
        UnityAppController *vc = GetAppController();
        float h =  [[vc.inputView getInputAreaHeight] floatValue] / SCREEN_HEIGHT;
        if(vc.inputView == nil)
            h = 0;
        const char * height = [[NSString stringWithFormat:@"%f",h] UTF8String];

        return strdup(height);
    };
    
    void showInputArea(int mId,int limit,char* str,char* hint,char* tag)
    {
        if(str == NULL || hint == NULL || tag == NULL)
            return;
        UnityAppController *vc = GetAppController();
        if(vc.inputView == nil)
        {
           InputView *inputView = [[InputView alloc] initWithFrame:CGRectMake(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT) Edit:mId limit:limit str:[NSString stringWithCString:str encoding:NSUTF8StringEncoding] hint:[NSString stringWithCString:hint encoding:NSUTF8StringEncoding] tag:[NSString stringWithCString:tag encoding:NSUTF8StringEncoding]];
            [vc.rootView addSubview:inputView];
            vc.inputView = inputView;
        }else
        {
            [vc.inputView showEdit];
        }
    }
    
    void invisibleInputArea()
    {
        UnityAppController *vc = GetAppController();
        [vc.inputView allScreenBtnHandler];
    }
    
    void hideInputArea()
    {
        UnityAppController *vc = GetAppController();
        if(vc.inputView)
            [vc.inputView removeFromSuperview];
        vc.inputView = nil;
    }
    
    char*  getDeviceModel()
    {
        const char * model = [[UtilsCommon iphoneType] UTF8String];
        //NSLog(@"getDeviceModel = %s",model);
        return strdup(model);
    }
    
    char*  getDeviceSystem()
    {
        const char * system = [[[UIDevice currentDevice] systemVersion] UTF8String];
        //NSLog(@"getDeviceSystem = %s",system);
        return strdup(system);
    }
    //新版获取需要补单的全部计费点
    char* getReorderData()
    {
        const char * system = [[PayHelper getReorderData] UTF8String];
        return strdup(system);
    }
    //新版清空全部补单计费点的缓存
    void clearReorderData()
    {
        [PayHelper clearReorderData];
    }
    //旧版获取需要最近补单的单个计费点
    char* getRamainsPaycode()
    {
        const char * system = [[PayHelper getRamainsPaycode] UTF8String];
        return strdup(system);
    }
    //旧版清空最近的补单订单号
    void clearRamainsPaycode()
    {
        [PayHelper clearRamainsPaycode];
    }
    
    void onBuyV2(char* dicJson)
    {
        if(dicJson == NULL)
            return;
        NSData *jsonData = [[NSString stringWithFormat:@"%s",dicJson] dataUsingEncoding:NSUTF8StringEncoding];
        NSError *err;
        NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONReadingMutableContainers error:&err];
        if(err)
        {
            NSLog(@"json解析失败：%@",err);
            return;
        }
        [PayHelper onBuyV2:dic];
    }
    
    void onBuy(char* dicJson)
    {
        if(dicJson == NULL)
            return;
        NSData *jsonData = [[NSString stringWithFormat:@"%s",dicJson] dataUsingEncoding:NSUTF8StringEncoding];
        NSError *err;
        NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONReadingMutableContainers error:&err];
        if(err)
        {
            NSLog(@"json解析失败：%@",err);
            return;
        }
        [PayHelper onBuy:dic];
    }
    
    void doRebuy(char* jsonMsg)
    {
        
    }
    
    //向支付平台请求恢复的订单记录
    void doRestore()
    {
        [PayHelper  doRestore];
    }
    
    const char* getTokenData(char* str)
    {
        
        NSString *key = [[NSUserDefaults standardUserDefaults] objectForKey:@"avg_notifytoken"];
        NSLog(@"ReportTokenToServer =%@",key);
        if(key == nil)
            key = @"";
        const char * system = [key UTF8String];
        return strdup(system);
    }
    
    const char* getClipboardStr()
    {
        UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
        const char * system = [pasteboard.string UTF8String];
        NSLog(@"粘贴---%@",pasteboard.string);
        return strdup(system);
    }
    
    void setClipboardStr(char* str)
    {
        if(str == NULL)
            return;
        UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
        pasteboard.string = [NSString stringWithFormat:@"%s",str];
        NSLog(@"复制---%@",pasteboard.string);
    }
    
    const char* GetVersion()
    {
        NSDictionary *infoDictionary = [[NSBundle mainBundle] infoDictionary];
        NSString *app_Version = [infoDictionary objectForKey:@"CFBundleShortVersionString"];
        const char * system = [app_Version UTF8String];
        return strdup(system);
    }
    
    void GoSetting()
    {
        UIApplication *application = [UIApplication sharedApplication];
        NSURL *url = [NSURL URLWithString:UIApplicationOpenSettingsURLString];
        if ([application canOpenURL:url]) {
            if ([application respondsToSelector:@selector(openURL:options:completionHandler:)]) {
                [application openURL:url options:@{} completionHandler:nil];
            } else {
                [application openURL:url];
            }
        }
    }
    
    const char* CheckNotifiSetting()
    {
        NSString *isEnable = @"0";
        UIUserNotificationSettings *setting = [[UIApplication sharedApplication] currentUserNotificationSettings];
        isEnable = (UIUserNotificationTypeNone == setting.types) ? @"0" : @"1";
        const char * system = [isEnable UTF8String];
        return strdup(system);
    }
    
//    void openPhoto(int32_t x, int32_t y, int32_t outX, int32_t outY, int32_t isClip)
//    {
//        NSLog(@"openPhoto");
//        [[AssetManager shareInstance] setImgclipSize:CGSizeMake(outX, outY) type:@""];
//        [[AssetManager shareInstance] showPhotoLibrary];
//    }
    
//    void openCamera(int32_t x, int32_t y, int32_t outX, int32_t outY, int32_t isClip)
//    {
//        NSLog(@"openCamera");
//        [[AssetManager shareInstance] setImgclipSize:CGSizeMake(outX, outY) type:@""];
//        [[AssetManager shareInstance] showCamera];
//    }
    
//    bool CheckCameraSetting()
//    {
//        AVAuthorizationStatus status = [AVCaptureDevice authorizationStatusForMediaType:AVMediaTypeVideo];
//        if (status == AVAuthorizationStatusRestricted || status == AVAuthorizationStatusDenied)
//        {
//            // 无权限
//            // do something...
//            return false;
//        }
//        return true;
//    }
    
//    void RequestCamera()
//    {
//        //相机
//        [AVCaptureDevice requestAccessForMediaType:AVMediaTypeVideo completionHandler:^(BOOL granted) {
//        }];
//    }
    
//    bool CheckReadAndWariteSetting()
//    {
//        PHAuthorizationStatus status = [PHPhotoLibrary authorizationStatus];
//        bool stas = false;
//        switch (status)
//        {
//            case PHAuthorizationStatusNotDetermined:
//            {
//                stas = false;
//                break;
//            }
//            case PHAuthorizationStatusRestricted:
//            case PHAuthorizationStatusDenied:
//            {
//                stas = false;
//                break;
//            }
//            case PHAuthorizationStatusAuthorized:
//            default:
//            {
//                stas = true;
//                break;
//            }
//        }
//
//        return stas;
//    }
    
//    void RequestReadAndWarite()
//    {
//        //相册
//        [PHPhotoLibrary requestAuthorization:^(PHAuthorizationStatus status) {
//        }];
//    }
    
    void setPlayerData(char* key,char* value)
    {
        if(key == NULL || value == NULL)
            return;
        NSString *saveValue = [NSString stringWithFormat:@"%s",value];
        [USER_DEFAULT setObject:saveValue forKey:[NSString stringWithFormat:@"%s",key]];
        [USER_DEFAULT synchronize];
    }
    
    const char* getPlayerData(char* key,char* defaultStr)
    {
        NSString *value = @"";
        if(key == NULL)
            value = @"";
        value = [USER_DEFAULT stringForKey:[NSString stringWithFormat:@"%s",key]];
        if(defaultStr != NULL)
        {
            if(value == nil || [value isEqualToString:@""])
                value = [NSString stringWithFormat:@"%s",defaultStr];
        }
        const char * system = [value UTF8String];
        return strdup(system);
    }
    
    void PayStart(char* paycode, char* price, char* transitionid)
    {
        if (paycode != NULL && price != NULL && transitionid != NULL)
            [Statistic payStart:[NSString stringWithFormat:@"%s",paycode] price:[NSString stringWithFormat:@"%s",price] transitionid:[NSString stringWithFormat:@"%s",transitionid]];
    }
    
    void PayFinish(char* paycode, int price, char* transitionid)
    {
        if (paycode != NULL && transitionid != NULL)
        {
            double priceDouble = price/100.0;
            [Statistic payFinish:[NSString stringWithFormat:@"%s",paycode] price:[NSString stringWithFormat:@"%.2f",priceDouble] transitionid:[NSString stringWithFormat:@"%s",transitionid]];
        }
    }
    
    void GameCureenyConsume(int num, char* currencytype, char* reason, char* storyid, char* chapterid, char* chatid)
    {
        if(currencytype != NULL && reason != NULL && storyid != NULL && chapterid != NULL && chatid != NULL)
            [Statistic gameCurrenyConsume:[NSString stringWithFormat:@"%d",num] currencytype:[NSString stringWithFormat:@"%s",currencytype] reason:[NSString stringWithFormat:@"%s",reason] storyid:[NSString stringWithFormat:@"%s",storyid] chapid:[NSString stringWithFormat:@"%s",chapterid] chatid:[NSString stringWithFormat:@"%s",chatid]];
    }
    
//    void chapBegin(char* chapid)
//    {
//        if(chapid != NULL)
//            [Statistic chapBegin:[NSString stringWithFormat:@"%s",chapid]];
//    }
//
//    void chapComplete(char* chapid)
//    {
//        if(chapid != NULL)
//            [Statistic chapComplete:[NSString stringWithFormat:@"%s",chapid]];
//    }
//
//    void chapStat(char* storyid ,char* chapid, char* chatid)
//    {
//        if(storyid != NULL && chapid != NULL && chatid != NULL)
//            [Statistic chatStat:[NSString stringWithFormat:@"%s",storyid] chapid:[NSString stringWithFormat:@"%s",chapid] chatid:[NSString stringWithFormat:@"%s",chatid]];
//    }
//
//    void choiceStat(char* storyid ,char* chapid, char* chatid , char* selectid)
//    {
//        if(storyid != NULL && chapid != NULL && chatid != NULL && selectid != NULL)
//            [Statistic choiceStat:[NSString stringWithFormat:@"%s",storyid] chapid:[NSString stringWithFormat:@"%s",chapid] chatid:[NSString stringWithFormat:@"%s",chatid] choiceid:[NSString stringWithFormat:@"%s",selectid]];
//    }
//
//    void bookCoverClick(char* storyid ,char* chapid, char* chatid , char* selectid)
//    {
//        if(storyid != NULL && chapid != NULL && chatid != NULL && selectid != NULL)
//            [Statistic bookCoverClick:[NSString stringWithFormat:@"%s",storyid] chapid:[NSString stringWithFormat:@"%s",chapid] chatid:[NSString stringWithFormat:@"%s",chatid] choiceid:[NSString stringWithFormat:@"%s",selectid]];
//    }
    
//    void bookBannerClick(char* storyid ,char* chapid, char* chatid , char* selectid)
//    {
//        if(storyid != NULL && chapid != NULL && chatid != NULL && selectid != NULL)
//            [Statistic bannerClick:[NSString stringWithFormat:@"%s",storyid] chapid:[NSString stringWithFormat:@"%s",chapid] chatid:[NSString stringWithFormat:@"%s",chatid] choiceid:[NSString stringWithFormat:@"%s",selectid]];
//    }
    
//    void remindStat(char* storyid ,char* chapid, char* chatid , char* selectid)
//    {
//        if(storyid != NULL && chapid != NULL && chatid != NULL && selectid != NULL)
//            [Statistic remindStat:[NSString stringWithFormat:@"%s",storyid] chapid:[NSString stringWithFormat:@"%s",chapid] chatid:[NSString stringWithFormat:@"%s",chatid] choiceid:[NSString stringWithFormat:@"%s",selectid]];
//    }
    
//    void registration(char* method, char* uuid)
//    {}
    
//    void statSpecialEvent(char* evetnName, char* typeName ,char* typevalue)
//    {
//        if(evetnName != NULL && typeName != NULL && typevalue != NULL)
//            [Statistic statSpecialEvent:[NSString stringWithFormat:@"%s",evetnName] typeName:[NSString stringWithFormat:@"%s",typeName] typeValue:[NSString stringWithFormat:@"%s",typevalue]];
//    }
    
//    void adjustEvent(char* evetnName)
//    {
//        if(evetnName != NULL)
//            [Statistic adjsutEventStat:[NSString stringWithFormat:@"%s",evetnName]];
//    }
    
//    void fbEvent(char* evetnName, char* value)
//    {
//        if(evetnName != NULL && value != NULL)
//            [Statistic fbEventStat:[NSString stringWithFormat:@"%s",evetnName] consumeNum:[NSString stringWithFormat:@"%s",value]];
//    }
    
//    void customEvent(char* evetnName)
//    {
//    }
    
    void setFacebookRevenueEvent(bool state)
    {
        [Statistic setFacebookRevenueReportOn:state];
    }
    
    void setGoogleRevenueEvent(bool state)
    {
        [Statistic setFirebaseRevenueReportOn:state];
    }
    
    void setAdjustRevenueEvent(bool state)
    {
        [Statistic setAdjustRevenueReportOn:state];
    }
    
//    void getAttributionInfo()
//    {
//        if ([[ADClient sharedClient] respondsToSelector:@selector(requestAttributionDetailsWithBlock:)]) {
//            [[ADClient sharedClient] requestAttributionDetailsWithBlock:^(NSDictionary<NSString *,NSObject *> * _Nullable attributionDetails, NSError * _Nullable error) {
//                if(!error){
//                    // Look inside of the returned dictionary for all attribution details
//                    NSLog(@"Attribution Dictionary: %@", attributionDetails);
//                    //NSMutableDictionary *jsonRaw = [NSMutableDictionary dictionary];
//                    NSString *jsonStr  = @"";
//                    BOOL isYes = [NSJSONSerialization isValidJSONObject:attributionDetails];
//                    if (isYes) {
//                        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:attributionDetails options:0 error:NULL];
//                        jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
//
//                        //此时应该回传
//                        UnitySendMessage("CommonManager", "setAttributtionInfoStr", [jsonStr UTF8String]);
//
//                    }else{
//                        NSLog(@"attribution Parse failed");
//                    }
//                }else{
//                    NSLog(@"attribution failed");
//                }
//            }];
//        }
//    }
    
    const char* getCountry()
    {
        const char * system = [[Statistic GetCountryCode] UTF8String];
        return strdup(system);
    }
    
//    void showWebView(char* evetnName)
//    {
//        if(evetnName == NULL)
//            return;
//        WebLoadViewController *vc = [[WebLoadViewController alloc] initWithNibName:@"WebLoadViewController" bundle:nil];
//        vc.titleString = NSLocalizedString(@"", nil);
//        vc.address = [NSString stringWithFormat:@"%s",evetnName];
//        vc.modalPresentationStyle = UIModalPresentationFullScreen;
//        UINavigationController *nacvc = [[UINavigationController alloc] initWithRootViewController:vc];
//        nacvc.modalPresentationStyle = UIModalPresentationFullScreen;
//        UIViewController *viewController = [UIApplication sharedApplication].keyWindow.rootViewController;
//        viewController.modalPresentationStyle = UIModalPresentationFullScreen;
//        [viewController presentViewController:nacvc animated:YES completion:nil];
//    }
    
    char*  GetDynamicLinkData()
    {
        const char * deeplink = [deeplinkStr UTF8String];
        NSLog(@"getDeviceModel = %s",deeplink);
        return strdup(deeplink);
    }
    
    void CloseIOSView()
    {
        UnityAppController *vc = GetAppController();
        if(vc != NULL && vc.launchView != NULL)
        {
            dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(0.5 * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
                [UIView animateWithDuration:0.5 animations:^{
                    vc.launchView.alpha = 0;
                } completion:^(BOOL finished) {
                    [vc.launchView removeFromSuperview];
                }];
            });
        }
    }
    
//    void connectToTapjoy(char* AndroidKey,char* uuid)
//    {
//        if(![Tapjoy isConnected])
//        {
//           [Tapjoy connect:TapjoyKey];
//        }else
//            UnitySendMessage("AdGameObject", "OnTJConnectSuccess","");
//    }
    
//    void preloadOffwall(char* name)
//    {
//        if (name != NULL)
//        {
//            NSString *adName = [NSString stringWithFormat:@"%s",name];
//            [[AdManager shareInstance] ContentTapjoyAd:adName];
//        }else
//            [[AdManager shareInstance] ContentTapjoyAd:nil];
//    }
    
    void getLatestRewards()
    {
        
    }
    
//    void showContent()
//    {
//        [[AdManager shareInstance] ShowTapjoyAd];
//    }
    
//    bool canPlay()
//    {
//        return [[AdManager shareInstance] isTapjoyContentAvailable];
//    }
//
//    void requestPlacement()
//    {
//        [[AdManager shareInstance] ContentTapjoyAd:nil];
//    }
//
    bool canReadyPlay()
    {
        return false;
    }
    
//    void spendCurrency(char* num)
//    {
//        NSString *str = [NSString stringWithFormat:@"%s",num];
//        if([str intValue] > 0)
//            [[AdManager shareInstance] TapjoyspendCurrency:[str intValue]];
//    }
    
//    void VKLogin()
//    {
//        [UtilsCommon doVKLogin];
//    }
//
//    void VKLogout()
//    {
//        [UtilsCommon doVKLogout];
//    }
    
//    void updateConversionValue(int32_t value)
//    {
//        if (@available(iOS 14.0, *)) {
//            [SKAdNetwork updateConversionValue:3];
//        } else {
//            // Fallback on earlier versions
//        }
//    }
    //report ATT result when show ATT popup.
    void reportATTResult(bool withParm1){
        NSMutableDictionary *dic = [NSMutableDictionary dictionary];
        if (dic) {
            if(withParm1==true){
                //allow
                dic[@"parm1"] = @"1";
            }else{
                //not allow
                dic[@"parm1"] = @"2";
            }
            dic[@"parm5"] = @"att_click";
            NSString *json=nil;
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dic options:0 error:NULL];
            json = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            if ([json length]>0) {
                NSLog(@"track ATT data %@",json);
//                UnitySendMessage("CommonManager", "OnSendClickGet", [json UTF8String]);
            }else{
                NSLog(@"track ATT data without json ?");
            }
        }
    }
    
//    void GetIdfarequset()
//    {
//        if (@available(iOS 14, *)) {
//            [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
//                // 获取到权限后，依然使用老方法获取idfa
//                if (status == ATTrackingManagerAuthorizationStatusAuthorized) {
//                    [FBSDKSettings setAdvertiserTrackingEnabled:YES];
//                    reportATTResult(true);
//                    NSLog(@"GetIdfarequset Tracking中允许App请求跟踪,set ATE YES");
//                } else {
//                    if(status == ATTrackingManagerAuthorizationStatusDenied){
//                        [FBSDKSettings setAdvertiserTrackingEnabled:NO];
//                        reportATTResult(false);
//                        NSLog(@"请在设置-隐私-Tracking中允许App请求跟踪");
//                        NSLog(@"GetIdfarequset Tracking中不允许App请求跟踪,set ATE NO");
//                    }else{
//                        NSLog(@"GetIdfarequset ATT 返回的其他状态");
//                    }
//                }
//            }];
//        }
//
//    }
//
//    char*  GetidfaStatus()
//    {
//        NSString *str = @"3";
//        if (@available(iOS 14, *))
//        {
//            ATTrackingManagerAuthorizationStatus status = ATTrackingManager.trackingAuthorizationStatus;
//
//            switch(status){
//                case ATTrackingManagerAuthorizationStatusNotDetermined:
//                    str = @"0";
//                    break;
//                case ATTrackingManagerAuthorizationStatusRestricted:
//                    //zongkaiguan
//                    str = @"1";
//                    break;
//                case ATTrackingManagerAuthorizationStatusAuthorized:
//                    //agree
//                    str = @"3";
//                    break;
//                case ATTrackingManagerAuthorizationStatusDenied:
//                    //refused
//                    str = @"2";
//                    break;
//                default:
//                    break;
//
//            }
//        }
//        NSString *idfa = [UtilsCommon getIDFA];
//        if (!STRING_IS_EMPTY(idfa) && ![idfa isEqualToString:@"00000000-0000-0000-0000-000000000000"])
//        {
//           str = @"3";
//        }
//
//        const char * stastr = [str UTF8String];
//        return strdup(stastr);
//    }
//
//    void requestidfa(char* status)
//    {
//        NSString *str = [NSString stringWithFormat:@"%s",status];
//        if (IS_IOS14LATER && [str isEqualToString:@"1"])
//        {
//            NSString *idfa = [UtilsCommon getIDFA];
//            if (!STRING_IS_EMPTY(idfa) && ![idfa isEqualToString:@"00000000-0000-0000-0000-000000000000"])
//            {
//                return;
//            }
//        }
//        if (@available(iOS 14, *)) {
//            [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
//
//                // 获取到权限后，依然使用老方法获取idfa
//                if (status == ATTrackingManagerAuthorizationStatusAuthorized) {
//                    [FBSDKSettings setAdvertiserTrackingEnabled:YES];
//                    reportATTResult(true);
//                    NSLog(@"requestidfa Tracking中允许App请求跟踪,set ATE YES");
//                } else {
//                    if(status == ATTrackingManagerAuthorizationStatusDenied){
//                        [FBSDKSettings setAdvertiserTrackingEnabled:NO];
//                        reportATTResult(false);
//                        NSLog(@"请在设置-隐私-Tracking中允许App请求跟踪");
//                        NSLog(@"requestidfa Tracking中不允许App请求跟踪,set ATE NO");
//                    }else{
//                        NSLog(@"requestidfa ATT 返回的其他状态");
//                    }
//                }
//            }];
//
//        } else {
//            // Fallback on earlier versions
//        }
//    }

    const char* getLaunchPushData()
    {
        isLaunchedFromLua = true;
        
        if (launchPushDataJsonStr == nil)
        {
            return nil;
        }
        
        NSData *data = [NSJSONSerialization dataWithJSONObject:launchPushDataArray options:NSJSONWritingPrettyPrinted error:NULL];
        NSString *arrayStr = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        const char* arrayCStr = [arrayStr UTF8String];
        return strdup(arrayCStr);
        
//        const char * launchPushData = [launchPushDataJsonStr UTF8String];
//        NSLog(@"getLaunchPushData = %s", launchPushData);
//        return strdup(launchPushData);
    }
    //向firebase crashlytic 上报uuid，log
    void firebaseRecord(char* uuidstr,char* logstr, bool istest, bool withlog)
    {
        if (uuidstr != NULL && logstr != NULL){
            FIRCrashlytics* crashlytic= [FIRCrashlytics crashlytics];
            if(crashlytic!=nil){
                NSString *uuid=[NSString stringWithFormat:@"%s",uuidstr];
                NSString *str=[NSString stringWithFormat:@"%s",logstr];
                if([str length]>0 && [uuid length] > 0){
                    //[crashlytic setUserID:uuid];
                    //use device id instead of uuid
                    str=[NSString stringWithFormat:@"uuid:%@\r%@",uuid,str];
                    [crashlytic setCustomValue:@"UnityHandlerLog" forKey:@"type"];
                    [crashlytic setCustomValue:@(istest) forKey:@"isTest"];
                    if(withlog){
                        [crashlytic log:str];
                    }
                    NSDictionary *userInfo = @{
                               NSLocalizedDescriptionKey: NSLocalizedString(@"The errlog is：", logstr),
                               @"The errlog is": str
                           };

                    NSError *error = [NSError errorWithDomain:NSCocoaErrorDomain code:-1001 userInfo:userInfo];
                    [crashlytic recordError:error];
                }
            }
        }
    }
    
    //设置firebase crashlaytic 的uuid
    void setCrashlyticsUUID(char* uuidstr)
    {
        if (uuidstr != NULL){
            FIRCrashlytics* crashlytic= [FIRCrashlytics crashlytics];
            if(crashlytic!=nil){
                NSString *str=[NSString stringWithFormat:@"%s",uuidstr];
                if([str length]>0){
                    [crashlytic setUserID:str];
                }
                
            }
        }
    }
    
     //包含测试的标识和日志,支持上报到firebase
    void firebaseRecordWithTestAndLog(char* uuidstr,char* logstr)
    {
        firebaseRecord(uuidstr, logstr, true, true);
    }
    
    //包含测试的标识,没有日志,支持上报到firebase
    void firebaseRecordWithTest(char* uuidstr,char* logstr)
    {
        firebaseRecord(uuidstr, logstr, true, false);
    }
    
    //不包含测试的标识，但是有日志,支持上报到firebase
    void firebaseRecordWithReleaseAndLog(char* uuidstr,char* logstr)
    {
        firebaseRecord(uuidstr, logstr, false, true);
    }
    
    //不包含测试的标识，也没有日志,支持上报到firebase
    void firebaseRecordWithRelease(char* uuidstr,char* logstr)
    {
        firebaseRecord(uuidstr, logstr, false,false);
    }
	
	float getCurVolume()
	{
		float vol = [[AVAudioSession sharedInstance] outputVolume];
		return vol;
	}

    //获取网络类型，2g ，3g ，4g
    const char* getNetworkType()
    {
        return nil;
    }
    
    //是否弹出GDPR可以弹出
//    bool shouldShowConsentDialog()
//    {
//        return [AdManager shareInstance].shouldShowConsentDialog;
//    }
    //玩家授权GDPR
//    void grantConsent()
//    {
//        [[AdManager shareInstance] grantConsent];
//    }
    //玩家拒绝授权GDPR
//    void revokeConsent()
//    {
//        [[AdManager shareInstance] revokeConsent];
//    }
    
//    const char* readbookconfig(char* file)
//    {
//        NSString *str=[NSString stringWithFormat:@"%s",file];
//        return strdup([[UtilsCommon readBookZip:str] UTF8String]);
//    }
    
    //设置插屏缓存的开关
//    void openInterstitialCache(bool isopen,char* adunit)
//    {
//        if (TEST_ADS) {
//            NSLog(@"ADMAXLOG:   no need use two interstitial ,it's just test .");
//            return;
//        }
//        NSString *str=[NSString stringWithFormat:@"%s",adunit];
//        if([str length]>0){
//            [[AdManager shareInstance] openInterstitialCache:isopen placeName:str];
//        }else{
//            NSLog(@"ADMAXLOG:   shoud set adunit value ");
//        }
//
//    }
    //设置加载插屏缓存
    //unusage from u3d
//    void loadInterstitialCache()
//    {
//        [[AdManager shareInstance] loadInterstitialCache];
//    }

    //判断全部插屏广告是否可以播放
    //unusage from u3d
//    bool isAllInterReady()
//    {
//        return [[AdManager shareInstance] isAllInterReady];
//    }
    
//    void showMediaDebugTool(){
//        [[AdManager shareInstance] showMediaDebugTool];
//    }
    
    //appsflyer event
    void appsflyerEventStat(char* evetnName)
    {
        if(evetnName != NULL){
            [[AppsFlyerStat getInstance] logEvent:[NSString stringWithFormat:@"%s",evetnName]];
        }
    }
    
    //appsflyer event
    void appsflyerEventWithJson(char* evetnName,char* json)
    {
        if(evetnName != NULL && json != NULL){
            [[AppsFlyerStat getInstance] logEventWithJson:[NSString stringWithFormat:@"%s",evetnName] json:[NSString stringWithFormat:@"%s",json]];
        }
    }
    
    
    //appsflyer归因
    const char* getAppsflyerData()
    {
        NSMutableDictionary* dic = [NSMutableDictionary dictionary];
        [dic setObject:afConversionFlag forKey:@"conversionFlag"];
        [dic setObject:afConversionData forKey:@"conversionData"];
        [dic setObject:afAttributionFlag forKey:@"attributionFlag"];
        [dic setObject:afAttributionData forKey:@"attributionData"];
        
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dic options:0 error:nil];
        NSString *json = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        
        const char * str = [json UTF8String];
        
        return strdup(str);
    }

    void appsflyerPayFinish(char* paycode, int price, char* goodid)
    {
        if (paycode != NULL && goodid != NULL)
        {
            double priceDouble = price/100.0;
            [[AppsFlyerStat getInstance] payFinish:[NSString stringWithFormat:@"%s",paycode] price:[NSString stringWithFormat:@"%.2f",priceDouble] goodid:[NSString stringWithFormat:@"%s",goodid]];
        }
    }

    void appsflyerPayBegin(char* paycode, int price)
    {
        if (paycode != NULL)
        {
            double priceDouble = price/100.0;
            [[AppsFlyerStat getInstance] payBegin:[NSString stringWithFormat:@"%s",paycode] price:[NSString stringWithFormat:@"%.2f",priceDouble]];
        }
    }

    void appsflyerPayEventReport(char* eventName,char* paycode, int price)
    {
        if (paycode != NULL && eventName != NULL)
        {
            double priceDouble = price/100.0;
            [[AppsFlyerStat getInstance] payEventReport:[NSString stringWithFormat:@"%s",eventName] paycode:[NSString stringWithFormat:@"%s",paycode] price:[NSString stringWithFormat:@"%.2f",priceDouble] ];
        }
    }

    //get appsflyer cuid
    const char* getAppsflyerCUID()
    {
        NSString *customerUserID = [[NSUserDefaults standardUserDefaults] stringForKey:@"appsflyerCustomUId"];
        if (customerUserID!=nil && [customerUserID length] > 0)
        {
            
        }else
        {
            customerUserID= @"";
            NSLog(@"get Appsflyer userid is empty.");
        }
        const char * str = [customerUserID UTF8String];   
        return strdup(str);
    }

    //set appsflyer cuid
    void setAppsflyerCUID(char* uuid)
    {
        NSString *str=[NSString stringWithFormat:@"%s",uuid];
        if (str!=nil && [str length] > 0) 
        {
            [AppsFlyerLib shared].customerUserID = str;
            [[NSUserDefaults standardUserDefaults] setValue:str forKey:@"appsflyerCustomUId"];
            NSLog(@"set Appsflyer userid %@",str);
        }
    }

    void fbMobileAdEvent(char* eventStr,char* contentTypeStr,char* contentIdStr)
    {
        NSString *eventName=[NSString stringWithFormat:@"%s",eventStr];
        NSString *contentType=[NSString stringWithFormat:@"%s",contentTypeStr];
        NSString *contentId=[NSString stringWithFormat:@"%s",contentIdStr];
        [Statistic fbMobileAdEvent:eventName contentType:contentType contentId:contentId];
    }
    const int64_t GetUnUseMemory()
    {
        return [UtilsCommon getFreeMemory];
    }

    const int64_t GetAppUsedMemory()
    {
        return [UtilsCommon getUsedMemory];
    }
//    void setMopubAdUnit(char* rewardAdUnit,char* interstitalAdUnit)
//    {
//
//        NSLog(@"set ad unit at runtime then init mopub.");
//    }
    
    void fbPurchaseEvent(char* eventStr,char* skuStr,char*priceStr)
    {
        NSString *eventName=[NSString stringWithFormat:@"%s",eventStr];
        NSString *sku=[NSString stringWithFormat:@"%s",skuStr];
        NSString *price=[NSString stringWithFormat:@"%s",priceStr];
        [Statistic fbPurchaseEvent:eventName sku:sku price:price];
    }
}

- (id)init
{
    if ((self = _UnityAppController = [super init]))
    {
        // due to clang issues with generating warning for overriding deprecated methods
        // we will simply assert if deprecated methods are present
        // NB: methods table is initied at load (before this call), so it is ok to check for override
        NSAssert(![self respondsToSelector: @selector(createUnityViewImpl)],
            @"createUnityViewImpl is deprecated and will not be called. Override createUnityView"
        );
        NSAssert(![self respondsToSelector: @selector(createViewHierarchyImpl)],
            @"createViewHierarchyImpl is deprecated and will not be called. Override willStartWithViewController"
        );
        NSAssert(![self respondsToSelector: @selector(createViewHierarchy)],
            @"createViewHierarchy is deprecated and will not be implemented. Use createUI"
        );
    }
    return self;
}

- (void)setWindow:(id)object        {}
- (UIWindow*)window                 { return _window; }


- (void)shouldAttachRenderDelegate  {}
- (void)preStartUnity               {}


- (void)startUnity:(UIApplication*)application
{
    NSAssert(_unityAppReady == NO, @"[UnityAppController startUnity:] called after Unity has been initialized");

    UnityInitApplicationGraphics();

    // we make sure that first level gets correct display list and orientation
    [[DisplayManager Instance] updateDisplayListCacheInUnity];

    UnityLoadApplication();
    Profiler_InitProfiler();

    [self showGameUI];
    [self createDisplayLink];
   
    UnitySetPlayerFocus(1);

#if UNITY_REPLAY_KIT_AVAILABLE
    void InitUnityReplayKit();  // Classes/Unity/UnityReplayKit.mm

    InitUnityReplayKit();
#endif
}

extern "C" void UnityDestroyDisplayLink()
{
    [GetAppController() destroyDisplayLink];
}

extern "C" void UnityRequestQuit()
{
    _didResignActive = true;
    if (GetAppController().quitHandler)
        GetAppController().quitHandler();
    else
        exit(0);
}
extern void SensorsCleanup();
extern "C" void UnityCleanupTrampoline()
{
    // Unity view and viewController will not necessary be destroyed right after this function execution.
    // We need to ensure that these objects will not receive any callbacks from system during that time.
    [_UnityAppController window].rootViewController = nil;
    [[_UnityAppController unityView] removeFromSuperview];

    // Prevent multiple cleanups
    if (_UnityAppController == nil)
        return;

    [KeyboardDelegate Destroy];

    SensorsCleanup();

    Profiler_UninitProfiler();

    [DisplayManager Destroy];

    UnityDestroyDisplayLink();

    _UnityAppController = nil;
}
#if UNITY_SUPPORT_ROTATION

- (NSUInteger)application:(UIApplication*)application supportedInterfaceOrientationsForWindow:(UIWindow*)window
{
    // No rootViewController is set because we are switching from one view controller to another, all orientations should be enabled
    if ([window rootViewController] == nil)
        return UIInterfaceOrientationMaskAll;

    // During splash screen show phase no forced orientations should be allowed.
    // This will prevent unwanted rotation while splash screen is on and application is not yet ready to present (Ex. Fogbugz cases: 1190428, 1269547).
    if (!_unityAppReady)
        return [_rootController supportedInterfaceOrientations];

    // Some presentation controllers (e.g. UIImagePickerController) require portrait orientation and will throw exception if it is not supported.
    // At the same time enabling all orientations by returning UIInterfaceOrientationMaskAll might cause unwanted orientation change
    // (e.g. when using UIActivityViewController to "share to" another application, iOS will use supportedInterfaceOrientations to possibly reorient).
    // So to avoid exception we are returning combination of constraints for root view controller and orientation requested by iOS.
    // _forceInterfaceOrientationMask is updated in willChangeStatusBarOrientation, which is called if some presentation controller insists on orientation change.
    return [[window rootViewController] supportedInterfaceOrientations] | _forceInterfaceOrientationMask;
}

- (void)application:(UIApplication*)application willChangeStatusBarOrientation:(UIInterfaceOrientation)newStatusBarOrientation duration:(NSTimeInterval)duration
{
    // Setting orientation mask which is requested by iOS: see supportedInterfaceOrientationsForWindow above for details
    _forceInterfaceOrientationMask = 1 << newStatusBarOrientation;
}

#endif

#if !PLATFORM_TVOS
- (void)application:(UIApplication*)application didReceiveLocalNotification:(UILocalNotification*)notification
{
    AppController_SendNotificationWithArg(kUnityDidReceiveLocalNotification, notification);
    UnitySendLocalNotification(notification);
}

#endif

#if UNITY_USES_REMOTE_NOTIFICATIONS
- (void)application:(UIApplication*)application didReceiveRemoteNotification:(NSDictionary*)userInfo
{
    // With swizzling disabled you must let Messaging know about the message, for Analytics
    if (userInfo != nil)
        [[FIRMessaging messaging] appDidReceiveMessage:userInfo];
   //接收apns消息内容
    [UIApplication sharedApplication].applicationIconBadgeNumber=0;
    
    AppController_SendNotificationWithArg(kUnityDidReceiveRemoteNotification, userInfo);
    UnitySendRemoteNotification(userInfo);
    
//    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:userInfo options:NSJSONWritingPrettyPrinted error:NULL];
//    NSString* jsonString = [[NSString alloc]initWithData:jsonData encoding:NSUTF8StringEncoding];
//    NSLog(@"-> didReceiveRemoteNotification: %@", jsonString);
    [self checkLaunchPushOnRunning: userInfo];
    if (userInfo != nil){
        [[AppsFlyerLib shared] handlePushNotification:userInfo];
    }
}

//注册推送
- (void)application:(UIApplication*)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData*)deviceToken
{
    AppController_SendNotificationWithArg(kUnityDidRegisterForRemoteNotificationsWithDeviceToken, deviceToken);
    UnitySendDeviceToken(deviceToken);
    
    [FIRMessaging messaging].APNSToken = deviceToken;
}

#if !PLATFORM_TVOS
- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo fetchCompletionHandler:(void (^)(UIBackgroundFetchResult result))handler
{
    
    AppController_SendNotificationWithArg(kUnityDidReceiveRemoteNotification, userInfo);
    UnitySendRemoteNotification(userInfo);
    if (handler)
    {
        handler(UIBackgroundFetchResultNoData);
    }
    handler(UIBackgroundFetchResultNewData);
    //app启动中接受到推送消息
    if (userInfo != nil) {
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:userInfo options:0 error:NULL];
        NSString *jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        if (jsonStr == nil) {
            jsonStr = @"";
        }
//        UnitySendMessage("CommonManager", "OnReceiveNotifyFromFirebase", [jsonStr UTF8String]);
        
//        NSString* jsonString = [[NSString alloc]initWithData:jsonData encoding:NSUTF8StringEncoding];
//        NSLog(@"-> didReceiveRemoteNotification(userinfo, handler): %@", jsonString);
        [self checkLaunchPushOnRunning: userInfo];
        // With swizzling disabled you must let Messaging know about the message, for Analytics
        [[FIRMessaging messaging] appDidReceiveMessage:userInfo];
        [[AppsFlyerLib shared] handlePushNotification:userInfo];
    }
}

//程序前台运行接受到通知
- (void)userNotificationCenter:(UNUserNotificationCenter *)center willPresentNotification:(UNNotification *)notification withCompletionHandler:(void (^)(UNNotificationPresentationOptions))completionHandler
{
    if (UIApplicationStateActive == [UIApplication sharedApplication].applicationState) {
        completionHandler(UNNotificationPresentationOptionNone);
    } else {
        completionHandler(UNNotificationPresentationOptionAlert|UNNotificationPresentationOptionBadge|UNNotificationPresentationOptionSound);
    }
    
    NSDictionary *userInfo = notification.request.content.userInfo;
    if (userInfo != nil) {
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:userInfo options:0 error:NULL];
        NSString *jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        if (jsonStr == nil) {
            jsonStr = @"";
        }
//        UnitySendMessage("CommonManager", "OnReceiveNotifyFromFirebase", [jsonStr UTF8String]);
        
//        NSString* jsonString = [[NSString alloc]initWithData:jsonData encoding:NSUTF8StringEncoding];
//        NSLog(@"-> userNotificationCenter.willPresentNotification(userinfo, handler): %@", jsonString);
        
        [UIApplication sharedApplication].applicationIconBadgeNumber = 0;
    }
}

//后台运行以及程序退出接受通知
- (void)userNotificationCenter:(UNUserNotificationCenter *)center didReceiveNotificationResponse:(UNNotificationResponse *)response withCompletionHandler:(void (^)())completionHandler
{
    completionHandler();
    NSDictionary *userInfo = response.notification.request.content.userInfo;
    if (userInfo != nil) {
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:userInfo options:0 error:NULL];
        NSString *jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        if (jsonStr == nil) {
            jsonStr = @"";
        }
//        UnitySendMessage("CommonManager", "OnReceiveNotifyFromFirebase", [jsonStr UTF8String]);
        
//        NSString* jsonString = [[NSString alloc]initWithData:jsonData encoding:NSUTF8StringEncoding];
//        NSLog(@"-> userNotificationCenter.didReceiveNotificationResponse(userinfo, handler): %@", jsonString);
        [self checkLaunchPushOnRunning:userInfo];
        
        [UIApplication sharedApplication].applicationIconBadgeNumber = 0;
        // With swizzling disabled you must let Messaging know about the message, for Analytics
        [[FIRMessaging messaging] appDidReceiveMessage:userInfo];
    }
}

#endif

- (void)application:(UIApplication*)application didFailToRegisterForRemoteNotificationsWithError:(NSError*)error
{
    AppController_SendNotificationWithArg(kUnityDidFailToRegisterForRemoteNotificationsWithError, error);
    UnitySendRemoteNotificationError(error);
    // alas people do not check remote notification error through api (which is clunky, i agree) so log here to have at least some visibility
    ::printf("\nFailed to register for remote notifications:\n%s\n\n", [[error localizedDescription] UTF8String]);
}

#endif

//缓存deeplink
-(void)cacheDeeplink:(NSURL*)url{
    //判断这条连接是否是指定的深度连接，因为bottomid肯定会在深度连接里
    if (url!=nil && url.absoluteString!=nil &&  [url.absoluteString length] > 0) {
        NSLog(@"deeplink is  %@",url.absoluteString);
        if ([url.absoluteString rangeOfString:@"bottomid"].location!=NSNotFound) {
            deeplinkStr=[url absoluteString];
        }
    }
}

-(void)checkLaunchPushOnRunning:(NSDictionary*) payload {
    if (payload == nil) {
        return;
    }
    if (![[payload allKeys] containsObject:@"extData"]) {
        return;
    }
    NSString *launchPushData = payload[@"extData"];
    
    if (launchPushData != nil) {
        if (isLaunchedFromLua) {
//            UnitySendMessage("CommonManager", "OnReceiveNotifyLaunch", [launchPushData UTF8String]);
        } else {
            if (launchPushDataJsonStr == nil) {
                launchPushDataJsonStr = launchPushData;
            }
            [launchPushDataArray addObject:launchPushData];
            
            // 第一次启动缓冲 deeplink
            NSData *jsonData = [launchPushData dataUsingEncoding:NSUTF8StringEncoding];
            NSDictionary *extDataDict = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONReadingMutableContainers error:nil];
            if ([[extDataDict allKeys] containsObject:@"skip_address"]) {
                NSString *urlStr = extDataDict[@"skip_address"];
                NSURL *url = [NSURL URLWithString:urlStr];
                [self cacheDeeplink:url];
            }
        }
    }
}

-(NSString*)convertPushData:(NSDictionary*) payload {
    if (payload == nil) {
        return nil;
    }
    
    if ([[payload allKeys] containsObject:@"push_id"]) {
        BOOL isYes = [NSJSONSerialization isValidJSONObject:payload];
        if (isYes) {
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:payload options:0 error:nil];
            NSString *jsonText  = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            return jsonText;
        }
    }
    return nil;
}

// UIApplicationOpenURLOptionsKey was added only in ios10 sdk, while we still support ios9 sdk
- (BOOL)application:(UIApplication*)app openURL:(NSURL*)url options:(NSDictionary<NSString*, id>*)options
{
    [self cacheDeeplink:url];
    //appsflyer
    [[AppsFlyerLib shared] handleOpenUrl:url options:options];
    id sourceApplication = options[UIApplicationOpenURLOptionsSourceApplicationKey], annotation = options[UIApplicationOpenURLOptionsAnnotationKey];

    NSMutableDictionary<NSString*, id>* notifData = [NSMutableDictionary dictionaryWithCapacity: 3];
    if (url) notifData[@"url"] = url;
    if (sourceApplication) notifData[@"sourceApplication"] = sourceApplication;
    if (annotation) notifData[@"annotation"] = annotation;

    AppController_SendNotificationWithArg(kUnityOnOpenURL, notifData);
    
    
    [[FBSDKApplicationDelegate sharedInstance] application:app
                                                   openURL:url
                                         sourceApplication:options[UIApplicationOpenURLOptionsSourceApplicationKey]
                                                annotation:options[UIApplicationOpenURLOptionsAnnotationKey]
     ];
    
    BFURL *parsedUrl = [BFURL URLWithInboundURL:url sourceApplication:sourceApplication];
    if ([parsedUrl appLinkData]!=nil) {
        //this is an applink url, handle it here
        NSURL *targetUrl = [parsedUrl targetURL];
        [self cacheDeeplink:targetUrl];
    }
    
//    [VKSdk processOpenURL:url fromApplication:options[UIApplicationOpenURLOptionsSourceApplicationKey]];
    
    return [[GIDSignIn sharedInstance] handleURL:url];
}

- (BOOL)application:(UIApplication*)application willFinishLaunchingWithOptions:(NSDictionary*)launchOptions
{
    AppController_SendNotificationWithArg(kUnityWillFinishLaunchingWithOptions, launchOptions);
    return YES;
}

//-(void)testShowReward:(UIButton*)sender{
//    NSLog(@"ADMAXLOG:   play btn to showreward");
//    if ([[AdManager shareInstance] isRewardVideoAvailable:AdRewardId]) {
//        [[AdManager shareInstance] showRewardVideo:[UIApplication sharedApplication].delegate.window.rootViewController placeName:AdRewardId];
//    }else{
//
//    }
//
//    //@[][1];
//}

//-(void)testShowInterstitial:(UIButton*)sender{
//    NSLog(@"ADMAXLOG:   play btn to showinterstitial");
//    if ([[AdManager shareInstance] isInterstitialReady]) {
//        [[AdManager shareInstance] showInterstitialVideo:[UIApplication sharedApplication].delegate.window.rootViewController placeName:AdInterstitial];
//    }else{
//        [[AdManager shareInstance] loadInterstitialVideo];
//    }
//}
//-(void)showMediaTool:(UIButton*)sender{
//    NSLog(@"ADMAXLOG:   play btn to showdebug tool");
//    [[AdManager shareInstance] showMediaDebugTool];
//    //[[AdManager shareInstance] grantConsent];
//    //[[AdManager shareInstance] revokeConsent];
//    //@[][1];
//}

-(void)testCrash{
        FIRCrashlytics* crashlytic= [FIRCrashlytics crashlytics];
        if(crashlytic!=nil){
            bool istest =true;
            bool withlog=true;
            NSString *uuid=@"test1234";
            NSString *str=@"this is log";
            if([str length]>0 && [uuid length] > 0){
                //[crashlytic setUserID:uuid];
                //use device id instead of uuid
                str=[NSString stringWithFormat:@"uuid:%@\r%@",uuid,str];
                NSLog(@"test uuid %@",str);
                [crashlytic setCustomValue:@"UnityHandlerLog" forKey:@"type"];
                [crashlytic setCustomValue:@(istest) forKey:@"isTest"];
                [crashlytic log:str];
                NSDictionary *userInfo = @{
                           NSLocalizedDescriptionKey: NSLocalizedString(@"The errlog is：", [str UTF8String]),
                           @"The errlog is": str
                       };
    
                NSError *error = [NSError errorWithDomain:NSCocoaErrorDomain code:-1001 userInfo:userInfo];
                [crashlytic recordError:error];
            }
        }
}

- (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions
{
    ::printf("-> applicationDidFinishLaunching()\n");
    //这里会和firebase的上报冲突，导致firbase无法上报，因此关闭admob crash上报.
	//[[GADMobileAds sharedInstance] disableSDKCrashReporting];
    //appsflyer init
    [AppsFlyerLib shared].appsFlyerDevKey = @"YMqy4FA8K8AHjhXuzyRoQg";
    [AppsFlyerLib shared].appleAppID = @"1592622967";
    [AppsFlyerLib shared].delegate = self;
    [AppsFlyerLib shared].deepLinkDelegate = self;
    /* Set isDebug to true to see AppsFlyer debug logs */
    [AppsFlyerLib shared].isDebug = false;
    
    //[[AppsFlyerLib shared]]
    //[AppsFlyerAdRevenue start];
    // send notfications
#if !PLATFORM_TVOS
    if (UILocalNotification* notification = [launchOptions objectForKey: UIApplicationLaunchOptionsLocalNotificationKey])
        UnitySendLocalNotification(notification);

    if ([UIDevice currentDevice].generatesDeviceOrientationNotifications == NO)
        [[UIDevice currentDevice] beginGeneratingDeviceOrientationNotifications];

    // if (launchOptions != nil) {
    //     NSURL *url = [NSURL URLWithString:@"chapter://?type=4&storytype=1&bookid=10001%26bottomid=0"];
    //     [self cacheDeeplink:url];
    // }
#endif
    UnityInitApplicationNoGraphics([[[NSBundle mainBundle] bundlePath] UTF8String]);

    [self selectRenderingAPI];
    [UnityRenderingView InitializeForAPI: self.renderingAPI];

    _window         = [[UIWindow alloc] initWithFrame: [UIScreen mainScreen].bounds];
    _unityView      = [self createUnityView];

    [DisplayManager Initialize];
    _mainDisplay    = [DisplayManager Instance].mainDisplay;
    [_mainDisplay createWithWindow: _window andView: _unityView];
    
    //添加启动遮罩挡住unity启动界面
//    _launchView = [[UIView alloc] initWithFrame:CGRectMake(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT)];
//    //_launchView.backgroundColor = [UIColor colorWithRed:172/255.0 green:122/255.0 blue:224/255.0 alpha:1];
//    _launchView.backgroundColor = [UIColor colorWithRed:1 green:1 blue:1 alpha:1];
//    [_unityView addSubview:_launchView];
//    UIImageView *imgView = [[UIImageView alloc] initWithFrame:CGRectMake(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT)];
//    imgView.image = [UIImage imageNamed:@"SplashIg"];
//    imgView.contentMode = UIViewContentModeCenter;
//    [_launchView addSubview:imgView];
//    if (TEST_ADS) {
//        //test ad
//        UIButton* btnInterstitial = [UIButton buttonWithType:UIButtonTypeCustom];
//        [btnInterstitial  addTarget:self action:@selector(testShowInterstitial:) forControlEvents:UIControlEventTouchUpInside];
//        [btnInterstitial  setTitle:@"testShowInterstitial" forState:UIControlStateNormal];
//        btnInterstitial.frame = CGRectMake(40.0, 100.0, 120, 60.0);
//        btnInterstitial.backgroundColor = [UIColor blueColor];
//        [_unityView  addSubview:btnInterstitial ];
//        
//        UIButton* btnReward = [UIButton buttonWithType:UIButtonTypeCustom];
//        [btnReward addTarget:self action:@selector(testShowReward:) forControlEvents:UIControlEventTouchUpInside];
//        [btnReward setTitle:@"testShowReward" forState:UIControlStateNormal];
//        btnReward.frame = CGRectMake(40.0+160.0, 100.0, 120., 60.0);
//        btnReward.backgroundColor = [UIColor blueColor];
//        [_unityView  addSubview:btnReward];
//        
//        UIButton* btnMediaTool = [UIButton buttonWithType:UIButtonTypeCustom];
//        [btnMediaTool addTarget:self action:@selector(showMediaTool:) forControlEvents:UIControlEventTouchUpInside];
//        [btnMediaTool setTitle:@"showMediaTool" forState:UIControlStateNormal];
//        btnMediaTool.frame = CGRectMake(40, 100.0+100, 120., 60.0);
//        btnMediaTool.backgroundColor = [UIColor blueColor];
//        [_unityView  addSubview:btnMediaTool];
//    }
    [self createUI];
    [self preStartUnity];

    // if you wont use keyboard you may comment it out at save some memory
    [KeyboardDelegate Initialize];
#if !PLATFORM_TVOS && DISABLE_TOUCH_DELAYS
    for (UIGestureRecognizer *g in _window.gestureRecognizers)
    {
        g.delaysTouchesBegan = false;
    }
#endif

//    [self checkATEStatus];
//    [[AdManager shareInstance] preInitAdMediations];
    
    //程序启动
    //fbsdk
    [[FBSDKApplicationDelegate sharedInstance] application:application
                             didFinishLaunchingWithOptions:launchOptions];
    //google
    [FIRApp configure];
    //推送
    [FIRMessaging messaging].delegate = self;
    [GIDSignIn sharedInstance].clientID = [FIRApp defaultApp].options.clientID;
    [GIDSignIn sharedInstance].delegate = self;
    
    [Statistic getInstance];
    //pay
    [[PayHelper getInstance] initSDK];
    [[PayHelper getInstance] addIAPTransactionObserver];
    
    //推送注册
    if ([UNUserNotificationCenter class] != nil) {
        // iOS 10 or later
        // For iOS 10 display notification (sent via APNS)
        UNUserNotificationCenter *center = [UNUserNotificationCenter currentNotificationCenter];
        center.delegate = self;
        UNAuthorizationOptions authOptions = UNAuthorizationOptionAlert |
        UNAuthorizationOptionSound | UNAuthorizationOptionBadge;
        [center
         requestAuthorizationWithOptions:authOptions
         completionHandler:^(BOOL granted, NSError * _Nullable error) {
             // ...
         }];
    } else {
        // iOS 10 notifications aren't available; fall back to iOS 8-9 notifications.
        UIUserNotificationType allNotificationTypes =
        (UIUserNotificationTypeSound | UIUserNotificationTypeAlert | UIUserNotificationTypeBadge);
        UIUserNotificationSettings *settings =
        [UIUserNotificationSettings settingsForTypes:allNotificationTypes categories:nil];
        [application registerUserNotificationSettings:settings];
    }
    [application registerForRemoteNotifications];
    
    [[FIRMessaging messaging] tokenWithCompletion:^(NSString * _Nullable token, NSError * _Nullable error) {
        if (error != nil) {
            NSLog(@"FCM registtation error: %@",error);
        }else{
            NSLog(@"FCM registtation token111: %@",token);
            [[NSUserDefaults standardUserDefaults]setObject:token forKey:@"avg_notifytoken"];
            [[NSUserDefaults standardUserDefaults]synchronize];
        }
    }];
    [[FIRInstanceID instanceID] instanceIDWithHandler:^(FIRInstanceIDResult * _Nullable result,
                                                        NSError * _Nullable error) {
        if (error != nil) {
            NSLog(@"Error fetching remote instance ID: %@", error);
        } else {
            NSLog(@"Remote instance ID token: %@", result.token);
            //NSString* message = [NSString stringWithFormat:@"Remote InstanceID token: %@", result.token];
            //self.instanceIDTokenMessage.text = message;
        }
    }];
    [FIRMessaging messaging].autoInitEnabled = YES;
    //添加facebook deeplink 的延迟解析
    [FBSDKAppLinkUtility fetchDeferredAppLink:^(NSURL *url, NSError *error) {
        if (url) {
            [self cacheDeeplink:url];
            [[UIApplication sharedApplication] openURL:url];
        }
    }];
//    VKSdk *sdkInstance = [VKSdk initializeWithAppId:@"7519382"];
//    [sdkInstance registerDelegate:self];
//    [sdkInstance setUiDelegate:self];
    
    
    if (@available(iOS 11.3, *)) {
        [SKAdNetwork registerAppForAdNetworkAttribution];
    } else {
        // Fallback on earlier versions
    }
//    NSString *uuid = [UtilsCommon idfvString];
//    if ([uuid length] > 0) {
//        FIRCrashlytics* crashlytic= [FIRCrashlytics crashlytics];
//        if(crashlytic!=nil){
//            [crashlytic setUserID:uuid];
//        }
//    }else{
//        NSLog(@"set frabase uuid failed with empty device id ");
//    }
    
//    [UtilsCommon copyBookZip];
    ::printf("-> applicationDidFinishLaunching()16\n");
    return YES;
}

//firbase 推送
- (void)messaging:(FIRMessaging *)messaging didReceiveRegistrationToken:(NSString *)fcmToken {
    NSLog(@"FCM registration token: %@", fcmToken);
    // Notify about received token.
    NSDictionary *dataDict = [NSDictionary dictionaryWithObject:fcmToken forKey:@"token"];
    [[NSNotificationCenter defaultCenter] postNotificationName:
     @"FCMToken" object:nil userInfo:dataDict];
     [[NSUserDefaults standardUserDefaults]setObject:fcmToken forKey:@"avg_notifytoken"];
     [[NSUserDefaults standardUserDefaults]synchronize];
    // TODO: If necessary send token to application server.
    // Note: This callback is fired at each app startup and whenever a new token is generated.
}

- (void)applicationDidEnterBackground:(UIApplication*)application
{
    ::printf("-> applicationDidEnterBackground()\n");
    [[PayHelper getInstance] SetBackgroundSatus:YES];
}

- (void)applicationWillEnterForeground:(UIApplication*)application
{
    ::printf("-> applicationWillEnterForeground()\n");

    // applicationWillEnterForeground: might sometimes arrive *before* actually initing unity (e.g. locking on startup)
    if (_unityAppReady)
    {
        // if we were showing video before going to background - the view size may be changed while we are in background
        [GetAppController().unityView recreateRenderingSurfaceIfNeeded];
//        UnitySendMessage("CommonManager", "OnForeground","");
        [[PayHelper getInstance] SetBackgroundSatus:NO];
    }
}

- (void)applicationDidBecomeActive:(UIApplication*)application
{
    ::printf("-> applicationDidBecomeActive()\n");
    [[AppsFlyerLib shared] start];
    [self removeSnapshotViewController];

    if (_unityAppReady)
    {
        if (UnityIsPaused() && _wasPausedExternal == false)
        {
            UnityWillResume();
            UnityPause(0);
//            UnitySendMessage("CommonManager", "OnBecomeActive","");
        }
        if (_wasPausedExternal)
        {
            if (UnityIsFullScreenPlaying())
                TryResumeFullScreenVideo();
        }
        UnitySetPlayerFocus(1);
    }
    else if (!_startUnityScheduled)
    {
        _startUnityScheduled = true;
        [self performSelector: @selector(startUnity:) withObject: application afterDelay: 0];
    }

    _didResignActive = false;
    [UIApplication sharedApplication].applicationIconBadgeNumber = 0;

}

- (void)addSnapshotViewController
{
    // This is done on the next frame so that
    // in the case where unity is paused while going
    // into the background and an input is deactivated
    // we don't mess with the view hierarchy while taking
    // a view snapshot (case 760747).
    dispatch_async(dispatch_get_main_queue(), ^{
        // if we are active again, we don't need to do this anymore
        if (!_didResignActive || _snapshotViewController)
        {
            return;
        }

        UIView* snapshotView = [self createSnapshotView];

        if (snapshotView != nil)
        {
            _snapshotViewController = [[UIViewController alloc] init];
            _snapshotViewController.modalPresentationStyle = UIModalPresentationFullScreen;
            _snapshotViewController.view = snapshotView;

            [_rootController presentViewController: _snapshotViewController animated: false completion: nil];
        }
    });
}

- (void)removeSnapshotViewController
{
    // do this on the main queue async so that if we try to create one
    // and remove in the same frame, this always happens after in the same queue
    dispatch_async(dispatch_get_main_queue(), ^{
        if (_snapshotViewController)
        {
            // we've got a view on top of the snapshot view (3rd party plugin/social media login etc).
            if (_snapshotViewController.presentedViewController)
            {
                [self performSelector: @selector(removeSnapshotViewController) withObject: nil afterDelay: 0.05];
                return;
            }

            [_snapshotViewController dismissViewControllerAnimated: NO completion: nil];
            _snapshotViewController = nil;

            // Make sure that the keyboard input field regains focus after the application becomes active.
            [[KeyboardDelegate Instance] becomeFirstResponder];
        }
    });
}

- (void)applicationWillResignActive:(UIApplication*)application
{
    ::printf("-> applicationWillResignActive()\n");

    if (_unityAppReady)
    {
        UnitySetPlayerFocus(0);

        _wasPausedExternal = UnityIsPaused();
        if (_wasPausedExternal == false)
        {
            // Pause Unity only if we don't need special background processing
            // otherwise batched player loop can be called to run user scripts.
            if (!UnityGetUseCustomAppBackgroundBehavior())
            {
                // Force player to do one more frame, so scripts get a chance to render custom screen for minimized app in task manager.
                // NB: UnityWillPause will schedule OnApplicationPause message, which will be sent normally inside repaint (unity player loop)
                // NB: We will actually pause after the loop (when calling UnityPause).
                UnityWillPause();
                [self repaint];
                UnityPause(1);

                [self addSnapshotViewController];
            }
        }
    }

    _didResignActive = true;
}

- (void)applicationDidReceiveMemoryWarning:(UIApplication*)application
{
    ::printf("WARNING -> applicationDidReceiveMemoryWarning()\n");
    UnityLowMemory();
}

- (void)applicationWillTerminate:(UIApplication*)application
{
    ::printf("-> applicationWillTerminate()\n");

    [[PayHelper getInstance] removeIAPTransactionObserver];
    
    Profiler_UninitProfiler();

    if (_unityAppReady)
    {
        UnityCleanup();
        UnityCleanupTrampoline();
        
    }

    //extern void SensorsCleanup();
//    SensorsCleanup();
}

- (void)application:(UIApplication*)application handleEventsForBackgroundURLSession:(nonnull NSString *)identifier completionHandler:(nonnull void (^)())completionHandler
{
    NSDictionary* arg = @{identifier: completionHandler};
    AppController_SendNotificationWithArg(kUnityHandleEventsForBackgroundURLSession, arg);
}

// [START signin_handler] for google =================================================>>
- (void)signIn:(GIDSignIn *)signIn
didSignInForUser:(GIDGoogleUser *)user
     withError:(NSError *)error {
    // Perform any operations on signed in user here.
    //NSString *userId = user.userID;                  // For client-side use only!
    //NSString *idToken = user.authentication.idToken; // Safe to send to the server
    //NSString *fullName = user.profile.name;
    //NSString *givenName = user.profile.givenName;
    //NSString *familyName = user.profile.familyName;
    //NSString *email = user.profile.email;
    if (user == nil) {
        return;
    }
    
    NSString* imgUrlStr = @"";
    if (user.profile.hasImage){
        NSURL* imgUrl = [user.profile imageURLWithDimension:100];
        imgUrlStr = [imgUrl absoluteString];
    }
    
    
    NSString* defaultValue = @"";
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"id"] = user.userID?user.userID:defaultValue;
    dict[@"link"]  = defaultValue;
    dict[@"name"] = user.profile.name?user.profile.name:defaultValue;
    dict[@"cover"] = defaultValue;
    dict[@"email"] = user.profile.email?user.profile.email:defaultValue;
    dict[@"gender"] = defaultValue;
    dict[@"birthday"] = defaultValue;
    dict[@"locale"] = defaultValue;
    dict[@"age_range"]  = defaultValue;
    dict[@"age"]  = defaultValue;
    dict[@"logintype"] = @"google";
    dict[@"photo"] = imgUrlStr;
    
    
    NSString *jsonStr  = @"";
    BOOL isYes = [NSJSONSerialization isValidJSONObject:dict];
    if (isYes) {
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:0 error:NULL];
        jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        NSLog(@"===>%@",jsonStr);
        UnitySendMessage("SDK", "onThirdLoginFinish", [jsonStr UTF8String]);
    }
    
}
// [END signin_handler]


// This callback is triggered after the disconnect call that revokes data
// access to the user's resources has completed.
// [START disconnect_handler]
- (void)signIn:(GIDSignIn *)signIn didDisconnectWithUser:(GIDGoogleUser *)user
     withError:(NSError *)error {
    // Perform any operations when the user disconnects from app here.
    // [START_EXCLUDE]
    NSDictionary *statusText = @{@"statusText": @"Disconnected user" };
    [[NSNotificationCenter defaultCenter]
     postNotificationName:@"ToggleAuthUINotification"
     object:nil
     userInfo:statusText];
    // [END_EXCLUDE]
}
//处理adjust的deeplink
- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring>> * _Nullable))restorationHandler{
    //appsflyer
    [[AppsFlyerLib shared] continueUserActivity:userActivity restorationHandler:restorationHandler];
    NSURL *deeplink = [userActivity webpageURL];
    if (deeplink==nil) {
        return false;
    }else
    {
        [self cacheDeeplink:deeplink];
        return FALSE;
    }
}

//处理adjust的延迟deeplink
//和appsflyer不兼容，注释这快代码
//- (BOOL)adjustDeeplinkResponse:(NSURL *)deeplink
//{
//    if (deeplink==nil)
//    {
//        return false;
//    }else
//    {
//        //判断这条连接是否是指定的深度连接，因为bottomid肯定会在深度连接里
//        [self cacheDeeplink:deeplink];
//        return YES;
//    }
//}
// [END signin_handler] for google =================================================<<

#pragma applelogin
- (void)doAppleLogin
{
    NSLog(@"doAppleLogin");
    if (@available(iOS 13.0, tvOS 13.0, *)) {
        ASAuthorizationAppleIDProvider* provider = [[ASAuthorizationAppleIDProvider alloc] init];
        ASAuthorizationAppleIDRequest *request = [provider createRequest];
        [request setRequestedScopes: @[ASAuthorizationScopeEmail, ASAuthorizationScopeFullName]];

        ASAuthorizationController* controller = [[ASAuthorizationController alloc] initWithAuthorizationRequests:@[request]];
        controller.delegate = self;
        controller.presentationContextProvider = self;
        [controller performRequests];
    } else {
        // Fallback on earlier versions
    }
}

#pragma mark - ASAuthorizationControllerDelegate
//授权成功的回调
/**
 当授权成功后，我们可以通过这个拿到用户的 userID、email、fullName、authorizationCode、identityToken 以及 realUserStatus 等信息。
 */
-(void)authorizationController:(ASAuthorizationController *)controller
  didCompleteWithAuthorization:(ASAuthorization *)authorization API_AVAILABLE(ios(13.0)) {
    NSLog(@"doAppleLogin44444");
    if ([authorization.credential isKindOfClass:[ASAuthorizationAppleIDCredential class]]) {
        
        // 用户登录使用ASAuthorizationAppleIDCredential
        ASAuthorizationAppleIDCredential *credential = authorization.credential;
        
        //苹果用户唯一标识符，该值在同一个开发者账号下的所有 App 下是一样的，开发者可以用该唯一标识符与自己后台系统的账号体系绑定起来。
        NSString *userId = credential.user;
        //NSString *state = credential.state;
        NSPersonNameComponents *fullName = credential.fullName;
        //苹果用户信息，邮箱
        NSString *email = credential.email;
        //NSString *authorizationCode = [[NSString alloc] initWithData:credential.authorizationCode encoding:NSUTF8StringEncoding]; // refresh token
        /**
         验证数据，用于传给开发者后台服务器，然后开发者服务器再向苹果的身份验证服务端验证本次授权登录请求数据的有效性和真实性，详见 Sign In with Apple REST API。如果验证成功，可以根据 userIdentifier 判断账号是否已存在，若存在，则返回自己账号系统的登录态，若不存在，则创建一个新的账号，并返回对应的登录态给 App。
         */
        //NSString *identityToken = [[NSString alloc] initWithData:credential.identityToken encoding:NSUTF8StringEncoding];
        /**
         用于判断当前登录的苹果账号是否是一个真实用户
         取值有：unsupported、unknown、likelyReal。
         */
        //ASUserDetectionStatus realUserStatus = credential.realUserStatus;
        // 存储userId到keychain中，代码省略
        NSString* defaultValue = @"";
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        dict[@"id"] = userId?userId:defaultValue;
        dict[@"link"]  = defaultValue;
        dict[@"name"] = fullName.familyName?fullName.familyName:defaultValue;
        dict[@"cover"] = defaultValue;
        dict[@"email"] = email?email:defaultValue;
        dict[@"gender"] = defaultValue;
        dict[@"birthday"] = defaultValue;
        dict[@"locale"] = defaultValue;
        dict[@"age_range"]  = defaultValue;
        dict[@"age"]  = defaultValue;
        dict[@"logintype"] = @"apple";
        dict[@"photo"] = defaultValue;
        
//        NSLog(@"1%@",fullName.nickname);
//        NSLog(@"2%@",fullName.familyName);
//        NSLog(@"3%@",fullName.middleName);
//        NSLog(@"4%@",fullName.givenName);
        
        NSString *jsonStr  = @"";
        BOOL isYes = [NSJSONSerialization isValidJSONObject:dict];
        if (isYes) {
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:0 error:NULL];
            jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            NSLog(@"===>%@",jsonStr);
            UnitySendMessage("SDK", "onThirdLoginFinish", [jsonStr UTF8String]);
        }
       
               
    } else if ([authorization.credential isKindOfClass:[ASPasswordCredential class]]) {
        NSLog(@"doAppleLogin55555");
        // 用户登录使用现有的密码凭证
        //ASPasswordCredential *passwordCredential = authorization.credential;
        // 密码凭证对象的用户标识 用户的唯一标识
        //NSString *user = passwordCredential.user;
        // 密码凭证对象的密码
        //NSString *password = passwordCredential.password;

        
    } else {
        
    }
}

//失败的回调
-(void)authorizationController:(ASAuthorizationController *)controller didCompleteWithError:(NSError *)error API_AVAILABLE(ios(13.0)) {
    
}

#pragma mark - ASAuthorizationControllerPresentationContextProviding
//告诉代理应该在哪个window 展示授权界面给用户
-(ASPresentationAnchor)presentationAnchorForAuthorizationController:(ASAuthorizationController *)controller API_AVAILABLE(ios(13.0)) {
    
    return [UIApplication sharedApplication].delegate.window;
}

////vk callback
//-(void)vkSdkAccessAuthorizationFinishedWithResult:(VKAuthorizationResult *)result{
//    switch (result.state) {
//        case VKAuthorizationAuthorized:
//        case VKAuthorizationPending:{
//            VKRequest *usersReq = [[VKApi users] get:@{VK_API_FIELDS:@"email,photo_200"}];
//            [usersReq executeWithResultBlock:^(VKResponse *response) {
//                if (response!=NULL && [[response responseString] length] > 0) {
//                    NSData *jsonData = [response.responseString dataUsingEncoding:NSUTF8StringEncoding];
//                    NSError *err;
//                    NSDictionary *dicJson = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONReadingMutableContainers error:&err];
//                    NSArray *body = [dicJson mutableArrayValueForKey:@"response"];
//                    if (body!=nil && body.count > 0) {
//                        NSDictionary *body1 = body[0];
//                        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
//                        if ([body1.allKeys containsObject:@"id"]) {
//                            dict[@"id"] = [body1[@"id"] stringValue];
//                        }else{
//                            dict[@"id"]=@"";
//                        }
//                        
//                        dict[@"link"]  = @"";
//                        dict[@"name"] =@"";
//                        NSString* firstname = @"";
//                        NSString* lastname = @"";
//                        if ([body1.allKeys containsObject:@"first_name"]){
//                            firstname=body1[@"first_name"];
//                        }else{
//                            NSLog(@"no first name ");
//                        }
//
//                        if ([body1.allKeys containsObject:@"last_name"]){
//                            lastname=body1[@"last_name"];
//                        }else{
//                            NSLog(@"no last name ");
//                        }
//                        dict[@"name"] =[NSString stringWithFormat:@"%@ %@", firstname ,lastname];
//                        
//                        dict[@"cover"] = @"";
//                        if ([[VKSdk accessToken].email length]>0) {
//                            dict[@"email"] = [VKSdk accessToken].email;
//                        }else{
//                            dict[@"email"] = @"";
//                        }
//                        dict[@"gender"] = @"";
//                        dict[@"birthday"] = @"";
//                        dict[@"locale"] = @"";
//                        dict[@"age_range"]  = @"";
//                        dict[@"age"]  = @"";
//                        dict[@"logintype"] = @"vkontake";
//                        if ([body1.allKeys containsObject:@"photo_200"]){
//                            dict[@"photo"] = body1[@"photo_200"];
//                        }else{
//                            dict[@"photo"] = @"";
//                            NSLog(@"no  photo ");
//                        }
//                        
//                        NSString *jsonStr  = @"";
//                        BOOL isYes = [NSJSONSerialization isValidJSONObject:dict];
//                        if (isYes) {
//                            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:0 error:NULL];
//                            jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
//                            NSLog(@"vkontake the string to unity %@",jsonStr);
////                            UnitySendMessage("CommonManager", "ThirdLoginFinish", [jsonStr UTF8String]);
//                        }
//                    }
//                }
//                } errorBlock:^(NSError * error) {
//                if (error.code != VK_API_ERROR) {
//                    [error.vkError.request repeat];
//                    NSLog(@"vkontake request error and request again %ld",[error vkError].errorCode);
//                } else {
//                    NSLog(@"vkontake request  error  %ld",[error vkError].errorCode);
//                }
//            }];
//            break;
//        }
//        case VKAuthorizationError:{
//            NSLog(@"vkontake  authorize error state");
//            break;
//        }
//            
//        default:{
//            break;
//        }
//          
//    }
//}
//
//-(void)vkSdkShouldPresentViewController:(UIViewController *)controller{
//    UnityAppController *vc = GetAppController();
//    [vc.rootViewController presentViewController:controller animated:YES completion:nil];
//}
//
//
//- (void)vkSdkNeedCaptchaEnter:(VKError *)captchaError {
//    UnityAppController *tempview = GetAppController();
//    VKCaptchaViewController *vc = [VKCaptchaViewController captchaControllerWithError:captchaError];
//    [vc presentIn:tempview.rootViewController];
//}
//
//-(void)vkSdkUserAuthorizationFailed{
//    NSLog(@"vkontake  authorize failed");
//}


-(NSString*) GetNetworkStatus
{
    NSString* r = @"-";
    return r;
}

- (void)dealloc
{
    
}

//check ATE status above ios 14.0
//-(void)checkATEStatus{
//    if (@available(iOS 14.0, *)){
//        ATTrackingManagerAuthorizationStatus status = ATTrackingManager.trackingAuthorizationStatus;
//        switch(status){
//            case ATTrackingManagerAuthorizationStatusAuthorized:
//                [FBSDKSettings setAdvertiserTrackingEnabled:YES];
//                NSLog(@"checkATEStatus Tracking中允许App请求跟踪,set ATE YES");
//                //agree
//                break;
//            case ATTrackingManagerAuthorizationStatusDenied:
//                //refused
//                [FBSDKSettings setAdvertiserTrackingEnabled:NO];
//                NSLog(@"checkATEStatus Tracking中允许App请求跟踪,set ATE NO");
//                break;
//            default:
//                NSLog(@"checkATEStatus ATT 返回的其他状态");
//                break;
//        }
//    }
//}

#pragma mark - appsflyer 归因和deeplink

// AppsFlyerLib implementation
//Handle Conversion Data (Deferred Deep Link)
-(void)onConversionDataSuccess:(NSDictionary*) installData {
    for (id key in installData)
    {
      //do something
        id value = [installData objectForKey:key];
        NSLog(@"AppsFlyer Conversion Data %@ = %@",key,value);
    }
    
    id status = [installData objectForKey:@"af_status"];
    if([status isEqualToString:@"Non-organic"]) {
        id sourceID = [installData objectForKey:@"media_source"];
        id campaign = [installData objectForKey:@"campaign"];
        NSLog(@"AppsFlyer Conversion non-organic install. Media source: %@  Campaign: %@",sourceID,campaign);
    } else if([status isEqualToString:@"Organic"]) {
        NSLog(@"AppsFlyer Conversion organic install.");
    }
    
    
    id is_first = [installData objectForKey:@"is_first_launch"];
    if(is_first){
        NSLog(@"AppsFlyer Conversion is first launch.");
        
        NSString *deeplinkstr = (NSString*)[installData objectForKey:@"deep_link_value"];
        if(deeplinkstr!=nil and ![deeplinkstr isEqualToString:@""]){
            NSLog(@"AppsFlyer Conversion deeplink:  %@",deeplinkstr);
            deeplinkStr=deeplinkstr;
        }
    }else{
        NSLog(@"AppsFlyer Conversion not first launch.");
    }
    
    
    if(installData!=NULL){
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:installData options:0 error:nil];
        NSString *jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        
        afConversionData = jsonStr;
        afConversionFlag = @1;
    }
}
-(void)onConversionDataFail:(NSError *) error {
    NSLog(@"AppsFlyer onConversionDataFail %@",error);
    afConversionData = [NSString stringWithFormat:@"%@", error];;
    afConversionFlag = @2;
}

//Handle Direct Deep Link
- (void) onAppOpenAttribution:(NSDictionary*) attributionData {
    NSLog(@"AppsFlyer onAppOpenAttribution %@",attributionData);
    
    if(attributionData!=NULL){
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:attributionData options:0 error:nil];
        NSString *jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];

        afAttributionData = jsonStr;
        afAttributionFlag = @1;
    }
}
- (void) onAppOpenAttributionFailure:(NSError *)error {
    NSLog(@"AppsFlyer onAppOpenAttributionFailure%@",error);
    
    afAttributionData = [NSString stringWithFormat:@"%@", error];;
    afAttributionFlag = @2;
}

-(void)didResolveDeepLink:(AppsFlyerDeepLinkResult *_Nonnull)result
{
    AFSDKDeepLinkResultStatus status = result.status;
    if(status==AFSDKDeepLinkResultStatus::AFSDKDeepLinkResultStatusFound){
        NSLog(@"AppsFlyer DeepLink Result found 111");
    } else if(status==AFSDKDeepLinkResultStatus::AFSDKDeepLinkResultStatusNotFound) {
        NSLog(@"AppsFlyer DeepLink Result not found 222");
        return;
    }else if(status==AFSDKDeepLinkResultStatus::AFSDKDeepLinkResultStatusFailure) {
        NSLog(@"AppsFlyer DeepLink Result not found 333 error:%@",result.error);
        return;
    }
    AppsFlyerDeepLink *deeplink = result.deepLink;
    if(deeplink!=NULL){
        NSString *linkstr  = deeplink.deeplinkValue;
        
        //NSURL *deepurl  = [[NSURL alloc] initWithString:deeplinkStr];
        //[self cacheDeeplink:deepurl];
        if (linkstr!=nil &&  [linkstr length] > 0) {
            NSLog(@"AppsFlyer deeplink is  %@",linkstr);
            deeplinkStr=linkstr;
        }
        
        NSLog(@"AppsFlyer DeepLink 444 deeplinkStr:%@",deeplinkStr);
    }
    
}
@end


void AppController_SendNotification(NSString* name)
{
    [[NSNotificationCenter defaultCenter] postNotificationName: name object: GetAppController()];
}

void AppController_SendNotificationWithArg(NSString* name, id arg)
{
    [[NSNotificationCenter defaultCenter] postNotificationName: name object: GetAppController() userInfo: arg];
}

void AppController_SendUnityViewControllerNotification(NSString* name)
{
    [[NSNotificationCenter defaultCenter] postNotificationName: name object: UnityGetGLViewController()];
}

extern "C" UIWindow*            UnityGetMainWindow()
{
    return GetAppController().mainDisplay.window;
}

extern "C" UIViewController*    UnityGetGLViewController()
{
    return GetAppController().rootViewController;
}

extern "C" UIView*              UnityGetGLView()
{
    return GetAppController().unityView;
}

extern "C" ScreenOrientation    UnityCurrentOrientation()   { return GetAppController().unityView.contentOrientation; }


bool LogToNSLogHandler(LogType logType, const char* log, va_list list)
{
    NSLogv([NSString stringWithUTF8String: log], list);
    return true;
}

static void AddNewAPIImplIfNeeded();

// From https://stackoverflow.com/questions/4744826/detecting-if-ios-app-is-run-in-debugger
static bool isDebuggerAttachedToConsole(void)
// Returns true if the current process is being debugged (either
// running under the debugger or has a debugger attached post facto).
{
    int                 junk;
    int                 mib[4];
    struct kinfo_proc   info;
    size_t              size;

    // Initialize the flags so that, if sysctl fails for some bizarre
    // reason, we get a predictable result.

    info.kp_proc.p_flag = 0;

    // Initialize mib, which tells sysctl the info we want, in this case
    // we're looking for information about a specific process ID.

    mib[0] = CTL_KERN;
    mib[1] = KERN_PROC;
    mib[2] = KERN_PROC_PID;
    mib[3] = getpid();

    // Call sysctl.

    size = sizeof(info);
    junk = sysctl(mib, sizeof(mib) / sizeof(*mib), &info, &size, NULL, 0);
    assert(junk == 0);

    // We're being debugged if the P_TRACED flag is set.

    return ((info.kp_proc.p_flag & P_TRACED) != 0);
}

void UnityInitTrampoline()
{
    InitCrashHandling();

    NSString* version = [[UIDevice currentDevice] systemVersion];
#define CHECK_VER(s) [version compare: s options: NSNumericSearch] != NSOrderedAscending
    _ios81orNewer  = CHECK_VER(@"8.1"),  _ios82orNewer  = CHECK_VER(@"8.2"),  _ios83orNewer  = CHECK_VER(@"8.3");
    _ios90orNewer  = CHECK_VER(@"9.0"),  _ios91orNewer  = CHECK_VER(@"9.1");
    _ios100orNewer = CHECK_VER(@"10.0"), _ios101orNewer = CHECK_VER(@"10.1"), _ios102orNewer = CHECK_VER(@"10.2"), _ios103orNewer = CHECK_VER(@"10.3");
    _ios110orNewer = CHECK_VER(@"11.0"), _ios111orNewer = CHECK_VER(@"11.1"), _ios112orNewer = CHECK_VER(@"11.2");
    _ios130orNewer  = CHECK_VER(@"13.0");

#undef CHECK_VER

    AddNewAPIImplIfNeeded();

#if !TARGET_IPHONE_SIMULATOR
    // Use NSLog logging if a debugger is not attached, otherwise we write to stdout.
    if (!isDebuggerAttachedToConsole())
        UnitySetLogEntryHandler(LogToNSLogHandler);
#endif
}

extern "C" bool UnityiOS81orNewer() { return _ios81orNewer; }
extern "C" bool UnityiOS82orNewer() { return _ios82orNewer; }
extern "C" bool UnityiOS90orNewer() { return _ios90orNewer; }
extern "C" bool UnityiOS91orNewer() { return _ios91orNewer; }
extern "C" bool UnityiOS100orNewer() { return _ios100orNewer; }
extern "C" bool UnityiOS101orNewer() { return _ios101orNewer; }
extern "C" bool UnityiOS102orNewer() { return _ios102orNewer; }
extern "C" bool UnityiOS103orNewer() { return _ios103orNewer; }
extern "C" bool UnityiOS110orNewer() { return _ios110orNewer; }
extern "C" bool UnityiOS111orNewer() { return _ios111orNewer; }
extern "C" bool UnityiOS112orNewer() { return _ios112orNewer; }
extern "C" bool UnityiOS130orNewer() { return _ios130orNewer; }

// sometimes apple adds new api with obvious fallback on older ios.
// in that case we simply add these functions ourselves to simplify code
static void AddNewAPIImplIfNeeded()
{
    if (![[CADisplayLink class] instancesRespondToSelector: @selector(setPreferredFramesPerSecond:)])
    {
        IMP CADisplayLink_setPreferredFramesPerSecond_IMP = imp_implementationWithBlock(^void(id _self, NSInteger fps) {
            typedef void (*SetFrameIntervalFunc)(id, SEL, NSInteger);
            UNITY_OBJC_CALL_ON_SELF(_self, @selector(setFrameInterval:), SetFrameIntervalFunc, (int)(60.0f / fps));
        });
//        class_replaceMethod([CADisplayLink class], @selector(setPreferredFramesPerSecond:), CADisplayLink_setPreferredFramesPerSecond_IMP, CADisplayLink_setPreferredFramesPerSecond_Enc);
    }

    if (![[UIScreen class] instancesRespondToSelector: @selector(maximumFramesPerSecond)])
    {
        IMP UIScreen_MaximumFramesPerSecond_IMP = imp_implementationWithBlock(^NSInteger(id _self) {
            return 60;
        });
        class_replaceMethod([UIScreen class], @selector(maximumFramesPerSecond), UIScreen_MaximumFramesPerSecond_IMP, UIScreen_maximumFramesPerSecond_Enc);
    }

    if (![[UIView class] instancesRespondToSelector: @selector(safeAreaInsets)])
    {
        IMP UIView_SafeAreaInsets_IMP = imp_implementationWithBlock(^UIEdgeInsets(id _self) {
            return UIEdgeInsetsZero;
        });
        class_replaceMethod([UIView class], @selector(safeAreaInsets), UIView_SafeAreaInsets_IMP, UIView_safeAreaInsets_Enc);
    }
}
