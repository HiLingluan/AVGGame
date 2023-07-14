package com.crazymaplestudio.sdk.event.eventmodel;

import com.crazymaplestudio.sdk.CMSConstant;

/**
 * description
 * Created by jin
 * Created date: 2021-06-21
 */
public class NetworkStateEvent extends CMSEvent {

    //变化前状态
    private int beforeChangeState = CMSConstant.NETWORK_CLASS_NONE;
    //变化后状态
    private int afterChangeState = CMSConstant.NETWORK_CLASS_NONE;

    public NetworkStateEvent(int before,int after){
        beforeChangeState = before;
        afterChangeState = after;
    }

    public int getBeforeChangeState() {
        return beforeChangeState;
    }

    public int getAfterChangeState() {
        return afterChangeState;
    }

    public String toString() {
        return "{" +
            "\"beforeChangeState\":" + beforeChangeState + "," +
            "\"afterChangeState\":" + afterChangeState +
            "}";
    }
}
