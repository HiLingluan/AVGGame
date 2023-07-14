package com.crazymaplestudio.sdk.tools;
import android.util.Log;

public class CMSLog {
	public static final String tag = "cms";

	//是否开启log
	public static Boolean flag = false;

	//设置log开关
	public static void setLogEnable(Boolean flag) {
		CMSLog.flag = flag;
	}

	public static Boolean getLogEnable() {
		return flag;
	}

	public static void i(String log) {
		// TODO Auto-generated method stub
		if (flag){
			Log.i(tag, log);
		}
	}

	public static void i(String stag,String log) {
		// TODO Auto-generated method stub
		if (flag){
			Log.i(stag, log);
		}
	}

	public static void e(String log) {
		// TODO Auto-generated method stub
		if (flag){
			Log.e(tag, log);
		}
	}

	public static void f(String log) {
		// TODO Auto-generated method stub
		if (flag){
			FileTool.writeToExternalStoragePublic("sendLog.txt" , tag + ":" + log);
//			Log.e(tag, log);
		}
	}



}
