package com.crazymaplestudio.sdk.okhttp.builder;

import com.crazymaplestudio.sdk.okhttp.OkHttpUtils;
import com.crazymaplestudio.sdk.okhttp.request.OtherRequest;
import com.crazymaplestudio.sdk.okhttp.request.RequestCall;

/**
 * Created by zhy on 16/3/2.
 */
public class HeadBuilder extends GetBuilder
{
    @Override
    public RequestCall build()
    {
        return new OtherRequest(null, null, OkHttpUtils.METHOD.HEAD, url, tag, params, headers,id).build();
    }
}
