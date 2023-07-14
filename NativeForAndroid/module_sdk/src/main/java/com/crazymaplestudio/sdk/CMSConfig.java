package com.crazymaplestudio.sdk;

/**
 * SDK初始化配置类
 * Created by jin
 * Created date: 2021-07-08
 */
public class CMSConfig {

    //设置测试log开关
    private Boolean isLogOn = false;

    //adjust token 初始化需要
    private String adjustInitToken;

    //appsflyer 开发key
    private String appsflyerKey;

    public Boolean getUseFacebookStat() {
        return useFacebookStat;
    }

    //是否开启facebook上报 默认关
    private Boolean useFacebookStat = true;
    //是否开启firebase上报
    private Boolean useFirebaseStat = true;
    //是否开启adjust上报 (2022年开始  Adjust 不再使用)
    private Boolean useAdjustStat = false;
    //是否开启appsflyer上报
    private Boolean useAppsflyerStat = true;

    //广告初始化id 接入广告必需
    private String adunitInit;

    //插屏广告id1
    private String interAdunit1;

    //插屏广告id2
    private String interAdunit2;

    //广告初始化id 接入广告必需
    private String googleLoginKey = "1088769040048-p6ij83qv2nfqm3ljns1n0cos2r90g2ho.apps.googleusercontent.com";

    //4.0上报地址
    private String cmsStatUrl;

    //adjust token 初始化需要
    public String getAdjustInitToken() {
        return adjustInitToken;
    }

    //adjust token 初始化需要
    public CMSConfig setAdjustInitToken(String adjustInitToken) {
        this.adjustInitToken = adjustInitToken;

        return this;
    }

    //广告初始化id
    public String getAdunitInit() {
        return adunitInit;
    }

    //广告初始化id
    public CMSConfig setAdunitInit(String adunitInit) {
        this.adunitInit = adunitInit;

        return this;
    }

    //插屏广告id1
    public String getInterAdunit1() {
        return interAdunit1;
    }
    //插屏广告id1
    public CMSConfig setInterAdunit1(String interAdunit1) {
        this.interAdunit1 = interAdunit1;
        return this;
    }

    //插屏广告id2
    public String getInterAdunit2() {
        return interAdunit2;
    }

    //插屏广告id2
    public CMSConfig setInterAdunit2(String interAdunit2) {
        this.interAdunit2 = interAdunit2;

        return this;
    }

    public Boolean getIsLogOn() {
        return isLogOn;
    }

    public CMSConfig setIsLogOn(Boolean isLogOn) {
        this.isLogOn = isLogOn;

        return this;
    }

    public String getAppsflyerKey() {
        return appsflyerKey;
    }

    public CMSConfig setAppsflyerKey(String appsflyerKey) {
        this.appsflyerKey = appsflyerKey;

        return this;
    }


    public CMSConfig setUseFacebookStat(Boolean isUse) {
        this.useFacebookStat = isUse;

        return this;
    }

    public Boolean getUseFirebaseStat() {
        return useFirebaseStat;
    }

    public CMSConfig setUseFirebaseStat(Boolean isUse) {
        this.useFirebaseStat = isUse;

        return this;
    }

    public Boolean getUseAdjustStat() {
        return useAdjustStat;
    }

    public CMSConfig setUseAdjustStat(Boolean isUse) {
        this.useAdjustStat = isUse;

        return this;
    }

    public Boolean getUseAppsflyerStat() {
        return useAppsflyerStat;
    }

    public CMSConfig setUseAppsflyerStat(Boolean isUse) {
        this.useAppsflyerStat = isUse;

        return this;
    }

    public String getGoogleLoginKey() {
        return googleLoginKey;
    }

    public CMSConfig setGoogleLoginKey(String googleLoginKey) {
        this.googleLoginKey = googleLoginKey;

        return this;
    }

    public String getCmsStatUrl() {
        return cmsStatUrl;
    }

    public CMSConfig setCmsStatUrl(String cmsStatUrl) {
        this.cmsStatUrl = cmsStatUrl;
        return this;
    }
}
