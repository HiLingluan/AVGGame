package com.crazymaplestudio.sdk.event;

import com.crazymaplestudio.sdk.tools.UnityTool;

import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;

/**
 * description
 * Created by jin
 * Created date: 2021-06-19
 */
public class CMSEventManager {
    private Collection listeners;

    private static CMSEventManager _instance = null;


    public static CMSEventManager getInstance() {
        if(_instance==null)
        {
            _instance = new CMSEventManager();
        }
        return _instance;
    }


    /**
     * 添加事件
     *
     * @param listener
     *            DoorListener
     */
    public void addListener(CMSEventListener listener) {
        if (listeners == null) {
            listeners = new HashSet();
        }
        listeners.add(listener);
    }

    /**
     * 移除事件
     *
     * @param listener
     *            DoorListener
     */
    public void removeListener(CMSEventListener listener) {
        if (listeners == null)
            return;
        listeners.remove(listener);
    }

    /**
     * 触发事件
     */
    public void postEvent(String eventName, Object eventObject) {

        //unity端发送事件
        UnityTool.sendMessageWithOBJ(eventName,eventObject.toString());

        if (listeners == null)
            return;
        notifyListeners(eventName,eventObject);
    }

    /**
     * 通知所有的DoorListener
     */
    private void notifyListeners(String eventName,Object eventObject) {
        if(listeners==null) return;
        if(listeners.isEmpty()) return;

        Iterator iter = listeners.iterator();
        while (iter.hasNext()) {
            CMSEventListener listener = (CMSEventListener) iter.next();
            String ename = listener.getEventName();
            if(ename.equals(CMSEventConst.CMS_EVENT_ALL_EVENT) || ename.equals(eventName)){
                listener.onEventBack(eventName,eventObject);
            }
        }
    }
}
