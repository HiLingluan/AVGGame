package com.crazymaplestudio.sdk.tools;

import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Build;
import android.provider.MediaStore;
//import android.support.annotation.NonNull;
//import android.support.v4.content.FileProvider;
import android.util.Log;
import android.widget.Toast;

import androidx.core.content.FileProvider;

import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.ImageSaveEvent;

import java.io.File;
import java.net.URI;
import java.net.URISyntaxException;
import java.text.SimpleDateFormat;
import java.util.List;

public class ImageTool {

    private static final int REQUEST_CODE_TAKE_PHOTO = 1; //拍照标记
    private static final int REQUEST_CODE_ALBUM = 2;    //相册
    private static final int REQUEST_CODE_CROUP_PHOTO = 3;  //裁剪

    private static Uri uri;
    private static File file;
    private static Activity _activity;
    private static String _type = "1";

    private static int _sw = 1;
    private static int _sh = 1;
    private static int _outw = 100;
    private static int _outh = 100;
    private static boolean _isNeedCrop = false;
    public static String chooseObject = "";

    public static void init(Activity context) {
        _activity = context;
    }

    public static void resetUri() {
        //格式化时间
        SimpleDateFormat df = new SimpleDateFormat("yyyyMMddHHmmss");
        String str = "temp" + df.format(System.currentTimeMillis()).toString() + ".jpg";

        file = new File(FileTool.getCachePath(_activity), str);

        if (Build.VERSION.SDK_INT < Build.VERSION_CODES.N) {
            uri = Uri.fromFile(file);
        } else {
            //通过FileProvider创建一个content类型的Uri(android 7.0需要这样的方法跨应用访问)
            uri = FileProvider.getUriForFile(_activity, _activity.getPackageName() + ".fileprovider", file);
            CMSLog.i("路径 resetUri: " + uri);
        }
    }

    //打开相机
    public static void openCamera(int sw, int sh, int outw, int outh, boolean isNeedCrop) {
        _sw = sw;
        _sh = sh;
        _outw = outw;
        _outh = outh;
        _isNeedCrop = isNeedCrop;
        CMSLog.i("jin --------------->  java openCamera sw:" + _sw);
        CMSLog.i("jin --------------->  java openCamera sh:" + _sh);
        CMSLog.i("jin --------------->  java openCamera outw:" + _outw);
        CMSLog.i("jin --------------->  java openCamera outh:" + _outh);

        resetUri();
        //MediaStore.ACTION_IMAGE_CAPTURE 图像捕捉
        Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        //intent.addFlags附加新的标记
        //Intent.FLAG_GRANT_READ_URI_PERMISSION 临时授权权限
        intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
        //要传递的值附加到Intent对象  参一：键名  参二：键对应的值
        intent.putExtra(MediaStore.Images.Media.ORIENTATION, 0);
        intent.putExtra(MediaStore.EXTRA_OUTPUT, uri);
        //启动active
        _activity.startActivityForResult(intent, REQUEST_CODE_TAKE_PHOTO);
    }

    //打开相机
    public static void openCamera(int sw, int sh, int outw, int outh) {
        _sw = sw;
        _sh = sh;
        _outw = outw;
        _outh = outh;
        CMSLog.i("jin --------------->  java openCamera sw:" + _sw);
        CMSLog.i("jin --------------->  java openCamera sh:" + _sh);
        CMSLog.i("jin --------------->  java openCamera outw:" + _outw);
        CMSLog.i("jin --------------->  java openCamera outh:" + _outh);
        resetUri();
        Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
        intent.putExtra(MediaStore.Images.Media.ORIENTATION, 0);
        intent.putExtra(MediaStore.EXTRA_OUTPUT, uri);
        _activity.startActivityForResult(intent, REQUEST_CODE_TAKE_PHOTO);
    }

    //打开相册，图片裁剪
    public static void openPhoto(int sw, int sh, int outw, int outh, boolean isNeedCrop) {
        _sw = sw;
        _sh = sh;
        _outw = outw;
        _outh = outh;
        _isNeedCrop = isNeedCrop;
        CMSLog.i( "jin --------------->  java openPhoto sw:" + _sw);
        CMSLog.i("jin --------------->  java openPhoto sh:" + _sh);
        CMSLog.i("jin --------------->  java openPhoto outw:" + _outw);
        CMSLog.i("jin --------------->  java openPhoto outh:" + _outh);

        resetUri();
        //Intent.ACTION_PICK 选择数据
        Intent photoPickerIntent = new Intent(Intent.ACTION_PICK);//intent action属性
        //选择图片   例：选择音频 intent.setType(“audio/*”); 选择视频 intent.setType(“video/*”);
        photoPickerIntent.setType("image/*");

        List<ResolveInfo> resolveInfos = _activity.getPackageManager().queryIntentActivities(photoPickerIntent,PackageManager.MATCH_DEFAULT_ONLY);
        if(resolveInfos.size()!=0){
            _activity.startActivityForResult(photoPickerIntent, REQUEST_CODE_ALBUM);
        }
    }

    //打开相册
    public static void openPhoto(int sw, int sh, int outw, int outh) {
        _sw = sw;
        _sh = sh;
        _outw = outw;
        _outh = outh;
        CMSLog.i( "jin --------------->  java openPhoto sw:" + _sw);
        CMSLog.i( "jin --------------->  java openPhoto sh:" + _sh);
        CMSLog.i( "jin --------------->  java openPhoto outw:" + _outw);
        CMSLog.i( "jin --------------->  java openPhoto outh:" + _outh);

        resetUri();
        Intent photoPickerIntent = new Intent(Intent.ACTION_PICK);
        photoPickerIntent.setType("image/*");

        List<ResolveInfo> resolveInfos = _activity.getPackageManager().queryIntentActivities(photoPickerIntent,PackageManager.MATCH_DEFAULT_ONLY);
        if(resolveInfos.size()!=0){
            _activity.startActivityForResult(photoPickerIntent, REQUEST_CODE_ALBUM);
        }
    }

    //图片筛选  界面跳转
    public static void onActivityResult(int requestCode, int resultCode, Intent data) throws URISyntaxException {
        if (resultCode != -1) {
            return;
        }
        //相册
        if (requestCode == REQUEST_CODE_ALBUM && data != null) {
            Uri newUri;
            //Build.VERSION.SDK_INT 判断Android SDK版本号，手机上操作系统版本号
            //Build.VERSION_CODES.N 与其对应的值位24，表示Android 7.0
            if (Build.VERSION.SDK_INT < Build.VERSION_CODES.N) {
                CMSLog.i( "Android SDK版本号 onActivityResult: " + Build.VERSION.SDK_INT);
                //Uri.parse 通过这个url可以访问到一个网络或者本地的资源
                newUri = Uri.parse("file:///" + FileTool.getPath(_activity, data.getData()));
            } else {
                newUri = data.getData(); //得到url为string类型的filepath
                CMSLog.i("newUri的路径onActivityResult: " + newUri);
            }
            if (newUri != null) {
                if (_isNeedCrop) {
                    startPhotoZoom(newUri);
                } else {
                    if (Build.VERSION.SDK_INT < Build.VERSION_CODES.N) {
                        file = new File(new URI(newUri.toString()));
                    } else {
                        String spath =  getImagePath(newUri, null);
						if (spath != null) {
                        	file = new File(spath);
						}else {
			                Toast.makeText(_activity, "没有得到相册图片", Toast.LENGTH_LONG).show();
			            }
                    }
                    //压缩 上传服务器
                    uploadAvatarFromPhoto();
                }
            } else {
                Toast.makeText(_activity, "没有得到相册图片", Toast.LENGTH_LONG).show();
            }
        } else if (requestCode == REQUEST_CODE_TAKE_PHOTO) {
            if (_isNeedCrop) {
                startPhotoZoom(uri);
            } else {
                uploadAvatarFromPhoto();
            }
        } else if (requestCode == REQUEST_CODE_CROUP_PHOTO) {
            uploadAvatarFromPhoto();
        }
    }

    //裁剪拍照裁剪
    public static void startPhotoZoom(Uri uri) {
        Intent intent = new Intent("com.android.camera.action.CROP");
        intent.setDataAndType(uri, "image/*");
        //添加标记
        intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
        intent.putExtra("crop", "true"); //crop=true 有这句才能出来最后的裁剪界面

        CMSLog.i( "jin --------------->startPhotoZoom:" + _type);

        intent.putExtra("aspectX", _sw);// 这两项为裁剪框的比例.
        intent.putExtra("aspectY", _sh);// x:y=1:1
        intent.putExtra("outputX", _outw);//图片输出大小
        intent.putExtra("outputY", _outh);

        if (file == null) {
            SimpleDateFormat df = new SimpleDateFormat("yyyyMMddHHmmss");
            String str = "temp" + df.format(System.currentTimeMillis()).toString() + ".jpg";
            file = new File(FileTool.getCachePath(_activity), str);
        }
        intent.putExtra("output", Uri.fromFile(file));
        intent.putExtra("outputFormat", "JPEG");// 返回格式
        _activity.startActivityForResult(intent, REQUEST_CODE_CROUP_PHOTO);
    }


    //获取图片路径
    private static String getImagePath(Uri uri, String selection) {
        String path = null;
        Cursor cursor = _activity.getContentResolver().query(uri, null, selection, null, null);
        if (cursor != null) {
            if (cursor.moveToFirst()) {
                path = cursor.getString(cursor.getColumnIndex(MediaStore.Images.Media.DATA));
            }
            cursor.close();
        }
        CMSLog.i( "getImagePath onActivityResult: " + path);
        return path;
    }

    //压缩 上传资源服务器 显示回调
    private static void uploadAvatarFromPhoto() {
        if(file==null){
            CMSLog.i("uploadAvatarFromPhoto 图片路径为空" );
            return;
        }
		
		try{
        	//某些机型图片有旋转角度 需修改为0
	        int degree = FileTool.readPictureDegree(file.getPath());
	        Bitmap bitmap = BitmapFactory.decodeFile(file.getPath());
	        if (degree != 0) {
	            bitmap = FileTool.toturn(bitmap, degree);
	        }
	        Bitmap endBit = uploadAvatarFromPhoto(bitmap, _outw, _outh);
	        final File newFile = FileTool.writeFileByBitmap2(endBit);
	        onImageSaved(newFile.getPath());
		}catch (Exception e){
            Log.i("e", "资源错误===>" + e.toString());
        }
    }

    //设置合适的缩放比例
    private static Bitmap uploadAvatarFromPhoto(Bitmap bitmap, int outw, int outh) {
        float realHeight = bitmap.getHeight();
        float realWidth = bitmap.getWidth();
        float scale = 1;
        //本身尺寸对的就不缩放
        if (realHeight < outw && realWidth < outh) {
            return bitmap;
        }
        if (realHeight > outh) {
            scale = outh / realHeight;
        }
        if (realWidth * scale > outw) {
            scale = outw / realWidth;
        }
        Bitmap endBitmap = Bitmap.createScaledBitmap(bitmap, (int) (realWidth * scale), (int) (realHeight * scale), true);
        return endBitmap;
    }

    //当前选择物体名字
    public static void Choose(String str) {
        chooseObject = str;
    }

    //调用Unity
    private static void onImageSaved(final String path) {
        CMSLog.i("onImageSaved: " + path);

        //UnityTool.OnImageBack( path);
        CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_IMAGE_BACK,new ImageSaveEvent(path));
    }
}
