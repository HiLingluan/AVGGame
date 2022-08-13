using CSObjectWrapEditor;
using UnityGameFramework.Editor;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Editor.ResourceTools;
using System.IO;
using StarForce.Editor.ResourceTools;

namespace Builds
{
    public class Build
    {
        public const string DEFAULT_OUTPUT = "AssetBundles";
    
        /// <summary>
        /// 切换iOS平台，并且设置宏
        /// </summary>
        public static void SetiOSDefineSymbols()
        {

            Debug.Log("BuildTarget: " + EditorUserBuildSettings.activeBuildTarget);
            // 切换平台
            if (!EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS))
            {
                Debug.LogError("切换平台失败");
                return;
            }

            Debug.Log("设置预编译");
            Command command = new Command(Environment.GetCommandLineArgs());

            Debug.Log("修改打包平台" + command.BuildPlatform);


            // 添加宏
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, command.Symbols);
        }

        /// <summary>
        /// 切换Android平台，并且设置宏
        /// </summary>
        public static void SetAndroidDefineSymbols()
        {   
            Debug.Log("BuildTarget: " + EditorUserBuildSettings.activeBuildTarget);

            if (!EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android))
            {
                Debug.LogError("切换平台失败");
                return;
            }

            Debug.Log("设置预编译");
            Command command = new Command(Environment.GetCommandLineArgs());

            Debug.Log("修改打包平台" + command.BuildPlatform);
            // 修改打包平台
            // ResOutputMenu.SetBuildServer(command.BuildPlatform);
            Debug.Log("设置宏:"+ command.Symbols);
            // 添加宏
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, command.Symbols);
        }

        public static void ClearAll()
        {
            Generator.ClearAll();
        }
        
        public static void GenAll()
        {
            Generator.GenAll();
        }

        /// <summary>
        /// 生成AB包
        /// </summary>
        public static void BuildAssetBundle()
        {
            Debug.Log("build AssetBundle");

            BuildSettings.DefaultScenes();
            ResourceRuleEditorUtility.RefreshResourceCollection();
            Command command = new Command(Environment.GetCommandLineArgs());
            string version = command.Version;


            ResourceBuilderController m_Controller = new ResourceBuilderController();
            m_Controller.AssetBundleCompression = AssetBundleCompressionType.LZ4;
            m_Controller.CompressionHelperTypeName = "UnityGameFramework.Runtime.DefaultCompressionHelper";
            m_Controller.RefreshCompressionHelper();
            m_Controller.AdditionalCompressionSelected = true;

            m_Controller.ForceRebuildAssetBundleSelected = !command.HotUpdate;
            m_Controller.BuildEventHandlerTypeName = "JMatrix.Editor.JMatrixBuildUpdateEventHandler";
            m_Controller.RefreshBuildEventHandler();

            string outputDir = Application.dataPath + "/../ResOutput";
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            m_Controller.OutputDirectory = outputDir;
            m_Controller.OutputPackageSelected = false;
            m_Controller.OutputFullSelected = true;
            m_Controller.OutputPackedSelected = true;

#if UNITY_IOS
            m_Controller.Platforms = Platform.IOS;
#elif UNITY_ANDROID
            m_Controller.Platforms = Platform.Android;
#endif
            //资源版本号
            m_Controller.InternalResourceVersion = int.Parse(command.BundleVersion.Split('.')[2]);
            m_Controller.Save();

            m_Controller.BuildResources();

            //BuildVM buildVM = new BuildVM();
            //buildVM.Build(!command.HotUpdate, command.BundleVersion);
            // ResOutputMenu.AutoBuildAssetBundle(command.BundleVersion, !command.HotUpdate, command.Channel);
        }

        /// <summary>
        /// 导出项目
        /// </summary>
        public static void ExportProject()
        {
            Debug.Log("export project");
            var command = new Command(Environment.GetCommandLineArgs());
            // VersionControllerTools.ConfigChannel(command.Channel);

#if HOTFIX_ENABLE
            XLua.Hotfix.HotfixInject();
#endif

#if UNITY_IOS
            BuildIOS.Export(command);
#elif UNITY_ANDROID
            BuildAndroid.Export(command);
#endif
        }


        //[MenuItem("Tools/测试构建")]
        //public static void Test()
        //{
        //    GenAll();
        //    BuildAssetBundle();
        //}
    }
}