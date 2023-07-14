//
//  Statistic.m
//  avgchoices
//
//  Created by MarsMini on 2017/8/31.
//
//

#import <Foundation/Foundation.h>
#import <FBSDKCoreKit/FBSDKCoreKit.h>
#import "Firebase.h"

#import "Statistic.h"
//#include "CCLuaEngine.h"
//USING_NS_CC;


#define USE_FACEBOOK  1
#define USE_FIREBASE  1
#define USE_ADJUST    0

//控制付费上报 (0关闭上报  1开启上报)   
static int revenue_report_on_facebook  = 0;
static int revenue_report_on_firebase  = 1;
static int revenue_report_on_adjust  = 1;


static NSString* ADJUST_EVENT_TOEKN_BANNER_CLICK= @"xgpwq8";
static NSString* ADJUST_EVENT_TOEKN_BOOKCOVER_CLICK= @"73osuw";
static NSString* ADJUST_EVENT_TOEKN_CHAP_COMPLETE =  @"axfbku";
static NSString* ADJUST_EVENT_TOEKN_CHAP_START= @"7gtws4";
static NSString* ADJUST_EVENT_TOEKN_CHOICE= @"w2cqtx";
static NSString* ADJUST_EVENT_TOEKN_GAME_CURRENY_CONSUME = @"cynv1g";
static NSString* ADJUST_EVENT_TOEKN_PURCHESE_COMPLETE = @"qv293n";
static NSString* ADJUST_EVENT_TOEKN_REMINDME= @"fsloxy";

static NSString* countryCode = nil;

@implementation Statistic
static Statistic* instance = nil;



+(id)getInstance
{
    if(instance == nil){
        instance = [[Statistic alloc]init];
        [Statistic openApp];
    }
    return instance;
}

-(id)init
{
    if(self = [super init]){
        
    }
    return self;
}

+ (NSString *)GetCountryCode
{
    if (countryCode == nil || [countryCode isEqualToString:@""]) {
        NSLocale *currentLocale = [NSLocale currentLocale];
        countryCode = [currentLocale objectForKey:NSLocaleCountryCode];
    }
    if (countryCode == nil) {
        countryCode = @"";
    }
    return countryCode;
}


//付费开始统计
+ (void)payStart:(NSString *)paycode price:(NSString *)price transitionid:(NSString *)transitionid{
    BOOL cansent=false;
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[@"paycode"] = paycode;
    eventInfoDictionary[@"price"] = price;
    eventInfoDictionary[@"transitionid"] = transitionid;
    
    #if USE_FACEBOOK
    //    [FBSDKAppEvents logEvent:@"payment_start" parameters:eventInfoDictionary];
        //init checkout统计   开始支付
        NSDictionary *params =
        @{
          FBSDKAppEventParameterNameContentType : @"product",
          FBSDKAppEventParameterNameContentID: paycode,
          FBSDKAppEventParameterNameNumItems : [NSNumber numberWithInt:1],
          FBSDKAppEventParameterNameCurrency : @"USD",
          @"price":price,
          @"paycode":paycode
          };
        [FBSDKAppEvents
         logEvent:FBSDKAppEventNameInitiatedCheckout
         parameters:params];
         cansent=true;
    #endif
    #if USE_FIREBASE
    //    [FIRAnalytics logEventWithName:@"payment_start" parameters:eventInfoDictionary];
    #endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"paycode" withStringValue:paycode];
        [Statistic addDic:argdic withStringKey:@"price" withStringValue:price];
        [Statistic addDic:argdic withStringKey:@"transitionid" withStringValue:transitionid];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"payStart" withArgjson:argdicjson withPriceDollar:price withPayCode:paycode withOrderId:transitionid];
    }
}

//付费结束统计
+ (void)payFinish:(NSString *)paycode price:(NSString *)price transitionid:(NSString *)transitionid{
    BOOL cansent=false;
    //NSString* receipt = [dict objectForKey:@"receipt"];   //receipt 
    
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[@"paycode"] = paycode;
    eventInfoDictionary[@"price"] = price;
    eventInfoDictionary[@"transitionid"] = transitionid;
#if USE_ADJUST
    if (revenue_report_on_adjust == 1){
        double adprice = [price doubleValue];
        ADJEvent *adEvent = [ADJEvent eventWithEventToken:ADJUST_EVENT_TOEKN_PURCHESE_COMPLETE];
        [adEvent setRevenue:adprice currency:@"USD"];
    
        
        [adEvent addPartnerParameter:@"price" value:price];
        [adEvent addPartnerParameter:@"paycode" value:paycode];
        [adEvent addPartnerParameter:@"transitionid" value:transitionid];
        [adEvent addPartnerParameter:@"currency" value:@"USD"];
        
        [adEvent addCallbackParameter:@"price" value:price];
        [adEvent addCallbackParameter:@"paycode" value:paycode];
        [adEvent addCallbackParameter:@"transitionid" value:transitionid];
        [adEvent addCallbackParameter:@"currency" value:@"USD"];

        [Adjust trackEvent:adEvent];
        cansent=true;
    }
 
#endif
    
//#if USE_KOCHAVA
//    [KochavaTracker.shared sendEventWithNameString:@"payment_finish" infoDictionary:eventInfoDictionary];
//
//    //原生统计
//    KochavaEvent *event = [KochavaEvent eventWithEventTypeEnum:KochavaEventTypeEnumPurchase];
//    if (event != nil)
//    {
//        NSNumber * dprice = @([price doubleValue]);
//        event.priceDoubleNumber = dprice;
//        event.currencyString = @"USD";
//        event.descriptionString =transitionid;
//        if (receipt!=nullptr && ![@"unkown" isEqualToString:receipt]) {
//                  event.appStoreReceiptBase64EncodedString = receipt;
//        }
//
//    }
//    [KochavaTracker.shared sendEvent: event];
//#endif
    
#if USE_FACEBOOK
        if (revenue_report_on_facebook== 1){
        //    [FBSDKAppEvents logEvent:@"purchase_finish" parameters:eventInfoDictionary];
            //official purchase loging
            NSDictionary *params =[[NSDictionary alloc] initWithObjectsAndKeys:
             [NSNumber numberWithInt:1],FBSDKAppEventParameterNameNumItems,
            @"product",FBSDKAppEventParameterNameContentType,
            paycode,FBSDKAppEventParameterNameContentID,
            @"USD",FBSDKAppEventParameterNameCurrency,
            nil];
            [FBSDKAppEvents logPurchase:price.doubleValue
                               currency: @"USD"
                             parameters: params];
            cansent=true;
    
        }
#endif
#if USE_FIREBASE
       if (revenue_report_on_firebase== 1){
            [FIRAnalytics logEventWithName:@"payment_finish" parameters:eventInfoDictionary];
           cansent=true;
       }
#endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"paycode" withStringValue:paycode];
        [Statistic addDic:argdic withStringKey:@"price" withStringValue:price];
        [Statistic addDic:argdic withStringKey:@"transitionid" withStringValue:transitionid];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"payFinish" withArgjson:argdicjson withPriceDollar:price withPayCode:paycode withOrderId:transitionid];
    }
}

//虚拟币消耗统计
+ (void)gameCurrenyConsume:(NSString *)num currencytype:(NSString *)currencytype reason:(NSString *)reason storyid:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid{
    BOOL cansent=false;
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[@"consume_num"] = num;
    eventInfoDictionary[@"consume_type"] = currencytype;
    eventInfoDictionary[@"consume_reason"] = reason;
    eventInfoDictionary[@"storyid"] = storyid;
    eventInfoDictionary[@"chapid"] = chapid;
    eventInfoDictionary[@"chatid"] = chatid;
    
    
#if USE_ADJUST
    ADJEvent *adEvent = [ADJEvent  eventWithEventToken:ADJUST_EVENT_TOEKN_GAME_CURRENY_CONSUME];
    [adEvent addCallbackParameter:@"consume_num" value:num];
    [adEvent addCallbackParameter:@"consume_type" value:currencytype];
    [adEvent addCallbackParameter:@"consume_reason" value:reason];
    [adEvent addCallbackParameter:@"storyid" value:storyid];
    [adEvent addCallbackParameter:@"chapid" value:chapid];
    [adEvent addCallbackParameter:@"chatid" value:chatid];
    
    [adEvent addPartnerParameter:@"consume_num" value:num];
    [adEvent addPartnerParameter:@"consume_type" value:currencytype];
    [adEvent addPartnerParameter:@"consume_reason" value:reason];
    [adEvent addPartnerParameter:@"storyid" value:storyid];
    [adEvent addPartnerParameter:@"chapid" value:chapid];
    [adEvent addPartnerParameter:@"chatid" value:chatid];

    [Adjust trackEvent:adEvent];
    cansent = true;
#endif
    

#if USE_FACEBOOK
//    [FBSDKAppEvents logEvent:@"game_currency_consume" parameters:eventInfoDictionary];
#endif
#if USE_FIREBASE
    [FIRAnalytics logEventWithName:@"game_currency_consume" parameters:eventInfoDictionary];
    cansent = true;
#endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"num" withStringValue:num];
        [Statistic addDic:argdic withStringKey:@"currencytype" withStringValue:currencytype];
        [Statistic addDic:argdic withStringKey:@"reason" withStringValue:reason];
        [Statistic addDic:argdic withStringKey:@"storyid" withStringValue:storyid];
        [Statistic addDic:argdic withStringKey:@"chapid" withStringValue:chapid];
        [Statistic addDic:argdic withStringKey:@"chatid" withStringValue:chatid];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"gameCurrenyConsume" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}

//章节开始结束
+ (void)chapBegin:(NSString *)chapid{
    BOOL cansent=false;
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[@"chapid"] = chapid;
#if USE_ADJUST
    ADJEvent *adEvent = [ADJEvent  eventWithEventToken:ADJUST_EVENT_TOEKN_CHAP_START];
    [adEvent addCallbackParameter:@"chapid" value:chapid];

    [adEvent addPartnerParameter:@"chapid" value:chapid];

    [Adjust trackEvent:adEvent];
    cansent = true;
#endif
    

#if USE_FACEBOOK
//    [FBSDKAppEvents logEvent:@"chap_start" parameters:eventInfoDictionary];
#endif
#if USE_FIREBASE
    [FIRAnalytics logEventWithName:@"chap_start" parameters:eventInfoDictionary];
    cansent = true;
#endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"chapid" withStringValue:chapid];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"chapBegin" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}

+ (void)chapComplete:(NSString *)chapid{
    BOOL cansent=false;
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[@"chapid"] = chapid;
    
#if USE_ADJUST
    cansent = true;
#endif

#if USE_FACEBOOK
//    [FBSDKAppEvents logEvent:@"chap_complete" parameters:eventInfoDictionary];
#endif
#if USE_FIREBASE
    [FIRAnalytics logEventWithName:@"chap_complete" parameters:eventInfoDictionary];
    cansent = true;
#endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"chapid" withStringValue:chapid];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"chapComplete" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}


//每句对话统计
+ (void)chatStat:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid{
    BOOL cansent=false;
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[@"storyid"] = storyid;
    eventInfoDictionary[@"chapid"] = chapid;
    eventInfoDictionary[@"chatid"] = chatid;
    


#if USE_FACEBOOK
//    [FBSDKAppEvents logEvent:@"chat_stat" parameters:eventInfoDictionary];
#endif
#if USE_FIREBASE
    [FIRAnalytics logEventWithName:@"chat_stat" parameters:eventInfoDictionary];
    cansent = true;
#endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"storyid" withStringValue:storyid];
        [Statistic addDic:argdic withStringKey:@"chapid" withStringValue:chapid];
        [Statistic addDic:argdic withStringKey:@"chatid" withStringValue:chatid];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"chatStat" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}


//选项统计
+ (void)choiceStat:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid choiceid:(NSString *)choiceid{
    BOOL cansent=false;
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[@"storyid"] = storyid;
    eventInfoDictionary[@"chapid"] = chapid;
    eventInfoDictionary[@"chatid"] = chatid;
    eventInfoDictionary[@"choiceid"] = choiceid;
#if USE_ADJUST
    cansent = true;
#endif

#if USE_FACEBOOK
//    [FBSDKAppEvents logEvent:@"choice_stat" parameters:eventInfoDictionary];
#endif
#if USE_FIREBASE
    [FIRAnalytics logEventWithName:@"choice_stat" parameters:eventInfoDictionary];
    cansent = true;
#endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"storyid" withStringValue:storyid];
        [Statistic addDic:argdic withStringKey:@"chapid" withStringValue:chapid];
        [Statistic addDic:argdic withStringKey:@"chatid" withStringValue:chatid];
        [Statistic addDic:argdic withStringKey:@"choiceid" withStringValue:choiceid];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"choiceStat" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}

//书封点击统计
+ (void)bookCoverClick:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid choiceid:(NSString *)choiceid{
    BOOL cansent=false;
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[@"storyid"] = storyid;
    eventInfoDictionary[@"chapid"] = chapid;
    eventInfoDictionary[@"chatid"] = chatid;
    eventInfoDictionary[@"click_storyid"] = choiceid;
    
#if USE_ADJUST
    cansent = true;
#endif
    

#if USE_FACEBOOK
//    [FBSDKAppEvents logEvent:@"bookcover_click_stat" parameters:eventInfoDictionary];
#endif
#if USE_FIREBASE
    [FIRAnalytics logEventWithName:@"bookcover_click_stat" parameters:eventInfoDictionary];
    cansent = true;
#endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"storyid" withStringValue:storyid];
        [Statistic addDic:argdic withStringKey:@"chapid" withStringValue:chapid];
        [Statistic addDic:argdic withStringKey:@"chatid" withStringValue:chatid];
        [Statistic addDic:argdic withStringKey:@"choiceid" withStringValue:choiceid];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"bookCoverClick" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}

//banner点击统计
+ (void)bannerClick:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid choiceid:(NSString *)choiceid{

    BOOL cansent=false;
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[@"storyid"] = storyid;
    eventInfoDictionary[@"chapid"] = chapid;
    eventInfoDictionary[@"chatid"] = chatid;
    eventInfoDictionary[@"click_storyid"] = choiceid;
    
#if USE_ADJUST
    cansent = true;
#endif
    

#if USE_FACEBOOK
//    [FBSDKAppEvents logEvent:@"banner_click_stat" parameters:eventInfoDictionary];
#endif
#if USE_FIREBASE
    [FIRAnalytics logEventWithName:@"banner_click_stat" parameters:eventInfoDictionary];
    cansent = true;
#endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"storyid" withStringValue:storyid];
        [Statistic addDic:argdic withStringKey:@"chapid" withStringValue:chapid];
        [Statistic addDic:argdic withStringKey:@"chatid" withStringValue:chatid];
        [Statistic addDic:argdic withStringKey:@"choiceid" withStringValue:choiceid];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"bannerClick" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}


//reminder me点击统计
+ (void)remindStat:(NSString *)storyid chapid:(NSString *)chapid chatid:(NSString *)chatid choiceid:(NSString *)choiceid{
    BOOL cansent=false;
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[@"storyid"] = storyid;
    eventInfoDictionary[@"chapid"] = chapid;
    eventInfoDictionary[@"chatid"] = chatid;
    eventInfoDictionary[@"click_storyid"] = choiceid;
    
#if USE_ADJUST
    ADJEvent *adEvent = [ADJEvent  eventWithEventToken:ADJUST_EVENT_TOEKN_REMINDME];
    [adEvent addCallbackParameter:@"storyid" value:storyid];
    [adEvent addCallbackParameter:@"chapid" value:chapid];
    [adEvent addCallbackParameter:@"chatid" value:chatid];
    [adEvent addCallbackParameter:@"click_storyid" value:choiceid];
                         
    [adEvent addPartnerParameter:@"storyid" value:storyid];
    [adEvent addPartnerParameter:@"chapid" value:chapid];
    [adEvent addPartnerParameter:@"chatid" value:chatid];
    [adEvent addPartnerParameter:@"click_storyid" value:choiceid];
                         
    [Adjust trackEvent:adEvent];
    cansent = true;
#endif
    

#if USE_FACEBOOK
//    [FBSDKAppEvents logEvent:@"remindme_stat" parameters:eventInfoDictionary];
#endif
#if USE_FIREBASE
    [FIRAnalytics logEventWithName:@"remindme_stat" parameters:eventInfoDictionary];
    cansent = true;
#endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"storyid" withStringValue:storyid];
        [Statistic addDic:argdic withStringKey:@"chapid" withStringValue:chapid];
        [Statistic addDic:argdic withStringKey:@"chatid" withStringValue:chatid];
        [Statistic addDic:argdic withStringKey:@"choiceid" withStringValue:choiceid];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"remindStat" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}

//预留登陆接口
+(void)onCompletedRegistration:(NSDictionary *)dict{
    NSString* registrationMethod =  [dict objectForKey:@"method"];
    NSString* uuid =  [dict objectForKey:@"uuid"];
    NSLog(@"uuid :%@",uuid);
    NSLog(@"registrationMethod :%@",registrationMethod);
//#if USE_KOCHAVA
//    // eventInfoDictionary
//    KochavaEvent *event = [KochavaEvent eventWithEventTypeEnum:KochavaEventTypeEnumRegistrationComplete];
//    if (event != nil)
//    {
//        event.userIdString = uuid;
//    }
//    [KochavaTracker.shared sendEvent: event];
//#endif
#if USE_FACEBOOK
//    NSDictionary *params = [[NSDictionary alloc] initWithObjectsAndKeys:
//     registrationMethod, FBSDKAppEventParameterNameRegistrationMethod,
//     nil];
//    [FBSDKAppEvents logEvent: FBSDKAppEventNameCompletedRegistration
//                  parameters: params];
#endif
}

//特殊事件
+ (void)statSpecialEvent:(NSString *)eventName typeName:(NSString *)typeName typeValue:(NSString *)typeValue{
    BOOL cansent=false;
    // eventInfoDictionary
    NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
    eventInfoDictionary[typeName] = typeValue;
    
#if USE_FIREBASE
    [FIRAnalytics logEventWithName:eventName parameters:eventInfoDictionary];
    cansent = true;
#endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"eventName" withStringValue:eventName];
        [Statistic addDic:argdic withStringKey:@"typeName" withStringValue:typeName];
        [Statistic addDic:argdic withStringKey:@"typeValue" withStringValue:typeValue];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"statSpecialEvent" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}

//adjust 通用事件
+ (void)adjsutEventStat:(NSString *)eventName {
    BOOL cansent=false;
    #if USE_ADJUST
            ADJEvent *adEvent = [ADJEvent  eventWithEventToken:eventName];
            [Adjust trackEvent:adEvent];
            cansent = true;
    #endif
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"eventName" withStringValue:eventName];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"adjsutEventStat" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}


//FB 通用事件
+ (void)fbEventStat:(NSString *)eventName consumeNum:(NSString *)consumeNum{
    BOOL cansent=false;
    #if USE_FACEBOOK
    
            NSMutableDictionary *eventInfoDictionary = [NSMutableDictionary dictionary];
            if (consumeNum != nil){
                        eventInfoDictionary[@"consume_num"] = consumeNum;
            }
            [FBSDKAppEvents logEvent:eventName parameters:eventInfoDictionary];
            cansent = true;
    #endif
    
    if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"eventName" withStringValue:eventName];
        [Statistic addDic:argdic withStringKey:@"consumeNum" withStringValue:consumeNum];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"fbEventStat" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:@""];
    }
}





    //定制事件统计
+ (void)customEventStat:(NSDictionary *)dict{
    NSString* eventName = [dict objectForKey:@"eventName"];
    NSMutableDictionary *dic = [NSMutableDictionary dictionary];
    dic[@"event_name"] = eventName;
}
    
//打开应用
+(void)openApp{
        NSMutableDictionary *dic = [NSMutableDictionary dictionary];
        dic[@"event_name"] = @"open_app";
    }


//设置各平台的revennue是否开启
+(void)setFacebookRevenueReportOn:(int )state{
    revenue_report_on_facebook = state;
}
+(void)setFirebaseRevenueReportOn:(int )state{
    revenue_report_on_firebase = state;
}
+(void)setAdjustRevenueReportOn:(int )state{
    revenue_report_on_adjust = state;
}


//track sdk send data
+(void)trackSendData:(NSString*)methodName withArgjson:(NSString*)argjson withPriceDollar:(NSString*)priceDollar withPayCode:(NSString*)payCode withOrderId:(NSString*)orderId{
    NSMutableDictionary *dic = [NSMutableDictionary dictionary];
    [Statistic addDic:dic withStringKey:@"methodName" withStringValue:methodName];
    [Statistic addDic:dic withStringKey:@"argjson" withStringValue:argjson];
    [Statistic addDic:dic withStringKey:@"priceDollar" withStringValue:priceDollar];
    [Statistic addDic:dic withStringKey:@"payCode" withStringValue:payCode];
    [Statistic addDic:dic withStringKey:@"orderId" withStringValue:orderId];
    NSString* json = [Statistic getjsonstr:dic];
    if ([json length]>0) {
        //NSLog(@"track data %@",json);
//        UnitySendMessage("CommonManager", "OnSendDataInvoke", [json UTF8String]);
    }
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

/*
//不要调用这个方法，否则会造成脏数据
+(void)testTrackSent{
    [Statistic payStart:@"_paycode" price:@"_price" transitionid:@"_tranid"];
    
    double priceDouble = 499/100.0;
    [Statistic payFinish:@"_paycode" price:[NSString stringWithFormat:@"%.2f",priceDouble] transitionid:@"_tranid"];
    
    int num=100;
    [Statistic gameCurrenyConsume:[NSString stringWithFormat:@"%d",num] currencytype:@"_currencytype" reason:@"_reason" storyid:@"_storyid" chapid:@"_chapid" chatid:@"_chatid"];
    
    [Statistic chapBegin:@"_chapid"];
    [Statistic chapComplete:@"_chapid"];
    
    //6
    [Statistic chatStat:@"_storyid" chapid:@"_chapid" chatid:@"_chatid"];
    [Statistic choiceStat:@"_storyid" chapid:@"_chapid" chatid:@"_chatid" choiceid:@"_choiceid"];
    [Statistic bookCoverClick:@"_storyid" chapid:@"_chapid" chatid:@"_chatid" choiceid:@"_choiceid"];
    [Statistic bannerClick:@"_storyid" chapid:@"_chapid" chatid:@"_chatid" choiceid:@"_selectid"];
    [Statistic remindStat:@"_storyid" chapid:@"_chapid" chatid:@"_chatid" choiceid:@"_selectid"];
    
    //11
    [Statistic statSpecialEvent:@"_eventname" typeName:@"_typename" typeValue:@"_tppevaue"];
    [Statistic adjsutEventStat:@"_eventname"];
    [Statistic fbEventStat:@"_eventname" consumeNum:@"_consumenunstr"];
}
*/

//用于移动应用的动态广告event
//FBSDKAppEventNameViewedContent:@"fb_mobile_content_view";
//FBSDKAppEventNameAddedToCart:@"fb_mobile_add_to_cart";
+ (void)fbMobileAdEvent:(NSString *)eventStr contentType:(NSString *)contentTypeStr contentId:(NSString *)contentIdStr{
    if ([eventStr length]>0) {
        BOOL cansent=false;
        if ([contentTypeStr length]>0 && [contentIdStr length]>0 )  {
            [FBSDKAppEvents logEvent:eventStr
              parameters:@{
                FBSDKAppEventParameterNameContentType : contentTypeStr,
                FBSDKAppEventParameterNameContentID   : contentIdStr
              }
            ];
            cansent = true;
            NSLog(@"fb event track type and id.");
        }else if ([contentIdStr length]>0){
            [FBSDKAppEvents logEvent:eventStr
              parameters:@{
                FBSDKAppEventParameterNameContentID   : contentIdStr
              }
            ];
            cansent = true;            
            NSLog(@"fb event track id.");
        }else if ([contentTypeStr length]>0){
            [FBSDKAppEvents logEvent:eventStr
              parameters:@{
                FBSDKAppEventParameterNameContentType : contentTypeStr,
              }
            ];
            cansent = true;            
            NSLog(@"fb event track type.");
        }
        if (cansent) {
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"eventName" withStringValue:eventStr];
        [Statistic addDic:argdic withStringKey:@"contentType" withStringValue:contentTypeStr];
        [Statistic addDic:argdic withStringKey:@"contentId" withStringValue:contentIdStr];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"fbMobileAdEvent" withArgjson:argdicjson withPriceDollar:@"0" withPayCode:@"" withOrderId:eventStr];
        }
    }
}

//首次内购事件 by wjp 2022/02/07
//目的:统计拉新的玩家
+ (void)fbPurchaseEvent:(NSString *)eventStr sku:(NSString *)skuStr price:(NSString *)priceStr{
    if ([eventStr length]>0 && [skuStr length]>0 && [priceStr length]>0) {
        [FBSDKAppEvents logEvent:eventStr
          parameters:@{
            @"sku" : skuStr,
            @"price"   : priceStr,
            @"currency" : @"USD"
          }
        ];
        NSLog(@"fbPurchaseEvent track sku and price.");
        NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
        [Statistic addDic:argdic withStringKey:@"eventName" withStringValue:eventStr];
        [Statistic addDic:argdic withStringKey:@"sku" withStringValue:skuStr];
        [Statistic addDic:argdic withStringKey:@"price" withStringValue:priceStr];
        [Statistic addDic:argdic withStringKey:@"currency" withStringValue:@"USD"];
        NSString *argdicjson =[Statistic getjsonstr:argdic];
        [Statistic trackSendData:@"fbPurchaseEvent" withArgjson:argdicjson withPriceDollar:priceStr withPayCode:skuStr withOrderId:eventStr];
    }
}

@end
