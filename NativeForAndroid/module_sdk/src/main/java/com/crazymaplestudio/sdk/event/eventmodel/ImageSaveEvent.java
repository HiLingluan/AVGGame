package com.crazymaplestudio.sdk.event.eventmodel;

/**
 * description
 * Created by jin
 * Created date: 2021-06-22
 */
public class ImageSaveEvent extends CMSEvent {

    //相机和相册，裁剪后图片的保存路径
    private String savepath;

    public ImageSaveEvent(String path){
        savepath = path;
    }

    public String getSavepath() {
        return savepath;
    }

    public String toString() {
        String p = "\"\"";
        if(savepath!=null && !savepath.isEmpty()){
            p = savepath;
        }
        return "{" +
                "\"savepath\":" + p +
                "}";
    }
}
