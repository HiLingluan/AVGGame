package com.crazymaplestudio.sdk.tools;

import android.Manifest;
import android.annotation.SuppressLint;
import android.annotation.TargetApi;
import android.app.Activity;
import android.app.ActivityManager;
import android.app.AppOpsManager;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.os.Build;
import android.os.Environment;
import android.os.StatFs;
import android.provider.Settings;
import android.telephony.TelephonyManager;
import android.text.TextUtils;
import android.util.DisplayMetrics;
import android.view.WindowManager;

import com.google.android.gms.ads.identifier.AdvertisingIdClient;
import com.google.android.gms.common.GooglePlayServicesNotAvailableException;
import com.google.android.gms.common.GooglePlayServicesRepairableException;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileFilter;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.util.HashMap;
import java.util.Locale;
import java.util.UUID;


/**
 * 设备 相关工具类
 * Created by jin
 * Created date: 2021-03-30
 */
public class DeviceTool {

    private static Context _context = null;

    /**
     * The default return value of any method in this class when an
     * error occurs or when processing fails (Currently set to -1). Use this to check if
     * the information about the device in question was successfully obtained.
     */
    public static final int DEVICEINFO_UNKNOWN = -1;
    private static final HashMap<String, String> mCarrierMap = new HashMap<String, String>() {
        {
            //中国移动
            put("46000", "中国移动");
            put("46002", "中国移动");
            put("46007", "中国移动");
            put("46008", "中国移动");

            //中国联通
            put("46001", "中国联通");
            put("46006", "中国联通");
            put("46009", "中国联通");

            //中国电信
            put("46003", "中国电信");
            put("46005", "中国电信");
            put("46011", "中国电信");

            //中国卫通
            put("46004", "中国卫通");

            //中国铁通
            put("46020", "中国铁通");

        }
    };


    public static void init(Context context)
    {
        _context = context;
    }

    //获取登录的设备号
    @SuppressLint("MissingPermission")
    public static String getDeviceId() {
        String id = null;
        TelephonyManager telephonyManager = (TelephonyManager) _context.getSystemService(Context.TELEPHONY_SERVICE);
        if (Build.VERSION.SDK_INT <23) {
            if (telephonyManager != null) {
                try {
                    id = telephonyManager.getDeviceId();
                    CMSLog.i("UUID11111111111:" + id);
                } catch (Exception e) {
                }
            }
            if (id == null || "000000000000000".equals(id)) {
                try {
                    id = telephonyManager.getSubscriberId();
                    CMSLog.i("UUID22222222222:" + id);
                } catch (Exception e) {
                }
            }
        }
        if (id == null || "000000000000000".equals(id)) {
            try {
                //android 6.0以上  获取mac地址无效   改为使用secure.android_id(设备第一次启动时生成)
                id = Settings.Secure.getString(_context.getContentResolver(), Settings.Secure.ANDROID_ID);
                CMSLog.i("UUID3333333333:" + id);
            } catch (Exception e) {
            }
        }
        if (Build.VERSION.SDK_INT <23) {
            if ((id == null || "000000000000000".equals(id)) && telephonyManager != null) {
                try {
                    //android 6.0以上  获取mac地址无效   改为使用secure.android_id(设备第一次启动时生成)
                    id = telephonyManager.getSimSerialNumber();
                    CMSLog.i("UUID444444444:" + id);
                } catch (Exception e) {
                }
            }
        }

        // 5. 最后的补救措施，用设备信息拼接生成UUID
        if (id == null || "000000000000000".equals(id)) {
            try {
                String m_szDevIDShort = "35" +
                        Build.BOARD.length() % 10 + Build.BRAND.length() % 10 +
                        Build.CPU_ABI.length() % 10 + Build.DEVICE.length() % 10 +
                        Build.DISPLAY.length() % 10 + Build.HOST.length() % 10 +
                        Build.ID.length() % 10 + Build.MANUFACTURER.length() % 10 +
                        Build.MODEL.length() % 10 + Build.PRODUCT.length() % 10 +
                        Build.TAGS.length() % 10 + Build.TYPE.length() % 10 +
                        Build.USER.length() % 10; //13 位
                String serial = android.os.Build.class.getField("SERIAL").get(null).toString();
                id = new UUID(m_szDevIDShort.hashCode(), serial.hashCode()).toString();
                CMSLog.i("UUID5555555:" + id);
            } catch (Exception exception) {
            }
        }

        if (id == null) {
            id = "";
        }

        CMSLog.i("UUID:" + id);
        return id;
    }


    //系统版本
    public static String getOSVersion() {
        try {
            String mtype = Build.VERSION.RELEASE; // 系统版本
            return mtype != null ? mtype : "UNKNOWN";
        } catch (Exception e) {

        }
        return "UNKNOWN";
    }

    //设备制造商
    public static String getManufacturer() {
        try {
            String manufacturer = Build.MANUFACTURER;
            if (manufacturer != null) {
                return manufacturer.trim().toUpperCase();
            }
        } catch (Exception e) {
        }
        return "UNKNOWN";
    }

    //设备品牌
    public static String getBrand() {
        try {
            String brand = Build.BRAND;
            if (brand != null) {
                return brand.trim().toUpperCase();
            }
        } catch (Exception e) {
        }
        return "UNKNOWN";
    }

    public static String getModel() {
        return TextUtils.isEmpty(Build.MODEL) ? "UNKNOWN" : Build.MODEL.trim();
    }

    /**
     * 获取当前手机系统语言。
     *
     * @return 返回当前系统语言。例如：当前设置的是“中文-中国”，则返回“zh-CN”
     */
    public static String getSystemLanguage() {
        return Locale.getDefault().getLanguage();
    }

    /**
     * 此方法谨慎修改
     * 插件配置 disableCarrier 会修改此方法
     * 获取运营商信息
     *
     * @return 运营商信息
     */
    public static String getCarrier() {
        try {
            if (PermissionRequest.checkPermission( Manifest.permission.READ_PHONE_STATE)) {
                try {
                    TelephonyManager telephonyManager = (TelephonyManager) _context.getSystemService(Context
                            .TELEPHONY_SERVICE);
                    if (telephonyManager != null) {
                        String operator = telephonyManager.getSimOperator();
                        String alternativeName = null;
                        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
                            CharSequence tmpCarrierName = telephonyManager.getSimCarrierIdName();
                            if (!TextUtils.isEmpty(tmpCarrierName)) {
                                alternativeName = tmpCarrierName.toString();
                            }
                        }
                        if (TextUtils.isEmpty(alternativeName)) {
                            if (telephonyManager.getSimState() == TelephonyManager.SIM_STATE_READY) {
                                alternativeName = telephonyManager.getSimOperatorName();
                            } else {
                                alternativeName = "UNKNOW";
                            }
                        }
                        if (!TextUtils.isEmpty(operator)) {
                            return operatorToCarrier(_context, operator, alternativeName);
                        }
                    }
                } catch (Exception e) {
                    CMSLog.e( e.toString());
                }
            }
        } catch (Exception e) {
            CMSLog.e( e.toString());
        } catch (Error error) {
            //针对酷派 B770 机型抛出的 IncompatibleClassChangeError 错误进行捕获
            CMSLog.e( error.toString());
        }
        return null;
    }

    /**
     * 根据 operator，获取本地化运营商信息
     *
     * @param context context
     * @param operator sim operator
     * @param alternativeName 备选名称
     * @return local carrier name
     */
    private static String operatorToCarrier(Context context, String operator, String alternativeName) {
        try {
            if (TextUtils.isEmpty(operator)) {
                return alternativeName;
            }
            if (mCarrierMap.containsKey(operator)) {
                return mCarrierMap.get(operator);
            }
            String carrierJson = getJsonFromAssets("sa_mcc_mnc_mini.json", context);
            if (TextUtils.isEmpty(carrierJson)) {
                mCarrierMap.put(operator, alternativeName);
                return alternativeName;
            }
            JSONObject jsonObject = new JSONObject(carrierJson);
            String carrier = getCarrierFromJsonObject(jsonObject, operator);
            if (!TextUtils.isEmpty(carrier)) {
                mCarrierMap.put(operator, carrier);
                return carrier;
            }
        } catch (Exception e) {
            CMSLog.e("operatorToCarrier "+e.toString());
        }
        return alternativeName;
    }

    private static String getJsonFromAssets(String fileName, Context context) {
        //将json数据变成字符串
        StringBuilder stringBuilder = new StringBuilder();
        BufferedReader bf = null;
        try {
            //获取assets资源管理器
            AssetManager assetManager = context.getAssets();
            //通过管理器打开文件并读取
            bf = new BufferedReader(new InputStreamReader(
                    assetManager.open(fileName)));
            String line;
            while ((line = bf.readLine()) != null) {
                stringBuilder.append(line);
            }
        } catch (IOException e) {
            CMSLog.e("getJsonFromAssets 11 "+e.toString());
        } finally {
            if (bf != null) {
                try {
                    bf.close();
                } catch (IOException e) {
                    CMSLog.e("getJsonFromAssets 22 "+e.toString());
                }
            }
        }
        return stringBuilder.toString();
    }

    private static String getCarrierFromJsonObject(JSONObject jsonObject, String mccMnc) {
        if (jsonObject == null || TextUtils.isEmpty(mccMnc)) {
            return null;
        }
        return jsonObject.optString(mccMnc);

    }

    //获取手机号码
    public static String getNativePhoneNum() {
        try {
            TelephonyManager telManager = (TelephonyManager) _context.getSystemService(Context.TELEPHONY_SERVICE);
            return "unkown";//telManager.getLine1Number()!=null?telManager.getLine1Number():"unknown";
        } catch (Exception e) {
        }
        return "unknown";
    }


    /**
     * Reads the number of CPU cores from the first available information from
     * {@code /sys/devices/system/cpu/possible}, {@code /sys/devices/system/cpu/present},
     * then {@code /sys/devices/system/cpu/}.
     *
     * @return Number of CPU cores in the phone, or DEVICEINFO_UKNOWN = -1 in the event of an error.
     */
    public static int getNumberOfCPUCores() {
        if (Build.VERSION.SDK_INT <= Build.VERSION_CODES.GINGERBREAD_MR1) {
            // Gingerbread doesn't support giving a single application access to both cores, but a
            // handful of devices (Atrix 4G and Droid X2 for example) were released with a dual-core
            // chipset and Gingerbread; that can let an app in the background run without impacting
            // the foreground application. But for our purposes, it makes them single core.
            return 1;
        }
        int cores;
        try {
            cores = getCoresFromFileInfo("/sys/devices/system/cpu/possible");
            if (cores == DEVICEINFO_UNKNOWN) {
                cores = getCoresFromFileInfo("/sys/devices/system/cpu/present");
            }
            if (cores == DEVICEINFO_UNKNOWN) {
                cores = getCoresFromCPUFileList();
            }
        } catch (SecurityException e) {
            cores = DEVICEINFO_UNKNOWN;
        } catch (NullPointerException e) {
            cores = DEVICEINFO_UNKNOWN;
        }
        return cores;
    }

    /**
     * Tries to read file contents from the file location to determine the number of cores on device.
     * @param fileLocation The location of the file with CPU information
     * @return Number of CPU cores in the phone, or DEVICEINFO_UKNOWN = -1 in the event of an error.
     */
    private static int getCoresFromFileInfo(String fileLocation) {
        InputStream is = null;
        try {
            is = new FileInputStream(fileLocation);
            BufferedReader buf = new BufferedReader(new InputStreamReader(is));
            String fileContents = buf.readLine();
            buf.close();
            return getCoresFromFileString(fileContents);
        } catch (IOException e) {
            return DEVICEINFO_UNKNOWN;
        } finally {
            if (is != null) {
                try {
                    is.close();
                } catch (IOException e) {
                    // Do nothing.
                }
            }
        }
    }

    /**
     * Converts from a CPU core information format to number of cores.
     * @param str The CPU core information string, in the format of "0-N"
     * @return The number of cores represented by this string
     */
    static int getCoresFromFileString(String str) {
        if (str == null || !str.matches("0-[\\d]+$")) {
            return DEVICEINFO_UNKNOWN;
        }
        int cores = Integer.valueOf(str.substring(2)) + 1;
        return cores;
    }

    private static int getCoresFromCPUFileList() {
        return new File("/sys/devices/system/cpu/").listFiles(CPU_FILTER).length;
    }

    private static final FileFilter CPU_FILTER = new FileFilter() {
        @Override
        public boolean accept(File pathname) {
            String path = pathname.getName();
            //regex is slow, so checking char by char.
            if (path.startsWith("cpu")) {
                for (int i = 3; i < path.length(); i++) {
                    if (!Character.isDigit(path.charAt(i))) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    };

    /**
     * Method for reading the clock speed of a CPU core on the device. Will read from either
     * {@code /sys/devices/system/cpu/cpu0/cpufreq/cpuinfo_max_freq} or {@code /proc/cpuinfo}.
     *
     * @return Clock speed of a core on the device, or -1 in the event of an error.
     */
    public static int GetCPUMaxFreqMHz() {
        int maxFreq = DEVICEINFO_UNKNOWN;
        try {
            for (int i = 0; i < getNumberOfCPUCores(); i++) {
                String filename =
                        "/sys/devices/system/cpu/cpu" + i + "/cpufreq/cpuinfo_max_freq";
                File cpuInfoMaxFreqFile = new File(filename);
                if (cpuInfoMaxFreqFile.exists() && cpuInfoMaxFreqFile.canRead()) {
                    byte[] buffer = new byte[128];
                    FileInputStream stream = new FileInputStream(cpuInfoMaxFreqFile);
                    try {
                        stream.read(buffer);
                        int endIndex = 0;
                        //Trim the first number out of the byte buffer.
                        while (Character.isDigit(buffer[endIndex]) && endIndex < buffer.length) {
                            endIndex++;
                        }
                        String str = new String(buffer, 0, endIndex);
                        Integer freqBound = Integer.parseInt(str);
                        if (freqBound > maxFreq) {
                            maxFreq = freqBound;
                        }
                    } catch (NumberFormatException e) {
                        //Fall through and use /proc/cpuinfo.
                    } finally {
                        stream.close();
                    }
                }
            }
            if (maxFreq == DEVICEINFO_UNKNOWN) {
                FileInputStream stream = new FileInputStream("/proc/cpuinfo");
                try {
                    int freqBound = parseFileForValue("cpu MHz", stream);
                    freqBound *= 1000; //MHz -> kHz
                    if (freqBound > maxFreq) maxFreq = freqBound;
                } finally {
                    stream.close();
                }
            }
        } catch (IOException e) {
            maxFreq = DEVICEINFO_UNKNOWN; //Fall through and return unknown.
        }
        return maxFreq;
    }

    /**
     * 获取设备内存
     * Calculates the total RAM of the device through Android API or /proc/meminfo.
     * @param c - Context object for current running activity.
     * @return Total RAM that the device has, or DEVICEINFO_UNKNOWN = -1 in the event of an error.
     */
    @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
    public static long getTotalMemory(Context c) {
        // memInfo.totalMem not supported in pre-Jelly Bean APIs.
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.JELLY_BEAN) {
            ActivityManager.MemoryInfo memInfo = new ActivityManager.MemoryInfo();
            ActivityManager am = (ActivityManager) c.getSystemService(Context.ACTIVITY_SERVICE);
            am.getMemoryInfo(memInfo);
            if (memInfo != null) {
                return memInfo.totalMem;
            } else {
                return DEVICEINFO_UNKNOWN;
            }
        } else {
            long totalMem = DEVICEINFO_UNKNOWN;
            try {
                FileInputStream stream = new FileInputStream("/proc/meminfo");
                try {
                    totalMem = parseFileForValue("MemTotal", stream);
                    totalMem *= 1024;
                } finally {
                    stream.close();
                }
            } catch (IOException e) {
            }
            return totalMem;
        }
    }

    /**
     * Helper method for reading values from system files, using a minimised buffer.
     *
     * @param textToMatch - Text in the system files to read for.
     * @param stream      - FileInputStream of the system file being read from.
     * @return A numerical value following textToMatch in specified the system file.
     * -1 in the event of a failure.
     */
    private static int parseFileForValue(String textToMatch, FileInputStream stream) {
        byte[] buffer = new byte[1024];
        try {
            int length = stream.read(buffer);
            for (int i = 0; i < length; i++) {
                if (buffer[i] == '\n' || i == 0) {
                    if (buffer[i] == '\n') i++;
                    for (int j = i; j < length; j++) {
                        int textIndex = j - i;
                        //Text doesn't match query at some point.
                        if (buffer[j] != textToMatch.charAt(textIndex)) {
                            break;
                        }
                        //Text matches query here.
                        if (textIndex == textToMatch.length() - 1) {
                            return extractValue(buffer, j);
                        }
                    }
                }
            }
        } catch (IOException e) {
            //Ignore any exceptions and fall through to return unknown value.
        } catch (NumberFormatException e) {
        }
        return DEVICEINFO_UNKNOWN;
    }

    /**
     * Helper method used by {@link #parseFileForValue(String, FileInputStream) parseFileForValue}. Parses
     * the next available number after the match in the file being read and returns it as an integer.
     * @param index - The index in the buffer array to begin looking.
     * @return The next number on that line in the buffer, returned as an int. Returns
     * DEVICEINFO_UNKNOWN = -1 in the event that no more numbers exist on the same line.
     */
    private static int extractValue(byte[] buffer, int index) {
        while (index < buffer.length && buffer[index] != '\n') {
            if (Character.isDigit(buffer[index])) {
                int start = index;
                index++;
                while (index < buffer.length && Character.isDigit(buffer[index])) {
                    index++;
                }
                String str = new String(buffer, 0, start, index - start);
                return Integer.parseInt(str);
            }
            index++;
        }
        return DEVICEINFO_UNKNOWN;
    }

    //获取可用内存
    public static long getAvailbleMemory() {
        try {
            ActivityManager myActivityManager = (ActivityManager) _context.getSystemService(Activity.ACTIVITY_SERVICE);
            ActivityManager.MemoryInfo memoryInfo = new ActivityManager.MemoryInfo();
            if (myActivityManager != null) {
                myActivityManager.getMemoryInfo(memoryInfo);
                long memSize = memoryInfo.availMem;
                return memSize / 1024 / 1024;
            }

            //MarsLog.i("getAvailMemo"+memSize/1024/1024+"MB");

        } catch (Exception e) {
        }
        return 0;
    }

    //获取androidId
    public static String getAndroidId() {
        String androidId = "";
        try {
            androidId = Settings.Secure.getString(_context.getContentResolver(), Settings.Secure.ANDROID_ID);
            //MarsLog.i("hx","get android id===>"+androidId);
        } catch (Exception e) {
            //MarsLog.i("hx","=====>get android id failed");
        }
        return androidId;
    }

    /**
     * get device adid
     * @return
     */
    public static String getADIDStr(){
        AdvertisingIdClient.Info adInfo = null;
        String id = "";
        try {
            adInfo = AdvertisingIdClient.getAdvertisingIdInfo(_context.getApplicationContext());
            if (adInfo != null){
                id = adInfo.getId();
            }
        } catch (IOException e) {
            // Unrecoverable error connecting to Google Play services (e.g.,
            // the old version of the service doesn't support getting AdvertisingId).

        } catch (GooglePlayServicesNotAvailableException e) {
            // Google Play services is not available entirely.
        } catch (IllegalStateException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        } catch (GooglePlayServicesRepairableException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        } catch (Exception e){
            // catch all exception here!!!!!!
        }
        return id;
    }


    public static int getDeviceWidth(){
        WindowManager mWindowManager  = (WindowManager) _context.getSystemService(Context.WINDOW_SERVICE);
        DisplayMetrics metrics = new DisplayMetrics();
        if (mWindowManager != null) {
            mWindowManager.getDefaultDisplay().getMetrics(metrics);
            return metrics.widthPixels;
        }
        return 1080;
    }

    public static int getDeviceHeight(){
        WindowManager mWindowManager  = (WindowManager) _context.getSystemService(Context.WINDOW_SERVICE);
        DisplayMetrics metrics = new DisplayMetrics();
        if (mWindowManager != null) {
            mWindowManager.getDefaultDisplay().getMetrics(metrics);
            return metrics.heightPixels;
        }
        return 1920;
    }

    //获取设备存储空间
    public static long GetFreeStorageSpace() {
        try {
            File file = Environment.getDataDirectory();
            StatFs sf = new StatFs(file.getPath());
            return sf.getAvailableBytes() / (1024 * 1024);
        } catch (Exception e) {

        }
        return -1;
    }

    /**
     *
     * @param info   预留参数
     * @return  通知开启状态   0 关闭  1开启   2无法判断
     */

    public static int getSysNotificationState(String info){
        final String CHECK_OP_NO_THROW = "checkOpNoThrow";
        final String OP_POST_NOTIFICATION = "OP_POST_NOTIFICATION";
        int stateCode = 2;
        if  (Build.VERSION.SDK_INT <= Build.VERSION_CODES.BASE) {
            stateCode = 2;
            return stateCode;
        }
        AppOpsManager mAppOps = (AppOpsManager) _context.getSystemService(Context.APP_OPS_SERVICE);
        ApplicationInfo appInfo = _context.getApplicationInfo();
        String pkg = _context.getApplicationContext().getPackageName();
        int uid = appInfo.uid;

        Class appOpsClass = null;
        /* Context.APP_OPS_MANAGER */
        try {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
                appOpsClass = Class.forName(AppOpsManager.class.getName());
            }
            Method checkOpNoThrowMethod = appOpsClass.getMethod(CHECK_OP_NO_THROW, Integer.TYPE, Integer.TYPE, String.class);
            Field opPostNotificationValue = appOpsClass.getDeclaredField(OP_POST_NOTIFICATION);
            int value = (Integer) opPostNotificationValue.get(Integer.class);
            if ((Integer) checkOpNoThrowMethod.invoke(mAppOps, value, uid, pkg) == AppOpsManager.MODE_ALLOWED){
                stateCode = 1;
            } else {
                stateCode = 0;
            }

        } catch (Exception e) {
            CMSLog.i("getSysNotificationState error=="+e.toString());
            e.printStackTrace();
        }
        CMSLog.i("getSysNotificationState==>"+stateCode);
        return stateCode;
    }

    /**
     * 执行命令获取对应内容
     *
     * @param command 命令
     * @return 命令返回内容
     */
    public static String exec(String command) {
        InputStreamReader ir = null;
        BufferedReader input = null;
        try {
            Process process = Runtime.getRuntime().exec(command);
            ir = new InputStreamReader(process.getInputStream());
            input = new BufferedReader(ir);
            String line;
            StringBuilder stringBuilder = new StringBuilder();
            while ((line = input.readLine()) != null) {
                stringBuilder.append(line);
            }
            return stringBuilder.toString();
        } catch (Throwable e) {
            CMSLog.i("SA.Exec:"+ e.getMessage());
        } finally {
            if (input != null) {
                try {
                    input.close();
                } catch (Throwable e) {
                    CMSLog.i("SA.Exec:"+ e.getMessage());
                }
            }
            if (ir != null) {
                try {
                    ir.close();
                } catch (IOException e) {
                    CMSLog.i("SA.Exec:"+ e.getMessage());
                }
            }
        }
        return null;
    }
}
