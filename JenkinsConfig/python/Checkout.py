#!/usr/bin/python
import os
import sys

LOCAL=sys.argv[1]
REMOTE=sys.argv[2] ##


username="zhangpanfeng0412"
password="LxoDhzn6D4X9uBWc"


if os.path.exists(LOCAL+"/.svn"):
    os.system("svn cleanup "+LOCAL) 
    os.system("svn upgrade "+LOCAL) 
    os.system("svn revert "+LOCAL+" --recursive")
    os.system("svn update "+LOCAL)
else:
    os.system("svn checkout https://172.16.20.10/svn/project_j/branches/"+ REMOTE+ " "+LOCAL+" --username="+username+" --password="+password)