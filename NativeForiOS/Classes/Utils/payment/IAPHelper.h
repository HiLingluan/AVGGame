//
//  IAPHelper.h
//  appstore
//
//

#ifndef __IAPHelper__
#define __IAPHelper__


#import <StoreKit/StoreKit.h>
#include <string>
#include <vector>

using namespace std;

class IAPHelper{
public:
    static void pay(const std::string& itemid);
};

@interface PayHelper : NSObject<SKPaymentTransactionObserver,SKProductsRequestDelegate>
@property (nonatomic,assign) BOOL isBackground; //判断是否进入过后台
-(void)addIAPTransactionObserver;
-(void)removeIAPTransactionObserver;
-(void)initSDK;
+(id)getInstance;
+(void)onBuy:(NSDictionary *)dict;
+(void)onBuyV2:(NSDictionary *)dict;

//支付订单缓存1.0
+(NSString*)getRamainsPaycode;
+(void)clearRamainsPaycode;

//2.0
+(NSString*)getReorderData;
+(void)clearReorderData;
+(void)restore;
- (void)SetBackgroundSatus:(BOOL)isBack;

//拉起恢复的购买记录
+(void)doRestore;
@end

#endif /* defined(__IAPHelper__) */
