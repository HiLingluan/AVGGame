package com.crazymaplestudio.sdk.event.eventmodel;

/**
 * description
 * Created by jin
 * Created date: 2021-06-22
 */
public class PermissionEvent extends CMSEvent {
    //当前申请的权限code
    private int requestCode;

    public PermissionEvent(int code){
        requestCode = code;
    }

    public int getRequestCode() {
        return requestCode;
    }

    public String toString() {
        return "{" +
                "\"requestCode\":" + requestCode +
                "}";
    }
}
