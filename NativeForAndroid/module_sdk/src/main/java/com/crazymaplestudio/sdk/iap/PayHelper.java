package com.crazymaplestudio.sdk.iap;

/***
 * Google  支付流程封装
 * 已支持 billing v4
 * usage:
 * 主Activity onCreate() 中调用 initActivity() 初始化支付服务
 *
 */

import android.app.Activity;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.android.billingclient.api.AcknowledgePurchaseParams;
import com.android.billingclient.api.AcknowledgePurchaseResponseListener;
import com.android.billingclient.api.BillingClient;
import com.android.billingclient.api.BillingClientStateListener;
import com.android.billingclient.api.BillingFlowParams;
import com.android.billingclient.api.BillingResult;
import com.android.billingclient.api.ConsumeParams;
import com.android.billingclient.api.ConsumeResponseListener;
import com.android.billingclient.api.Purchase;
import com.android.billingclient.api.PurchasesResponseListener;
import com.android.billingclient.api.PurchasesUpdatedListener;
import com.android.billingclient.api.SkuDetails;
import com.android.billingclient.api.SkuDetailsParams;
import com.android.billingclient.api.SkuDetailsResponseListener;

import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.PayEvent;
import com.crazymaplestudio.sdk.tools.CMSLog;

import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;


/**
 * The type Pay helper.
 */
public class PayHelper {

    private final static String  LOG_TAG = "cms_pay";

    private static Activity mContext = null;

    //未消耗普通订单列表
    private static List<Purchase> mPurchasesList = null;
    //订阅订单列表
    private static List<Purchase> mSubList = null;

    //支付工具实例
    private static BillingClient mBillingClient;

    //购买回调(普通+订阅)
    private static CMSPurchasesUpdatedListener mPurchasesUpdatedListener = new CMSPurchasesUpdatedListener();

    private static String mCurrentSkuType = BillingClient.SkuType.INAPP;

    //region 初始化google应用内购买服务
    /**
     * 初始化
     *
     * @param context the context
     */
    public static void init(Activity context){
        mContext = context;

        if(mBillingClient==null)
        {
            CMSLog.i(LOG_TAG,"支付 初始化 111");
            BillingClient.Builder builder = BillingClient.newBuilder(context);
            mBillingClient = builder.setListener(mPurchasesUpdatedListener)
                    .enablePendingPurchases()
                    .build();
        }

        startConnection();
    }


    //获取IAP 是否初始化完成
    public static boolean isIAPReady(){
        if(mContext!=null && mBillingClient!=null && mBillingClient.isReady()){
            return isConnected();
        }
        return false;
    }

    public static boolean isConnected(){
        if(mBillingClient!=null&&mBillingClient.getConnectionState()==BillingClient.ConnectionState.CONNECTED){
            return true;
        }
        return false;
    }


    /**
     * 与Google Play建立连接
     */
    public static void startConnection() {
        CMSLog.i(LOG_TAG,"与Google Play建立连接 111");

        if(mBillingClient.getConnectionState()==BillingClient.ConnectionState.CONNECTED ||
                mBillingClient.getConnectionState()==BillingClient.ConnectionState.CONNECTING) {
            CMSLog.i(LOG_TAG,"与Google Play建立连接 333");
            return ;
        }

        mContext.runOnUiThread(() -> {
            mBillingClient.startConnection(new BillingClientStateListener() {
                @Override
                public void onBillingSetupFinished(@NonNull BillingResult billingResult) {
                    if (billingResult.getResponseCode() == BillingClient.BillingResponseCode.OK) {
                        //queryPurchases();
                        //链接成功最好去查询订单，做掉单处理
                        CMSLog.i(LOG_TAG,"与Google Play建立连接 成功 444!!");

                        queryPurchasedInfo();
                        queryPurchasesSubs();
                    }else
                    {
                        CMSLog.i(LOG_TAG,"与Google Play建立连接 失败 555 code:"+billingResult.getResponseCode()+",info:"+billingResult.getDebugMessage());
                    }
                }
                @Override
                public void onBillingServiceDisconnected() {
                    // Try to restart the connection on the next request to
                    // Google Play by calling the startConnection() method.
                    //建议断开时重连或在使用时判断连接状态，没有连接就手动再调一次 startConnection，确保在执行任何方法时都与 BillingClient 保持连接。
                    CMSLog.i(LOG_TAG,"支付 初始化失败 断开连接 666");
                }
            });
        });
    }

    //应用销毁时候调用
    public static void onDestory(){
        if(mBillingClient!=null && (mBillingClient.getConnectionState()==BillingClient.ConnectionState.CONNECTED))
        {
            mBillingClient.endConnection();
            mBillingClient = null;
        }

        mContext = null;
        mSubList = null;
        mPurchasesList = null;
        mPurchasesUpdatedListener = null;
    }

    /**
     * 生命周期方法   在主Activity中调用
     */
    public static void onResume() {

    }

    public static boolean isPayNotAliviable(int code) {
        switch (code){
            case PAY_CODE.SERVICE_TIMEOUT:
            case PAY_CODE.FEATURE_NOT_SUPPORTED:
            case PAY_CODE.SERVICE_DISCONNECTED:
            case PAY_CODE.SERVICE_UNAVAILABLE:
            case PAY_CODE.BILLING_UNAVAILABLE:
            case PAY_CODE.SDK_NOT_READY:
                return true;
        }
        return false;
    }

    //endregion





    //region  普通支付(非订阅)
    /**
     支付流程接口
     itemSKU 计费点代码
     tranUuid 透传uuid
     tranOrder 透传订单号
     */
    public static void doPay(final String itemSKU, final String tranUuid, final String tranOrder){
        CMSLog.i(LOG_TAG,"start fo pay  itemSKU===>"+itemSKU);
        CMSLog.i(LOG_TAG,"start fo pay  tranUuid===>"+tranUuid);
        CMSLog.i(LOG_TAG,"start fo pay  tranOrder===>"+tranOrder);


        doGooglePurchase(itemSKU,tranUuid,tranOrder,BillingClient.SkuType.INAPP);
    }

    //google  play 购买
    //普通支付和订阅都走这个流程
    private static void doGooglePurchase(final String itemSKU, final String tranUuid, final String tranOrder,final String skuType){
        if(!isConnected()){
            CMSLog.i(LOG_TAG,"支付未初始化");
            startConnection();
            onPayFailed(null, PAY_CODE.SDK_NOT_READY,"pay service not ready");
            return;
        }

        mContext.runOnUiThread(() -> {
            List<String> skuList = new ArrayList<>();
            skuList.add(itemSKU);
            SkuDetailsParams.Builder params = SkuDetailsParams.newBuilder();
            params.setSkusList(skuList).setType(skuType);//INAPP应用内支付

            //设置计费点类型
            mCurrentSkuType = skuType;

            mBillingClient.querySkuDetailsAsync(params.build(), new SkuDetailsResponseListener() {
                @Override
                public void onSkuDetailsResponse(BillingResult billingResult, List<SkuDetails> skuDetailsList) {
                    if (billingResult.getResponseCode() == BillingClient.BillingResponseCode.OK ) {
                        if(skuDetailsList!=null&&!skuDetailsList.isEmpty()){
                            SkuDetails skuDetails= skuDetailsList.get(0);
                            if (itemSKU.equals(skuDetails.getSku())) {
                                //启动购买
                                BillingFlowParams purchaseParams =
                                        BillingFlowParams.newBuilder()
                                                .setSkuDetails(skuDetails)
                                                .setObfuscatedAccountId(tranUuid)
                                                .setObfuscatedProfileId(tranOrder)
                                                .build();
                                mBillingClient.launchBillingFlow(mContext, purchaseParams);
                                //购买状态将在PurchasesUpdatedListener.onPurchasesUpdated返回
                            }else{
                                //计费点不匹配
                                onPayFailed(null, PAY_CODE.SKU_NOT_FIND,"pay sku not match");
                            }
                        }else {
                            //计费点列表为空
                            onPayFailed(null, PAY_CODE.SKU_NOT_FIND,"skuDetailsList is null");
                        }
                    }else {
                        //获取计费点详情 失败
                        onPayFailed(null, PAY_CODE.PAY_SKUDETAIL_FAIL,"get sku detail info fail "+billingResult.getResponseCode());
                    }
                }
            });
        });
    }


    /*
        支付完成回调
     */
    public static void onPaySuccess(Purchase purchase, int error_code, String reason ){
        CMSLog.i(LOG_TAG,"OnPaySuccess:"+parsePayInfo2JsonStr(purchase)+"====error_code:"+error_code+"===reason:"+reason);
        //purchasedInventory.addPurchase(purchase);

        //测试log，上线后不要留下
        PayEvent event = new PayEvent(error_code,reason,purchase);
        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PAY_SUCCESS,event);
    }

    /*
        支付失败
     */
    public static  void onPayFailed(Purchase purchase, int error_code, String reason ){
        CMSLog.i(LOG_TAG,"onPayFailed:"+parsePayInfo2JsonStr(purchase)+"====error_code:"+error_code+"===reason:"+reason);

        //DebugTool.i(LOG_TAG,"OnPayFailed "+info2Lua);
        //UnityTool.OnPayFailedV3(info2Lua);
        PayEvent event = new PayEvent(error_code,reason,purchase);
        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PAY_FAILED,event);
    }


    /*
        支付取消
     */
    public static void onPayCanceled(Purchase purchase, int error_code, String reason ){
        CMSLog.i(LOG_TAG,"onPayCanceled:"+parsePayInfo2JsonStr(purchase)+"====error_code:"+error_code+"===reason:"+reason);

        PayEvent event = new PayEvent(error_code,reason,purchase);
        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PAY_CANCELED,event);
    }

    /**
     * Google购买商品回调接口(订阅和内购都走这个接口)
     */
    private static class CMSPurchasesUpdatedListener implements PurchasesUpdatedListener
    {
        @Override
        public void onPurchasesUpdated(@NonNull BillingResult billingResult, @Nullable List<Purchase> list) {
            //交易更新将会在这里回调
            int responseCode = billingResult.getResponseCode();
            String responseMsg = billingResult.getDebugMessage();

            CMSLog.i(LOG_TAG,"购买商品回调 cdoe:"+responseCode+",info:" +responseMsg);
            //DebugTool.i(LOG_TAG,"购买商品回调 list:"+list.size());
            if (responseCode == BillingClient.BillingResponseCode.OK && list != null) {
                CMSLog.i(LOG_TAG, "购买商品回调 list:" + list.size());

                Purchase purchase = list.get(0);
                if (purchase != null){
                    //String googlePayOrderId = purchase.getOrderId();
                    //String purchaseToken = purchase.getPurchaseToken();
                    //服务器验证
                    //verifyPayment(orderId, googlePayOrderId, productId, purchaseToken);
                    CMSLog.i(LOG_TAG, "购买成功 计费点 sku:" + purchase.getSkus().get(0));
                    //DebugTool.i(LOG_TAG, "购买成功 purchaseToken:" + purchaseToken);
                    if (purchase.getAccountIdentifiers() != null) {
                        CMSLog.i(LOG_TAG, "购买成功 透传用户 tranUuid:" + purchase.getAccountIdentifiers().getObfuscatedAccountId());
                        CMSLog.i(LOG_TAG, "购买成功 透传订单 tranOrder:" + purchase.getAccountIdentifiers().getObfuscatedProfileId());
                    }

                    if(mCurrentSkuType.equals(BillingClient.SkuType.SUBS))
                    {
                        mSubList.add(purchase);
                    }else {
                        mPurchasesList.add(purchase);
                    }


                    onPaySuccess(purchase, PAY_CODE.PAY_SUCCCESS, "Purchase success");
                }else
                {
                    onPayFailed(null, PAY_CODE.SKU_NOT_FIND,"pay success not find purchase data");
                }
            } else if (responseCode == BillingClient.BillingResponseCode.USER_CANCELED) {
                //取消支付
                CMSLog.i(LOG_TAG,"取消支付");

                onPayCanceled(null, PAY_CODE.PAY_USER_CANCEL,"User canceled");
            } else if (responseCode == BillingClient.BillingResponseCode.ITEM_ALREADY_OWNED) {
                //已存在这个未完成订单，查询订单验证然后消耗掉
                //queryPurchases();
                onPayFailed(null,responseCode,"Item Already Owned");
                CMSLog.i(LOG_TAG,"支付异常 计费点已拥有");
            } else {
                //还有很多其他状态，判断进行相应处理
                CMSLog.i(LOG_TAG,"支付异常 其他异常 cdoe:"+responseCode+",info:" +responseMsg );
                onPayFailed(null,responseCode,responseMsg);
            }
        }
    }

    //endregion

    //region  消耗计费点

    /*
       获取已购买项目信息
    */
    public static void queryPurchasedInfo(){
        CMSLog.i(LOG_TAG,"queryPurchasedInfo 111");
        if (mContext == null) {
            //不可在mContext未初始化情况下调用此方法
            CMSLog.i(LOG_TAG,"queryPurchasedInfo 222");
            return;
        }
        if (!isIAPReady()) {
            CMSLog.i(LOG_TAG,"queryPurchasedInfo 支付未初始化  333");
            PayEvent event = new PayEvent(PAY_CODE.SDK_NOT_READY,"play severice not ready",null);
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PAY_QUERY_FAIL,event);
            return;
        }
        CMSLog.i(LOG_TAG,"queryPurchasedInfo 开始查询  444");

        mContext.runOnUiThread(() -> {
            PurchasesResponseListener listener = new PurchasesResponseListener() {
                @Override
                public void onQueryPurchasesResponse(@NonNull BillingResult billingResult, @NonNull List<Purchase> purchasesList) {
                    if(billingResult.getResponseCode()==BillingClient.BillingResponseCode.OK){
                        mPurchasesList = purchasesList;
                        CMSLog.i(LOG_TAG, "queryPurchasedInfo 查询成功 555 mPurchasesList:" + purchasesList.size());
                        PayEvent event = new PayEvent(PAY_CODE.QUERY_SUCCESS,"query purchased success",null);
                        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PAY_QUERY_SUCCESS,event);
                    }else
                    {
                        CMSLog.i(LOG_TAG, "queryPurchasedInfo 查询失败 666" );
                        PayEvent event = new PayEvent(billingResult.getResponseCode(),billingResult.getDebugMessage(),null);
                        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PAY_QUERY_FAIL,event);
                    }
                }
            };
            mBillingClient.queryPurchasesAsync(BillingClient.SkuType.INAPP, listener );
        });
        return;
    }

    /**
     * 消费计费点记录
     * @param sku
     */
    public static void consumePurchaseRecord(final String sku) {

        CMSLog.i(LOG_TAG,"consumePurchase 开始 111==>"+sku);
        if(!isIAPReady()) {
            CMSLog.i(LOG_TAG,"consumePurchase 支付未初始化 222==>"+sku);
            onConsumeFailed(null, PAY_CODE.SDK_NOT_READY,"pay service not ready");
            return;
        }

        if(mPurchasesList==null|| mPurchasesList.size()==0) {
            CMSLog.i(LOG_TAG,"consumePurchase 计费点列表为空 333==>"+sku);
            onConsumeFailed(null, PAY_CODE.SKU_NOT_FIND,"mPurchasesList is null");
            return;
        }

        Purchase purchase = getPurchaseBySku(sku);
        if(purchase==null){
            CMSLog.i(LOG_TAG,"consumePurchase 未找到计费点数据 444==>"+sku);
            onConsumeFailed(null, PAY_CODE.SKU_NOT_FIND,"sku data not find");
            return;
        }

        String purchaseToken = purchase.getPurchaseToken();
        if(purchaseToken.isEmpty()) {
            CMSLog.i(LOG_TAG,"consumePurchase purchaseToken为空 555==>"+sku);
            onConsumeFailed(null, PAY_CODE.CONSUME_TOKEN_FAIL,"purchase token is empty");
            return;
        }

        mContext.runOnUiThread(() -> {
            ConsumeParams consumeParams = ConsumeParams.newBuilder()
                    .setPurchaseToken(purchaseToken)
                    .build();
            ConsumeResponseListener listener = new ConsumeResponseListener() {
                @Override
                public void onConsumeResponse(BillingResult billingResult, String token) {
                    if (billingResult.getResponseCode() == BillingClient.BillingResponseCode.OK) {
                        // Handle the success of the consume operation.
                        CMSLog.i(LOG_TAG,"消费计费点 成功 "+sku);

                        queryPurchasedInfo();

                        onConsumeSuccess(purchase, PAY_CODE.CONSUME_SUCCESS);
                    }else
                    {
                        CMSLog.i(LOG_TAG,"消费计费点 失败 "+sku +",code:"+billingResult.getResponseCode()+",info:"+billingResult.getDebugMessage());
                        onConsumeFailed(purchase,billingResult.getResponseCode(),billingResult.getDebugMessage());
                    }
                }
            };
            mBillingClient.consumeAsync(consumeParams, listener);
        });
    }

    /**
     *
     * @param sku 计费点
     * @return  1=有未消费记录 0=无未消费记录
     */
    public static int getPurchaseRecordState(final String sku) {
        Purchase result = getPurchaseBySku(sku);
        if(result!=null){
            return PURCHASE_STATE.HAS;
        }
        return PURCHASE_STATE.NONE;
    }

    /**
     *
     * @param sku 计费点
     * @return  返回订单json信息
     */
    public static String getPurchaseRecordData(final String sku) {
        Purchase result = getPurchaseBySku(sku);
        if(result==null){
            return "";
        }
        return parsePayInfo2JsonStr(result);
    }

    /**
     *
     * @param sku 计费点
     * @return  订单信息
     */
    public static Purchase getPurchaseBySku(final String sku) {
        if (mPurchasesList == null){
            CMSLog.i(LOG_TAG,"getPurchaseRecordData mPurchasesList is null !!!");
            return null;
        }
        if(mPurchasesList.size()>0){
            for (int i = 0; i < mPurchasesList.size(); i++) {
                Purchase item = mPurchasesList.get(i);
                if(item.getSkus().get(0).equals(sku)){
                    return item;
                }
            }
        }
        return null;
    }



    /**
     * 订单消耗失败
     * @param error_code  错误码
     * @param error_reason   错误原因
     */
    public static  void onConsumeFailed(Purchase purchase , final int error_code,final String error_reason){
        CMSLog.i(LOG_TAG,"onConsumeFailed:"+parsePayInfo2JsonStr(purchase)+"====error_code:"+error_code+"===reason:"+error_reason);

        PayEvent event = new PayEvent(error_code,error_reason,purchase);
        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PAY_CONSUME_FAIL,event);
    }

    /**
     * 订单消耗成功
     * @param s_code 成功码
     */
    public static  void onConsumeSuccess(Purchase purchase,final int s_code){
        CMSLog.i(LOG_TAG,"onConsumeSuccess:"+parsePayInfo2JsonStr(purchase));

        PayEvent event = new PayEvent(s_code,"consume success",purchase);
        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PAY_CONSUME_SUCCESS,event);
    }

    //endregion

    //region  订阅相关
    /**
     * 执行订阅操作
     * @param itemSKU 计费点代码
     * @param tranUuid 透传用户uuid
     * @param tranOrder 透传订单信息
     */
    public static void doSubscription(final String itemSKU, final String tranUuid, final String tranOrder){
        CMSLog.i(LOG_TAG,"doSubscription sku: "+itemSKU);
        CMSLog.i(LOG_TAG,"doSubscription tranUuid: "+tranUuid);
        CMSLog.i(LOG_TAG,"doSubscription tranOrder: "+tranOrder);

        doGooglePurchase(itemSKU,tranUuid,tranOrder,BillingClient.SkuType.SUBS);
    }

    //根据计费点获取订阅json字符串
    public static String getSubInfo(final String sku) {
        Purchase result = getSubBySku(sku);
        if(result==null){
            return "";
        }
        return parsePayInfo2JsonStr(result);
    }

    //根据计费点获取订阅订单数据
    public static Purchase getSubBySku(final String sku) {
        if (mSubList == null){
            CMSLog.i(LOG_TAG,"getPurchaseRecordData mPurchasesList is null !!!");
            return null;
        }
        if(mSubList.size()>0){
            for (int i = 0; i < mSubList.size(); i++) {
                Purchase item = mSubList.get(i);
                if(item.getSkus().get(0).equals(sku)){
                    return item;
                }
            }
        }
        return null;
    }

    /**
     * 查询订阅订单列表
     */
    public static void  queryPurchasesSubs() {
        CMSLog.i(LOG_TAG,"queryPurchasesSubs 111");
        if (mContext == null) {
            //不可在mContext未初始化情况下调用此方法
            CMSLog.i(LOG_TAG,"queryPurchasesSubs 222");
            return;
        }
        if (!isIAPReady()) {
            CMSLog.i(LOG_TAG,"queryPurchasesSubs 支付未初始化  333");
            return;
        }
        CMSLog.i(LOG_TAG,"queryPurchasesSubs 开始查询  444");

        mContext.runOnUiThread(() -> {
            PurchasesResponseListener listener = new PurchasesResponseListener() {
                @Override
                public void onQueryPurchasesResponse(@NonNull BillingResult billingResult, @NonNull List<Purchase> subList) {

                    if(billingResult.getResponseCode()==BillingClient.BillingResponseCode.OK){
                        mSubList = subList;
                        CMSLog.i(LOG_TAG, "queryPurchasesSubs 查询成功 555 mSubList:" + subList.size());
                    }else
                    {
                        CMSLog.i(LOG_TAG, "queryPurchasesSubs 查询失败 666" );
                    }
                }
            };
            mBillingClient.queryPurchasesAsync(BillingClient.SkuType.SUBS, listener );
        });
    }
    //endregion

    //region  确认订单
    //所有订阅与服务器校验完之后需要确认订单
    //未在三天主动确认订单，google将会主动退款
    public static void acknowledgeSub(String sku){
        CMSLog.i(LOG_TAG,"acknowledgeSub 确认订单 111==>"+sku);
        if(!isIAPReady())
        {
            CMSLog.i(LOG_TAG,"acknowledgeSub 支付未初始化 222==>"+sku);
            onAcknowledgeFailed(null, PAY_CODE.SDK_NOT_READY,"pay service not ready");
            return ;
        }
        Purchase sub =  getSubBySku(sku);
        if(sub==null){
            CMSLog.i(LOG_TAG,"acknowledgeSub 未找到计费点数据 333==>"+sku);
            onAcknowledgeFailed(null, PAY_CODE.SKU_NOT_FIND,"sku data not find");
            return;
        }

        String purchaseToken = sub.getPurchaseToken();
        if(purchaseToken.isEmpty()) {
            CMSLog.i(LOG_TAG,"acknowledgeSub purchaseToken为空 444==>"+sku);
            onAcknowledgeFailed(sub, PAY_CODE.ACKNOWLEDGE_FAIL,"purchase token is empty");
            return;
        }

        mContext.runOnUiThread(() -> {
            AcknowledgePurchaseParams params = AcknowledgePurchaseParams.newBuilder()
                    .setPurchaseToken(sub.getPurchaseToken())
                    .build();
            mBillingClient.acknowledgePurchase(params, new AcknowledgePurchaseResponseListener() {
                @Override
                public void onAcknowledgePurchaseResponse(@NonNull BillingResult billingResult) {
                    if (billingResult.getResponseCode() == BillingClient.BillingResponseCode.OK) {
                        //确认订单成功
                        onAcknowledgeSuccess(sub, PAY_CODE.ACKNOWLEDGE_SUCCESS);
                    }else{
                        //确认订单失败
                        onAcknowledgeFailed(sub,billingResult.getResponseCode(),billingResult.getDebugMessage());
                    }
                }
            });
        });
    }

    //检查订阅订单是否确认过
    public static int checkSubAcknowledge(String sku){
        Purchase sub =  getSubBySku(sku);
        if(sub!=null){
            if(sub.isAcknowledged())
                return SUB_STATE.ACKNOWLEDGED;
            else
                return SUB_STATE.UNACKNOWLEDGED;
        }

        return SUB_STATE.NORECORD;
    }

    /**
     * 确认订单失败
     * @param error_code  错误码
     * @param error_reason   错误原因
     */
    public static  void onAcknowledgeFailed(Purchase purchase , final int error_code,final String error_reason){
        CMSLog.i(LOG_TAG,"onAcknowledgeFailed:"+parsePayInfo2JsonStr(purchase)+"====error_code:"+error_code+"===reason:"+error_reason);

        PayEvent event = new PayEvent(error_code,error_reason,purchase);
        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PAY_ACKNOWLEDGE_FAIL,event);
    }

    /**
     * 确认订单成功
     * @param success_code 成功码
     */
    public static  void onAcknowledgeSuccess(Purchase purchase,final int success_code){
        CMSLog.i(LOG_TAG,"onAcknowledgeSuccess:"+parsePayInfo2JsonStr(purchase));

        PayEvent event = new PayEvent(success_code,"Acknowledge success",purchase);
        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PAY_ACKNOWLEDGE_SUCCESS,event);
    }


    //endregion


    /**
     * 解析订单对象
     * @param purchase
     * @return  josn string
     */
    public static  String parsePayInfo2JsonStr(Purchase purchase){
        if (purchase==null){
            return "";
        }
        String ret = "";
        try{
            JSONObject jsonObject = new JSONObject();

            String resultSKU =purchase.getSkus().get(0);
            /*
            交易的唯一订单标识符。此标识符对应于 Google Payments 订单 ID。
             如果订单为应用内购买结算沙盒中的测试订单，orderId 将为空。
             */
            String orderId = purchase.getOrderId();
            /*
           商品的购买时间（从新纪年（1970 年 1 月 1 日）开始计算的毫秒数）。
             */
            Long purchaseTime = purchase.getPurchaseTime();
            /*
                订单的购买状态。可能的值为 0（已购买）、1（已取消）或者 2（已退款）
             */
            int purchaseState = purchase.getPurchaseState();
            /*
            此处透传app uuid
             */
            String tranUuid = "";
            /*
            此处透传服务器订单信息
             */
            String tranOrder = "";
            if(purchase.getAccountIdentifiers()!=null){
                tranUuid = purchase.getAccountIdentifiers().getObfuscatedAccountId();
                tranOrder = purchase.getAccountIdentifiers().getObfuscatedProfileId();
            }

            //可能会存在旧版支付掉单到新版sdk补单的情况
            //这个时候还会把旧版透传参数返回
            String developerpayload = "";
            if(!purchase.getDeveloperPayload().isEmpty()){
                CMSLog.i("pay getDeveloperPayload =>"+purchase.getDeveloperPayload());
                developerpayload = purchase.getDeveloperPayload();
            }

            /*
            用于对给定商品和用户对进行唯一标识的令牌
             */
            String purchaseToken = purchase.getPurchaseToken();

            String originalJson = purchase.getOriginalJson();
            String signature = purchase.getSignature();




            jsonObject.put("sku",resultSKU);
            jsonObject.put("orderidsku",orderId);
            jsonObject.put("purchasetime",purchaseTime+"");
            jsonObject.put("purchasestate",purchaseState+"");
            jsonObject.put("tranUuid",tranUuid);
            jsonObject.put("tranOrder",tranOrder);
            jsonObject.put("purchaseToken",purchaseToken);
            jsonObject.put("originalJson",originalJson);
            jsonObject.put("signature",signature);
            jsonObject.put("developerpayload",developerpayload);

            ret = jsonObject.toString();
        } catch (Exception e) {
            e.printStackTrace();
            CMSLog.i("parsePayInfo2JsonStr error=>"+e.toString());
        }
        return ret;
    }

}


