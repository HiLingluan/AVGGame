package com.crazymaplestudio.sdk.event.eventmodel;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * description
 * Created by jin
 * Created date: 2021-06-22
 */
public class LoginFailEvent extends CMSEvent {

    //错误码
    private int error_code;
    //错误信息
    private String error_message;
    //登录类型
    private String logintype;

    public LoginFailEvent(int error_code,String error_message,String logintype) {
        this.error_code = error_code;
        this.error_message = error_message;
        this.logintype = logintype;
    }

    public String toString() {
        String jsonstr = "";
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("error_code", error_code);
            jsonObject.put("error_message", error_message);
            jsonObject.put("logintype", logintype);
            jsonstr = jsonObject.toString();
        } catch (JSONException e) {
            e.printStackTrace();

        }
        return jsonstr;
    }

    public int getErrorCode() {
        return error_code;
    }


    public String getErrorMessage() {
        return error_message;
    }

    public String getLoginType() {
        return logintype;
    }
}
