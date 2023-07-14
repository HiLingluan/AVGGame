package com.crazymaplestudio.sdk.sqlitedb;

/**
 * description
 * Created by jin
 * Created date: 2021-07-27
 */
public class DbConstant {

    //数据库版本
    public static final int DB_VERSION = 1;

    //数据库名称
    public static final String DB_NANME = "cms_stat_db";

    //事件表 名称
    public static final String TABLE_NAME = "stat_event_table";

    //数据库字段
    //自增id
    public static final String KEY_ID = "_id";
    //json 数据
    public static final String KEY_DATA = "data";
    //插入时间
    public static final String KEY_CREAT_AT = "created_at";
    //插入时间
    public static final String KEY_IS_UP = "is_up";

}
