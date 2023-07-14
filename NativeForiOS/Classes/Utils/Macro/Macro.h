//
//  Macro.h
//  Flip
//
//  Created by 玩贝 on 2018/7/24.
//  Copyright © 2018年 玩贝. All rights reserved.
//

#ifndef Macro_h
#define Macro_h


#endif /* Macro_h */



#pragma mark - 系统版本及设备型号
#define IS_IPHONE_X     (SCREEN_WIDTH == 375 && SCREEN_HEIGHT == 812)
#define IS_IOS9LATER    ([[[UIDevice currentDevice] systemVersion] floatValue] >= 9.0 ? YES : NO)
#define IS_IOS10LATER   ([[UIDevice currentDevice].systemVersion doubleValue] >= 10.0? YES : NO)
#define IS_IOS11LATER   ([[UIDevice currentDevice].systemVersion doubleValue] >= 11.0)
#define IS_IOS13LATER   ([[UIDevice currentDevice].systemVersion doubleValue] >= 13.0)
#define IS_IOS14LATER   ([[UIDevice currentDevice].systemVersion doubleValue] >= 14.0)

#pragma mark - 系统值获取
#define SCREEN_WIDTH     ([UIScreen mainScreen].bounds.size.width)
#define SCREEN_HEIGHT    ([UIScreen mainScreen].bounds.size.height)

#define STATUSBAR_HEIGHT    (IS_IPHONE_X ? 44 : [[UIApplication sharedApplication] statusBarFrame].size.height)
#define NAVBAR_HEIGHT       (STATUSBAR_HEIGHT + 44)
#define TABBAR_HEIGHT       44.0


#pragma mark - 颜色
#define RGB_COLOR(r,g,b) [UIColor colorWithRed:(r)/255.0f green:(g)/255.0f blue:(b)/255.0f alpha:1]
#define RGBA_COLOR(r,g,b,a) [UIColor colorWithRed:(r)/255.0f green:(g)/255.0f blue:(b)/255.0f alpha:(a)]
//UIColorFromRGB(0x345678)
#define HEX_RGB_COLOR(rgbValue) [UIColor colorWithRed:((float)((rgbValue & 0xFF0000) >> 16))/255.0 green:((float)((rgbValue & 0xFF00) >> 8))/255.0 blue:((float)(rgbValue & 0xFF))/255.0 alpha:1.0]


#pragma mark - 字体
#define NORMALTEXTFONT   @"SofiaProMedium"


#pragma mark - GCD
#define GCD_BACK(block) dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), block)
#define GCD_MAIN(block) dispatch_async(dispatch_get_main_queue(),block)



#pragma mark - 系统单例获取
#define USER_DEFAULT        [NSUserDefaults standardUserDefaults]
#define NOTIFICATIONCENTER  [NSNotificationCenter defaultCenter]



#pragma mark - 数值常量
#define HUD_MISS_TIME           1.5f
#define kThumbnailLength    (SCREEN_WIDTH - 30) / 3
#define kThumbnailSize      CGSizeMake(kThumbnailLength, kThumbnailLength)
#define SYSTEM_VERSION_FLOAT   [[[UIDevice currentDevice] systemVersion] floatValue]
//#define PHOTOS_DB_NAME        @"photos.db"



#pragma mark - 判断
#define DEVICE_IS_IPAD          (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
#define OBJECT_IS_NULL(object)         ((NSNull *)object == [NSNull null])
#define OBJECT_IS_NIL(object)          ((NSNull *)object == [NSNull null] || object == nil)
#define STRING_IS_EMPTY(object) ((NSNull *)object == [NSNull null] || object == nil || object.length == 0)
#define ARRAY_IS_EMPTY(object) ((NSNull *)object == [NSNull null] || object == nil || [object count] == 0)

//适配判断刘海屏幕
#define kIsBangsScreen ({\
    BOOL isBangsScreen = NO; \
    if (@available(iOS 11.0, *)) { \
    UIWindow *window = [[UIApplication sharedApplication].windows firstObject]; \
    isBangsScreen = window.safeAreaInsets.bottom > 0; \
    } \
    isBangsScreen; \
})

#pragma mark - 缓存
//暂无ß
#define TEST_ADS         false
//这个地方是mopub广告sdk接入用到的key
#define AdInterstitial @"e1f24fbd99dc3c64000000" //进入阅读前强制广告 10012
#define AdInterstitial2 @"64e14a19bd81f38a000000" //进入阅读前强制广告 10012
#define AdRewardId @"99d39d1abc271b71000000"  //IOS视觉小说免费玩家完成章节阅读后Bonus弹框  10003
#define TapjoyKey @"qiDdlMGrQrenpFGA8pkBaAEBCPwxOA7U7rxPHTwhlO6hYKd2-dA3_g0ALIQb000000"
#define HyprmxUserIdKey @"hyprUserID000000"
//test id :6517200
//product id :xxxx
#pragma mark - 属性引用
#define WS(obj) __weak typeof(&*self) obj = self

#ifdef DEBUG
#define DLog(...) NSLog(@"%@\n",[NSString stringWithFormat:__VA_ARGS__])
#else
#define DLog(...)


#endif


