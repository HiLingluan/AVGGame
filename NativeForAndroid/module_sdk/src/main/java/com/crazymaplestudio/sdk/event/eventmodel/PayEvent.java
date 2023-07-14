package com.crazymaplestudio.sdk.event.eventmodel;

import com.android.billingclient.api.Purchase;
import com.crazymaplestudio.sdk.iap.PayHelper;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * description
 * Created by jin
 * Created date: 2021-06-22
 */
public class PayEvent extends CMSEvent {

    //返回码
    private int error_code;
    //返回信息
    private String error_reason;
    //订单信息
    private Purchase purchase_info;

    public PayEvent(int code, String msg, Purchase purchase){
        error_code      = code;
        error_reason    = msg;
        purchase_info   = purchase;
    }

    //消耗订单回调需要增加来源s
//    public PayEvent(int code,String msg,Purchase purchase,int source){
//        error_code      = code;
//        error_reason    = msg;
//        purchase_info   = purchase;
//        source_code     = source;
//    }

    public int getError_code() {
        return error_code;
    }

    public String getError_reason() {
        return error_reason;
    }

    public Purchase getPurchase_info() {
        return purchase_info;
    }


    public String toString() {
        String jsonstr = "";
        try {
            String purchaseInfo = PayHelper.parsePayInfo2JsonStr(purchase_info);
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("data", purchaseInfo);
            jsonObject.put("error_code", error_code);
            jsonObject.put("error_reason", error_reason);
            jsonstr = jsonObject.toString();
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return jsonstr;
    }
}
