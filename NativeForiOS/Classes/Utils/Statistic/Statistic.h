//
//  Statistic.h
//  avgchoices
//
//  Created by MarsMini on 2017/8/31.
//
//

#ifndef Statistic_h
#define Statistic_h

#import <Foundation/Foundation.h>



@interface Statistic : NSObject
+(id)getInstance;
-(id)init;

+ (void)payStart:(NSString *)paycode price:(NSString *)price transitionid:(NSString *)transitionid;
+ (void)payFinish:(NSString *)paycode price:(NSString *)price transitionid:(NSString *)transitionid;
+ (void)gameCurrenyConsume:(NSString *)num currencytype:(NSString *)currencytype reason:(NSString *)reason storyid:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid;
+ (void)chapBegin:(NSString *)chapid;
+ (void)chapComplete:(NSString *)chapid;
+ (void)chatStat:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid;
+ (void)choiceStat:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid choiceid:(NSString *)choiceid;
+ (void)bookCoverClick:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid choiceid:(NSString *)choiceid;
+ (void)bannerClick:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid choiceid:(NSString *)choiceid;
+ (void)remindStat:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid choiceid:(NSString *)choiceid;
+ (void)statSpecialEvent:(NSString *)eventName typeName:(NSString *)typeName typeValue:(NSString *)typeValue;
+ (void)adjsutEventStat:(NSString *)eventName;
+ (void)fbEventStat:(NSString *)eventName consumeNum:(NSString *)consumeNum;
+(void)setFacebookRevenueReportOn:(int )state;
+(void)setFirebaseRevenueReportOn:(int )state;
+(void)setAdjustRevenueReportOn:(int )state;
+ (NSString *)GetCountryCode;
+(void)addDic:(NSMutableDictionary*)dic withStringKey:(NSString*)strkey withStringValue:(NSString*)strvalue;
+(void)trackSendData:(NSString*)methodName withArgjson:(NSString*)argjson withPriceDollar:(NSString*)priceDollar withPayCode:(NSString*)payCode withOrderId:(NSString*)orderId;
+(NSString*)getjsonstr:(NSMutableDictionary*)dic;
+ (void)fbMobileAdEvent:(NSString *)eventStr contentType:(NSString *)contentTypeStr contentId:(NSString *)contentIdStr;
+ (void)fbPurchaseEvent:(NSString *)eventStr sku:(NSString *)skuStr price:(NSString *)priceStr;
@end


#endif /* Statistic_h */
