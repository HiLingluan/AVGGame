//
//  UitilsCommon.m
//  Unity-iPhone
//
//  Created by 玩贝 on 2018/11/22.
//

#import "UtilsCommon.h"
#import "KeychainItemWrapper.h"
#import "Macro.h"
#import "UnityAppController.h"
#import "UnityFramework/UnityFramework.h"

#import <FBSDKCoreKit/FBSDKCoreKit.h>
#import <FBSDKLoginKit/FBSDKLoginManager.h>
#import <FBSDKLoginKit/FBSDKLoginManagerLoginResult.h>
#import <FBSDKCoreKit/FBSDKGraphRequest.h>
#import <Firebase.h>
#import <GoogleSignIn/GoogleSignIn.h>
#import <AdSupport/AdSupport.h>

#import <sys/utsname.h>


static NSString *  KEY_USERNAME_IDFV_DIC = @"";
static NSString *  KEY_USERNAME_IDFV_KEY = @"";



@implementation UtilsCommon


#pragma mark - 保存在系统密钥里
+ (NSString *)idfvString
{
    NSString *idfvString = nil;
    KEY_USERNAME_IDFV_DIC = [[NSString alloc] initWithFormat:@"%@,%@", [[NSBundle mainBundle] bundleIdentifier], @".appstore.username" ];
    KEY_USERNAME_IDFV_KEY = [[NSString alloc] initWithFormat:@"%@,%@", [[NSBundle mainBundle] bundleIdentifier], @".appstore.password" ];
    //获取UUID 并保存到keychain
    NSMutableDictionary *dic = [KeychainItemWrapper load:KEY_USERNAME_IDFV_DIC];
    if (OBJECT_IS_NIL(dic)) {
        NSMutableDictionary *idfvDic = [NSMutableDictionary dictionary];
        idfvString = [[[UIDevice currentDevice] identifierForVendor] UUIDString];
        [idfvDic setObject:idfvString forKey:KEY_USERNAME_IDFV_KEY];
        [KeychainItemWrapper save:KEY_USERNAME_IDFV_DIC data:idfvDic];
    }else{
        idfvString = [dic objectForKey:KEY_USERNAME_IDFV_KEY];
    }
    
    return idfvString;
}

//+(NSString*)getIDFA{
//    // if (IS_IOS14LATER) {
//    //     if ([ASIdentifierManager.sharedManager isAdvertisingTrackingEnabled]) {
//    //         NSString * idfa = [ASIdentifierManager.sharedManager advertisingIdentifier].UUIDString;
//    //         return idfa;
//    //     } else {
//    //         return @"";
//    //     }
//    
//    // }else
//    // {
//        NSString *idfa = [ASIdentifierManager sharedManager].advertisingIdentifier.UUIDString;
//        return idfa;
//    // }
//}


/*
 * 执行google登录
 */
+(void)doGoogleLogin
{
    NSLog(@"doGoogleLogindoGoogleLogindoGoogleLogindoGoogleLogin");
    UIViewController *currentView = [UIApplication sharedApplication].delegate.window.rootViewController;
    [GIDSignIn sharedInstance].presentingViewController = currentView;
    [[GIDSignIn sharedInstance] signIn];
}


/*
 * 执行facebook登录
 */
+(void)doFacebookLogin
{
    UIViewController* currentView = [UIApplication sharedApplication].delegate.window.rootViewController;
    FBSDKLoginManager *login = [[FBSDKLoginManager alloc] init];
    [login logOut];////这个一定要写，不然会出现换一个帐号就无法获取信息的错误
    //执行登录
    [login logInWithPermissions:@[@"public_profile",@"email"] fromViewController:currentView handler:^(FBSDKLoginManagerLoginResult * _Nullable result, NSError * _Nullable error) {
        NSLog(@"%@",result);
        if (error) {
            NSLog(@"Process error");
        } else if (result.isCancelled) {
            NSLog(@"Cancelled");
        } else {
            //get uerinfo by login reult
            [self getFacebookUserInfoWithResult:result];
        }
    }];
    
}

#pragma mark - 退出登录
+(void)doFacebookLogOut
{
    FBSDKLoginManager *login = [[FBSDKLoginManager alloc] init];
    [login logOut];
}

+(void)doGoogleLogOut
{
    [[GIDSignIn sharedInstance] signOut];
}

//获取用户信息 picture用户头像

//data format
/*
 {
 "age_range" =     {
 min = 21;
 };
 "first_name" = "\U6dd1\U5a1f";
 gender = female;
 id = 320561731689112;
 "last_name" = "\U6f58";
 link = "https://www.facebook.com/app_scoped_user_id/320561731689112/";
 locale = "zh_CN";
 name = "\U6f58\U6dd1\U5a1f";
 picture =     {
 data =         {
 "is_silhouette" = 0;
 url = "https://fb-s-c-a.akamaihd.net/h-ak-fbx/v/t1.0-1/p50x50/18157158_290358084709477_3057447496862917877_n.jpg?oh=01ba6b3a5190122f3959a3f4ed553ae8&oe=5A0ADBF5&__gda__=1509731522_7a226b0977470e13b2611f970b6e2719";
 };
 };
 timezone = 8;
 "updated_time" = "2017-04-29T07:54:31+0000";
 verified = 1;
 }
 */

+(void)getFacebookUserInfoWithResult:(FBSDKLoginManagerLoginResult *)result
{
    if ([FBSDKAccessToken currentAccessToken]) {
        FBSDKGraphRequest *request = [[FBSDKGraphRequest alloc]
                                      initWithGraphPath:result.token.userID
                                      parameters:@{@"fields": @"id,name,cover,email,age_range,first_name,last_name,link,gender,birthday,locale,picture,timezone,updated_time,verified"}
                                      tokenString:[FBSDKAccessToken currentAccessToken].tokenString
                                      version:[FBSDKSettings graphAPIVersion]
                                      HTTPMethod:@"GET"];
        [request startWithCompletion:^(id<FBSDKGraphRequestConnecting>  _Nullable connection, id  _Nullable result, NSError * _Nullable error) {
            if (error) {
                NSLog(@"Error: facebokk login error!!!");
                return;
            }
            // Handle the result
            NSString* defaultValue = @"";
            NSMutableDictionary *dict = [NSMutableDictionary dictionary];
            dict[@"id"] = result[@"id"]?result[@"id"]:defaultValue;
            dict[@"link"] = result[@"link"]?result[@"link"]:defaultValue;
            dict[@"name"] = result[@"name"]?result[@"name"]:defaultValue;
            dict[@"cover"] = result[@"cover"]?result[@"cover"]:defaultValue;
            dict[@"email"] = result[@"email"]?result[@"email"]:defaultValue;
            dict[@"gender"] = result[@"gender"]?result[@"gender"]:defaultValue;
            dict[@"birthday"] = result[@"birthday"]?result[@"birthday"]:defaultValue;
            dict[@"locale"] = result[@"locale"]?result[@"locale"]:defaultValue;
            dict[@"logintype"] = @"facebook";
            NSString* picUrl = result[@"picture"]? result[@"picture"][@"data"][@"url"] :defaultValue;
            dict[@"photo"] = picUrl?picUrl:defaultValue;
            //age and age_range
            NSMutableDictionary *age_rangeDict  =result[@"age_range"];
            if ( [NSJSONSerialization isValidJSONObject:age_rangeDict]){
                NSData *jsonData = [NSJSONSerialization dataWithJSONObject:age_rangeDict options:0 error:NULL];
                dict[@"age_range"]  = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            }else{
                dict[@"age_range"]  = @"";
            }
            dict[@"age"]= result[@"age_range"]? result[@"age_range"][@"min"]:defaultValue;

            //此时应该回传到lua
            NSString *jsonStr  = @"";
            BOOL isYes = [NSJSONSerialization isValidJSONObject:dict];
            if (isYes) {
                NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:0 error:NULL];
                jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
                NSLog(@"===>%@",jsonStr);
//                [UnityFramework getInstance];
                // sendMessageToGOWithName("SDK", "onThirdLoginFinish",[jsonStr UTF8String])
                UnitySendMessage("SDK", "onThirdLoginFinish", [jsonStr UTF8String]);
            }
        }];
    }
}


#pragma mark - 分享
+(void)sharType:(int)type url:(NSString *)url
{
    NSMutableArray *activityItems = [NSMutableArray array];
    if(type == 2)
    {
        UIImage *img = [[UIImage alloc] initWithContentsOfFile:url];
        [activityItems addObject:img];
    }else
    {
        [activityItems addObject:url];
    }
    
    UIActivityViewController *activity = [[UIActivityViewController alloc] initWithActivityItems:activityItems applicationActivities:nil];
    UIViewController* currentView = [UIApplication sharedApplication].delegate.window.rootViewController;
    // show action sheet
    if (DEVICE_IS_IPAD) {
        if ([activity respondsToSelector:@selector(popoverPresentationController)]) {        activity.popoverPresentationController.sourceView = currentView.view;
        }
    }
    [currentView presentViewController:activity animated:YES completion:NULL];
    activity.completionWithItemsHandler = ^(UIActivityType  _Nullable activityType, BOOL completed, NSArray * _Nullable returnedItems, NSError * _Nullable activityError) {
        if (completed) {
            
            NSLog(@"分享成功");
//            UnitySendMessage("UITop", "ShareOverCallBack", [@"1" UTF8String]);

        }else{
            
            NSLog(@"分享取消");
//            UnitySendMessage("UITop", "ShareOverCallBack", [@"0" UTF8String]);
        }
    };
}

+(void)sharType2:(NSString *)filepath:(NSString *)title:(NSString *)shareUrl
{
    NSArray *activityItems = [NSMutableArray array];

    UIImage *img = [[UIImage alloc] initWithContentsOfFile:filepath];
    NSString* shareMsg = title;
    NSURL* urlToShare = [NSURL URLWithString:shareUrl];
    
    activityItems = @[urlToShare,shareMsg,img];
    
    UIActivityViewController *activity = [[UIActivityViewController alloc] initWithActivityItems:activityItems applicationActivities:nil];
    UIViewController* currentView = [UIApplication sharedApplication].delegate.window.rootViewController;
    // show action sheet
    if (DEVICE_IS_IPAD) {
        if ([activity respondsToSelector:@selector(popoverPresentationController)]) {        activity.popoverPresentationController.sourceView = currentView.view;
        }
    }
    [currentView presentViewController:activity animated:YES completion:NULL];
    activity.completionWithItemsHandler = ^(UIActivityType  _Nullable activityType, BOOL completed, NSArray * _Nullable returnedItems, NSError * _Nullable activityError) {
        NSLog(@"act type %@",activityType);
        if (completed) {
            
            NSLog(@"分享成功");
//            UnitySendMessage("UITop", "ShareOverCallBack", [@"1" UTF8String]);

        }else{
            
            NSLog(@"分享取消");
//            UnitySendMessage("UITop", "ShareOverCallBack", [@"0" UTF8String]);
        }
    };
}

#pragma mark - 手机型号
+ (NSString *)iphoneType {
    
    struct utsname systemInfo;
    
    uname(&systemInfo);
    
    NSString *platform = [NSString stringWithCString:systemInfo.machine encoding:NSASCIIStringEncoding];
    
    if ([platform isEqualToString:@"iPhone1,1"]) return @"iPhone 2G";
    
    if ([platform isEqualToString:@"iPhone1,2"]) return @"iPhone 3G";
    
    if ([platform isEqualToString:@"iPhone2,1"]) return @"iPhone 3GS";
    
    if ([platform isEqualToString:@"iPhone3,1"]) return @"iPhone 4";
    
    if ([platform isEqualToString:@"iPhone3,2"]) return @"iPhone 4";
    
    if ([platform isEqualToString:@"iPhone3,3"]) return @"iPhone 4";
    
    if ([platform isEqualToString:@"iPhone4,1"]) return @"iPhone 4S";
    
    if ([platform isEqualToString:@"iPhone5,1"]) return @"iPhone 5";
    
    if ([platform isEqualToString:@"iPhone5,2"]) return @"iPhone 5";
    
    if ([platform isEqualToString:@"iPhone5,3"]) return @"iPhone 5c";
    
    if ([platform isEqualToString:@"iPhone5,4"]) return @"iPhone 5c";
    
    if ([platform isEqualToString:@"iPhone6,1"]) return @"iPhone 5s";
    
    if ([platform isEqualToString:@"iPhone6,2"]) return @"iPhone 5s";
    
    if ([platform isEqualToString:@"iPhone7,1"]) return @"iPhone 6 Plus";
    
    if ([platform isEqualToString:@"iPhone7,2"]) return @"iPhone 6";
    
    if ([platform isEqualToString:@"iPhone8,1"]) return @"iPhone 6s";
    
    if ([platform isEqualToString:@"iPhone8,2"]) return @"iPhone 6s Plus";
    
    if ([platform isEqualToString:@"iPhone8,4"]) return @"iPhone SE";
    
    if ([platform isEqualToString:@"iPhone9,1"]) return @"iPhone 7";
    
    if ([platform isEqualToString:@"iPhone9,2"]) return @"iPhone 7 Plus";
    
    if ([platform isEqualToString:@"iPhone10,1"]) return @"iPhone_8";
    
    if ([platform isEqualToString:@"iPhone10,4"]) return @"iPhone_8";
    
    if ([platform isEqualToString:@"iPhone10,2"]) return @"iPhone_8_Plus";
    
    if ([platform isEqualToString:@"iPhone10,5"]) return @"iPhone_8_Plus";
    
    if ([platform isEqualToString:@"iPhone10,3"]) return @"iPhone_X";
    
    if ([platform isEqualToString:@"iPhone10,6"]) return @"iPhone_X";
    
    if ([platform isEqualToString:@"iPhone11,2"]) return @"iPhone XS";
    
    if ([platform isEqualToString:@"iPhone11,4"]) return @"iPhone XS Max";
    
    if ([platform isEqualToString:@"iPhone11,6"]) return @"iPhone XS Max";
    
    if ([platform isEqualToString:@"iPhone11,8"]) return @"iPhone XR";
    
    if ([platform isEqualToString:@"iPhone12,1"]) return @"iPhone 11";
    
    if ([platform isEqualToString:@"iPhone12,3"]) return @"iPhone 11 Pro";
    
    if ([platform isEqualToString:@"iPhone12,5"]) return @"iPhone 11 Pro Max";

    if ([platform isEqualToString:@"iPhone13,1"]) return @"iPhone 12 mini";

    if ([platform isEqualToString:@"iPhone13,2"]) return @"iPhone 12";

    if ([platform isEqualToString:@"iPhone13,3"]) return @"iPhone 12 Pro";

    if ([platform isEqualToString:@"iPhone13,4"]) return @"iPhone 12 Pro Max";
    
    if ([platform isEqualToString:@"iPod1,1"])   return @"iPod Touch 1G";
    
    if ([platform isEqualToString:@"iPod2,1"])   return @"iPod Touch 2G";
    
    if ([platform isEqualToString:@"iPod3,1"])   return @"iPod Touch 3G";
    
    if ([platform isEqualToString:@"iPod4,1"])   return @"iPod Touch 4G";
    
    if ([platform isEqualToString:@"iPod5,1"])   return @"iPod Touch 5G";
    
    if ([platform isEqualToString:@"iPad1,1"])   return @"iPad 1G";
    
    if ([platform isEqualToString:@"iPad2,1"])   return @"iPad 2";
    
    if ([platform isEqualToString:@"iPad2,2"])   return @"iPad 2";
    
    if ([platform isEqualToString:@"iPad2,3"])   return @"iPad 2";
    
    if ([platform isEqualToString:@"iPad2,4"])   return @"iPad 2";
    
    if ([platform isEqualToString:@"iPad2,5"])   return @"iPad Mini 1G";
    
    if ([platform isEqualToString:@"iPad2,6"])   return @"iPad Mini 1G";
    
    if ([platform isEqualToString:@"iPad2,7"])   return @"iPad Mini 1G";
    
    if ([platform isEqualToString:@"iPad3,1"])   return @"iPad 3";
    
    if ([platform isEqualToString:@"iPad3,2"])   return @"iPad 3";
    
    if ([platform isEqualToString:@"iPad3,3"])   return @"iPad 3";
    
    if ([platform isEqualToString:@"iPad3,4"])   return @"iPad 4";
    
    if ([platform isEqualToString:@"iPad3,5"])   return @"iPad 4";
    
    if ([platform isEqualToString:@"iPad3,6"])   return @"iPad 4";
    
    if ([platform isEqualToString:@"iPad4,1"])   return @"iPad Air";
    
    if ([platform isEqualToString:@"iPad4,2"])   return @"iPad Air";
    
    if ([platform isEqualToString:@"iPad4,3"])   return @"iPad Air";
    
    if ([platform isEqualToString:@"iPad4,4"])   return @"iPad Mini 2G";
    
    if ([platform isEqualToString:@"iPad4,5"])   return @"iPad Mini 2G";
    
    if ([platform isEqualToString:@"iPad4,6"])   return @"iPad Mini 2G";
    
    if ([platform isEqualToString:@"i386"])      return @"iPhone Simulator";
    
    if ([platform isEqualToString:@"x86_64"])    return @"iPhone Simulator";
    
    return platform;
    
}

/*
 * 执行vk登录
 */
//+(void)doVKLogin
//{
//    [VKSdk wakeUpSession:nil completeBlock:^(VKAuthorizationState state, NSError *error) {
//               if (state==VKAuthorizationAuthorized) {
//                   [VKSdk forceLogout];
//                   NSArray *SCOPE = @[@"email"];
//                   [VKSdk authorize:SCOPE];
//               }else if(state==VKAuthorizationInitialized){
//                   NSArray *SCOPE = @[@"email"];
//                   [VKSdk authorize:SCOPE];
//               }else if (error){
//                   NSLog(@"vkontake  wakeUpSession error %@",error.description);
//               }}];
//}

#pragma mark - 退出vk登录
//+(void)doVKLogout
//{
//    [VKSdk forceLogout];
//}

#pragma mark -拷贝文件
//+ (void )copyBookZip
//{
//    GCD_BACK(^{
//        NSString *path = [NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES) lastObject];
//        NSFileManager *fileMrg = [NSFileManager defaultManager];
//        NSArray *storyArr = [[NSBundle mainBundle] pathsForResourcesOfType:@"txt" inDirectory:@"Data/Raw/StoryConfigs"];
//        NSArray *bookArr = [[NSBundle mainBundle] pathsForResourcesOfType:@"txt" inDirectory:@"Data/Raw/BooksZip/data_s"];
//        NSArray *languaArr = [[NSBundle mainBundle] pathsForResourcesOfType:@"txt" inDirectory:@"Data/Raw/Language"];
//
//        if (![fileMrg fileExistsAtPath:[path stringByAppendingPathComponent:@"StoryConfigs"]]) {
//            [fileMrg createDirectoryAtPath:[path stringByAppendingPathComponent:@"StoryConfigs"] withIntermediateDirectories:YES attributes:nil error:NULL];
//            //NSLog(@"error--1");
//        }
//        if (![fileMrg fileExistsAtPath:[path stringByAppendingPathComponent:@"BooksZip/data_s"]]) {
//            [fileMrg createDirectoryAtPath:[path stringByAppendingPathComponent:@"BooksZip/data_s"] withIntermediateDirectories:YES attributes:nil error:NULL];
//            //NSLog(@"error--1");
//        }
//        if (![fileMrg fileExistsAtPath:[path stringByAppendingPathComponent:@"ChaptersAssets"]]) {
//            [fileMrg createDirectoryAtPath:[path stringByAppendingPathComponent:@"ChaptersAssets"] withIntermediateDirectories:YES attributes:nil error:NULL];
//            //NSLog(@"error--1");
//        }
//
//        for (NSString *file in storyArr) {
//            NSString *savepath = [path stringByAppendingPathComponent:@"StoryConfigs"];
//            savepath = [savepath stringByAppendingPathComponent:[file lastPathComponent]];
//            if (![fileMrg fileExistsAtPath:savepath]) {
//                [fileMrg copyItemAtPath:file toPath:savepath error:NULL];
//            }
//        }
//        for (NSString *file in bookArr) {
//            NSString *savepath = [path stringByAppendingPathComponent:@"BooksZip/data_s"];
//            savepath = [savepath stringByAppendingPathComponent:[file lastPathComponent]];
//            if (![fileMrg fileExistsAtPath:savepath]) {
//                [fileMrg copyItemAtPath:file toPath:savepath error:NULL];
//            }
//        }
//        for (NSString *file in languaArr) {
//            NSString *savepath = [path stringByAppendingPathComponent:@"ChaptersAssets"];
//            savepath = [savepath stringByAppendingPathComponent:[file lastPathComponent]];
//            if (![fileMrg fileExistsAtPath:savepath]) {
//                [fileMrg copyItemAtPath:file toPath:savepath error:NULL];
//            }
//        }
//    });
//
//}


//+ (NSString *)readBookZip:(NSString *)filename
//{
//    NSString *path = [[NSBundle mainBundle] pathForResource:[NSString stringWithFormat:@"Data/Raw/%@",filename] ofType:@"txt"];
//    if (path != NULL) {
//        NSString *json = [[NSString alloc] initWithContentsOfFile:path encoding:NSUTF8StringEncoding error:nil];
//        if (json == NULL || json == nil) {
//            json = @"";
//        }
//        return json;
//    }
//    return @"";
//}

+ (BOOL)isPhoneX {
    BOOL iPhoneX = NO;
    if (UIDevice.currentDevice.userInterfaceIdiom != UIUserInterfaceIdiomPhone) {//判断是否是手机
        return iPhoneX;
    }
    if (@available(iOS 11.0, *)) {
        UIWindow *mainWindow = [[[UIApplication sharedApplication] delegate] window];
        if (mainWindow.safeAreaInsets.bottom > 0.0) {
            iPhoneX = YES;
        }
    }
    return iPhoneX;
}

+(BOOL)isNotchScreen {
    
    if ([UIDevice currentDevice].userInterfaceIdiom == UIUserInterfaceIdiomPad) {
        return NO;
    }
    
    CGSize size = [UIScreen mainScreen].bounds.size;
    NSInteger notchValue = size.width / size.height * 100;
    
    if (216 == notchValue || 46 == notchValue) {
        return YES;
    }
    
    return NO;
}

//add string key and value.
+(void)addDic:(NSMutableDictionary*)dic withStringKey:(NSString*)strkey withStringValue:(NSString*)strvalue{
    if (dic && [strkey length]>0 && [strvalue length]>0) {
        dic[strkey] =strvalue;
    }
}

//get jsonstr
+(NSString*)getjsonstr:(NSMutableDictionary*)dic{
    NSString *dicjson=nil;
    if (dic) {
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dic options:0 error:NULL];
        dicjson = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    }
    return dicjson;
}

//+(NSString*)getTestAdunit:(BOOL)isRewward{
//    if (isRewward==YES) {
//        return AdRewardId;
//    }else{
//        return  AdInterstitial;
//    }
//}
@end
