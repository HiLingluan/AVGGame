package com.crazymaplestudio.sdk.ads;

import android.app.Activity;


import com.applovin.sdk.AppLovinSdk;
import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.AdBackEvent;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.facebook.ads.AudienceNetworkAds;

import com.mopub.common.MoPub;
import com.mopub.common.SdkConfiguration;
import com.mopub.common.SdkInitializationListener;
import com.mopub.common.logging.MoPubLog;
import com.mopub.mobileads.MoPubInterstitial;
import com.mopub.mobileads.MoPubRewardedVideos;

public class AdsHelper  {

    public static Activity _context;

    //初始化的 adunit id
    private static  String mInitAdUint;

    //插屏第一个实例
    private static String mInterAdunit1 ="";
    //插屏第二个实例
    private static String mInterAdunit2 ="";

    //第一个插屏实例
    private static MoPubInterstitial mInter1;
    //第二个插屏实例
    private static MoPubInterstitial mInter2;
	//判断是否使用另外的插屏实例
	private static boolean mIsOpenInterCache = false;


    public static boolean isFacebookInit =false;
    public static boolean isMoPubInit =false;
    public static boolean isIniting =false;

    //region  初始化
    //在facebook 初始化完成以后再去初始哈MoPub
    public static void init(Activity context,String rewardAdunit,String interAdunit1,String interAdunit2)
    {
        if(isIniting) return;


        if(rewardAdunit==null||rewardAdunit.isEmpty()){
            CMSLog.i("AdsHelper initActivity rewardAdunit is null 初始化失败没有初始广告id");
            return;
        }
        mInitAdUint = rewardAdunit;

        mInterAdunit1 = interAdunit1;
        mInterAdunit2 = interAdunit2;

        _context = context;

        CMSLog.i("AdsHelper initActivity mInitAdUint:"+mInitAdUint+",mInterAdunit1:"+mInterAdunit1+",mInterAdunit2"+mInterAdunit2);

        //判断是否开启两个插屏
        if(mInterAdunit1!=null&&mInterAdunit2!=null&&!mInterAdunit1.isEmpty()&&!mInterAdunit2.isEmpty()){
            mIsOpenInterCache = true;
        }

        if(isFacebookInit){
            initMopub();
        }else {
            initFacebookAudience();
        }
    }

    public static boolean isInitFinish(){
        return isFacebookInit&&isMoPubInit;
    }

    //用来设置是否使用两个插屏的机制
//    public static void openInterstitialCache(boolean isopen,String adunit){
//        if (_context!=null){
//            mIsOpenInterCache = isopen;
//            //String adunit = "27124fe7da154a73acbda13e0999ae25";
//            if (adunit!=null && !adunit.isEmpty())
//            {
//                mInterAdunit2 = adunit;
//                //instance.mInterCacheAdunit = "d20c56fe0ff24f8f80ada69b0ae4cbc4";
//                DebugTool.i("jin-->set interstitial cache video name adunit:"+adunit);
//            }
//            if (isopen){
//                loadInterstitialCache();
//            }
//        }
//    }

    public static boolean isOpenInter(){
        return mInterAdunit1!=null&&!mInterAdunit1.isEmpty();
    }

    public static boolean isOpenInterCache(){
        return mIsOpenInterCache;
    }


    //初始化facebook 的广告
    public static void initFacebookAudience(){
        AudienceNetworkAds.buildInitSettings(_context).withInitListener(new AudienceNetworkAds.InitListener() {
            @Override
            public void onInitialized(AudienceNetworkAds.InitResult initResult) {
                //mFacebookInitialize=true;
                if (initResult != null && initResult.isSuccess()) {
                    CMSLog.i("jin facebook ad  initialize success ");
                    //facebook 的广告初始化成功
                    isFacebookInit = true;
                    isIniting = false;
                    initMopub();
                } else {
                    CMSLog.i("jin facebook ad  nitialize failed ");
                    isIniting = false;
                    initMopub();
                }
            }
        }).initialize();
    }

    //初始化这个类
    public static void initMopub(){
        if(isMoPubInit) return;

        //初始化AppLovin
        AppLovinSdk.initializeSdk(_context);

        isIniting = true;

        SdkConfiguration sdkConfiguration = new SdkConfiguration.Builder(mInitAdUint)
                .withLogLevel(MoPubLog.LogLevel.NONE)
                .withLegitimateInterestAllowed(false)
                .build();

        MoPub.initializeSdk(_context, sdkConfiguration, new SdkInitializationListener() {
            @Override
            public void onInitializationFinished() {
                isMoPubInit = true;
                isIniting = false;

                loadRewardVideo(mInitAdUint);
                if(isOpenInter()){
                    loadInterstitial();
                }
                if(isOpenInterCache()){
                    loadInterstitialCache();
                }

                //UnityTool.OnMopubInitBack("");
                CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_AD_INIT,new AdBackEvent("initActivity"));
                CMSLog.i("jin MoPub initActivity success ");
            }
        });


        MoPubRewardedVideos.setRewardedVideoListener(AdsRewardedListener.getInstance());
    }
    //endregion


    //region  激励广告
    //判断是否有激励视频
    public static boolean isRewardVideoAvaliable(String adid){
        boolean result = false;
        CMSLog.i("jin isRewardVideoAvaliable 1111 ");
        if (_context!=null) {
            if(!isMoPubInit){
//                _context.runOnUiThread(new Runnable() {
//                    @Override public void run() {
//                        initActivity(_context,mInitAdUint);
//                    }
//                });
                CMSLog.i("jin isRewardVideoAvaliable 2222 ");
                return false;
            }
            try {
                result = MoPubRewardedVideos.hasRewardedVideo(adid);
                CMSLog.i("jin isRewardVideoAvaliable 333 ");
                if(!result) {
                    loadRewardVideo(adid);
                }
            } catch (Exception e) {
                CMSLog.i("jin isRewardVideoAvaliable error:");
            }
        }else{

        }
        CMSLog.i("jin isRewardVideoAvaliable: "+result);
        return result;
    }

    //加载激励视频
    //不用重复加载，每个广告位被用掉，才会去请求下一个广告
    public static void loadRewardVideo(String adid){
        if (_context!=null && isMoPubInit){
            _context.runOnUiThread(new Runnable() {
                @Override public void run() {
                    MoPubRewardedVideos.loadRewardedVideo(adid);
                }
            });
        }
    }

    //播放激励视频
    public static void showRewardVideo(String adid){
        final String tempid = adid;
        if ( _context!=null && isMoPubInit){
            _context.runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    MoPubRewardedVideos.showRewardedVideo(tempid);
                }
            });
        }
    }
    //endregion

    //region  插屏广告
    //加载插屏广告，从unity端加载
    //不用重复加载，每个广告位被用掉，才会去请求下一个广告
    public static void loadInterstitial(){
        if ( _context!=null && isMoPubInit){
            _context.runOnUiThread(new Runnable() {
                @Override public void run() {
                    if (mInter1 ==null){
                        //初始化插屏广告，YOUR_INTERSTITIAL_AD_UNIT_ID_HERE是你在后台创建的插屏广告ID
                        mInter1 = new MoPubInterstitial(_context, mInterAdunit1);
                        //设置监听器
                        mInter1.setInterstitialAdListener(AdsInterstitialListener.getInstance());

                        //请求加载广告
                        mInter1.load();
                    }else {
                        //请求加载广告
                        if (!mInter1.isReady()){
                            mInter1.load();
                        }
                    }
                }
            });
        }
    }

    //判断是否有插屏广告
    public static boolean isInterstitialReady(){
        boolean has=false;
		if(!isMoPubInit){
//            _context.runOnUiThread(new Runnable() {
//                @Override public void run() {
//                    initActivity(_context,mInitAdUint);
//                }
//            });
            CMSLog.i("jin-->isInterstitialReady 000 ");
            return false;
        }
		if (mIsOpenInterCache){
            if (mInter1 !=null){
                has= mInter1.isReady();
                CMSLog.i("jin-->isInterstitialReady 111 "+has);
            }
            //如果拉不到插屏，再去检查另外一个插屏
            if (has){
                CMSLog.i("jin-->isInterstitialReady 222 ");
            }else {
                if (mInter2 !=null){
                    has= mInter2.isReady();
                    CMSLog.i("jin-->isInterstitialReady 333 "+has);
                }
            }
        }else{
            if (mInter1 !=null){
                has= mInter1.isReady();
                CMSLog.i("jin-->isInterstitialReady 444 "+has);
            }
        }
        return has;
    }

    //播放插屏
    //这个参数没有意义，只是为了跟以前sdk广告的接口保持一致
    public static void showInterstitial(){
        if (_context!=null){
            if (isOpenInterCache()){
                _context.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        if (mInter1 !=null && mInter1.isReady()){
                            mInter1.show();
                            CMSLog.i("jin-->show interstitial ");
                        }else if (mInter2 !=null && mInter2.isReady()){
                            mInter2.show();
                            CMSLog.i("jin-->show interstitial cache");
                        }
                    }
                });
            }else {
                _context.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        if (mInter1 !=null && mInter1.isReady()){
                            mInter1.show();
                        }
                    }
                });
            }
        }
    }


    //实例化，加载插屏的缓存
    public static void loadInterstitialCache(){
        if (_context!=null){
            _context.runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    if (mInterAdunit2 !=null && !mInterAdunit2.isEmpty())
                    {
                        if (mInter2 ==null){
                            //初始化第二个插屏的实例
                            mInter2 = new MoPubInterstitial(_context, mInterAdunit2);
                            //设置监听器
                            mInter2.setInterstitialAdListener(AdsInterstitialListener.getInstance());
                            //请求加载广告
                            mInter2.load();
                            CMSLog.i("jin-->initialize and load interstitial cache ");
                        }else {
                            if (!mInter2.isReady()){
                                mInter2.load();
                                CMSLog.i("jin-->ineterstitial is not ready , then load intersitial cache again ");
                            }
                        }
                    }else
                    {
                        CMSLog.i("jin-->adunit name is not set , can't initialize interstitial cache video  ");
                    }
                }
            });
        }
    }

    //检查去加载哪个插屏广告
    public static void checkToLoadInterstitialCache(){
        if (_context!=null){
            _context.runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    if(mInter1 !=null && !mInter1.isReady()){
                        mInter1.load();
                    }else if(mInter2 !=null && !mInter2.isReady()){
                        mInter2.load();
                    }
                }
            });
        }
    }

    //判断两个插屏是否都可以使用广告
    public static boolean isAllInterReady(){
        boolean has1=false;
        boolean has2=false;
        if (isOpenInterCache()){
            if (mInter1 !=null){
                has1= mInter1.isReady();
                CMSLog.i("jin-->check Interstitial state is  "+Boolean.toString(has1));
            }
            if (mInter2 !=null){
                has2= mInter2.isReady();
                CMSLog.i("jin-->check InterstitialCache state is  "+Boolean.toString(has2));
            }
        }
        return has1&&has2;
    }
    //endregion

}
