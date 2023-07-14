package com.crazymaplestudio.sdk.event;

import java.util.EventListener;

/**
 * description
 * Created by jin
 * Created date: 2021-06-19
 */
public class CMSEventListener implements EventListener {

    private String _eventName;

    public CMSEventListener(String eventName) {
        _eventName = eventName;
    }

    public String getEventName(){
        return _eventName;
    }

    public void onEventBack(String eventName, Object eventObject){
        //do something
    };
}
