package com.crazymaplestudio.sdk.ads;

import androidx.annotation.NonNull;

import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.AdBackEvent;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.mopub.common.MoPubReward;
import com.mopub.mobileads.MoPubErrorCode;
import com.mopub.mobileads.MoPubRewardedVideoListener;

import java.util.Set;

/**
 * Singleton class to handle Rewarded Video events, as this is the one ad format to follow a
 * singleton pattern and we need to manage these events outside of the plugin lifecycle.
 */
public class AdsRewardedListener implements MoPubRewardedVideoListener {

    private static volatile AdsRewardedListener sInstance;

    //判断广告是否播放完
    private boolean isComplete=false;

    //当前加载播放激励id
    private String mRewardId ="";

    private AdsRewardedListener() {}

    public static AdsRewardedListener getInstance() {
        if (sInstance == null) {
            synchronized (AdsRewardedListener.class) {
                if (sInstance == null) {
                    sInstance = new AdsRewardedListener();
                }
            }
        }
        return sInstance;
    }

    @Override
    public void onRewardedVideoLoadSuccess(@NonNull final String adUnitId) {
        CMSLog.i("jin-->Load success "+adUnitId);
        String info = "";
        try {
//            JSONObject jsonObject = new JSONObject();
//            jsonObject.put("placementName",adUnitId);
//            info = jsonObject.toString();
//            UnityTool.OnMopubVideoLoadSuccess(info);
            AdBackEvent event = new AdBackEvent(adUnitId);
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_REWARD_LOAD_SUCCESS,event);
        }catch (Exception e) {

        }
    }

    @Override
    public void onRewardedVideoLoadFailure(@NonNull final String adUnitId, @NonNull final MoPubErrorCode errorCode) {

        CMSLog.i("jin-->Load failure "+adUnitId);
        String info = "";
        try {
//            JSONObject jsonObject = new JSONObject();
//            jsonObject.put("placementName",adUnitId);
//            jsonObject.put("errorinfo",errorCode.toString());
//            info = jsonObject.toString();
//            UnityTool.OnMopubVideoLoadFailure(info);
            AdBackEvent event = new AdBackEvent(adUnitId,errorCode.toString());
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_REWARD_LOAD_FAIL,event);
        }catch (Exception e) {

        }
    }

    @Override
    public void onRewardedVideoStarted(@NonNull final String adUnitId) {
        CMSLog.i("jin-->video started  "+adUnitId);
        isComplete=false;
        mRewardId = adUnitId;
        String info = "";
        try {
//            JSONObject jsonObject = new JSONObject();
//            jsonObject.put("placementName",adUnitId);
//            info = jsonObject.toString();
//            UnityTool.OnMopubVideoOpened(info);
            AdBackEvent event = new AdBackEvent(adUnitId);
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_REWARD_PLAY,event);
        }catch (Exception e) {

        }
    }

    @Override
    public void onRewardedVideoClicked(@NonNull final String adUnitId) {
        CMSLog.i("jin-->video clicked  "+adUnitId);
        String info = "";
        try {
//            JSONObject jsonObject = new JSONObject();
//            jsonObject.put("placementName",adUnitId);
//            info = jsonObject.toString();
//            UnityTool.OnMopubVideoClicked(info);
            AdBackEvent event = new AdBackEvent(adUnitId);
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_REWARD_CLICK,event);
        }catch (Exception e) {

        }
    }

    @Override
    public void onRewardedVideoPlaybackError(@NonNull final String adUnitId, @NonNull final MoPubErrorCode errorCode)
    {

        CMSLog.i("jin-->playback error  "+adUnitId);
        String info = "";
        try {
//            JSONObject jsonObject = new JSONObject();
//            jsonObject.put("placementName",adUnitId);
//            jsonObject.put("errorinfo",errorCode.toString());
//            info = jsonObject.toString();
//            UnityTool.OnMopubVideoShowFailed(info);

            AdBackEvent event = new AdBackEvent(adUnitId,errorCode.toString());
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_REWARD_PLAY_FAIL,event);
        }catch (Exception e) {

        }
    }

    @Override
    public void onRewardedVideoClosed(@NonNull final String adUnitId) {
        CMSLog.i("jin-->video close  "+adUnitId);
        CMSLog.i("jin-->video close  isComplete:"+isComplete);
        //if (isComplete){
        String info = "";
        try {
//            JSONObject jsonObject = new JSONObject();
//
//            jsonObject.put("rewardType","");
//            if (isComplete){
//                jsonObject.put("rewardNum",1);
//            }else {
//                jsonObject.put("rewardNum",0);
//            }
//            jsonObject.put("placementName",adUnitId);
//            info = jsonObject.toString();
//            UnityTool.OnMopubVideoClosed(info);
            if(isComplete) {
                AdBackEvent event = new AdBackEvent(adUnitId);
                CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_REWARD_PLAY_SUCCESS, event);
            }else
            {
                AdBackEvent event = new AdBackEvent(adUnitId,"Not Complete");
                CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_REWARD_PLAY_FAIL, event);
            }
        }catch (Exception e) {

        }
        isComplete=false;
        //}

        AdsHelper.loadRewardVideo(adUnitId);
    }

    @Override
    public void onRewardedVideoCompleted(@NonNull final Set<String> adUnitIds, @NonNull final MoPubReward reward) {
        CMSLog.i("jin-->video Complete  "+mRewardId);

        isComplete = true;
//        String info = "";
//        try {
//            JSONObject jsonObject = new JSONObject();
//
//            jsonObject.put("rewardType","");
//            jsonObject.put("rewardNum",reward.getAmount());
//            jsonObject.put("placementName",mRewardId);
//            info = jsonObject.toString();
//            UnityTool.OnMopubVideoCompleted(info);

//        }catch (Exception e) {

//        }
    }
}
