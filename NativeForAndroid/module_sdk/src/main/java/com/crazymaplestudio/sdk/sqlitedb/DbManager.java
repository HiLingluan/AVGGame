package com.crazymaplestudio.sdk.sqlitedb;

/**
 * description
 * Created by jin
 * Created date: 2021-07-27
 */

import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;

import com.crazymaplestudio.sdk.tools.CMSLog;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/**
 * 数据库管理者 - 提供数据库封装
 *
 */
public class DbManager {
    private static final String TAG = "CMS_DB";
    // 静态引用
    private volatile static DbManager mInstance;
    // DatabaseHelper
    private DbOpenHelper mDbOpenHelper;

    //插入数据SQL语句
    //private static final String INSERT_SQL = String.format("INSERT INTO %s(%s,%s) VALUES(?,?)", DbConstant.TABLE_NAME, DbConstant.KEY_DATA,DbConstant.KEY_CREAT_AT);

    private DbManager(Context context) {
        mDbOpenHelper = new DbOpenHelper(context.getApplicationContext());
    }

    /**
     * 获取单例引用
     *
     * @param context
     * @return
     */
    public static DbManager getInstance(Context context) {
        DbManager inst = mInstance;
        if (inst == null) {
            synchronized (DbManager.class) {
                inst = mInstance;
                if (inst == null) {
                    inst = new DbManager(context);
                    mInstance = inst;
                }
            }
        }
        return inst;
    }

    public static DbManager getInstance() {
        return mInstance;
    }

    public void onDestroy()
    {
        SQLiteDatabase db = mDbOpenHelper.getReadableDatabase();
        db.close();
    }
    
    /**
     * 插入数据
     */
    public void insertData(String jsondata,long timeMills) {
        try {
            String INSERT_SQL = String.format("insert into %s(%s,%s,%s) values(?,?,?)", DbConstant.TABLE_NAME, DbConstant.KEY_DATA,DbConstant.KEY_CREAT_AT,DbConstant.KEY_IS_UP  );

            execSQL(INSERT_SQL,new Object[]{jsondata,timeMills,0});

        }catch (SQLException e)
        {
            CMSLog.i("DbManager insertData error:"+e.toString());
        }
    }

    /**
     * 删除数据
     */
    public void deleteData(String jsondata,long timeMills) {
        try {
            String sql = String.format("delete from %s where %s=? and %s=?", DbConstant.TABLE_NAME, DbConstant.KEY_DATA,DbConstant.KEY_CREAT_AT);

            execSQL(sql,new Object[]{jsondata,timeMills});

        }catch (SQLException e)
        {
            CMSLog.i("DbManager deleteData error:"+e.toString());
        }
    }

    /**
     * 删除整个表 所有数据
     */
    public void deleteDatas()
    {
        try {
            String sql="delete from "+ DbConstant.TABLE_NAME;
            execSQL(sql);
        }catch (SQLException e)
        {
            CMSLog.i("DbManager deleteDatas error:" + e.toString());
        }
    }

    /**
     * 更新数据
     */
    public void updateData(String jsondata,int timeMills) {
        try {
            String sql = String.format("update %s set %s=? where %s=? and %s=?", DbConstant.TABLE_NAME, DbConstant.KEY_IS_UP, DbConstant.KEY_DATA, DbConstant.KEY_CREAT_AT);

            execSQL(sql,new Object[]{1,jsondata,timeMills});

        }catch (SQLException e)
        {
            CMSLog.i("DbManager updateData error:" + e.toString());
        }
    }

    /**
     * 指定条件查询数据
     */
    public JSONArray queryDatas(int limit){
        // 调用getWritableDatabase()方法对SQLiteDatabase类的对象sqlDatabase进行实例化


        String sql = String.format("select * from %s where %s=? limit ?", DbConstant.TABLE_NAME, DbConstant.KEY_IS_UP);
        SQLiteDatabase db = mDbOpenHelper.getReadableDatabase();

        try {
            Cursor cursor = db.rawQuery(sql, new String[]{String.valueOf(0),String.valueOf(limit)});
            CMSLog.i("DbManager queryDatas 000:"+ sql);
            if (cursor == null) return null;

            int count = cursor.getCount();

            JSONArray list = new JSONArray();

            while (cursor.moveToNext()) {
                int id = cursor.getInt(cursor.getColumnIndex(DbConstant.KEY_ID));
                String data = cursor.getString(cursor.getColumnIndex(DbConstant.KEY_DATA));
                long creatat = cursor.getLong(cursor.getColumnIndex(DbConstant.KEY_CREAT_AT));
                int isup = cursor.getInt(cursor.getColumnIndex(DbConstant.KEY_IS_UP));

                try {
                    JSONObject obj = new JSONObject();
                    obj.put(DbConstant.KEY_ID, id);
                    obj.put(DbConstant.KEY_DATA, data);
                    obj.put(DbConstant.KEY_CREAT_AT, creatat);
                    obj.put(DbConstant.KEY_IS_UP, isup);

                    //将查询到的结果加入到persons集合中
                    list.put(obj);

                } catch (JSONException e) {
                    CMSLog.i("DbManager queryDatas error 111:" + e.toString());
                }
            }

            cursor.close();
            return list;
        }catch (SQLException e) {
            CMSLog.i("DbManager queryDatas error 222:" + e.toString());
        }
        // if(db.isOpen()) db.close();
        return null;
    }


    /**
     * 查询全部数据
     */
    public int queryAllCounts(){

        int count = 0;
        String sql = String.format("select from %s where %s=?", DbConstant.TABLE_NAME, DbConstant.KEY_IS_UP);
        SQLiteDatabase db = mDbOpenHelper.getReadableDatabase();

        try {
            Cursor cursor = db.rawQuery(sql, new String[]{String.valueOf(0)});
            if (cursor != null){
                count = cursor.getCount();
                cursor.close();
            }
        }catch (SQLException e) {

        }

        // if(db.isOpen()) db.close();

        return count;
    }

    /**
     * 执行sql语句
     */
    private void execSQL(String sql, Object[] bindArgs) throws SQLException{
        try {
            //获取写数据库
            SQLiteDatabase db = mDbOpenHelper.getWritableDatabase();
            //直接执行sql语句
            db.execSQL(sql,bindArgs);//或者
            //关闭数据库
            //db.close();

        }catch (SQLException e)
        {
            throw e;
        }
    }

    private void execSQL(String sql) throws SQLException{
        try {
            //获取写数据库
            SQLiteDatabase db = mDbOpenHelper.getWritableDatabase();
            //直接执行sql语句
            db.execSQL(sql);
            //关闭数据库
            //db.close();

        }catch (SQLException e)
        {
            throw e;
        }
    }
}
