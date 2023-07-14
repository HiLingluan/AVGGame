#coding=UTF-8

import os,json
import sys      #调用实参

SVN_LOG          = "SvnLog.txt"
UPDATE_LOG       = "UpdateLog.txt"

curdir = sys.argv[1]

# svnlogs = []
# updatelogs = []

def filterLog(linestr,updatelogs):
    # print json.dumps(linestr, encoding="UTF-8", ensure_ascii=False)
    # print json.dumps(linestr.strip(), encoding="UTF-8", ensure_ascii=False)
    # print len(linestr.strip())
    # print(linestr)
    linestr=linestr.replace('........','').strip()
    if linestr=="\r\n":
        return
    if linestr.strip()=="":
        return
    if len(linestr.strip())<5:
        # @ling加了个encode（），因为中文长度需要encode .encode()
        return
    if linestr=="........":
        return
    if linestr.find("Merged revision")>=0:
        return
    
    for v in updatelogs:
        if v==linestr:
            return
    updatelogs.append(linestr)

def readLogFile(svnlog,tagetlog):
    # os.system('cls')
    print 'log genrate start'

    #脚本文件夹
    # curdir      = os.getcwd()

    #两个log文件
    svn_path    = os.path.join(curdir,svnlog)
    update_path = os.path.join(curdir,tagetlog)

    if os.path.exists(svn_path):
        pass
    else:
        return
    
    svnlogs = []
    updatelogs = []

    #读取svnlog信息
    if os.path.exists(svn_path):
        with open(svn_path, 'r') as file:
            try:
                svnlogs = file.readlines()
            finally:
                file.close()
    
    #过滤需要的log
    for line in svnlogs:
        # print(line)
        filterLog(line,updatelogs)

    #重新处理log
    outputstr = ''
    index = 0
    for v in updatelogs:
        index = index + 1
        log = ""
        if index>1: 
            log = log + "|"
        log = log + str(index) + ". " + v
        log = log.replace('\n' , '')
        outputstr = outputstr + log
    
    # print json.dumps(outputstr, encoding="UTF-8", ensure_ascii=False)
    # print outputstr
    # print(outputstr)
    # print(update_path)
    #写入文件
    with open(update_path, 'w')as f:
        f.write(outputstr)

    print 'log genrate end'


def main():
    readLogFile(SVN_LOG,UPDATE_LOG)

main()
