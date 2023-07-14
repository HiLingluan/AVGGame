package com.maplehouse.gp.magic;

import androidx.multidex.MultiDexApplication;

import com.crazymaplestudio.sdk.CMSConfig;
import com.crazymaplestudio.sdk.CMSHelper;

/**
 * description
 * Created by jin
 * Created date: 2021-06-05
 */
public class JApplication extends MultiDexApplication {
    @Override
    public void onCreate() {
        super.onCreate();

        //初始sdk配置
        CMSConfig config = new CMSConfig()
                .setIsLogOn(true)
                .setUseAdjustStat(false)
                .setAppsflyerKey(JConstant.APPSFLYER_DEV_KEY)
                .setCmsStatUrl(JConstant.CMS_STAT_URL)
                ;


        CMSHelper.initApplication(this,config);
    }
}
