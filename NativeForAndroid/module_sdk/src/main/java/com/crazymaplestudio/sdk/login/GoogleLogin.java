package com.crazymaplestudio.sdk.login;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;

import androidx.annotation.NonNull;

import com.crazymaplestudio.sdk.CMSConstant;
import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.LoginFailEvent;
import com.crazymaplestudio.sdk.event.eventmodel.LoginSuccessEvent;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.signin.GoogleSignIn;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInClient;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthCredential;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.auth.GoogleAuthProvider;

/**
 * description
 * Created by jin
 * Created date: 2021-05-27
 */
public class GoogleLogin {

    private static Context _context = null;

    private static GoogleSignInClient mGoogleSignInClient;

    public static void init(Context context,String key) {
        _context = context;

        if(key==null || key.isEmpty()) return;

        //google sign initActivity
        GoogleApiAvailability.getInstance().makeGooglePlayServicesAvailable((Activity) context);
        GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
                .requestIdToken(key)
                .requestEmail()
                .build();
        mGoogleSignInClient = GoogleSignIn.getClient((Activity) context, gso);


    }




    public static void doGoogleLogin() {
        doGoogleLoginOut();
        Intent signInIntent = mGoogleSignInClient.getSignInIntent();
        ((Activity)_context).startActivityForResult(signInIntent, CMSConstant.REQUEST_CODE_GOOGLE_SIGN);
    }

    public static void doGoogleLoginOut() {
        FirebaseAuth.getInstance().signOut();
    }

    public static void firebaseAuthWithGoogle(GoogleSignInAccount account) {
        try {
            CMSLog.i("firebaseAuthWithGoogle", "firebaseAuthWithGoogle: " + account.getId());
            AuthCredential credential = GoogleAuthProvider.getCredential(account.getIdToken(), null);

            FirebaseAuth.getInstance().signInWithCredential(credential).addOnCompleteListener((Activity) _context, new OnCompleteListener<AuthResult>() {
                @Override
                public void onComplete(@NonNull Task<AuthResult> task) {
                    if (task.isSuccessful()) {
                        // Sign in success, update UI with the signed-in user's information
                        CMSLog.i("signInWithCredential", "onComplete: Successful");
                        FirebaseUser userInfo = FirebaseAuth.getInstance().getCurrentUser();
                        handleGoogleSignInResult(userInfo);
                        doGoogleLoginOut();
                    }
                }
            });
        } catch (Exception e) {
            CMSLog.i("firebaseAuthWithGoogle excepiton==>: " + e.toString());
        }
    }

    //Google登录信息回传
    public static void handleGoogleSignInResult(FirebaseUser userInfo) {
        LoginSuccessEvent event = new LoginSuccessEvent();
        event.setId(userInfo.getUid());
        event.setName(userInfo.getDisplayName());
        event.setEmail(userInfo.getEmail());
        //firebase上有报空导致的fatal error
        if(userInfo.getPhotoUrl() != null)
        {
            event.setPhoto(userInfo.getPhotoUrl().toString());
        }
        else
        {
            event.setPhoto("");
        }

        event.setLogintype(CMSConstant.LOGIN_TYPE_GOOGLE);

        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_LOGIN_FINISH,event);
        //UnityTool.ThirdLoginFinish(result);

        LoginHelper.endLogining();
    }

    public static void onActivityResult(int requestCode, int resultCode, Intent data){
        if (requestCode!= CMSConstant.REQUEST_CODE_GOOGLE_SIGN)
            return;

        GoogleSignInResult result = Auth.GoogleSignInApi.getSignInResultFromIntent(data);
        if (result != null && result.isSuccess()) {
            GoogleSignInAccount account = result.getSignInAccount();
            firebaseAuthWithGoogle(account);
        } else {

            CMSLog.i("Login Fail>>>>>>>: " + result.getStatus() + result.toString());
//            JSONObject jsonObject = new JSONObject();
//            try {
//                jsonObject.put("error_code", result.getStatus().getStatusCode());
//                jsonObject.put("error_message", result.getStatus().getStatusMessage());
//                jsonObject.put("logintype", "google");
//            } catch (Exception e) {
//                e.printStackTrace();
//            }
//            String jsonStr = jsonObject.toString();

//            Log.i("返回结果", "failed>>>>>>>: " + jsonStr);
//            UnityTool.ThirdLoginFailed(jsonStr);

            LoginFailEvent event = new LoginFailEvent(result.getStatus().getStatusCode(),result.getStatus().getStatusMessage(), CMSConstant.LOGIN_TYPE_GOOGLE);
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_LOGIN_FAILED,event);

            LoginHelper.endLogining();
        }
    }

}
