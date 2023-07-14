//
//  IAPHelper.cpp
//  appstore
//
//

#include "IAPHelper.h"
#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>
#import "UtilsCommon.h"

@implementation PayHelper

UIActivityIndicatorView* loadView = nil;
SKProductsRequest* mProductsRequest = nil;


static PayHelper* _payHelper = nullptr;

static BOOL isPayStart = false;     //进入游戏有是否开始支付标志  用于区别进入游戏时的自动补单


static NSString*  lua_code_version = @"0.0";  //当前底包脚本versioncode

//缓存的订单透传信息
static  NSString*  _extraJsonStr = @"";

static int pay_version = 2; //支付流程版本 1 = 1.0 2=2.0默认1版本

static NSMutableDictionary *reorderDic = [NSMutableDictionary dictionary];  //缓存的待补单信息列表（支付1.0版本使用
static  NSString*  remainsOrderJsonStr = @"";  //缓存的补单信息（支付1.0版本使用）

//用来记录恢复购买的数据，避免重复保存订单
static NSMutableArray* temparray = [NSMutableArray arrayWithCapacity:1];

//add subscribe pay sku
static  NSString*  Pay_Subscribe = @"";
static  SKProduct* SubscribePruduct=nil;
+(id)getInstance
{
    if(_payHelper == nullptr){
        _payHelper = [[PayHelper alloc] init ];
    }
    return _payHelper;
}

-(void)initSDK
{
    Pay_Subscribe = [[NSString alloc] initWithFormat:@"%@,%@", [[NSBundle mainBundle] bundleIdentifier], @".Chapterpass" ];
}

//加入loading图 屏蔽UI操作
void buyBegin(){
    if(loadView != nil){
        [loadView removeFromSuperview];
        loadView = nil;
    }
    
    //初始化:
    loadView = [[UIActivityIndicatorView alloc] initWithFrame:CGRectMake(0, 0, 100, 100)];
    //设置显示样式,见UIActivityIndicatorViewStyle的定义
    loadView.activityIndicatorViewStyle = UIActivityIndicatorViewStyleWhiteLarge;
    CGRect rect = [[UIScreen mainScreen] bounds];
    CGSize size = rect.size;
    
    //设置显示位置
    [loadView setCenter:CGPointMake(size.width/2,size.height/2)];
    loadView.backgroundColor = [UIColor blackColor];
    loadView.alpha = 0.5;
    loadView.layer.cornerRadius = 6;
    [[UIApplication sharedApplication].keyWindow addSubview:loadView];
    
    //开始显示Loading动画
    [loadView startAnimating];
    
    if (isPayStart) {
        UnitySendMessage("SDK", "OnBuyStart", [@"" UTF8String]);
    }
}

//移除loading图 恢复UI操作
void buyEnd(){
    dispatch_async(dispatch_get_main_queue(), ^{
        if(loadView != nil){
            [loadView removeFromSuperview];
            
            loadView = nil;
        }
    });
    if (isPayStart) {
        UnitySendMessage("SDK", "OnBuyEnd", [@"" UTF8String]);
    }
}

//加入购买事件监听
-(void)addIAPTransactionObserver
{
    NSLog(@"addIAPTransactionObserver");
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
}

//移除购买事件监听
-(void)removeIAPTransactionObserver
{
    [[SKPaymentQueue defaultQueue] removeTransactionObserver:self];
}


- (void)productsRequest:(SKProductsRequest *)request
     didReceiveResponse:(SKProductsResponse *)response
{
    NSArray *myProduct = response.products;
    if (myProduct.count == 0) {
        NSLog(@"无法获取产品信息，购买失败。");
        [self ShowMessage:@"No products info"];
        buyEnd();
        [self trackSendData:@"ProductRequest"  withPayCode:@"" withOrderId:@"" withReason:@"failed get products info"];
        return;
    }
//    SKPayment * payment = [SKPayment paymentWithProduct:myProduct[0]];
    SKMutablePayment* payment = [SKMutablePayment paymentWithProduct:myProduct[0]];
    payment.applicationUsername = _extraJsonStr;
    //shoud store subscription product info
    SKProduct* product= myProduct[0];
    //how to check is nil ?
    if (product!=nil && [product.productIdentifier isEqualToString:Pay_Subscribe]) {
        SubscribePruduct = product;
    }
    [[SKPaymentQueue defaultQueue] addPayment:payment];
}

- (void)requestDidFinish:(SKRequest *)request
{
    NSLog(@"request finished");
}

- (void)request:(SKRequest *)request didFailWithError:(NSError *)error
{
     NSLog(@"request error:%@",error);
    buyEnd();
    
    //track request with error.
    NSString *reason = @"request failed.";
    [self trackSendData:@"ProductRequestError"  withReason:reason withError:error];
}

//交易中.
- (void)transcationPurchasing:(SKPaymentTransaction *)transaction {
    NSURL *receiptURL = [[NSBundle mainBundle] appStoreReceiptURL];
    NSData *receipt = [NSData dataWithContentsOfURL:receiptURL];
    if (!receipt) {
        NSLog(@"没有收据, 处理异常");
        [self trackSendData:@"Purchasing"  withPayCode:@"" withOrderId:@"" withReason:@"no receipt"];
        return;
    }
    
    // 存储到本地先.
    // 发送到服务器, 等待验证结果.
    //    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

// 交易成功.
- (void)transcationPurchased:(SKPaymentTransaction *)transaction {
    NSLog(@"-----交易完成 --------");
    auto paycode =[transaction.payment.productIdentifier UTF8String];
    //获取订单中的透传信息
    NSString *extraStr = transaction.payment.applicationUsername;
    
    if (isPayStart)
    {
        //appStoreReceiptURL iOS7.0增加的，购买交易完成后，会将凭据存放在该地址
        NSURL *receiptURL = [[NSBundle mainBundle] appStoreReceiptURL];
        //从沙盒中获取到购买凭据
        NSData *receiptData = [NSData dataWithContentsOfURL:receiptURL];
        NSString *encodeStr = [receiptData base64EncodedStringWithOptions:NSDataBase64EncodingEndLineWithLineFeed];
        
        NSMutableDictionary *jsonRaw = [NSMutableDictionary dictionary];
        jsonRaw[@"sku"] = transaction.payment.productIdentifier;
        jsonRaw[@"orderidsku"] = transaction.transactionIdentifier;
        jsonRaw[@"purchaseToken"] = encodeStr;
        if (extraStr!=nil && [extraStr length] > 0) {
            jsonRaw[@"tranOrder"] = extraStr;
        }else
        {
            jsonRaw[@"tranOrder"] = @"";
        }
        buyEnd();
        if (pay_version==2) {
            //2.0版本
            NSString *ordeJsonStr  = @"";
            if ([NSJSONSerialization isValidJSONObject:jsonRaw]) {
                NSData *jsonData = [NSJSONSerialization dataWithJSONObject:jsonRaw options:0 error:NULL];
                ordeJsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            } else {
                NSLog(@"jsoncreate failed 183");
            }
            NSString *callbackJsonStr  = @"";
            NSMutableDictionary *callbackDic = [NSMutableDictionary dictionary];
            callbackDic[@"data"] = ordeJsonStr;
            callbackDic[@"error_code"] = 0;
            callbackDic[@"error_info"] = @"ios pay success";
            NSString * reasonArg = @"";
            if ([NSJSONSerialization isValidJSONObject:callbackDic]) {
                NSData *jsonData = [NSJSONSerialization dataWithJSONObject:callbackDic options:0 error:NULL];
                callbackJsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            } else {
                NSLog(@"jsoncreate failed 194");
                reasonArg =@"failed to create callbackjson.";
            }
            UnitySendMessage("SDK", "OnPaySuccessV2", [callbackJsonStr UTF8String]);
            [self adjsutTrackSubscribe:transaction withReceipt:receiptData];
            //track purchased with transaction.
            [self trackSendData:@"Purchased" withTransation:transaction withReason:reasonArg];
        }
        else
        {
            //此时应该回传到lua
            NSString *jsonStr  = @"";
            BOOL isYes = [NSJSONSerialization isValidJSONObject:jsonRaw];
            if (isYes) {
                NSData *jsonData = [NSJSONSerialization dataWithJSONObject:jsonRaw options:0 error:NULL];
                jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            } else {
                NSLog(@"jsoncreate failed");
            }
            
            //NSLog(@"pay info : %@",jsonStr);
            UnitySendMessage("SDK", "OnPaySuccess", [jsonStr UTF8String]);
        }
        
        
        //buyEnd();
        
    }else{
        [self updateRestoreDic:transaction withDic:reorderDic];
    }
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
}

// 交易失败.
- (void)transcationFailed:(SKPaymentTransaction *)transaction {
    NSLog(@"-----交易失败 --------");
    NSString* reason = @"";
    if(transaction.error.code != SKErrorPaymentCancelled )
    {
        if(loadView != nil){
            [self ShowMessage:@"Pay fail"];
        }
        reason=@"Faild";
    }else if(transaction.error.code == SKErrorPaymentCancelled){
        [self ShowMessage:@"Pay cancel"];
        reason=@"User Cancelled";
    }
    if (transaction!=nil && transaction.error!=nil && transaction.error.localizedDescription!=nil && [transaction.error.localizedDescription length]>0) {
        reason =transaction.error.localizedDescription;
    }
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
    buyEnd();
    UnitySendMessage("SDK", "OnPayFailedV2", [reason UTF8String]);
    //track failed with transaction.
    [self trackSendData:@"Failed"  withTransation:transaction withReason:reason];
}

// 已经购买过该商品.
- (void)transcationRestored:(SKPaymentTransaction *)transaction {
    
    [self updateRestoreDic:transaction withDic:reorderDic];
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
    
    //track restored with transaction.
    [self trackSendData:@"Restored"  withTransation:transaction withReason:@""];
}

// 交易延期.
- (void)transcationDeferred:(SKPaymentTransaction *)transaction {
    NSLog(@"-----交易延期 --------");
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
    
    //track defreed with transaction.
    [self trackSendData:@"Deferred"  withTransation:transaction withReason:@""];
}



- (void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions
{
    for (SKPaymentTransaction *transaction in transactions) {
        std::string identifier([transaction.payment.productIdentifier UTF8String]);
        auto orderid = transaction.transactionIdentifier;
        /*
        if(loadView != nil){
            [loadView removeFromSuperview];
            
            loadView = nil;
        }
         */
        switch (transaction.transactionState) {
            case SKPaymentTransactionStatePurchasing:
                
                [self transcationPurchasing:transaction];
                break;
            case SKPaymentTransactionStatePurchased:
                [self transcationPurchased:transaction];
                break;
            case SKPaymentTransactionStateFailed:
                [self transcationFailed:transaction];
                break;
            case SKPaymentTransactionStateRestored:
                [self transcationRestored:transaction];
                break;
            case SKPaymentTransactionStateDeferred:
                [self transcationDeferred:transaction];
                break;
        }
        
    }
    
    
    
}

-(void)paymentQueue:(SKPaymentQueue *)queue restoreCompletedTransactionsFailedWithError:(NSError *)error
{
    if (error.code == 0 ){
        [self ShowMessage:@"Resume pay"];
    }
    buyEnd();
    //send unity restore failed . by wjp 2019/12/22
    UnitySendMessage("SDK", "OnRestoreCallback2", [@"" UTF8String]);
    
    //track restore faild .
    NSString* reason = @"";
    [self trackSendData:@"RestoreFailed"  withReason:reason withError:error];
}

- (void)paymentQueueRestoreCompletedTransactionsFinished:(SKPaymentQueue *)queue{
    if (0 >= [queue.transactions count]) {
       [self ShowMessage:@"No transaction"];
    }
    
    //[self printOrderInfo2];
    
    buyEnd();
    //send unity restore completed finished . by wjp 2019/12/22
//    UnitySendMessage("PurchaseGo", "OnRestoreCallback1", [@"" UTF8String]);
    
    //track restore completed.
    [self trackSendData:@"RestoreCompleted"  withPayCode:@"" withOrderId:@"" withReason:@""];
}

-(void)ShowMessage:(NSString *)msg
{
    if (!self.isBackground) {
        dispatch_async(dispatch_get_main_queue(), ^{
        UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"Magic!"
                                                        message:msg
                                                       delegate:nil
                                              cancelButtonTitle:@"OK"
                                              otherButtonTitles:nil, nil];
        [alert show];
        });
    }
    
}



// 支付1.0接口
-(void)doPay:(NSDictionary *)dict
{
    isPayStart = true;
    if([SKPaymentQueue canMakePayments]) {
        NSString *extra =  [dict objectForKey:@"extraInfo"];
        if (extra != nullptr){
            _extraJsonStr =[[NSString alloc] initWithString:(extra) ];
        }
        [self request:[NSString stringWithString:[dict objectForKey:@"order"]]];
    }
}

// 支付2.0接口
-(void)doPayV2:(NSDictionary *)dict
{
    isPayStart = true;
    if([SKPaymentQueue canMakePayments]) {
        NSString *extra =  [dict objectForKey:@"tranOrder"];
        if (extra != nullptr){
            _extraJsonStr =[[NSString alloc] initWithString:(extra) ];
        }
        [self request:[NSString stringWithString:[dict objectForKey:@"sku"]]];
    }
}


-(void)request:(NSString *)itemid{
    if(mProductsRequest != nil){
        [mProductsRequest cancel];
        mProductsRequest = nil;
    }
    //查询商品
    NSArray *product = nil;
    product=[[NSArray alloc] initWithObjects:itemid,nil];
    NSSet *nsset = [NSSet setWithArray:product];

    mProductsRequest = [[SKProductsRequest alloc] initWithProductIdentifiers:nsset];
    mProductsRequest.delegate = self;
    [mProductsRequest start];
    
    buyBegin();
    //track request begin .
    NSString *paycodearg = @"";
    if (itemid!=nil && [itemid length]>0) {
        paycodearg = itemid;
    }
    //track request begin.
    [self trackSendData:@"RequestBegin"  withPayCode:paycodearg withOrderId:@"" withReason:@""];
}

- (void)SetBackgroundSatus:(BOOL)isBack
{
    self.isBackground = isBack;
}
/*
 * order            sdu
 * transactionId    订单号
 * extraInfo        订单信息
 */
+(void)onBuy:(NSDictionary *)dict
{
    if(!_payHelper){
        [PayHelper getInstance];
    }
    [_payHelper doPay:dict];
}

/*
 * order  订单号
 * count  价钱（RMB）
 * itemid 商品id(暂时无用)
 */
+(void)onBuyV2:(NSDictionary *)dict
{
    if(!_payHelper){
        [PayHelper getInstance];
    }
    [_payHelper doPayV2:dict];
}

+(NSString*)getChannel
{
    return @"appstore";
}

//获取待补单订单dic for payV2
+(NSString*)getReorderData
{
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:reorderDic options:0 error:NULL];
    auto jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return jsonStr;
}

//清理待补单订单dic  for payV2
+(void)clearReorderData
{
    [reorderDic removeAllObjects];
}


//获取补单订单号(只保存近待补单订单信息) for PayV1
+(NSString*)getRamainsPaycode
{
    return remainsOrderJsonStr;
}

//清理补单订单号(只保存近待补单订单信息)  for PayV1
+(void)clearRamainsPaycode
{
     remainsOrderJsonStr = @"";
}


+(void)setCodeVersion:(NSDictionary *)dict{
    
    NSString* codeverison = [dict objectForKey:@"code"];
    lua_code_version =codeverison;
}

+(void)setPayVersion:(NSDictionary *)dict{
    int state =[[dict valueForKey:@"payversion"] intValue];
    pay_version = state;
}

//print order info for debug
-(void)printOrderInfo:(NSString*)str{
    if (str!=nil and str.length > 0) {
        int length = 32760;
        int times = ceil(str.length / length);
        int begin =0;
        int num = length;
        for (int i=0; i<=times; i++) {
            begin = i* length;
            num = length;
            if ((begin+num)>str.length) {
                num = str.length - begin;
            }
            if (num<=0) {
                break;
            }
            NSRange range = NSMakeRange(begin, num);
            NSString* result = [str substringWithRange:range];
            //NSLog(@"-->%@",result);
        }
    }
}

//print order info for debug
-(void)printOrderInfo2{
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:reorderDic options:0 error:NULL];
    NSString* str = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    if (str!=nil and str.length > 0) {
        int length = 32760;
        int times = ceil(str.length / length);
        int begin =0;
        int num = length;
        for (int i=0; i<=times; i++) {
            begin = i* length;
            num = length;
            if ((begin+num)>str.length) {
                num = str.length - begin;
            }
            if (num<=0) {
                break;
            }
            NSRange range = NSMakeRange(begin, num);
            NSString* result = [str substringWithRange:range];
            //NSLog(@"--restore >%@",result);
        }
    }
}

-(void)restore
{
    [[SKPaymentQueue defaultQueue] restoreCompletedTransactions];
}

+(void)doRestore
{
     if(!_payHelper){
         [PayHelper getInstance];
     }
     [_payHelper restore];
}

//更新补单或者恢复购买的订单记录
-(void)updateRestoreDic:(SKPaymentTransaction*)transaction withDic:(NSMutableDictionary*)dic
{
    //appStoreReceiptURL iOS7.0增加的，购买交易完成后，会将凭据存放在该地址
    NSURL *receiptURL = [[NSBundle mainBundle] appStoreReceiptURL];
    //从沙盒中获取到购买凭据
    NSData *receiptData = [NSData dataWithContentsOfURL:receiptURL];
    NSString *encodeStr = [receiptData base64EncodedStringWithOptions:NSDataBase64EncodingEndLineWithLineFeed];
    
    //获取订单中的透传信息
    NSString *extraStr = transaction.payment.applicationUsername;
    
    NSMutableDictionary *jsonRaw = [NSMutableDictionary dictionary];
    jsonRaw[@"paycode"] = transaction.payment.productIdentifier;
    jsonRaw[@"orderId"] = transaction.transactionIdentifier;
    jsonRaw[@"receipt"] = encodeStr;
    if (extraStr!=nil && [extraStr length] > 0) {
        //old format 
        //"{\"transactionId\":\"403901598774243M5t32LD9441061706\",\"uuid\":\"40390\",\"rewarddata\":[{\"id\":\"91000\",\"num\":10,\"story_id\":\"\"}]}"
        //先检查是否存在老的透传参数，如果有，那么就返回老的;否则返回新的
        if (([extraStr rangeOfString:@"transactionId"].location!=NSNotFound) && ([extraStr rangeOfString:@"uuid"].location!=NSNotFound)) {
            jsonRaw[@"extraJsonStr"] = extraStr;
        }else{
            jsonRaw[@"tranOrder"] = extraStr;
        }
    }else
    {
        jsonRaw[@"tranOrder"] = @"";
    }
    
    NSString *jsonStr  = @"";
    BOOL isYes = [NSJSONSerialization isValidJSONObject:jsonRaw];
    if (isYes) {
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:jsonRaw options:0 error:NULL];
        jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    } else {
        NSLog(@"jsoncreate failed");
    }
    
    bool issame = false;
    for (NSMutableDictionary* item in temparray) {
        if ([[item valueForKey:@"receipt"] isEqualToString:encodeStr]) {
            //NSLog(@"wjp same receipt  %@",transaction.transactionIdentifier);
            issame=true;
        }
    }
    
    if (!issame)
    {
        //NSLog(@"wjp add dic %@   %@",transaction.payment.productIdentifier,transaction.transactionIdentifier);
        [temparray  addObject:jsonRaw];
        dic[transaction.transactionIdentifier]= jsonStr;
    }
}

//adjust track ios buy subscribe
-(void)adjsutTrackSubscribe:(SKPaymentTransaction*)transaction withReceipt:(NSData*)receiptData{
    if (SubscribePruduct!=nil && transaction!=nil && receiptData!=nil && [transaction.payment.productIdentifier isEqualToString:Pay_Subscribe]) {
        //delete adjust track.
    }
}

//track with paycode , orderid and reason.
-(void)trackSendData:(NSString*)stepName  withPayCode:(NSString*)payCode withOrderId:(NSString*)orderId withReason:(NSString*)reason{
    NSMutableDictionary *dic = [NSMutableDictionary dictionary];
    [UtilsCommon addDic:dic withStringKey:@"eid" withStringValue:@"13"];
    [UtilsCommon addDic:dic withStringKey:@"parm5" withStringValue:@"ios_pay_step"];
    [UtilsCommon addDic:dic withStringKey:@"parm6" withStringValue:stepName];
    [UtilsCommon addDic:dic withStringKey:@"parm7" withStringValue:payCode];
    [UtilsCommon addDic:dic withStringKey:@"parm8" withStringValue:orderId];
    [UtilsCommon addDic:dic withStringKey:@"parm9" withStringValue:reason];
    NSString* json = [UtilsCommon getjsonstr:dic];
    if ([json length]>0) {
        //NSLog(@"track data %@",json);
        UnitySendMessage("SDK", "OnReportChaptersMsg", [json UTF8String]);
    }
}

//track with transaction and reason.
-(void)trackSendData:(NSString*)stepName withTransation:(SKPaymentTransaction*)transaction
          withReason:(NSString*)reason{
    NSString* paycodearg = @"";
    NSString* orderidarg = @"";
    if (transaction!=nil) {
        if (transaction.payment!=nil && transaction.payment.productIdentifier!=nil && [transaction.payment.productIdentifier length]>0) {
            paycodearg = transaction.payment.productIdentifier;
        }
        if (transaction.transactionIdentifier!=nil && [transaction.transactionIdentifier length]>0) {
            orderidarg = transaction.transactionIdentifier;
        }
    }
    [self trackSendData:stepName  withPayCode:paycodearg   withOrderId:orderidarg withReason:reason];
}

//track with error.
-(void)trackSendData:(NSString*)stepName withReason:(NSString*)reason withError:(NSError*)error{
    if (error!=nil && error.localizedDescription!=nil && [error.localizedDescription length]>0) {
        reason =error.localizedDescription;
    }
    [self trackSendData:stepName  withPayCode:@""   withOrderId:@"" withReason:reason];
}
@end



