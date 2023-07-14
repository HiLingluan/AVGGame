//
//  UitilsCommon.h
//  Unity-iPhone
//
//  Created by 玩贝 on 2018/11/22.
//

#import <Foundation/Foundation.h>

@interface UtilsCommon : NSObject




#pragma mark - 设备识别码
+ (NSString *)idfvString;
+ (NSString*)getIDFA;


#pragma mark - 登录
+(void)doFacebookLogin;
+(void)doGoogleLogin;
//+(void)doVKLogin;
#pragma mark - 退出登录
+(void)doFacebookLogOut;
+(void)doGoogleLogOut;
//+(void)doVKLogout;
#pragma mark - 分享
+(void)sharType:(int)type url:(NSString *)url;
+(void)sharType2:(NSString *)filepath:(NSString *)title:(NSString *)shareUrl;

#pragma mark - 手机型号
+ (NSString *)iphoneType;


#pragma mark -拷贝文件,读取
//+ (void )copyBookZip;
//+ (NSString *)readBookZip:(NSString *)filename;
+(BOOL)isPhoneX;
+(BOOL)isNotchScreen;

#pragma mark - josn read ,write
+ (void )addDic:(NSMutableDictionary*)dic withStringKey:(NSString*)strkey withStringValue:(NSString*)strvalue;
+ (NSString*)getjsonstr:(NSMutableDictionary*)dic;
+(NSString*)getTestAdunit:(BOOL)isRewward;

#pragma mark - 内存
+ (int64_t)getFreeMemory;
+ (int64_t)getUsedMemory;






@end
