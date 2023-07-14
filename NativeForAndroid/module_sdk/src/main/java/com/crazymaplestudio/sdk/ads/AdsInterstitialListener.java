package com.crazymaplestudio.sdk.ads;

import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.AdBackEvent;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.mopub.mobileads.MoPubErrorCode;
import com.mopub.mobileads.MoPubInterstitial;


/**
 * Provides an API that bridges the Unity Plugin with the MoPub Interstitial SDK.
 */
public class AdsInterstitialListener implements MoPubInterstitial.InterstitialAdListener
{

    private static volatile AdsInterstitialListener sInstance;

    /**
     * Creates a {@link AdsInterstitialListener}
     *
     */
    private AdsInterstitialListener() {

    }

    public static AdsInterstitialListener getInstance() {
        if (sInstance == null) {
            synchronized (AdsInterstitialListener.class) {
                if (sInstance == null) {
                    sInstance = new AdsInterstitialListener();
                }
            }
        }
        return sInstance;
    }

    //下面的是插屏的事件
    @Override
    public void onInterstitialLoaded(MoPubInterstitial interstitial) {
        CMSLog.i("jin-->onInterstitialLoaded  success "+interstitial.getAdUnitId());
        String info = "";
        try {
            //JSONObject jsonObject = new JSONObject();
            //jsonObject.put("placementName",interstitial.getAdUnitId());
            //info = jsonObject.toString();
            //UnityTool.OnMopubInterstitialLoaded(info);
            AdBackEvent event = new AdBackEvent(interstitial.getAdUnitId());
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_INTER_LOAD_SUCCESS,event);
        }catch (Exception e) {

        }

        if (AdsHelper.isOpenInterCache()){
            AdsHelper.checkToLoadInterstitialCache();
        }
    }

    @Override
    public void onInterstitialFailed(MoPubInterstitial interstitial, MoPubErrorCode errorCode) {
        CMSLog.i("jin-->onInterstitialFailed  "+interstitial.getAdUnitId() +" errorinfo:"+errorCode.toString());
        String info = "";
        try {
            //JSONObject jsonObject = new JSONObject();
            //jsonObject.put("placementName",interstitial.getAdUnitId());
            //jsonObject.put("errorinfo",errorCode.toString());
            //info = jsonObject.toString();
            //UnityTool.OnMopubInterstitialFailed(info);

            AdBackEvent event = new AdBackEvent(interstitial.getAdUnitId(),errorCode.toString());
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_INTER_LOAD_FAIL,event);
        }catch (Exception e) {

        }

    }

    @Override
    public void onInterstitialShown(MoPubInterstitial interstitial) {
        CMSLog.i("jin-->InterstitialShown "+interstitial.getAdUnitId());
        String info = "";
        try {
//            JSONObject jsonObject = new JSONObject();
//            jsonObject.put("placementName",interstitial.getAdUnitId());
//            info = jsonObject.toString();
//            UnityTool.OnMopubInterstitialShown(info);
            AdBackEvent event = new AdBackEvent(interstitial.getAdUnitId());
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_INTER_PLAY,event);
        }catch (Exception e) {

        }
    }

    @Override
    public void onInterstitialClicked(MoPubInterstitial interstitial) {
        CMSLog.i("jin-->InterstitialClicked "+interstitial.getAdUnitId());
        String info = "";
        try {
//            JSONObject jsonObject = new JSONObject();
//            jsonObject.put("placementName",interstitial.getAdUnitId());
//            info = jsonObject.toString();
//            UnityTool.OnMopubInterstitialClicked(info);
            AdBackEvent event = new AdBackEvent(interstitial.getAdUnitId());
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_INTER_CLICK,event);
        }catch (Exception e) {

        }
    }

    @Override
    public void onInterstitialDismissed(MoPubInterstitial interstitial) {
        CMSLog.i("jin-->InterstitialDismissed " + interstitial.getAdUnitId());
        String info = "";
        try {
//            JSONObject jsonObject = new JSONObject();
//            jsonObject.put("placementName",interstitial.getAdUnitId());
//            info = jsonObject.toString();
//            UnityTool.OnMopubInterstitialDismissed(info);
            AdBackEvent event = new AdBackEvent(interstitial.getAdUnitId());
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_INTER_COMPLETE,event);
        }catch (Exception e) {

        }

        if (AdsHelper.isOpenInterCache()){
            AdsHelper.checkToLoadInterstitialCache();
        }else {
            AdsHelper.loadInterstitial();
        }
    }
}
