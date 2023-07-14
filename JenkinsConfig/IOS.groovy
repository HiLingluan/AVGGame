/*
    iOS 工程打包脚本
    GLOBAL_ 开头为jenkins定义的全局变量
*/

node
{
    String root = ""                                // 工程根目录
    String xcode_path = ""                          // xcode 工程目录
    String project_path = ""                        // unity 工程目录
    String archive_path = ""                        // xcode 导出工程目录
    String ipa_path = ""                            // ipa 路径
    String ipa_name = ""                            // ipa 名字
    String log_path = ""                            // 日志路径
    String symbols = ""                             // 打包宏
    String bundle_zip_name = ""                     // 资源压缩包名字
    String bundle_zip_path = ""                     // 资源压缩包路径
    String bundle_zip_save = ""                     // 資源包存放路徑
    String qrcode_path = ""                         // 二维码路径
    String app_env = ""                             // 项目环境
    String app_project = ""                         // 项目名称
    String app_platform = ""                        // 项目平台    
    String svn_path = ""                            // svn 路径
    String build_platform = ""                      // 资源打包平台  10：测试 20：发布 30：灰度  40:正式
    String res_server_type = ""                     // 上传资源服务器类型 10: 测试  20: 发布  30: 灰度  40:正式
    String ios_native_path = ""                     // ios 原生脚本路径
    String branch = ""                              // 分支号
    String app_version = ""                         // app 版本号
    String bundle_version = ""                      // 资源版本号
    String res_version = ""                         //资源实际版本号
    String resource_folder_name= ""                 // 资源打包文件夹
    String app_channel = ""                         // 渠道号
    String environment = ""                         // 环境

    String xcode_team_id = ""                       // 项目id
    String xcode_config = ""                        // xcode环境
    String xcode_signing = ""                       // xcode 版本 （dev/release)
    String xcode_profile = ""                       // 证书
    String xcode_export_plist = ""                  // 上传所需plist文件
    String xcode_build_version = ""                 // xcode版本build号

    String robot = ""                               // 钉钉机器人

    def update_content = []                         // 版本更新内容

    String logStr = "\r\n IOS出新包啦!"
    
    String zip_path = "sss/ss/ss/"

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
        bundle_zip_name = "IOS-${bundle_version}"
        app_project = "Magic"
        robot = "a46024b5-3597-4bd8-abc4-9706a81f04fc"

        if (symbols.contains("TEST"))
        {
            root = "${WORKSPACE}/${app_version}/Test/"
            project_path = "${root}${app_project}/"   //"/Users/zhangzhen/Jenkins/Workspace/workspace/2021/"
            svn_path = "${app_project}/${app_version}/Test"
            app_env = "test"
            build_platform = "10"
            res_server_type = "10"
            ipa_name = "${app_project}_${app_version}_${BUILD_NUMBER}"
         
            environment = "测试服"

            manager.addShortText(environment)
        }
        else if (symbols.contains("AUDIT"))
        {
            root = "${WORKSPACE}/${app_version}/Audit/"
            project_path = "${root}${app_project}/"     
            svn_path =  "${app_project}/${app_version}/Audit"
            app_env = "audit"
            build_platform = "20"
            res_server_type = "20"
            ipa_name = "${app_project}_${app_version}_${BUILD_NUMBER}"

            environment = "审核服"

            manager.addShortText(environment)
        }
        else if (symbols.contains("GRAY"))
        {
            root = "${WORKSPACE}/${app_version}/Audit/"
            project_path = "${root}${app_project}/"       
            svn_path =  "${app_project}/${app_version}/Audit"
            app_env = "gray"
            build_platform = "30"
            res_server_type = "30"
            ipa_name = "${app_project}_${app_version}_${BUILD_NUMBER}"
        
            environment = "灰度服"

            manager.addShortText(environment)
        }
        else if (symbols.contains("RELEASE"))
        {
            root = "${WORKSPACE}/${app_version}/Release/"
            project_path = "${root}${app_project}/"       
            svn_path =  "${app_project}/${app_version}/Release"
            app_env = "release"
            build_platform = "40"
            res_server_type = "40"
            ipa_name = "${app_project}_${app_version}_${BUILD_NUMBER}"

            environment = "正式服"

            manager.addShortText(environment)
        }

        bundle_zip_path = "${project_path}ResOutput/Full/${resource_folder_name}/IOS" 
        bundle_zip_save = "${project_path}ResOutput/Full/${resource_folder_name}/${bundle_zip_name}" 
        
        xcode_team_id = "${GLOBAL_TEAM_ID}"
        xcode_path = "${root}xcode/${app_project}${app_version}/"
        ipa_path = "${root}ipa/"
        log_path = "${root}Logs/${BUILD_NUMBER}/"
        qrcode_path = "${root}QrCodes"
        ios_native_path = "${project_path}NativeForiOS"
        archive_path = "${root}Archive/archive.xcarchive"
        app_platform = "IOS"

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
        
        file = new File("${ipa_path}")
        if (!file.exists())
        {
            file.mkdirs()
        }

        file = new File("${qrcode_path}")
        if (!file.exists())
        {
            file.mkdirs()
        }

        file = new File("${xcode_path}")
        if (!file.exists())
        {
            file.mkdirs()
        }

        manager.addShortText("版本号：${app_version}")
        manager.addShortText("资源版本号：${bundle_version}")

        if (HOT_UPDATE == "true")
        {
            manager.addShortText("热更资源包")
        }

        if("${XCODE_BUILD_VERSION}" == "")   // 如果配置了自定义参数，则使用自定义参数
        {
            xcode_build_version = "${BUILD_NUMBER}";
        }
        else
        {
            xcode_build_version = "${XCODE_BUILD_VERSION}"
        }

        if(TEST_FLIGHT == "true")  // 上传TF
        {
            xcode_config="Release"
            xcode_signing="${GLOBAL_SIGNING_RELEASE}"
            xcode_profile="${GLOBAL_PROFILE_RELEASE}"
            xcode_export_plist="${GLOBAL_CONFIG_PATH}plist/ReleaseExportOptions.plist"
        }
        else
        {
            xcode_config="Debug"
            xcode_signing="${GLOBAL_SIGNING_DEV}"
            xcode_profile="${GLOBAL_PROFILE_DEV}"
            xcode_export_plist="${GLOBAL_CONFIG_PATH}plist/DevExportOptions.plist"
        }
       if (HOT_UPDATE == "false")
       {
        sh "python ${GLOBAL_CONFIG_PATH}python/Tools.py ${project_path}/Assets/3ThirdPart/XLua/Gen/"
       }
    }

    // 拉取代码
    stage("svn")
    {
        sh "python ${GLOBAL_CONFIG_PATH}python/Checkout.py ${project_path} ${svn_path}"
    }

    // lua
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
        sh "/Applications/Unity/Hub/Editor/2019.4.31f1/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath ${project_path} -executeMethod Builds.Build.SetiOSDefineSymbols " +
        "-buildTarget iOS -symbols ${symbols} -channel ${app_channel} -logFile ${log_path}unity_symbols.log -buildPlatform ${build_platform}"
    }

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
            sh "chmod -R 777 ${WORKSPACE}"

            sh "/Applications/Unity/Hub/Editor/2019.4.31f1/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath ${project_path} "+
            "-executeMethod Builds.Build.ExportProject -logFile ${log_path}unity_build.log "+
            "-config ${ios_native_path} "+
            "-appversion ${bundle_version} "+ //app_version
            "-build ${xcode_build_version} "+ 
            "-profile ${xcode_profile} "+
            "-team ${xcode_team_id} "+
            "-export ${xcode_path} "+
            "-channel ${app_channel} "+
            "-hotupdate ${HOT_UPDATE} "+
            "-profiler ${PROFILER}"
            sh "chmod +x ${GLOBAL_CONFIG_PATH}shell/InstallPod.sh"
            sh "${GLOBAL_CONFIG_PATH}shell/InstallPod.sh ${xcode_path}"
        }
    }

    stage("build xcode")
    {
        if (HOT_UPDATE == "true")
        {
            echo "热更包，不build xcode"
        }
        else
        {
            sh "xcodebuild clean -workspace ${xcode_path}${GLOBAL_SCHEME}.xcworkspace -scheme ${GLOBAL_SCHEME} -configuration ${xcode_config}"
            
            echo "export build xcode"
            sh "xcodebuild archive -workspace ${xcode_path}${GLOBAL_SCHEME}.xcworkspace -scheme ${GLOBAL_SCHEME} -archivePath ${archive_path} -configuration ${xcode_config} -sdk iphoneos build CODE_SIGN_IDENTITY=${xcode_signing}"

            echo "export ipa"
            sh "xcodebuild -exportArchive -archivePath ${archive_path} -exportPath ${ipa_path} -exportOptionsPlist ${xcode_export_plist}"

            File newIpa = new File("${ipa_path}${ipa_name}.ipa");
            if(!newIpa.exists())
            {
                File oldIpa = new File("${ipa_path}Magic.ipa");
                oldIpa.renameTo(newIpa);
            }
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

        sh "${GLOBAL_ZIP_SH} ${bundle_zip_name} ${bundle_zip_path} ${bundle_zip_save}" // 第一个参数是压缩后的名字，第二个参数是要压缩的文件夹路径 ,第三个是压缩文件存放路径

        echo "上传 zip"
        sh "python ${GLOBAL_UPLOAD_BUNDLES} ${bundle_zip_save}.zip ${app_channel} ${app_env} ${bundle_version} 2"
    }

    // 上传apk
    stage("upload")
    {
        if(HOT_UPDATE == "true")
        {
            echo "热更包，不用上传ipa"
            return
        }

        if(TEST_FLIGHT == "lingtesttrue")
        {
            echo "validate"
            sh "xcrun altool --validate-app -f ${ipa_path}${ipa_name}.ipa -t ios -u ${APPLE_USER} -p ${APPLE_PASS} --verbose" 
            echo "upload"
            sh "xcrun altool --upload-app -f ${ipa_path}${ipa_name}.ipa -t ios -u ${APPLE_USER} -p ${APPLE_PASS} --verbose"
        }
        else
        {
            echo "上传 ipa"

            //----测试数据----//
            // String file = "/Users/zhangzhen/Jenkins/Workspace/workspace/jpro_android/Test/Android/811/AndroidStudio/${app_project}/launcher/build/outputs/apk/release/PWK_1.0.0.apk"   // 这是导出的apk文件路径
            // sh "python ${GLOBAL_UPLOAD_APP} ${file} ${app_channel} ${app_env} ${app_version} ${app_project} ${app_platform} ${root}${app_version}_url.txt"

            sh "python ${GLOBAL_UPLOAD_APP} ${ipa_path}${ipa_name}.ipa ${app_channel} ${app_env} ${app_version} ${app_project} ${app_platform} ${root}${app_version}_url.txt"
        }
    }

    // 获取更新日志
    stage("update log")
    {
        echo "获取更新日志"

        sh "python ${GLOBAL_CONFIG_PATH}python/SvnMerge.py ${project_path}"
        
        new File("${project_path}UpdateLog.txt").eachLine("UTF-8") 
        {
            String log = it
            UpdateContent = log.split("\\|")
        }
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
            "- 平台: iOS",
            "- 渠道: ${app_channel}",
            "# 更新内容",
        ]

        for (element in UpdateContent) {
            contents << element
        }
        contents <<  logStr

        List dingtalkContent = contents

        // 热更资源包机器人通知
        if(HOT_UPDATE == "true")  
        {   
            String messageUri = ""

            manager.addShortText("热更资源版本号: ${bundle_version}")

            dingtalk(
                robot: robot,
                type: "ACTION_CARD",
                title: "${environment} -> ${app_version} -> iOS 热更包上传完成",
                text: dingtalkContent,
                at: [ "+86-13715021413","+86-13640990848" ],    //这个地方是群内需要@人员的手机号
                messageUrl: "${messageUri}",
            )
            return
        }

        // 上传TF机器人通知
        if(TEST_FLIGHT == "true")
        {
            dingtalk(
                robot: robot,
                type: 'ACTION_CARD',
                text: dingtalkContent,
                at: [  "+86-13715021413","+86-13640990848" ],
                btns: 
                [
                    [
                        title: 'Apple后台',
                        actionUrl: "https://itunesconnect.apple.com/"
                    ]
                ]
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
    
        String actionUrl = ""
        if ("${environment}" == "测试服")
        {
            manager.addHtmlBadge("<img src=${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/Test/IOS/${app_version}/QrCodes/${BUILD_NUMBER}.png>")
        }
        else if ("${environment}" == "灰度环境")
        {
            manager.addHtmlBadge("<img src=${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/Gray/IOS/${app_version}/QrCodes/${BUILD_NUMBER}.png>")
        }
        else
        {
            manager.addHtmlBadge("<img src=${JOB_URL}${BUILD_NUMBER}/execution/node/3/ws/Release/IOS/${app_version}/QrCodes/${BUILD_NUMBER}.png>")
        }

        dingtalk (
            robot: robot,
            type: "ACTION_CARD",
            text: dingtalkContent,
            at: [  "+86-13640990848"],    //这个地方是群内需要@人员的手机号
            btns:
            [
                [
                    title: "扫一扫(${BUILD_NUMBER})",
                    actionUrl: "${JOB_URL}"
                ]
            ]
        )
    }
}