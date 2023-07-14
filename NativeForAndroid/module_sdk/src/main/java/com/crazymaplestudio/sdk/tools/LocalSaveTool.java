package com.crazymaplestudio.sdk.tools;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;


/**
 * description
 * Created by jin
 * Created date: 2021-12-10
 */
public class LocalSaveTool {

    private static Context _context = null;

    public static final String SDK_SHARE_KEY = "cms_sdk_share";

    public static final String INSTRALL_ID_KEY = "instrall_id";

    //主acvity 调用
    public static void init(Context context) {
        _context = context;
    }

    public static void save(String key,String value) {
        try {
            //本地持久化保存
            SharedPreferences sp = _context.getSharedPreferences(SDK_SHARE_KEY, Activity.MODE_PRIVATE);
            SharedPreferences.Editor editor = sp.edit();
            editor.putString(key, value);
            editor.commit();
        } catch (Exception e) {
            e.toString();
        }
    }

    public static String load(String key) {
        try {
            String result = _context.getSharedPreferences(SDK_SHARE_KEY, Context.MODE_PRIVATE).getString(key,"");
            return result;
        } catch (Exception e) {
            e.toString();
        }
        return "";
    }
}
