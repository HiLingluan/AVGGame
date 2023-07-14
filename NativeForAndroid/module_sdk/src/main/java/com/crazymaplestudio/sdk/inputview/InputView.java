package com.crazymaplestudio.sdk.inputview;

import android.app.Activity;
import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.crazymaplestudio.sdk.R;
import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.InputChangeEvent;

/**
 * 评论输入框
 * create by green 2018/04/17
 */
public class InputView extends Dialog {

    private EditText mInputEditText;
    private Button mSendBtn;
    private TextView mTxtNum;
    private LinearLayout mInputlayout;
    private Activity mAc = null;
    private int mLimitLen = 500;
    private int mMinLen = 1;
    private String mPMessage = "";
    private String mHint = "";
    private String mTag = "";

    private int mInputAreaHeight = 0;


//    private enum POS_STATE  {
//        UNKNOW,
//        UP_POS,
//        DOWN_POS,
//        HIDE_POS;
//    };
//    private POS_STATE mEtPosState =POS_STATE.UNKNOW;


    public InputView(final Context pContext, final String pTitle, final String pMessage, final int limitLen, final int pInputMode, final int pInputFlag, final int pReturnType, final int pMaxLength) {
        super(pContext, android.R.style.Theme_Translucent_NoTitleBar_Fullscreen);
        mAc = (Activity) pContext;
        mLimitLen = limitLen;
        mPMessage = pMessage;
    }

    public InputView(final Context pContext, final String pTitle, final String pMessage, final int limitLen, final String hint, final String tag, final int pInputMode, final int pInputFlag, final int pReturnType, final int pMaxLength) {
        super(pContext, android.R.style.Theme_Translucent_NoTitleBar_Fullscreen);
        mAc = (Activity) pContext;
        mLimitLen = limitLen;
        mTag = tag;
        mHint = hint;
        mPMessage = pMessage;
    }

    public InputView(final Context pContext, final String pTitle, final String pMessage, final int minLen, final int limitLen, final String hint, final String tag, final int pInputMode, final int pInputFlag, final int pReturnType, final int pMaxLength) {
        super(pContext, android.R.style.Theme_Translucent_NoTitleBar_Fullscreen);
        mAc = (Activity) pContext;
        mLimitLen = limitLen;
        mMinLen = minLen;
        mTag = tag;
        mHint = hint;
        mPMessage = pMessage;
    }


    @Override
    protected void onCreate(final Bundle pSavedInstanceState) {
        super.onCreate(pSavedInstanceState);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        setContentView(R.layout.input);

        mInputlayout = (LinearLayout) findViewById(R.id.reply_layout);
        mInputEditText = (EditText) findViewById(R.id.edit_text);
        mSendBtn = (Button) findViewById(R.id.btn_send);
        mTxtNum = (TextView)findViewById(R.id.tv_txt_num);

        ((InputMethodManager) mAc.getSystemService(Context.INPUT_METHOD_SERVICE)).hideSoftInputFromWindow(mInputEditText.getWindowToken(), 0);

        //send button unenable

        mSendBtn.setText("Send");
        mSendBtn.setTextSize(15);
        mSendBtn.setTextColor(mAc.getResources().getColor(R.color.white));

        //initial txt count
        mTxtNum.setText(mPMessage.length()+"/"+mLimitLen);

        WindowManager wm = (WindowManager) mAc.getSystemService(Context.WINDOW_SERVICE);
        DisplayMetrics dm = new DisplayMetrics();
        wm.getDefaultDisplay().getMetrics(dm);


        //设置editbox
        mInputEditText.setMaxEms(mLimitLen);//设置输入框字数限制
        mInputEditText.setText(mPMessage);
        Log.i("Content", "hao======再次打开内容====>> " + mPMessage);
        //再次打开有内容改变send按钮
        if (mPMessage == "" || mPMessage.length() < 1) {
            mSendBtn.setBackgroundResource(R.drawable.bg_set_01);
            mSendBtn.setEnabled(false);
        } else {
            mSendBtn.setBackgroundResource(R.drawable.bg_set_02);
            mSendBtn.setEnabled(true);
        }
        mInputEditText.setHint(mHint);//设置Ediitext的提示文字
        mInputEditText.setHighlightColor(mAc.getResources().getColor(R.color.gray));//设置Ediitext的提示文字颜色
        mInputEditText.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                return false;
            }
        });
        mInputEditText.addTextChangedListener(new TextWatcher() {
            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                try {
                    //UnityTool.onGetChangeContent( s.toString());
                    InputChangeEvent event = new InputChangeEvent(s.toString());
                    CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_INPUT_GET_CHANGE,event);
                    String content = mInputEditText.getText().toString();
                    Log.i("Content", "hao===输入内容=======>> " + s.toString());
                    //当前输入的数量小于1或者小于字数限制
                    if (content.length() < mMinLen || content.length() > mLimitLen) {
                        mSendBtn.setEnabled(false);
                        mSendBtn.setBackgroundResource(R.drawable.bg_set_01);
                    } else {
                        mSendBtn.setEnabled(true);
                        mSendBtn.setBackgroundResource(R.drawable.bg_set_02);
                    }
                    mSendBtn.setTextColor(mAc.getResources().getColor(R.color.white));
                    mSendBtn.setText("Send");
                    mSendBtn.setTextSize(15);
                    mTxtNum.setText(content.length() + "/" + mLimitLen);

                } catch (Exception e) {
                }
            }

            @Override
            public void beforeTextChanged(CharSequence s, int start, int count,
                                          int after) {
                //MarsLog.i( "beforeTextChanged");
                Log.i("Content", "hao===输入之前内容=======>> " + s.toString());
            }

            @Override
            public void afterTextChanged(Editable s) {
                Log.i("Content", "hao===输入之后内容=======>> " + s.toString());
                if (s.length() > mLimitLen) {
                    CharSequence temp = s.subSequence(0, mLimitLen);
                    Log.i("Content", "hao===裁剪之后=======>> " + temp.toString());
                    mInputEditText.setText(temp);
                    mInputEditText.setSelection(temp.length());
                }
            }
        });
        mSendBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String content = mInputEditText.getText().toString();
                //if (content.length()>=mMinLen && content.length()<=mLimitLen) {
                //当前输入数量大于1
                if (content.length() >= mMinLen && content.length() <= mLimitLen) {
                    
                    //UnityTool.OnEditInfoBack(mInputEditText.getText().toString());
                    InputChangeEvent event = new InputChangeEvent(mInputEditText.getText().toString());
                    CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_INPUT_EDIT_BACK,event);
                    //发送完成后把输入框变为空
                    mInputEditText.setText("");
                }
            }
        });

        onAttch();

    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        InputView.this.closeKeyboard();
        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                try {
                    InputView.this.hide();
                }catch (Exception e){}
            }
        }, 100);

        return super.onTouchEvent(event);
    }

    // ===========================================================
    // Getter & Setter
    // ===========================================================

    // ===========================================================
    // Methods for/from SuperClass/Interfaces
    // ===========================================================

    // ===========================================================
    // Methods
    // ===========================================================

    private int convertDipsToPixels(final float pDIPs) {
        final float scale = this.getContext().getResources().getDisplayMetrics().density;
        return Math.round(pDIPs * scale);
    }

    /**
     * 延迟打开键盘
     */
    public void onAttch() {

        final Handler initHandler = new Handler();
        ///postDelayed 延迟执行某事
        initHandler.postDelayed(new Runnable() {
            @Override
            public void run() {
                try {
                    mInputEditText.requestFocus();
                    InputView.this.mInputEditText.setSelection(InputView.this.mInputEditText.length());
                    InputView.this.openKeyboard();
                } catch (Exception e) {
                }
            }
        }, 200);
    }

    public void openKeyboard() {
        try {
            final InputMethodManager imm = (InputMethodManager) this.getContext().getSystemService(Context.INPUT_METHOD_SERVICE);
            imm.showSoftInput(this.mInputEditText, 0);
        } catch (Exception e) {

        }
    }

    public void closeKeyboard() {
        try {
            final InputMethodManager imm = (InputMethodManager) this.getContext().getSystemService(Context.INPUT_METHOD_SERVICE);
            imm.hideSoftInputFromWindow(this.mInputEditText.getWindowToken(), 0);
        } catch (Exception e) {

        }
    }

    // 获取控件高度
    public int getLayoutHeight() {
        try {
            mInputAreaHeight = mInputlayout.getMeasuredHeight();
        } catch (Exception e) {
            //MarsLog.i(e.toString());
        }
        return mInputAreaHeight;
    }

    //获取输入框内容
    public String getInputContent() {
        String content = "";
        try {
            content = mInputEditText.getText().toString();
        } catch (Exception e) {
            //MarsLog.i("get input content error===>"+e.toString());
        }
        return content;
    }

    public void onKeyBoradHide(int keyboradH){
        if (mInputlayout != null) {
            LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(mInputlayout.getLayoutParams());
            lp.setMargins(0, 0, 0, -5000);
            mInputlayout.setLayoutParams(lp);
        }
       InputHelper.invisibleInputArea();
    }



    public void onKeyBoradShow(int keyboradH){
        if (mInputlayout != null) {
            LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(mInputlayout.getLayoutParams());
            lp.setMargins(0, 0, 0, dip2px(mAc, 280));
            mInputlayout.setLayoutParams(lp);
        }
    }

    private static int px2dip(Context context, float pxValue) {
        final float scale = context.getResources().getDisplayMetrics().density;
        return (int) (pxValue / scale + 0.5f);
    }


    public static int dip2px(Context context, float dpValue) {
        final float scale = context.getResources().getDisplayMetrics().density;
        return (int) (dpValue * scale + 0.5f);
    }

}
