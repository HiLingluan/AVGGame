using System;
using UnityEditor;
using UnityEditor.iOS.Xcode.Custom;
using System.IO;
using UnityEditor.Build.Reporting;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEditor.Callbacks;
// using UnityEditor.iOS.Xcode;
using UnityEngine;
#if UNITY_IOS
using uPBXProject = UnityEditor.iOS.Xcode.PBXProject;
using UnityEditor.iOS.Xcode.Custom.PBX;
#endif

//using ;
namespace Builds
{
    public class BuildIOS
    {
#if UNITY_IOS
        private static string projPath;

        public static void Export(Command command)
        {
            Debug.Log("导出xcode");

            Debug.Log("export is: " + command.Export);

            if (Directory.Exists(command.Export))
                Directory.Delete(command.Export, true);

            EditorUserBuildSettings.development = command.Profiler;

            BuildOptions op = BuildOptions.CompressWithLz4;
            if (command.Profiler)
            {
                op = BuildOptions.CompressWithLz4 | BuildOptions.Development
                                                  | BuildOptions.AllowDebugging | BuildOptions.ConnectWithProfiler;
            }

            BuildReport report = BuildPipeline.BuildPlayer(BuildUtilitys.GetBuildSettingScenes().ToArray(),
                command.Export, BuildTarget.iOS, op);

            if (report.summary.result == BuildResult.Succeeded)
            {
                Debug.Log("导出成功");
                XCodeSetting(command);
                return;
            }

            Debug.LogError("导出失败: " + report.summary.result);
            EditorApplication.Exit(1);
        }

        /// <summary>
        /// 修改xcode设置
        /// </summary>
        /// <param name="command"></param>
        private static void XCodeSetting(Command command)
        {
            projPath = PBXProject.GetPBXProjectPath(command.Export);           
            var xcodeProj = new PBXProject();
            xcodeProj.ReadFromFile(projPath);
            string targetName = xcodeProj.TargetGuidByName(PBXProject.GetUnityTargetName());

            // xcodeProj.AddFrameworkToProject(targetName, "AuthenticationServices.framework", true);
            // xcodeProj.AddFrameworkToProject(targetName, "LocalAuthentication.framework", true);
            // xcodeProj.AddFrameworkToProject(targetName, "MessageUI.framework", true);
            // xcodeProj.AddFrameworkToProject(targetName, "SafariServices.framework", true);
            // xcodeProj.AddFrameworkToProject(targetName, "Photos.framework", false);
            // xcodeProj.AddFrameworkToProject(targetName, "UserNotifications.framework", false);
            // xcodeProj.AddFrameworkToProject(targetName, "MobileCoreServices.framework", false);
            // xcodeProj.AddFrameworkToProject(targetName, "CoreTelephony.framework", false);
            // xcodeProj.AddFrameworkToProject(targetName, "AppTrackingTransparency.framework", true);
            // xcodeProj.AddFrameworkToProject(targetName, "AdServices.framework", true);

            xcodeProj.AddBuildProperty(targetName, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
            xcodeProj.SetBuildProperty(targetName, "ENABLE_BITCODE", "NO");
            xcodeProj.SetBuildProperty(targetName, "DEBUG_INFORMATION_FORMAT", "dwarf-with-dsym");
            xcodeProj.SetBuildProperty(targetName, "PRODUCT_NAME", "PWK");
            xcodeProj.SetBuildProperty(targetName, "PRODUCT_MODULE_NAME", "PWK");
            xcodeProj.SetBuildProperty(targetName, "CODE_SIGN_STYLE", "Manual");
            // xcodeProj.SetBuildProperty(targetName, "CODE_SIGN_ENTITLEMENTS", "pwk.entitlements");
            xcodeProj.SetBuildProperty(targetName, "PRODUCT_BUNDLE_IDENTIFIER", "com.CrazyMaple.ProjectJ");
            xcodeProj.SetBuildProperty(targetName, "GCC_C_LANGUAGE_STANDARD", "gnu11");
            xcodeProj.SetBuildProperty(targetName, "MARKETING_VERSION", command.Version);
            xcodeProj.SetBuildProperty(targetName, "CURRENT_PROJECT_VERSION", command.Build);
            xcodeProj.SetBuildProperty(targetName, "IPHONEOS_DEPLOYMENT_TARGET ", "10.0");
            // xcodeProj.SetBuildProperty(targetName, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

            // xcodeProj.SetBuildProperty(targetName, "SWIFT_VERSION", "4.2");
            
            xcodeProj.SetBuildProperty(targetName, "PROVISIONING_PROFILE_SPECIFIER", command.Profile);

            xcodeProj.SetTeamId(targetName, command.Team);
            
            // xcodeProj.AppendShellScriptBuildPhase(targetName, "firebase", "/bin/sh", "${PODS_ROOT}/FirebaseCrashlytics/run",
                // new List<string>{"${DWARF_DSYM_FOLDER_PATH}/${DWARF_DSYM_FILE_NAME}/Contents/Resources/DWARF/${TARGET_NAME}"});

            //--------------------------添加文件引用---------------------------------------
            CopyXcodeSettings(command.Config, command.Export);    //先把svn上保存的配置复制到导出的工程目录下
            
            // string plistFileGuid = xcodeProj.AddFile("GoogleService-Info.plist", "GoogleService-Info.plist");
            // xcodeProj.AddFileToBuild(targetName, plistFileGuid);

             AddGroup(xcodeProj, targetName, "Libraries", new DirectoryInfo(Path.Combine(command.Export ,"Libraries/U3dFrameWork/")));
            //---------------------------------------------------------------------------

            //保存工程配置
            xcodeProj.WriteToFile(projPath);
            
            //--------------------------修改plist-----------------------------------------
            var plistPath = Path.Combine(command.Export, "Info.plist");
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
            var rootDict = plist.root;
            rootDict.SetString("CFBundleVersion", command.Build);
            rootDict.SetString("CFBundleShortVersionString", command.Version);
            xcodeProj.SetBuildProperty(targetName, "DEVELOPMENT_TEAM", command.Team);
            xcodeProj.SetBuildProperty(targetName, "PROVISIONING_PROFILE_SPECIFIER", command.Profile);
            //保存plist 
            plist.WriteToFile(plistPath);
            //----------------------------------------------------------------------------

            //--------------------------添加新的entitlements-------------------------------
            // var mr = new ProjectCapabilityManager(projPath, "pwk.entitlements", PBXProject.GetUnityTargetName());
            // mr.AddInAppPurchase();
            // mr.AddPushNotifications(true);    
            // mr.AddSigin();
            // string[] domains = new string[] { "applinks:nc5w.adj.st" };
            // mr.AddAssociatedDomains(domains);
            // mr.WriteToFile();
            //----------------------------------------------------------------------------
        }

        private static void AddGroup(PBXProject project, string target, string parentGroup, DirectoryInfo folder)
        {
            parentGroup += "/" + folder.Name;
            PBXGroupData group = project.CreateSourceGroup(parentGroup);
            
            FileInfo[] files = folder.GetFiles();
            foreach (FileInfo file in files)
            {                
                string guid = project.AddFile(parentGroup + "/" + file.Name, parentGroup + "/" + file.Name);
                project.AddFileToBuild(target, guid);
            }

            DirectoryInfo[] directorys = folder.GetDirectories();
            foreach (DirectoryInfo directory in directorys)
            {
                if(directory.Name.Contains(".bundle"))
                {
                    Debug.Log($"文件夹：{directory.Name}");
                    string guid = project.AddFolderReference(parentGroup + "/" + directory.Name, parentGroup + "/" + directory.Name);
                    project.AddFileToBuild(target, guid);
                }
                else
                {
                    AddGroup(project, target, parentGroup, directory);
                }
            }
        }

        #region 文件拷贝
        static string src_path;
        static string output_path;

        public static void CopyXcodeSettings(string srcPath, string targetPath)
        {
            src_path = srcPath;
            output_path = targetPath;

            Debug.Log($"执行文件拷贝。根目录{src_path}，目标目录{targetPath}");
            DoCopyBuildConfiguration();
        }

        /// <summary>
        /// 拷贝打包配置
        /// </summary>
        private static void DoCopyBuildConfiguration()
        {
            var srcFils = new List<string>();
            var outputFiles = new List<string>();

            // srcFils.Add(Path.Combine(src_path, "Classes/UnityAppController.h"));
            // srcFils.Add(Path.Combine(src_path, "Classes/UnityAppController.mm"));
            // srcFils.Add(Path.Combine(src_path, "Classes/UI/UnityViewControllerBase+iOS.mm"));
            // srcFils.Add(Path.Combine(src_path, "GoogleService-Info.plist"));
            // srcFils.Add(Path.Combine(src_path, "Info.plist"));
            srcFils.Add(Path.Combine(src_path, "Podfile"));
            srcFils.Add(Path.Combine(src_path, "Podfile.lock"));

            // outputFiles.Add(Path.Combine(output_path, "Classes/UnityAppController.h"));
            // outputFiles.Add(Path.Combine(output_path, "Classes/UnityAppController.mm"));
            // outputFiles.Add(Path.Combine(output_path, "Classes/UI/UnityViewControllerBase+iOS.mm"));
            // outputFiles.Add(Path.Combine(output_path, "GoogleService-Info.plist"));
            // outputFiles.Add(Path.Combine(output_path, "Info.plist"));
            outputFiles.Add(Path.Combine(output_path, "Podfile"));
            outputFiles.Add(Path.Combine(output_path, "Podfile.lock"));

            for (int i = 0; i < srcFils.Count; i++)
            {
                if (File.Exists(srcFils[i]))
                {
                    File.Copy(srcFils[i], outputFiles[i], true);
                    Debug.Log($"工程配置文件 -- {srcFils[i]} -- 拷贝成功");
                }
                else
                {
                    Debug.LogError($"工程配置文件不存在 -- {srcFils[i]} -- ");
                }
            }

            string srcSDKDir = Path.Combine(src_path, "Libraries/U3dFrameWork");
            string outputSDKDir = Path.Combine(output_path, "Libraries/U3dFrameWork");
                
            if(!Directory.Exists(outputSDKDir))
                Directory.CreateDirectory(outputSDKDir);
            
            CopyFolder(srcSDKDir, outputSDKDir);

            string srcSDKDir2 = Path.Combine(src_path, "Pods");
            string outputSDKDir2 = Path.Combine(output_path, "Pods");

            if (!Directory.Exists(outputSDKDir2))
                Directory.CreateDirectory(outputSDKDir2);

            CopyFolder(srcSDKDir2, outputSDKDir2);
        }

        /// <summary>
        /// 文件夹拷贝
        /// </summary>
        /// <param name="sourcePath">源目录</param>
        /// <param name="destPath">目的目录</param>
        private static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //目标目录不存在则创建
                    try
                    {
                        FileInfo fi = new FileInfo(c);
                        if(fi.Attributes != FileAttributes.Hidden && !fi.Name.Contains(".meta"))
                        {
                           var parent =  Directory.GetParent(destFile).ToString();
                           if(!Directory.Exists(parent))
                               Directory.CreateDirectory(parent);
                            
                            File.Copy(c, destFile, true);
                            Debug.Log($"工程配置文件 -- {c} -- 拷贝成功");
                        }
                    }
                    catch (Exception ex)
                    {
                        UnityEngine.Debug.LogError("iOS同步开始-->拷贝文件失败："+ destFile +" -->error:" + ex.Message);
                    }
                    
                });

                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });

            }
        }

        #endregion
#endif
    }
}