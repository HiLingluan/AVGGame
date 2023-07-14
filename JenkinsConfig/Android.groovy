/*
    Android 工程打包脚本
    GLOBAL_ 开头为jenkins定义的全局变量
*/
node
{
    String root = ""                                // 工程根目录
    String android_studio_path = ""                 // Android 工程目录
    String project_path = ""                        // unity 工程目录
    String apk_path = ""                            // apk 路径
    String apk_name = ""                            // apk 名字
    String log_path = ""                            // 日志路径
    String symbols = ""                             // 打包宏
    String bundle_zip_name = ""                     // 资源压缩包名字
    String bundle_zip_path = ""                     // 资源压缩包路径
    String bundle_zip_save = ""                     // 資源包存放路徑
    String lua_config_path = ""                     //lua 配置文件路径
    String qrcode_path = ""                         // 二维码路径
    String app_env = ""                             // 项目环境
    String app_project = ""                         // 项目名称
    String app_platform = ""                        // 项目平台    
    String svn_path = ""                            // svn 路径
    String svn_client_config_path = ""                     // svn 配置路径
    String build_platform = ""                      // 资源打包平台 10：测试 20：发布 30：灰度  40:正式
    String res_server_type = ""                     // 上传资源服务器类型 10: 测试  20: 发布  30: 灰度  40:正式
    String android_native_path = ""                 // Android 原生脚本路径
    String branch = ""                              // 分支号
    String app_version = ""                         // app 版本号
    String bundle_version = ""                      // 资源全版本号
    String resource_folder_name= ""                 // 资源打包文件夹
    String res_version = ""                         //资源实际版本号
    String app_channel = ""                         // 渠道号
    String environment = ""                         // 环境
    //String UpdateContent = ""                       //更新Log文本
    String robot = ""                               // 钉钉机器人
    def update_content = []                         // 版本更新内容

    String logStr = "\r\n 安卓出新包啦!"
    
    // 初始化
    stage ("init")
    {
        symbols = "${SYMBOLS}"
        if("${CUSTOM_SYMBOLS}" != "")
        {
            symbols = "${CUSTOM_SYMBOLS}"
        }
        bundle_version = "${VERSION}"
        app_version = bundle_version.substring(0,bundle_version.indexOf('.'))
        res_version = bundle_version.split("\\.")[2]
        //资源全版本号一定是三位
        resource_folder_name = app_version+"_"+res_version
        app_channel = "${APP_CHANNEL}"
        bundle_zip_name = "Android-${bundle_version}"
        app_project = "Magic"
        svn_client_config_path = "ConfigExcel/Config/Output/Client/"

        robot = "a46024b5-3597-4bd8-abc4-9706a81f04fc"
        
        if (symbols.contains("TEST"))
        {
            root = "${WORKSPACE}/${app_version}/Test/"
            project_path = "${root}${app_project}/"       
            svn_path = "${app_project}/${app_version}/Test"
            svn_client_config_path = svn_client_config_path+"Test"
            app_env = "test"
            build_platform = "10"
            res_server_type = "10"
            apk_name = "${app_project}_${app_version}_${BUILD_NUMBER}_test.apk"
            environment = "测试服"

            manager.addShortText(environment)
        }
        else if (symbols.contains("AUDIT"))
        {
            root = "${WORKSPACE}/${app_version}/Audit/"
            project_path = "${root}${app_project}/"       
            svn_path =  "${app_project}/${app_version}/Audit"
            svn_client_config_path = svn_client_config_path+"Audit"
            app_env = "audit"
            build_platform = "20"
            res_server_type = "20"
            apk_name = "${app_project}_${app_version}_${BUILD_NUMBER}_audit.apk"
            environment = "审核服"

            manager.addShortText(environment)
        }
        else if (symbols.contains("GRAY"))
        {
            root = "${WORKSPACE}/${app_version}/Audit/"
            project_path = "${root}${app_project}/"       
            svn_path =  "${app_project}/${app_version}/Audit"
            svn_client_config_path = svn_client_config_path+"Audit"
            app_env = "gray"
            build_platform = "30"
            res_server_type = "30"
            apk_name = "${app_project}_${app_version}_${BUILD_NUMBER}_gray.apk"
            environment = "灰度服"
    
            manager.addShortText(environment)
        }
        else if (symbols.contains("RELEASE"))
        {
            root = "${WORKSPACE}/${app_version}/Release/"
            project_path = "${root}${app_project}/"       
            svn_path =  "${app_project}/${app_version}/Release"
            svn_client_config_path = svn_client_config_path+"Release"
            app_env = "release"
            build_platform = "40"
            res_server_type = "40"
            apk_name = "${app_project}_${app_version}_${BUILD_NUMBER}_release.apk"
            environment = "正式服"

            manager.addShortText(environment)
        }

        
        bundle_zip_path = "${project_path}ResOutput/Full/${resource_folder_name}/Android" 
        bundle_zip_save = "${project_path}ResOutput/Full/${resource_folder_name}/${bundle_zip_name}" 

        lua_config_path = "${project_path}Assets/GameMain/Scripts/Lua/CommonConfig"

        android_studio_path = "${root}AndroidStudio"
        apk_path = "${root}Apk/"
        log_path = "${root}Logs/${BUILD_NUMBER}/"
        qrcode_path = "${root}QrCodes"
        android_native_path = "${project_path}NativeForAndroid"
        app_platform = "Android"

        def file = new File("${log_path}")
        if (!file.exists())
        {
            file.mkdirs()
        }
    
        file = new File("${bundle_zip_path}")
        if (!file.exists())
        {
            file.mkdirs()
        }
        
        file = new File("${apk_path}")
        if (!file.exists())
        {
            file.mkdirs()
        }

        file = new File("${qrcode_path}")
        if (!file.exists())
        {
            file.mkdirs()
        }

        file = new File("${android_studio_path}")
        if (!file.exists())
        {
            file.mkdirs()
        }

        manager.addShortText("版本号：${app_version}")
        manager.addShortText("资源版本号：${bundle_version}")
       if (HOT_UPDATE == "false")
       {
            sh "python ${GLOBAL_CONFIG_PATH}python/Tools.py ${project_path}Assets/3ThirdPart/XLua/Gen/"
       }
        
    }
    
    // 拉取代码
    stage("svn project")
    {
        sh "python ${GLOBAL_CONFIG_PATH}/python/Checkout.py ${project_path} ${svn_path}"
    }

    // 拉取lua 配置
    stage("svn lua config")
    {
        sh "python ${GLOBAL_CONFIG_PATH}/python/Checkout.py ${lua_config_path} ${svn_client_config_path}"
    }
    
    stage("gen lua")
    {
       if (HOT_UPDATE == "false")
       {
        sh "/Applications/Unity/Hub/Editor/2019.4.31f1/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath ${project_path} -executeMethod Builds.Build.GenAll " +
        "-logFile ${log_path}unity_gen_lua.log"
       }
    }

    // 修改宏
    stage("set symbols")
    {
        sh "/Applications/Unity/Hub/Editor/2019.4.31f1/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath ${project_path} -executeMethod Builds.Build.SetAndroidDefineSymbols " +
        "-buildTarget Android -symbols ${symbols} -channel ${app_channel} -logFile ${log_path}unity_symbols.log -buildPlatform ${build_platform}"
    }

    // lua


    // build ab
    stage("build ab")
    {
        sh "chmod -R 777 ${WORKSPACE}"
        sh "/Applications/Unity/Hub/Editor/2019.4.31f1/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath ${project_path} "+
        "-executeMethod Builds.Build.BuildAssetBundle -logFile ${log_path}unity_ab.log "+
        "-bundleVersion ${bundle_version} "+
        "-hotupdate ${HOT_UPDATE} "+
        "-channel ${app_channel}"
    }

    // build unity
    stage("build unity")
    {
        if (HOT_UPDATE == "true")
        {
            echo "热更包，不build unity"
        }
        else
        {
            sh "/Applications/Unity/Hub/Editor/2019.4.31f1/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath ${project_path} "+
            "-executeMethod Builds.Build.ExportProject -logFile ${log_path}unity_build.log "+
            "-config ${android_native_path} "+
            "-androidstudio ${android_studio_path}/${app_project}${res_version} "+
            "-export ${android_studio_path}/${app_project}${res_version} "+
            "-code ${GOOGLE_PLAY_CODE} "+
            "-name ${app_version} "+
            "-apkoutpath ${apk_path} "+ 
            "-apkname ${apk_name} "+
            "-hotupdate ${HOT_UPDATE} "+
            "-profiler ${PROFILER}"
        }
    }

    // build apk
    stage("build apk")
    {
        if (HOT_UPDATE == "true")
        {
            echo "热更包，不用build apk"
        }
        else
        {
            sh "gradle clean -Dorg.gradle.internal.http.socketTimeout=300000 -p ${android_studio_path}/${app_project}${res_version}"
            sh "gradle assembleRelease --stacktrace -p ${android_studio_path}/${app_project}${res_version}"
        }
    }

    // build aab
    stage("build aab")
    {
        if (HOT_UPDATE == "true")
        {
            echo "热更包，不用build aab"
        }
        else if ("${ANDROID_BUNDLE}" == "true")
        {
            sh "gradle clean -Dorg.gradle.internal.http.socketTimeout=300000 -p ${android_studio_path}/${app_project}${res_version}"
            sh "gradle bundleRelease --stacktrace -p ${android_studio_path}/${app_project}${res_version}"
        }
    }

    // 压缩bundle包
    stage("zip")
    {
        if(ZIP_FLAG == "false")
        {
            echo "不需要压缩zip"
            return
        }

        echo "压缩zip"
        sh "chmod -R 777 ${GLOBAL_CONFIG_PATH}"

        //----测试数据----//
        // bundle_zip_name = "PWK_1.0.0"
        // bundle_zip_path = "/Users/zhangzhen/Jenkins/Workspace/workspace/jpro_android/Test/Android/811/AndroidStudio/${app_project}/launcher/build/outputs/apk/release/"

        sh "${GLOBAL_ZIP_SH} ${bundle_zip_name} ${bundle_zip_path} ${bundle_zip_save}" // 第一个参数是压缩后的名字，第二个参数是要压缩的文件夹路径

        echo "上传 zip"
        sh "python ${GLOBAL_UPLOAD_BUNDLES} ${bundle_zip_save}.zip ${app_channel} ${app_env} ${bundle_version} 2"
    }

    // 上传apk
    stage("upload")
    {
        if(HOT_UPDATE == "true")
        {
            echo "热更包，不用上传apk"
            return
        }

        echo "上传 apk"

        //----测试数据----//
        //apk_path = "${android_studio_path}/${app_project}${app_version}/launcher/build/outputs/apk/release/"
        //apk_name = "launcher-release.apk"

        sh "python ${GLOBAL_UPLOAD_APP} ${apk_path}${apk_name} ${app_channel} ${app_env} ${app_version} ${app_project} ${app_platform} ${root}${app_version}_url.txt"
    }

    // 获取更新日志
    stage("update log")
    {
        echo "获取更新日志"

        sh "python ${GLOBAL_CONFIG_PATH}python/SvnMerge.py ${project_path}"
        sh "chmod -R 777 ${project_path}UpdateLog.txt"
        new File("${project_path}UpdateLog.txt").eachLine("UTF-8") 
        {
            String log = it
            UpdateContent = log.split("\\|")
        }
        // print(UpdateContent);
    }

    // 钉钉机器人通知
    stage("ding ding")
    {
        String profilterTxt = ""
        if (PROFILER == "true")
        {
            profilterTxt = "性能模式"
        }
        // 整理版本更新内容格式
        def contents = [
            "![screenshot](https://project-j.oss-cn-shenzhen.aliyuncs.com/webroot/env_local/app_package/client-packer-cover.jpg)",   
            "- 版本: ${bundle_version}",
            "- 环境: ${environment} ${profilterTxt}",
            "- 平台: Android",
            "- 渠道: ${app_channel}",
            "# 更新内容",
        ]
        
        //if(UpdateContent != "" && UpdateContent != null)
        //{
        for (element in UpdateContent) {
            contents << element
        }
        //}
        
        contents <<  logStr

        List dingtalkContent = contents

        if(ANDROID_BUNDLE == "true")
        {
            manager.addShortText("AAB 包体")
        }
        // 热更资源包机器人通知
        if(HOT_UPDATE == "true")  
        {   
            String messageUri = ""

            manager.addShortText("热更资源版本号: ${bundle_version}")
            echo "ding ding 56"
            dingtalk(
                robot: robot,
                type: "ACTION_CARD",
                title: "${environment} -> ${app_version} -> Android 热更包上传完成",
                text: dingtalkContent,
                at: [  "+86-13640990848"  ],    //这个地方是群内需要@人员的手机号
                messageUrl: "${messageUri}",
            )
            return
        }

        // 上传SPM后台机器人通知
        def url_file = new File("${root}${app_version}_url.txt")
        String url = ""
        if(url_file.exists())
        {
            url = url_file.text
            url = url_file.text.replaceAll("&", "@")
            echo "url: " + url
        }
        sh "python ${GLOBAL_CONFIG_QRCODE_PATH} ${url} ${qrcode_path}/${BUILD_NUMBER}.png"
    
        //----测试数据----//
        // apk_name = "launcher-release.apk"

        String actionUrl = ""
        if ("${environment}" == "测试服")
        {
            manager.addHtmlBadge("<img src=${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/${app_version}/Test/QrCodes/${BUILD_NUMBER}.png>")
            actionUrl = "${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/${app_version}/Test/APK/${apk_name}"
        }
        else if ("${environment}" == "审核服")
        {
            manager.addHtmlBadge("<img src=${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/${app_version}/Audit/QrCodes/${BUILD_NUMBER}.png>")
            actionUrl = "${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/${app_version}/Audit/APK/${apk_name}"
        }
        else if ("${environment}" == "灰度服")
        {
            manager.addHtmlBadge("<img src=${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/Gray/Android/${app_version}/QrCodes/${BUILD_NUMBER}.png>")
            actionUrl = "${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/Gray/Android/${app_version}/AndroidStudio/${app_project}${app_version}/launcher/build/outputs/apk/release/${apk_name}"
        }
        else
        {
            manager.addHtmlBadge("<img src=${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/${app_version}/Release/QrCodes/${BUILD_NUMBER}.png>")
            actionUrl = "${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/${app_version}/Release/APK/${apk_name}"
        }

         dingtalk (
             robot: robot,
             type: "ACTION_CARD",
             text: dingtalkContent,
             at: ["+86-13640990848"],    //这个地方是群内需要@人员的手机号
             btns:
             [
                 [
                     title: "扫一扫(${BUILD_NUMBER})",
                     actionUrl: "${JOB_URL}"
                 ],
                 [
                     title: "下载APK",
                     actionUrl: "${actionUrl}"
                 ]
             ]
         )
    }
}