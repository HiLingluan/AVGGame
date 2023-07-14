package com.crazymaplestudio.sdk.tools;

/**
 * 自定义回调
 */
public interface CMSCallback {

    /**
     * On event back.
     *
     * @param funname 方法名称
     * @param info    其他信息，如json信息
     */
    void onEventBack(String funname, String info);
}
