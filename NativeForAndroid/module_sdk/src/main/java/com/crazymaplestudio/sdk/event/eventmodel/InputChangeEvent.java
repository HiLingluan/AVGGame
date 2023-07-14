package com.crazymaplestudio.sdk.event.eventmodel;

/**
 * description
 * Created by jin
 * Created date: 2021-06-22
 */
public class InputChangeEvent extends CMSEvent {
    //返回的内容
    private String _content;

    public InputChangeEvent(String msg){
        _content = msg;
    }

    public String getContent() {
        return _content;
    }

    public String toString() {
        String p = "\"\"";
        if(_content !=null && !_content.isEmpty()){
            p = _content;
        }
        return "{" +
                "\"content\":" + p +
                "}";
    }
}
