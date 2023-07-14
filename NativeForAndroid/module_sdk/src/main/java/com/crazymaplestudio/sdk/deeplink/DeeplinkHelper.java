package com.crazymaplestudio.sdk.deeplink;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;

import com.crazymaplestudio.sdk.tools.CMSLog;
import com.facebook.applinks.AppLinkData;

/**
 * description
 * Created by jin
 * Created date: 2021-05-28
 */
public class DeeplinkHelper {

    private static Context _context = null;
    public  static String linkurlStr = "";

    public static void init(Context context) {
        _context = context;

        AppLinkData.fetchDeferredAppLinkData(context,
                new AppLinkData.CompletionHandler() {
                    @Override
                    public void onDeferredAppLinkDataFetched(AppLinkData appLinkData) {
                        // Process app link data
                        if (appLinkData!=null)
                        {

                            Uri deferredDeeplink=appLinkData.getTargetUri();
                            //缓存facebook的延迟深度链接
                            CMSLog.i("jin--->facebook的延迟深度链接:"+deferredDeeplink.toString());
                            CacheDepplink(deferredDeeplink.toString());
                        }else
                        {

                        }
                    }
                }
        );

        //推送link
        checkLink(((Activity)context).getIntent());
    }


    public static void checkLink(Intent intent){
        if(intent!=null)
        {
            String text = intent.getData() != null ? intent.getData().toString() : "<NULL>";
            String extrasText = "<NULL>";
            if (intent.getExtras() != null) {
                StringBuilder sb = new StringBuilder();
                Bundle bundle = intent.getExtras();
                for (String key : bundle.keySet()) {
                    sb.append("\t" + key + ": " + bundle.get(key) + "\n");
                }
                extrasText = sb.toString();
            }
            Log.i("jin", "deeplink Data: " + text);
            Log.i("jin", "deeplink Extras: " + extrasText);

            if(intent.getData() != null) {
                CacheDepplink(intent.getData().toString());
            } else if(intent.getExtras() != null) {
                // 推送的跳转地址
                String skip_address = intent.getExtras().getString("redirect_link");
                if (!TextUtils.isEmpty(skip_address)) {
                    try {
                        Uri link = Uri.parse(skip_address);
                        CacheDepplink(link.toString());
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        }
    }

    //缓存最近生成的deeplink
    public static void CacheDepplink(String targetUrl)
    {
        if (targetUrl != null) {
            CMSLog.i("jin--->deeplink:"+targetUrl);

            if ( !targetUrl.isEmpty() && linkurlStr.isEmpty())
            {
                linkurlStr = targetUrl;
            }
        }
    }

    //c# 获取深度链接的方法
    public String GetDeepLink()
    {
        return  linkurlStr;
    }
}
