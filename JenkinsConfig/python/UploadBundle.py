# -*- coding:utf-8 -*-
import requests
import json
import sys

FILE = sys.argv[1]
CHANNEL = sys.argv[2]
ENV = sys.argv[3]
VERSION = sys.argv[4]
TYPE = sys.argv[5]

# upload_file = {"file":open(FILE,mode='rb')}

# data = {"version":VERSION,"channel":CHANNEL,"type":TYPE,"env":ENV}
# print(data)
# r = requests.post("http://dev-project-j-web.stardustworld.cn/api/api/v1/app/version/upload_app",data=data,files=upload_file) 
# print(r.text)

# download_url = "http://project-j-cdn.stardustgod.com/webroot/env_local/app_package/archive/20210818/Test-ver1629254071.apk"

# download_url = json.loads(r.text).get("data").get("package_link")
# download_url = "http://project-j-cdn.stardustgod.com/webroot/env_local/" + download_url
# f = open(OUT_URL_FILE, 'w')
# f.write(download_url)
# f.close()


HTTP = "http://dev-project-j-web.stardustworld.cn"                          #测试服
#HTTP = "http://dev-project-j-web.stardustworld.cn"                         #灰度服
#HTTP = "http://projectj-admin.stardustgod.com"                             #正式服

LOGIN_URL = "/api/login"                                                    #登录
UPLOAD_URL = "/api/api/v1/app/version/upload_app"                           #上传Url
USERNAME = "zhangpanfeng@crazymaplestudio.com"
PASSWORD = "hi656897516"

#------------------------------------------------------------------------------

# 登录
def login():
    print "替换服务器地址"
    if ENV == "release":
        HTTP = "http://test-j2-admin-web.stardustgod.com"                   #正式服
    elif ENV == "test":
        HTTP = "http://172.16.20.61:8003"                                   #测试服
    elif ENV == "audit":
        HTTP = "http://test-j2-admin-web.stardustgod.com"                   #审核服
    else:
        HTTP = "http://172.16.20.61:8003"                                   #灰度服
    
    print "替换完成，当前服务器地址为: ", HTTP

    print "登录"
    data = {"username":USERNAME,"password":PASSWORD}
    r = requests.post(HTTP+LOGIN_URL, data=data) 
    
    if r.ok:
        global userinfo
        userinfo = json.loads(r.text)    
        print(userinfo)
        
        uploadBundle()
    else:
        print "登录失败，code: ", r.status_code, " -- message: ", r.text
        sys.exit(1)

    return

# 上传AB包
def uploadBundle():
    print "上传AB包"

    if ENV == "test":
        HTTP = "http://172.16.20.61:8003"                                   #测试服
    elif ENV == "audit":
        HTTP = "http://test-j2-admin-web.stardustgod.com"                   #审核服
    elif ENV == "gray":
        HTTP = "http://test-j2-admin-web.stardustgod.com"                   #灰度服
    else:
        HTTP = "http://projectj2-admin.stardustgod.com"                   #正式服
    
    print "替换完成，当前服务器地址为: ", HTTP

    upload_file = {"file":open(FILE,mode='rb')}
    data = {"version":VERSION,"channel":CHANNEL,"type":TYPE,"env":ENV}
    print data
    r = requests.post(HTTP+UPLOAD_URL,data=data,files=upload_file) 
    print r.text

    return

uploadBundle()