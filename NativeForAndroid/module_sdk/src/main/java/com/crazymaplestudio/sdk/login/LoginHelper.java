package com.crazymaplestudio.sdk.login;

import android.content.Context;
import android.os.Handler;

import com.crazymaplestudio.sdk.CMSConstant;

/**
 * description
 * Created by jin
 * Created date: 2021-07-12
 */
public class LoginHelper {

    private static Context _context = null;

    private static Boolean _isLogining = false;

    public static void init(Context context,String googleKey) {
        _context = context;

        GoogleLogin.init(context,googleKey);
        FacebookLogin.init(context);
        AppleLogin.init(context);
    }

    //登录
    public static void doLogin(String loginType) {
        switch (loginType) {
            case CMSConstant.LOGIN_TYPE_GOOGLE:
                _isLogining = true;
                GoogleLogin.doGoogleLogin();
                break;
            case CMSConstant.LOGIN_TYPE_FACEBOOK:
                _isLogining = true;
                FacebookLogin.doFacebookLogin();
                break;
            case CMSConstant.LOGIN_TYPE_APPLE:
                //登录
                _isLogining = true;
                AppleLogin.doAppleLogin();
                break;
        }

        new Handler().postDelayed(() -> {
            _isLogining = false;
        },2000);
    }

    //注销
    public static void doLoginOut(String loginType) {
        switch (loginType) {
            case CMSConstant.LOGIN_TYPE_GOOGLE:
                GoogleLogin.doGoogleLoginOut();
                break;
            case CMSConstant.LOGIN_TYPE_FACEBOOK:
                FacebookLogin.doFacebookLoginOut();
                break;
            case CMSConstant.LOGIN_TYPE_APPLE:
                AppleLogin.doAppleLoginOut();
                break;
        }
    }

    public static Boolean isLogining() {
        return _isLogining;
    }

    public static void endLogining() {
        _isLogining = false;
    }
}


