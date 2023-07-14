package com.crazymaplestudio.sdk.tools;

import android.annotation.SuppressLint;

import java.util.Calendar;
import java.util.Locale;

/**
 * 时间相关工具类
 * Created by jin
 * Created date: 2021-03-30
 */
public class TimeTool {

    /**
     * 获取设备当前时间戳
     *
     * @return long
     */
    public static int getNowTimestamp(){
        return (int) (System.currentTimeMillis()/1000);
    }

    /**
     * 获取当前时间戳毫秒值
     *
     * @return long
     */
    public static long getNowTimeMills(){
        return System.currentTimeMillis();
    }


    /**
     * 获取时区偏移值
     *
     * @return 时区偏移值，单位：分钟
     */
    public static Integer getZoneOffset() {
        try {
            Calendar cal = Calendar.getInstance(Locale.getDefault());
            int zoneOffset = cal.get(java.util.Calendar.ZONE_OFFSET) + cal.get(Calendar.DST_OFFSET);
            //return -zoneOffset / (1000 * 60 * 60);
            return zoneOffset / (1000 * 60);
        } catch (Exception e) {

        }
        return null;
    }

    @SuppressLint("DefaultLocale")
    public static String getOffsetFormat(){
        Integer zoneOffset = getZoneOffset();
        String result = "+00:00";

        if(zoneOffset!=null){
            int hour = Math.abs(zoneOffset)/60;
            int min = Math.abs(zoneOffset) - hour*60;

            if(zoneOffset>0){
                result = String.format("+%2d:%2d",hour,min);
            }else {
                result = String.format("-%2d:%2d",hour,min);
            }
        }

        return result;
    }
}
