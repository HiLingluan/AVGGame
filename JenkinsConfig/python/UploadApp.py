# -*- coding:utf-8 -*-
import requests
import json
import sys

FILE = sys.argv[1]
CHANNEL = sys.argv[2]
ENV = sys.argv[3]
VERSION = sys.argv[4]
PROJECT = sys.argv[5]
PLATFORM = sys.argv[6]
OUT_URL_FILE = sys.argv[7]




HTTP = "http://spm.stardustgod.com"                                     # 国内测试服
# HTTP = "http://dev_spmp.stardustworld.cn"                             # 审核服
# HTTP = "http://spm.stardustgod.com"                                   # 正式服
UPLOAD_URL = "/api/tool/uploadTool"

def upload():
    print "上传APK"
    # print(FILE)
    #@ling 这里升级了python3所以要加‘rb’,'rb'
    upload_file = {"file":open(FILE)}
    
    data = {"channel":CHANNEL,"env":ENV,"version":VERSION,"project":"Magic","platform":PLATFORM}
    # print(HTTP+UPLOAD_URL)
    r = requests.post(url=HTTP+UPLOAD_URL,data=data,files=upload_file)
    print r.text

    download_url = json.loads(r.text).get("data").get("url")
    f = open(OUT_URL_FILE, 'w')
    f.write(download_url)
    f.close()

    return

upload()