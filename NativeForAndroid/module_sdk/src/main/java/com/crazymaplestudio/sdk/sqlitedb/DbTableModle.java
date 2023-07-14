package com.crazymaplestudio.sdk.sqlitedb;

/**
 * description
 * Created by jin
 * Created date: 2021-07-28
 */
public class DbTableModle {

    private int _id;

    private String data;//上报内容json

    private int creat_at;//入库时间

    private int isup;//是否还需要

    public DbTableModle(int id, String data, int cretetime, int is_up){
        this._id = id;
        this.data = data;
        this.creat_at = cretetime;
        this.isup = is_up;
    }

    public int getId() {
        return _id;
    }

    public String getData() {
        return data;
    }

    public int getIsup() {
        return isup;
    }

    public int getCreatAt() {
        return creat_at;
    }
}
