package com.crazymaplestudio.sdk.tools;


import java.util.Random;

/**
 * 随机数、字母 工具类
 * Created by jin
 * Created date: 2021-03-30
 */
public class RandomTool {

    private static Random _random = new Random(System.currentTimeMillis());

    /**
     * 生成一个0 到 endNum 之间的随机数
     *
     * @param endNum 结束数字
     * @return 0 到 endNum 之间的随机数
     */
    public static int getNum(int endNum){
        if(endNum > 0){

            return _random.nextInt(endNum);
        }
        return 0;
    }

    /**
     * 生成一个startNum 到 endNum之间的随机数(不包含endNum的随机数)
     *
     * @param startNum 开始数字
     * @param endNum   结束数字
     * @return startNum 到 endNum 之间的随机数
     */
    public static int getNum(int startNum,int endNum){
        if(endNum > startNum){
            return _random.nextInt(endNum - startNum) + startNum;
        }
        return 0;
    }

    /**
     * 生成随机大写字母
     *
     * @return 随机字母 string
     */
    public static String getLargeLetter(){
        return String.valueOf ((char) (_random.nextInt(27) + 'A'));
    }

    /**
     * 生成随机大写字母字符串
     *
     * @param size the size
     * @return 随机字母 string
     */
    public static String getLargeLetter(int size){
        StringBuilder buffer = new StringBuilder();
        for(int i=0; i<size;i++){
            buffer.append((char) (_random.nextInt(27) + 'A'));
        }
        return buffer.toString();
    }

    /**
     * 生成随机小写字母
     *
     * @return 随机字母 string
     */
    public static String getSmallLetter(){
        return String.valueOf ((char) (_random.nextInt(27) + 'a'));
    }

    /**
     * 生成随机小写字母字符串
     *
     * @param size the size
     * @return 随机小写字符串 string
     */
    public static String getSmallLetter(int size){
        StringBuilder buffer = new StringBuilder();
        for(int i=0; i<size;i++){
            buffer.append((char) (_random.nextInt(27) + 'a'));
        }
        return buffer.toString();
    }

    /**
     * 数字与小写字母混编字符串
     *
     * @param size 字符串长度
     * @return 随机小写 +数字字符串
     */
    public static String getNumSmallLetter(int size){
        StringBuilder buffer = new StringBuilder();
        for(int i=0; i<size;i++){
            if(_random.nextInt(2) % 2 == 0){//字母
                buffer.append((char) (_random.nextInt(27) + 'a'));
            }else{//数字
                buffer.append(_random.nextInt(10));
            }
        }
        return buffer.toString();
    }

    /**
     * 数字与大写字母混编字符串
     *
     * @param size 字符串长度
     * @return 随机大写 +数字字符串
     */
    public static String getNumLargeLetter(int size){
        StringBuilder buffer = new StringBuilder();
        Random random = new Random();
        for(int i=0; i<size;i++){
            if(random.nextInt(2) % 2 == 0){//字母
                buffer.append((char) (random.nextInt(27) + 'A'));
            }else{//数字
                buffer.append(random.nextInt(10));
            }
        }
        return buffer.toString();
    }

    /**
     * 数字与大小写字母混编字符串
     *
     * @param size 字符串长度
     * @return 随机大小写 +数字字符串
     */
    public static String getNumLargeSmallLetter(int size){
        StringBuilder buffer = new StringBuilder();
        Random random = new Random();
        for(int i=0; i<size;i++){
            if(random.nextInt(2) % 2 == 0){//字母
                if(random.nextInt(2) % 2 == 0){
                    buffer.append((char) (random.nextInt(27) + 'A'));
                }else{
                    buffer.append((char) (random.nextInt(27) + 'a'));
                }
            }else{//数字
                buffer.append(random.nextInt(10));
            }
        }
        return buffer.toString();
    }
}
