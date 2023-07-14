package com.crazymaplestudio.sdk.login;

import android.app.Activity;
import android.content.Context;
import android.net.Uri;

import androidx.annotation.NonNull;

import com.crazymaplestudio.sdk.CMSConstant;
import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.LoginFailEvent;
import com.crazymaplestudio.sdk.event.eventmodel.LoginSuccessEvent;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.auth.OAuthProvider;

/**
 * description
 * Created by jin
 * Created date: 2021-05-27
 */
public class AppleLogin {

    private static Context _context = null;


    public static void init(Context context) {
        _context = context;
    }

    public static void doAppleLogin() {
        OAuthProvider.Builder provider = OAuthProvider.newBuilder("apple.com");

//        List<String> scopes =
//                new ArrayList<String>() {
//                    {
//                        add("email");
//                        add("name");
//                    }
//                };
//        provider.setScopes(scopes);
        FirebaseAuth mAuth = FirebaseAuth.getInstance();

        Task<AuthResult> pending = mAuth.getPendingAuthResult();
        if (pending != null) {
            pending.addOnSuccessListener(new OnSuccessListener<AuthResult>() {
                @Override
                public void onSuccess(AuthResult authResult) {
                    onLoginSucessBack(authResult);
                }
            }).addOnFailureListener(new OnFailureListener() {
                @Override
                public void onFailure(@NonNull Exception e) {
                    onLoginFailBack(e);
                }
            });
        } else {
            mAuth.startActivityForSignInWithProvider((Activity) _context, provider.build())
                    .addOnSuccessListener(
                            new OnSuccessListener<AuthResult>() {
                                @Override
                                public void onSuccess(AuthResult authResult) {
                                    onLoginSucessBack(authResult);
                                }
                            })
                    .addOnFailureListener(
                            new OnFailureListener() {
                                @Override
                                public void onFailure(@NonNull Exception e) {
                                    onLoginFailBack(e);
                                }
                            });
        }
    }

    public static void doAppleLoginOut() {
        FirebaseAuth.getInstance().signOut();
    }

    public static void onLoginSucessBack(AuthResult authResult){

        CMSLog.i("apple login Sucess Back ");
        // Sign-in successful!
        ((Activity)_context).runOnUiThread(()->{
            FirebaseUser user = authResult.getUser();

            if (user != null) {
                LoginSuccessEvent event = new LoginSuccessEvent();
                event.setId(user.getUid());
                event.setName(user.getDisplayName());
                event.setEmail(user.getEmail());
                event.setLogintype(CMSConstant.LOGIN_TYPE_APPLE);

                Uri photo = user.getPhotoUrl();
                if(photo!=null) event.setPhoto(photo.toString());

                CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_LOGIN_FINISH,event);
                CMSLog.i("apple login success :" + event.toString());

                LoginHelper.endLogining();
            }else {
                LoginFailEvent event2 = new LoginFailEvent(-2,"apple login back user info is null", CMSConstant.LOGIN_TYPE_APPLE);
                CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_LOGIN_FAILED,event2);

                LoginHelper.endLogining();
            }
        });
    }

    public static void onLoginFailBack(Exception e){
        CMSLog.i("apple login onFailure " + e.getMessage());

        ((Activity)_context).runOnUiThread(()->{
            LoginFailEvent event = new LoginFailEvent(-1,e.getMessage(), CMSConstant.LOGIN_TYPE_APPLE);
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_LOGIN_FAILED,event);

            LoginHelper.endLogining();
        });
    }
}
