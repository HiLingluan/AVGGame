import os

testStr = "101.0.2"


def TestStr(version):
    index = version.find('.')
    print(version[:index])


TestStr(testStr)
