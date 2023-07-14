package com.crazymaplestudio.sdk.event.eventmodel;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * description
 * Created by jin
 * Created date: 2021-06-22
 */
public class AdBackEvent extends CMSEvent {

    //广告id
    private String placementName;

    //错误原因 失败时候传递
    private String errorinfo;

    public AdBackEvent(String adunit) {
        placementName = adunit;
    }

    //失败时候创建
    public AdBackEvent(String adunit, String errorMsg) {
        placementName = adunit;
        errorinfo = errorMsg;
    }

    public String getPlacementName() {
        return placementName;
    }

    public String getErrorinfo() {
        return errorinfo;
    }


    public void setErrorinfo(String errorMsg) {
        this.errorinfo = errorMsg;
    }

    public String toString() {
        String jsonstr = "";
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("placementName",placementName);
            jsonObject.put("errorinfo",errorinfo);
            jsonstr = jsonObject.toString();
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return jsonstr;
    }
}
