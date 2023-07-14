#!/bin/sh
echo "start"
echo "$1"
echo "$2"
echo "$3"

zip_user(){
    cd "$2"    
    zip -q -r "$3".zip *
}

zip_user "$1" "$2" "$3"
