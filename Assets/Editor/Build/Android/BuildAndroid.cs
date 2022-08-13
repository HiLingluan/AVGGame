//=======================================================
// 作者：daniel
// 描述：
// 时间：2020-08-21
//=======================================================

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Builds
{
    public class BuildAndroid
    {
#if UNITY_ANDROID
        public static void Export(Command command)
        {
            if (Directory.Exists(command.AndroidStudio))
                Directory.Delete(command.AndroidStudio, true);
            Directory.CreateDirectory(command.AndroidStudio);

            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
            EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
            EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ETC2;

            BuildOptions op = BuildOptions.CompressWithLz4;
            if (command.Profiler)
            {
                op = BuildOptions.CompressWithLz4 | BuildOptions.Development |
                     BuildOptions.AllowDebugging | BuildOptions.ConnectWithProfiler;
            }

            BuildReport report = BuildPipeline.BuildPlayer(BuildUtilitys.GetBuildSettingScenes().ToArray(),
                command.AndroidStudio, BuildTarget.Android, op);

            if (report.summary.result != BuildResult.Succeeded)
            {
                Debug.LogError("导出失败: " + report.summary.result);
                EditorApplication.Exit(1);
                return;
            }

            Debug.Log("拷贝android配置");
            CopyAndroidConfig(command.Config, command.Export);
            Debug.Log("导出结束");

            EditConfig(command.AndroidStudio , command);
        }

        private static void CopyAndroidConfig(string config, string export)
        {
            Copy(config, export);
        }

        public static void Copy(string srcPath, string destPath)
        {
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            string[] dirs = Directory.GetDirectories(srcPath);//目录
            string[] files = Directory.GetFiles(srcPath);//文件
            if (files.Length > 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    File.Copy(Path.Combine(srcPath, Path.GetFileName(files[i])), Path.Combine(destPath, Path.GetFileName(files[i])), true);
                }
            }
            if (dirs.Length > 0)
            {
                for (int j = 0; j < dirs.Length; j++)
                {
                    Directory.GetDirectories(Path.Combine(srcPath, Path.GetFileName(dirs[j])));
                    //递归调用
                    Copy(Path.Combine(srcPath, Path.GetFileName(dirs[j])), Path.Combine(destPath, Path.GetFileName(dirs[j])));
                }
            }
        }

        public static void EditConfig(string root, Command command)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            StringReader reader = new StringReader(File.ReadAllText(root + "/gradle.properties"));
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] ss = line.Split('=');
                switch (ss[0])
                {
                    case "JENKINS":
                        data.Add(ss[0], "true");
                        break;
                    case "VERSION_CODE":
                        data.Add(ss[0], command.version_code);
                        break;
                    case "VERSION_NAME":
                        data.Add(ss[0], command.version_name);
                        break;
                    case "OUTPUT_PATH":
                        data.Add(ss[0], command.apk_out_path);
                        break;
                    case "APK_NAME":
                        data.Add(ss[0], command.apk_name);
                        break;
                    default:
                        data.Add(ss[0], ss[1]);
                        break;
                }

                line = reader.ReadLine();
            }

            StringWriter writer = new StringWriter();
            foreach (KeyValuePair<string, string> pair in data)
            {
                string s = pair.Key + "=" + pair.Value;
                Debug.Log(s);
                writer.WriteLine(s);
            }

            File.WriteAllText(root + "/gradle.properties", writer.ToString());
        }
#endif
    }
}