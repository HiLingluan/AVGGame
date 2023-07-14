#!/bin/sh
echo "start install pod"

InstallPod(){
    chmod -R 777 $1 
    cd "$1"
    chmod +x MapFileParser.sh
    # pod update --no-repo-update
    pod install --no-repo-update
}

echo "文件路径: $1"
InstallPod "$1"