package com.crazymaplestudio.sdk.login;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.crazymaplestudio.sdk.CMSConstant;
import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.LoginFailEvent;
import com.crazymaplestudio.sdk.event.eventmodel.LoginSuccessEvent;
import com.facebook.AccessToken;
import com.facebook.CallbackManager;
import com.facebook.FacebookCallback;
import com.facebook.FacebookException;
import com.facebook.GraphRequest;
import com.facebook.GraphResponse;
import com.facebook.login.LoginManager;
import com.facebook.login.LoginResult;

import org.json.JSONObject;

import java.util.Arrays;

/**
 * description
 * Created by jin
 * Created date: 2021-05-27
 */
public class FacebookLogin {
    private static Context _context = null;

    private static CallbackManager callbackManager;
    public static void init(Context context) {
        _context = context;

        callbackManager = CallbackManager.Factory.create();
    }

    //Facebook登录
    public static void doFacebookLogin() {
        doFacebookLoginOut();

        LoginManager.getInstance().logInWithReadPermissions((Activity) _context,
                Arrays.asList(
                        "public_profile",
                        "email"));
        LoginManager.getInstance().registerCallback(callbackManager,
                new FacebookCallback<LoginResult>() {
                    @Override
                    public void onSuccess(LoginResult loginResult) {
                        // App code
                        AccessToken token = loginResult.getAccessToken();
                        String userId = token.getUserId();
                        Log.i("Facebook登錄的token：", "onSuccess: " + token);
                        Log.i("Facebook登錄的userId：", "onSuccess: " + userId);
                        getFacebookInfo(token);
                        doFacebookLoginOut();
                        Log.d("结果", "facebook onSuccess: ");
                    }

                    @Override
                    public void onCancel() {
                        Log.d("结果", "facebook onCancel: ");
//                        JSONObject jsonObject = new JSONObject();
//                        try{
//                            jsonObject.put("error_code","");
//                            jsonObject.put("error_message","cancel");
//                            jsonObject.put("logintype", "facebook");
//                        }catch (Exception e){
//                            e.printStackTrace();
//                        }
//                        String jsonStr = jsonObject.toString();
//                        UnityTool.ThirdLoginFailed(jsonStr);
                        LoginFailEvent event = new LoginFailEvent(0,"cancel", CMSConstant.LOGIN_TYPE_FACEBOOK);
                        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_LOGIN_FAILED,event);
                        LoginHelper.endLogining();
                    }

                    @Override
                    public void onError(FacebookException error) {
                        Log.d("结果", "facebook onError: " + error.toString());
//                        JSONObject jsonObject = new JSONObject();
////                        try{
////                            jsonObject.put("error_code","");
////                            jsonObject.put("error_message",error.toString());
////                            jsonObject.put("logintype", "facebook");
////                        }catch (Exception e){
////                            e.printStackTrace();
////                        }
////                        String jsonStr = jsonObject.toString();
////                        UnityTool.ThirdLoginFailed(jsonStr);
                        LoginFailEvent event = new LoginFailEvent(-1,error.toString(), CMSConstant.LOGIN_TYPE_FACEBOOK);
                        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_LOGIN_FAILED,event);

                        LoginHelper.endLogining();
                    }
                });
    }

    public static void doFacebookLoginOut() {
        LoginManager.getInstance().logOut();
    }

    //获取Facebook信息
    public static void getFacebookInfo(AccessToken accessToken) {
        String userId = accessToken.getUserId();
        GraphRequest request = GraphRequest.newMeRequest(accessToken, new GraphRequest.GraphJSONObjectCallback() {
            @Override
            public void onCompleted(JSONObject object, GraphResponse response) {
                if (object != null) {
                    String id = object.optString("id"); //1565455221565
                    String cover = object.optString("cover");  //比如：Zhang San
                    String link = object.optString("link");  //比如：Zhang San
                    String name = object.optString("name");  //比如：Zhang San
                    String gender = object.optString("gender");  //性别：比如 male （男）  female （女）
                    String email = object.optString("email");  //邮箱：比如：56236545@qq.com
                    String locale = object.optString("locale");   //zh_CN 代表中文简体

                    //获取用户头像
                    String photo = "";
                    JSONObject object_pic = object.optJSONObject("picture");
                    try {
                        JSONObject object_data = object_pic.optJSONObject("data");
                        photo = object_data.optString("url");
                    } catch (Exception e) {

                    }


                    LoginSuccessEvent event = new LoginSuccessEvent();
                    event.setId(id);
                    event.setCover(cover);
                    event.setLink(link);
                    event.setName(name);
                    event.setGender(gender);
                    event.setEmail(email);
                    event.setPhoto(photo);
                    event.setLocale(locale);
                    event.setLogintype(CMSConstant.LOGIN_TYPE_FACEBOOK);

                    CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_LOGIN_FINISH,event);

                    Log.e("Facebook", "Facebook onCompleted: " + event.toString());

                    LoginHelper.endLogining();
                }
            }
        });
        //请求信息
        Bundle parameters = new Bundle();
        //去除非必须权限
        parameters.putString("fields", "id,name,email,picture,first_name,last_name");
        request.setParameters(parameters);
        request.executeAsync();
    }


    public static void onActivityResult(int requestCode, int resultCode, Intent data){
        callbackManager.onActivityResult(requestCode, resultCode, data);
    }
}
