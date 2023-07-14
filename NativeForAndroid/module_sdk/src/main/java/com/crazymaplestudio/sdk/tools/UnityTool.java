package com.crazymaplestudio.sdk.tools;
/**
 * unity 工具类
 * Created by jin
 * Created date: 2021-05-29
 */
public class UnityTool {

    //unity 项目需要打开
    private static final Boolean IS_UNITY_MODE = true;

    //unity 目标gameobj 不能销毁
    private static final String UNIY_OBJ_NAME = "SDK";

    /**
     * 给unity 发送消息
     *
     * @param gameobjName the gameobj name
     * @param funName     the fun name
     * @param msg         其他信息 比赛字符串
     */
    public static void sendMessage(String gameobjName, String funName, String msg) {

        if (!IS_UNITY_MODE) return;

//        UnityPlayer.UnitySendMessage(gameobjName, funName, msg);

//        try {
//            Class unityclass = Class.forName("com.unity3d.player.UnityPlayer");
//
//            try {
//                Class[] cArg = new Class[3];
//                cArg[0] = gameobjName.getClass();
//                cArg[1] = funName.getClass();
//                cArg[2] = msg.getClass();
//                Method method = unityclass.getMethod("UnitySendMessage",cArg);
//                try {
//                    method.invoke(gameobjName, funName, msg);
//                } catch (IllegalAccessException e) {
//                    e.printStackTrace();
//                } catch (InvocationTargetException e) {
//                    e.printStackTrace();
//                }
//            } catch (NoSuchMethodException e) {
//                e.printStackTrace();
//            }
//        } catch (ClassNotFoundException e) {
//            e.printStackTrace();
//        }
    }

    /**
     * Send message with obj.
     *
     * @param funName the fun name
     * @param msg     the msg
     */
    public static void sendMessageWithOBJ(String funName, String msg){
        sendMessage(UNIY_OBJ_NAME,funName,msg);

//        if(CMSHelper.getCmsCallback()!=null){
//            CMSHelper.getCmsCallback().onEventBack(funName,msg);
//        }
    }
}

