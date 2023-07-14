package com.crazymaplestudio.sdk.sqlitedb;

import android.content.Context;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import com.crazymaplestudio.sdk.tools.CMSLog;

/**
 * 管理数据库 创建和升级
 * Created by jin
 * Created date: 2021-07-27
 */
public class DbOpenHelper extends SQLiteOpenHelper {


    private static final String SQL_CREATE_TABLE = String.format("CREATE TABLE %s (_id INTEGER PRIMARY KEY AUTOINCREMENT, %s TEXT NOT NULL, %s INTEGER NOT NULL,%s INTEGER NOT NULL);"
                                                    , DbConstant.TABLE_NAME, DbConstant.KEY_DATA, DbConstant.KEY_CREAT_AT,DbConstant.KEY_IS_UP);


    DbOpenHelper(Context context) {
        super(context, DbConstant.DB_NANME,null, DbConstant.DB_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        CMSLog.i( "DbHelper onCreate 0000");
        //创建表
            try {
            db.execSQL(SQL_CREATE_TABLE);
        } catch (SQLException e) {
            CMSLog.e( "DbHelper onCreate Error:" + e.toString());
        }
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {

    }

//    JSONArray getAllEvents() {
//        final JSONArray arr = new JSONArray();
//        Cursor c = null;
//        try {
//            final SQLiteDatabase db = getReadableDatabase();
//            c = db.rawQuery(String.format("SELECT * FROM %s ORDER BY %s", DbParams.TABLE_EVENTS, DbParams.KEY_CREATED_AT), null);
//            while (c.moveToNext()) {
//                JSONObject jsonObject = new JSONObject();
//                jsonObject.put("created_at", c.getString(c.getColumnIndex("created_at")));
//                jsonObject.put("data", c.getString(c.getColumnIndex("data")));
//                arr.put(jsonObject);
//            }
//        } catch (Exception e) {
//            com.sensorsdata.analytics.android.sdk.SALog.printStackTrace(e);
//        } finally {
//            close();
//            if (c != null) {
//                c.close();
//            }
//        }
//
//        return arr;
//    }
}
