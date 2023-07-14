package com.crazymaplestudio.sdk.inputview;

import android.app.Activity;
import android.content.Context;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;

import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.KeyboardHeightEvent;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.crazymaplestudio.sdk.tools.NotchTool;

/**
 * description
 * Created by jin
 * Created date: 2021-05-28
 */
public class InputHelper {

    private static Context _context = null;

    private static InputView mGEditBox = null;
    private static int mKeyBoradHeight = 0;  //弹出的键盘高度
    private static int mInputAreaHeight = 0; //平台端文本域的高度

    public static void init(Context context) {
        _context = context;

        SoftKeyBoardListener.setListener((Activity) context, new SoftKeyBoardListener.OnSoftKeyBoardChangeListener() {
            @Override
            public void keyBoardShow(final int height) {
                Log.d("1", "SHOW键盘显示 高度" + height);
                mKeyBoradHeight = height;
                if (mGEditBox != null) {
                    mGEditBox.onKeyBoradShow(height);
                }
                //UnityTool.OnKeyBoardShow( height + "");
                CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_INPUT_KEYBOARD_SHOW,new KeyboardHeightEvent(height));
            }

            @Override
            public void keyBoardHide(final int height) {
                Log.d("1", "Hide键盘显示 高度" + height);
                mKeyBoradHeight = 0;
                if (mGEditBox != null) {
                    mGEditBox.onKeyBoradHide(height);
                }
                //UnityTool.OnKeyBoardHide( height + "");
                CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_INPUT_KEYBOARD_HIDE,new KeyboardHeightEvent(height));
            }
        });
    }

    //===============================输入框开始=====

    /**
     * @param minLen 输入最小值
     * @param limitLen  限制输入的最大值
     * @param content   输入的文本
     * @param hint  输入框提示内容
     * @param tag  此值没用
     * @return
     */
    public static boolean showInputArea(final int minLen, final int limitLen, final String content, final String hint, final String tag) {
        //runOnUiThread：把需要更新的UI需要返回到主线程，因为只有主线程才能更新UI
        //new Runnable  一个新的任务
        ((Activity) _context).runOnUiThread(new Runnable() {
            @Override
            public void run() {
                try {
                    ((Activity) _context).getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
                    if (mGEditBox != null) {
                        //show 显示
                        mGEditBox.show();
                        mGEditBox.onAttch();
                    } else {
                        mGEditBox = new InputView(
                                ((Context) _context),
                                "title ",
                                content,
                                minLen,
                                limitLen,
                                hint,
                                tag,
                                0, //pInputMode
                                2, //pInputFlag
                                0, //pReturnType
                                100 //pMaxLength
                        );
                        mGEditBox.show();
                    }

                } catch (Exception e) {
                    //MarsLog.i(e.toString());
                }
            }
        });

        return true;
    }


    //关闭输入框
    public static boolean hideInputArea() {
        if (mGEditBox != null) {
            mGEditBox.closeKeyboard();
            //dismiss关闭
            mGEditBox.dismiss();
            mGEditBox = null;
        }
        return true;
    }

    //隐藏输入框
    public static boolean invisibleInputArea() {
        if (mGEditBox != null) {
            //hide隐藏
            mGEditBox.hide();
        }
        return true;
    }

    //获取键盘高度
    public static int getKeyBoardHeight() {
        return mKeyBoradHeight;
    }

    //获取输入框高度
    public static int getInputAreaHeight(int tag) {
        try {
            //无法准确获取键盘高度   固定输入框高度
            mInputAreaHeight = dip2px(416);
            if (NotchTool.isNavigationBarExist()) {
                CMSLog.i("SHOW導航欄高度" + NotchTool.getNavigationBarHeight());
                mInputAreaHeight = dip2px(416) + NotchTool.getNavigationBarHeight();
            }
        } catch (Exception e) {
            //MarsLog.i(e.toString());
        }
        CMSLog.i( "SHOW文本框高度" + mInputAreaHeight);
        return mInputAreaHeight;
    }

    //获取输入框内容
    public static String getInputAreaConent(int tag) {
        String content = "";
        try {
            content = mGEditBox.getInputContent();
        } catch (Exception e) {
            //MarsLog.i(e.toString());
        }
        return content;
    }


    //主动隐藏系统UI
    public static String hideSysUi() {
        //MarsLog.i("start hide sysUi");
        try {
            ((Activity) _context).runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    ((Activity) _context).getWindow().getDecorView().setSystemUiVisibility(
//                                    View.SYSTEM_UI_FLAG_LAYOUT_STABLE |
                            View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
                                    | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
                                    | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION // hide nav bar
                                    | View.SYSTEM_UI_FLAG_FULLSCREEN // hide status bar
                                    | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY);
                }
            });
        } catch (Exception e) {
            //MarsLog.e(e.toString());
        }
        return "";
    }

    /**
     * 把dip(设备独立像素)转为pix像素值的方法
     */
    public static int dip2px(float dpValue) {
        final float scale = _context.getResources().getDisplayMetrics().density;
        return (int) (dpValue * scale + 0.5f);
    }

}
