package com.crazymaplestudio.sdk.event.eventmodel;

import org.json.JSONException;
import org.json.JSONObject;

/**
 * description
 * Created by jin
 * Created date: 2021-06-22
 */
public class LoginSuccessEvent extends CMSEvent {

    //第三方返回id
    private String id;
    //
    private String cover;
    //平台链接
    private String link;
    //名字
    private String name;
    //性别
    private String gender;
    //邮箱
    private String email;
    //平台头像
    private String photo;
    //位置
    private String locale;
    //生日
    private String birthday;
    //年龄范围
    private String ageRange;
    //年龄
    private String age;
    //登录类型
    //Constant.LOGIN_TYPE_GOOGLE
    //Constant.LOGIN_TYPE_APPLE
    //Constant.LOGIN_TYPE_FACEBOOK
    private String logintype;

    public LoginSuccessEvent() {

    }

    public String toString() {
        String jsonstr = "";
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("id", id == null ? "" : id);
            jsonObject.put("cover", cover == null ? "" : cover);
            jsonObject.put("link", link == null ? "" : link);
            jsonObject.put("name", name == null ? "" : name);
            jsonObject.put("gender", gender == null ? "" : gender);
            jsonObject.put("email", email == null ? "" : email);
            jsonObject.put("photo", photo == null ? "" : photo);
            jsonObject.put("locale", locale == null ? "" : locale);
            jsonObject.put("birthday", birthday == null ? "" : birthday);
            jsonObject.put("age_range", ageRange == null ? "" : ageRange);
            jsonObject.put("age", age == null ? "" : age);
            jsonObject.put("logintype", logintype == null ? "" : logintype);
            jsonstr = jsonObject.toString();
        } catch (JSONException e) {
            e.printStackTrace();

        }
        return jsonstr;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getCover() {
        return cover;
    }

    public void setCover(String cover) {
        this.cover = cover;
    }

    public String getLink() {
        return link;
    }

    public void setLink(String link) {
        this.link = link;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPhoto() {
        return photo;
    }

    public void setPhoto(String photo) {
        this.photo = photo;
    }

    public String getLocale() {
        return locale;
    }

    public void setLocale(String locale) {
        this.locale = locale;
    }

    public String getBirthday() {
        return birthday;
    }

    public void setBirthday(String birthday) {
        this.birthday = birthday;
    }

    public String getAgeRange() {
        return ageRange;
    }

    public void setAgeRange(String ageRange) {
        this.ageRange = ageRange;
    }

    public String getAge() {
        return age;
    }

    public void setAge(String age) {
        this.age = age;
    }

    //登录类型
    public String getLoginType() {
        return logintype;
    }

    public void setLogintype(String logintype) {
        this.logintype = logintype;
    }
}
