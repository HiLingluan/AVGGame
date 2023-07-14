//
//  AppsflyerUtil.mm
//
//  Created by jin on 2021/8/19.
//
//

#import <AppsFlyerLib/AppsFlyerLib.h>


#import "AppsFlyerStat.h"
#import "Statistic.h"

#define IS_ON  1


//static NSString* ADJUST_EVENT_TOEKN_PURCHESE_COMPLETE = @"6hdza0";


@implementation AppsFlyerStat
static AppsFlyerStat* instance = nil;



+(id)getInstance
{
    if(instance == nil){
        instance = [[AppsFlyerStat alloc]init];
    }
    return instance;
}

// -(id)init
// {
//     if(self = [super init]){
        
//     }
//     return self;
// }





//付费结束统计
- (void)payFinish:(NSString *)paycode price:(NSString *)price goodid:(NSString *) goodidArg{
    
    double adprice = [price doubleValue];
    
    NSMutableDictionary *dic = [NSMutableDictionary dictionary];
    dic[@"sku"] = paycode;
    dic[AFEventParamRevenue] = @(adprice);
    dic[AFEventParamCurrency] = @"USD";
    dic[AFEventParamContentId] = goodidArg;
    
    [[AppsFlyerLib shared] logEvent: AFEventPurchase withValues:dic];

    NSMutableDictionary *argdic = [NSMutableDictionary dictionary];
    [Statistic addDic:argdic withStringKey:@"paycode" withStringValue:paycode];
    [Statistic addDic:argdic withStringKey:@"price" withStringValue:price];
    NSString *argdicjson =[Statistic getjsonstr:argdic];
    [Statistic trackSendData:@"payFinish" withArgjson:argdicjson withPriceDollar:price withPayCode:paycode withOrderId:@""];
}



//adjust 通用事件
- (void)logEvent:(NSString *)eventName {
    [[AppsFlyerLib shared] logEvent:eventName withValues:nil];
}

- (void)logEventWithJson:(NSString *)eventName json:(NSString*)jsonStr {
    
    if(jsonStr == NULL)
        return;
    NSData *jsonData = [[NSString stringWithFormat:@"%@", jsonStr] dataUsingEncoding:NSUTF8StringEncoding];
    NSError *err;
    NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONReadingMutableContainers error:&err];
    if(err)
    {
        NSLog(@"logEventWithJson decode json fail：%@",err);
        return;
    }
    [[AppsFlyerLib shared] logEvent:eventName withValues:dic];
}


    
//设置各平台的revennue是否开启
-(void)setRevenueReportOn:(int )state{
    //revenue_report_on_facebook = state;
}

//付费开始统计
- (void)payBegin:(NSString *)paycode price:(NSString *)price{
    
    double adprice = [price doubleValue];
    
    NSMutableDictionary *dic = [NSMutableDictionary dictionary];
    //sku
    dic[@"sku"] = paycode;
    //AFInAppEventParameterName.PRICE 小数点后两位
    dic[AFEventParamPrice] = @(adprice);
    //AFInAppEventParameterName.CURRENCY 
    dic[AFEventParamCurrency] = @"USD";
    
    //AFInAppEventType.INITIATED_CHECKOUT
    [[AppsFlyerLib shared] logEvent: AFEventInitiatedCheckout withValues:dic];

}

//付费统计
//price 由c# 整形传入，然后c++ 转为.xx 2位数小数的字符串 再传入在这个方法
- (void)payEventReport:(NSString *)eventNameArg paycode:(NSString *)paycodeArg price:(NSString *)price {
    
    double adprice = [price doubleValue];
    
    NSMutableDictionary *dic = [NSMutableDictionary dictionary];
    //sku
    dic[@"sku"] = paycodeArg;
    //AFInAppEventParameterName.PRICE float 小数点后两位
    dic[AFEventParamPrice] = @(adprice);
    //AFInAppEventParameterName.CURRENCY
    dic[AFEventParamCurrency] = @"USD";
    //af_custom_newuser_purchase_day0
    [[AppsFlyerLib shared] logEvent: eventNameArg withValues:dic];

}

@end
