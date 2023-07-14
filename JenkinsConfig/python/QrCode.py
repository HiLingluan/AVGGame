#!usr/bin/python
# -*- coding:utf-8 -*-
import qrcode
import sys
import image
# from PIL import Image

qr = qrcode.QRCode(     
    version=1,     
    error_correction=qrcode.constants.ERROR_CORRECT_M,     
    box_size=5,     
    border=4, 
) 

url = sys.argv[1].replace("@","&")
qr.add_data(data= url) 

img = qr.make_image()
#img = img.resize((256,256),image.ANTIALIAS)
img.save(sys.argv[2])