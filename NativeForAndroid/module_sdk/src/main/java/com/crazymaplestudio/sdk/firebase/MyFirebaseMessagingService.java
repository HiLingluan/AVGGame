package com.crazymaplestudio.sdk.firebase;
/*
 firebase cloud message service
 */


import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.content.Context;
import android.media.RingtoneManager;
import android.net.Uri;
import android.os.Build;
//import android.support.v4.app.NotificationCompat;

import androidx.core.app.NotificationCompat;

import com.crazymaplestudio.sdk.tools.CMSLog;
import com.google.firebase.messaging.FirebaseMessagingService;
import com.google.firebase.messaging.RemoteMessage;
import com.crazymaplestudio.sdk.statthird.StatThirdHelper;

import java.util.Map;


public class MyFirebaseMessagingService extends FirebaseMessagingService {
    private final static String TAG = "MyFirebaseMessagingService";
    @Override
    public void onCreate() {
        super.onCreate();
        CMSLog.i(TAG,"MyFirebaseMessagingService was created");
    }

    @Override
    public void onMessageReceived(RemoteMessage remoteMessage) {
        super.onMessageReceived(remoteMessage);
        // TODO(developer): Handle FCM messages here.
        // Not getting messages here? See why this may be: https://goo.gl/39bRNJ
        CMSLog.i(TAG,"From: " + remoteMessage.getFrom()); //430606718509
        //sendNotification(remoteMessage);
        // Check if message contains a data payload.
        if (remoteMessage.getData().size() > 0) {
            CMSLog.i("hx","Message data payload: " + remoteMessage.getData());
            if (/* Check if data needs to be processed by long running job */ true) {
                // For long-running tasks (10 seconds or more) use Firebase Job Dispatcher.
            } else {
                // Handle message within 10 seconds
            }

        }
        // Check if message contains a notification payload.
        if (remoteMessage.getNotification() != null) {
            CMSLog.i( TAG,"Message Notification Body: " + remoteMessage.getNotification().getBody()); //You received a new notification.
            CMSLog.i( TAG,"Message Notification Icon: " + remoteMessage.getNotification().getIcon()); //dist/avgcontent/IMG1530951217xJnD4s.png
            CMSLog.i( TAG,"Message Notification Tag: " + remoteMessage.getNotification().getTag());  //
            //  tag  {"push_type":"SYSTEM_NOTICE","app_version":"all","code_version":"all","prop":"","channel":"AVG10003","icon":"dist\/avgcontent\/IMG1530951217xJnD4s.png","created_at":"2018-07-07 08:13:37","push_content":"test content 1","title":"test 1"}
            FirebaseHelper.onFCMCall(remoteMessage.getNotification().getTag());
        }

        // Also if you intend on generating your own notifications as a result of a received FCM
        // message, here is where that should be initiated. See sendNotification method below.
    }

    /**
     * Create and show a simple notification containing the received FCM message.
     * @param remoteMessage FCM RemoteMessage received.
     */
    private void sendNotification(RemoteMessage remoteMessage) {

        try{
            //Intent intent = new Intent(this, ctivity.class);
            //intent.addFlags(Intent.FLAG_ACTIVITY_REORDER_TO_FRONT);

            Map<String, String> payloadData = remoteMessage.getData();
//            if (payloadData != null && payloadData.size() > 0) {
//                for (String key : payloadData.keySet()) {
//                    intent.putExtra(key, payloadData.get(key));
//                }
//            }

            //PendingIntent pendingIntent = PendingIntent.getActivity(this, 0 /* Request code */, intent, PendingIntent.FLAG_ONE_SHOT);

            Uri defaultSoundUri= RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);
            //int icon = R.mipmap.app_icon;
            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this,"spotlight_notify")
                    //.setSmallIcon(icon)
                    .setContentTitle(remoteMessage.getNotification().getTitle())
                    .setContentText(remoteMessage.getNotification().getBody())
                    .setAutoCancel(true)
                    .setSound(defaultSoundUri);
                    //.setContentIntent(pendingIntent);

            NotificationManager notificationManager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);

            // Since android Oreo notification channel is needed.
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
                NotificationChannel channel = new NotificationChannel("spotlight_notify",
                        "spotlight notification channel",
                        NotificationManager.IMPORTANCE_DEFAULT);
                notificationManager.createNotificationChannel(channel);
            }

            Integer push_id = 0;
            if (payloadData != null && payloadData.containsKey("push_id")) {
                push_id = Integer.parseInt(payloadData.get("push_id"));
            }

            notificationManager.notify(push_id /* ID of notification */, notificationBuilder.build());
        } catch (Exception e){
            CMSLog.i("send notification Error");
        }
    }

    @Override
    public void onNewToken(String token) {
        CMSLog.i("FirebaseInstanceId Refreshed token: " + token);
        super.onNewToken(token);
        if(token!=null)
        {
            StatThirdHelper.setNotifyToken(token);
            FirebaseHelper.setFCMToken(token);
            CMSLog.i("Refreshed token: " + token);
        }
    }
}
