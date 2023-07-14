package com.crazymaplestudio.sdk.networkstate;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.util.Log;

import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.NetworkStateEvent;

/**
 * 网络状态变化 监听
 */
public class NetBroadcastReceiver extends BroadcastReceiver {

    @Override
    public void onReceive(Context context, Intent intent) {
        // TODO Auto-generated method stub
        // 如果相等的话就说明网络状态发生了变化

        if (intent.getAction().equals(ConnectivityManager.CONNECTIVITY_ACTION)) {
            if(NetworkStateHelper.getContext()==null) return;

            Log.i("网络状态变化", "网络状态变化 变化前:"+NetworkStateHelper.curNetState);
            int netWorkState = NetworkStateHelper.getNetworkState();
            // 当网络发生变化，判断当前网络状态，并通过NetEvent回调当前网络状态

            if(NetworkStateHelper.curNetState != netWorkState)
            {
                Log.i("网络状态变化", "网络状态变化 变化后:"+netWorkState);

                //UnityTool.onNetworkChangeBack(NetworkStateHelper.curNetState + "#" + netWorkState);

                NetworkStateEvent event = new NetworkStateEvent(NetworkStateHelper.curNetState,netWorkState);
                CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_NETWORK_CHANGE,event);

                NetworkStateHelper.curNetState = netWorkState;
            }
        }
    }
}