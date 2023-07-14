package com.crazymaplestudio.sdk.cmsstat;

import android.text.TextUtils;

import com.crazymaplestudio.sdk.firebase.FirebaseHelper;
import com.crazymaplestudio.sdk.sqlitedb.DbConstant;
import com.crazymaplestudio.sdk.sqlitedb.DbManager;
import com.crazymaplestudio.sdk.tools.AppInfoTool;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.crazymaplestudio.sdk.okhttp.OkHttpUtils;
import com.crazymaplestudio.sdk.okhttp.callback.Callback;
import com.crazymaplestudio.sdk.tools.TimeTool;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.Dictionary;
import java.util.HashMap;
import java.util.List;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;
import java.util.zip.DeflaterOutputStream;
import java.util.zip.InflaterOutputStream;

import okhttp3.Call;
import okhttp3.MediaType;
import okhttp3.Response;
import okio.ByteString;


/**
 * The type Stat 35 request.
 */
public class StatRequest
{

    /**
     * The constant STAT_SUCCESS.
     */
    public static final int STAT_SUCCESS = 1;
    /**
     * The constant STAT_FAIL.
     */
    public static final int STAT_FAIL    = -1;

    //private static final String SKEY = "MARS_SECRET_AVG_!@#$%";
    //private static final String CHA20_KEY = "spotlight#stardust#12389882020*#";

    private static final int CONNET_TIMEOUT = 40;

    private static final int TIMEOUT        = 25;

    //最大请求数
    private static final int MAX_NUMS  = 50;



    private static final Lock lock = new ReentrantLock();
    //上报地址
    private static  String requestUrl = "";
    //测试服地址
    //private static  String requestTestUrl = "https://dev-pfdmw-appsflyer.stardustgod.com/dt/api/event/pLog";

    //    //冷却秒数
    private static float COLD_TIME = 1.0f;
    //
    //    //实时请求时间戳
    //    private static  float _realTime = 0;
    //    //延时请求时间戳
    //    private static  float _delayTime = 0;
    //    //当前冷却时间戳
    private static  long _curColdTime = 0;
    //
    //    //实时事件列表
    private static JSONArray _realList = new JSONArray();
    //非实时事件列表
    //private static JSONArray _delayList = new JSONArray();mars1

    //
    private static JSONArray tempSendList = new JSONArray();

    private static HashMap<Integer,Integer> sendedMap = new HashMap<Integer,Integer>();

    private static HashMap<String,Integer> sendedmd5 = new HashMap<String,Integer>();

    //private static readonly int error_code = 9999;
    /**
     * The interface Network callback.
     */
    public interface NetworkCallback{
        /**
         * Execute.
         *
         * @param code the code
         * @param msg  the msg
         */
        void execute(int code, String msg , JSONArray sendedList2);
    }


    /// <summary>
    /// 外部回调
    /// </summary>
    /// <param name="url"></param>
    /// <param name="dic"></param>
    /// <param name="response"></param>
    /// <param name="method"></param>

    /**
     * Http send.
     *
     * @param url      the url
     * @param datalist the send JSONArray
     * @param callback the callback
     */
    public static void HttpSend(String url, JSONArray datalist, NetworkCallback callback)
    {
        JSONArray list = new JSONArray();
        try
        {
            for (int i = 0; i < datalist.length(); i++) {
                JSONObject obj = datalist.getJSONObject(i);
                String jstr = obj.getString(DbConstant.KEY_DATA);
                int statid = obj.getInt(DbConstant.KEY_ID);
                //如果发送成功过.就不发了
                if(sendedMap.get(statid) != null && sendedMap.get(statid) == 2)
                {
//                    CMSLog.e("lideling sended : statid = " + statid + "__" + m5 + "____" + jstr);
                    continue;
                }
                CMSLog.f("ling preSend : id = " + statid + "__content: " + jstr);
                sendedMap.put(statid , 1);


//                if(sendedmd5.get(m5) != null && sendedmd5.get(m5) == 2)
//                {
//                    CMSLog.e("lideling sended : statid = " + statid + "__" + m5 + "____" + jstr);
//                }
//                sendedmd5.put(m5 , 2);
                JSONObject nJsonObj = new JSONObject(jstr);
                list.put(nJsonObj);
            }
        }
        catch (JSONException e) {
            e.printStackTrace();
        }
        finally {

        }
        String sendJson =  list.toString();
        if(sendJson.equals("")){
            return;
        }
//        CMSLog.e("stat sendStr:" + sendJson);
        // CMSLog.i("添加统计 开始上报 :"+sendJson);
        OkHttpUtils
                .postString()
                .url(url)
                .mediaType(MediaType.parse("application/gzip;charset=utf-8"))
                .content(zipBase64(sendJson))
                //.addHeader("platform", "ios")
                .build()
                .execute(new Callback<String>() {
                    @Override
                    public String parseNetworkResponse(Response response, int id) throws Exception {
                        if (response.body() != null) {
                            return response.body().string();
                        }
                        return null;
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                        CMSLog.i("jin--->onError id:"+id);
                        callback.execute(STAT_FAIL, e.getMessage() , datalist);
                    }

                    @Override
                    public void onResponse(String response, int id) {
                        CMSLog.i("jin--->onResponse id:"+id);
                        String r = "";
                        if(response!=null)
                            r = response;

                        CMSLog.i("jin--->onResponse response:"+r);
//                        CMSLog.e("stat sendStr:" + sendJson);
                        callback.execute(STAT_SUCCESS,r , datalist);
                    }
                });
}

    /**
     * 压缩字符串,默认梳utf-8
     *
     * @param text the text
     * @return string
     */
    public static String zipBase64(String text) {
        try (ByteArrayOutputStream out = new ByteArrayOutputStream()) {
            try (DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream(out)) {
                deflaterOutputStream.write(text.getBytes(StandardCharsets.UTF_8));
                deflaterOutputStream.close();
            }

            String base64 = ByteString.of(out.toByteArray()).base64();
            out.close();
            return base64;
        } catch (IOException e) {
            CMSLog.e("jin zipBase64 fail 压缩文本失败");
        }
        return "";
    }

    /**
     * 解压字符串,默认utf-8
     *
     * @param text the text
     * @return string string
     */
    public static String unzipBase64(String text) {
        try (ByteArrayOutputStream os = new ByteArrayOutputStream()) {
            byte[] bytes = ByteString.decodeBase64(text).toByteArray();
            try (OutputStream outputStream = new InflaterOutputStream(os)) {
                outputStream.write(bytes);
                outputStream.close();
            }
            return new String(os.toByteArray(), StandardCharsets.UTF_8);
        } catch (IOException e) {
            CMSLog.e("jin unzipBase64 fail 解压文本失败:"+e.getMessage());
        }
        return "{\"code\": 1,\"error_msg\": \"" + "unzipBase64 fail" + "\",\"error_ver\": 0}";
    }

    /**
     * Md 5 string.
     *
     * @param values the values
     * @return the string
     */
    public static String md5(String values)
    {
        if (TextUtils.isEmpty(values)) {
            return "";
        }
        MessageDigest md5;
        try {
            md5 = MessageDigest.getInstance("MD5");
            byte[] bytes = md5.digest(values.getBytes());
            StringBuilder result = new StringBuilder();
            for (byte b : bytes) {
                String temp = Integer.toHexString(b & 0xff);
                if (temp.length() == 1) {
                    temp = "0" + temp;
                }
                result.append(temp);
            }
            return result.toString();
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }
        return "";
    }

    /**
     * 从本地中
     */
    public static void refreshAllRecords()
    {
        JSONArray list = DbManager.getInstance(null).queryDatas(100);
        if(list!=null){
            CMSLog.i("StatHelper getAllRecords 统计记录:"+list.length());

            _realList = list;
            int count = DbManager.getInstance(null).queryAllCounts();
            if(count > 100)
            {
                FirebaseHelper.reportCrash("StatHelper getAllRecords 所有记录:" + count  , AppInfoTool.getUuid() , true);
            }
        }
    }


    /**
     *  测试
     */
    public static void delAllRecords()
    {
        try {
            for (int i = 0; i < _realList.length(); i++) {
                JSONObject obj = (JSONObject)_realList.get(i);

                DbManager.getInstance(null).deleteData(obj.getString(DbConstant.KEY_DATA),obj.getLong(DbConstant.KEY_CREAT_AT));
            }
            _realList = new JSONArray();
        } catch (JSONException e){

        }
    }



    /**
     * Add request.
     *
     * @param json 传入上报json
     */
    public static void addRequest(String json , int STAT_ID) {
        //String jsonstr = "{\"list\":\"[{\\\"version\\\":\\\"unknow\\\",\\\"parm6\\\":\\\"event_view_group\\\",\\\"uuid\\\":\\\"23556187\\\",\\\"parm1\\\":0,\\\"parm3\\\":0,\\\"netstatus\\\":1,\\\"parm4\\\":0,\\\"run_id\\\":35638844,\\\"install_id\\\":46257309,\\\"parm5\\\":\\\"ab_test_event\\\",\\\"cversion\\\":\\\"1.146.1\\\",\\\"parm10\\\":\\\"HIGH\\\",\\\"channel_id\\\":6,\\\"parm11\\\":\\\"\\\",\\\"platform\\\":3,\\\"parm9\\\":\\\"\\\",\\\"eid\\\":13,\\\"parm2\\\":0,\\\"ctime\\\":1616830304,\\\"parm7\\\":\\\"event_view_testb\\\",\\\"parm8\\\":\\\"\\\",\\\"language\\\":\\\"English\\\"}]\",\"ctime\":1616830304}";

        //long curtimestamp = System.currentTimeMillis()/1000;

        if(requestUrl.isEmpty()) {
            CMSLog.i("cms stat request url is empty!!!");
            return;
        }

        try {
            long timeMills = TimeTool.getNowTimeMills();

            JSONObject obj = new JSONObject();
            obj.put(DbConstant.KEY_ID, STAT_ID);
            obj.put(DbConstant.KEY_DATA, json);
            obj.put(DbConstant.KEY_CREAT_AT, timeMills);
            obj.put(DbConstant.KEY_IS_UP, "0");

            lock.lock();
            _realList.put(obj);

            DbManager.getInstance().insertData(json,timeMills);
        } catch (JSONException e) {

        }
        finally {
            lock.unlock();
        }

        checkList();
    }

    /**
     * Send stat list.
     *
     * @param datalist the datalist
     * @param callback the callback
     */
    public static void sendStatList(JSONArray datalist, NetworkCallback callback){

        long nowtime = System.currentTimeMillis()/1000;

//        try {
//            //JSONObject sendObj = new JSONObject();
//            //sendObj.put("ctime",nowtime);
//            //sendObj.put("list", datalist.toString());
//            sendstr =  datalist.toString();
//        } catch (JSONException e) {
//            //e.printStackTrace();
//            CMSLog.e("jin--->sendObj error: "+e.getMessage());
//        }

        //sendstr = "{\"list\":\"[{\\\"version\\\":\\\"1.0.0\\\",\\\"uuid\\\":\\\"dfcb6e8ac06172df\\\",\\\"parm5\\\":\\\"dfcb6e8ac06172df\\\",\\\"parm3\\\":0,\\\"netstatus\\\":1,\\\"parm4\\\":0,\\\"run_id\\\":75431781,\\\"install_id\\\":46257309,\\\"parm10\\\":\\\"device_get\\\",\\\"cversion\\\":\\\"1.146.1\\\",\\\"parm6\\\":\\\"HUAWEI DUK-AL20\\\",\\\"channel_id\\\":6,\\\"parm11\\\":\\\"\\\",\\\"platform\\\":3,\\\"parm9\\\":\\\"\\\",\\\"eid\\\":13,\\\"parm2\\\":0,\\\"ctime\\\":1617276264,\\\"language\\\":\\\"English\\\"}]\",\"ctime\":1617276264}";

        HttpSend(requestUrl, datalist,  (code, msg , sendedList2) -> {
            CMSLog.e("stat sendStatList code:" + code);
            CMSLog.e("stat sendStatList msg:" + msg);
            if(code==STAT_SUCCESS && msg!=null && !msg.equals("")){
                //String respstr = unzipBase64(msg);
                //CMSLog.i("jin--->unzipBase64: "+respstr);
                try {
                    JSONObject data = new JSONObject(msg);
                    if(!data.isNull("code") && data.getInt("code")==10000){
                        callback.execute(STAT_SUCCESS,"success" , sendedList2);
                        return;
                    }
                } catch (JSONException e) {
                    e.printStackTrace();
                }
                finally {
                    return;
                }
            }
            else
            {
                callback.execute(STAT_FAIL,"fail" , sendedList2);
            }


        });
    }


    /**
     * Check list.
     */
    public static void checkList(){

        long nowtime = System.currentTimeMillis()/1000;
        //改成每秒发一次
        if( (nowtime - _curColdTime) < COLD_TIME ){
            return;
        }
        _curColdTime = nowtime;
        //CMSLog.i("jin--->checkList 111");

        //检测实时上报列表
//        if ( ((nowtime - _realTime) > 10 && _realList.length() > 0) || _realList.length() >= MAX_NUMS  )
//        {
//            _realTime = nowtime;
//            _curColdTime = nowtime;
//
//            int popLength = _realList.length();
//            if(popLength>MAX_NUMS)
//                popLength = MAX_NUMS;
//
//
//
//        }

        CMSLog.i("jin--->checkList length:" + _realList.length() + "__" + _curColdTime);

        if(_realList.length()>0){

            JSONArray list = new JSONArray();
            int rlength = _realList.length();
//            List<String> list = new ArrayList<String>();
//            List<Long> sendedList = new ArrayList<Long>() ;
            try {
                lock.lock();
                for (int i = 0; i < rlength; i++) {
                    JSONObject obj = null;

                    obj = _realList.getJSONObject(i);

//                    tempSendList.put(obj);

//                    String jstr = obj.getString(DbConstant.KEY_DATA);
//                    Long statid = obj.getLong(DbConstant.KEY_ID);
//                    sendedList.add(statid);
//                    JSONObject nJsonObj = new JSONObject(jstr);
                    list.put(obj);
                }
                _realList = new JSONArray();
                //下面这句也有问题
//                for (int j = 0; j < rlength; j++) {
//                    _realList.remove(j);
//                }
            } catch (JSONException e) {
                e.printStackTrace();
            }
            finally {
                lock.unlock();
            }

            if (list.length()==0) return;

            sendStatList(list, (code, msg , sendedList2) -> {
                if(code==STAT_FAIL){
                    CMSLog.i("jin--->checkList send fail!");
                    try {
                        lock.lock();
                        for (int i = 0; i < sendedList2.length(); i++) {//原来是tempSendList
                            JSONObject obj = sendedList2.getJSONObject(i);//原来是tempSendList
                            String jstr = obj.getString(DbConstant.KEY_DATA);
                            int statid = obj.getInt(DbConstant.KEY_ID);
                            CMSLog.f("ling send fail : id = " + statid + "__content: " + jstr);
                            _realList.put(obj);
                        }
                        //如果失败了,延缓发送埋点时间
                        COLD_TIME = 3.0f;
                        //因为现在加了1秒发一次,而且失败需要网络返回,上报一下给Firebase可能看一下状态
                        FirebaseHelper.reportCrash(_realList.toString() , AppInfoTool.getUuid() , true);
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }
                    finally {
                        lock.unlock();
                    }
                }else
                {
                    //如果成功的话还是1秒一次
                    COLD_TIME = 1.0f;
                    try {
                        for (int i = 0; i < sendedList2.length(); i++) {
                            JSONObject obj = sendedList2.getJSONObject(i);
                            String jstr = obj.getString(DbConstant.KEY_DATA);
                            long time = obj.getLong(DbConstant.KEY_CREAT_AT);
                            int statid = obj.getInt(DbConstant.KEY_ID);
                            CMSLog.f("ling send success : id = " + statid + "__content: " + jstr);
                            sendedMap.put(statid , 2);
                            DbManager.getInstance().deleteData(jstr,time);
                        }
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }
                    CMSLog.i("jin--->checkList send success!");
                }
            });
        }
    }


    public static void setRequestUrl(String url) {
        requestUrl = url;
    }
}


