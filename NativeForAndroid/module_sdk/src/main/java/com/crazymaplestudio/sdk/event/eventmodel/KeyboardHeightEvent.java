package com.crazymaplestudio.sdk.event.eventmodel;

/**
 * description
 * Created by jin
 * Created date: 2021-06-22
 */
public class KeyboardHeightEvent extends CMSEvent {
    //键盘高度
    private int keyboardHeight;

    public KeyboardHeightEvent(int height){
        keyboardHeight = height;
    }

    public int getKeyboardHeight() {
        return keyboardHeight;
    }

    public String toString() {
        return "{" +
                "\"keyboardHeight\":" + keyboardHeight +
                "}";
    }
}
