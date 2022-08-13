//=======================================================
// 作者：范佳林
// 描述：
// 时间：2020-08-17
//=======================================================

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Builds
{
    public static class BuildUtilitys
    {
        public static List<EditorBuildSettingsScene> GetBuildSettingScenes()
        {
            List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    scenes.Add(scene);
                }
            }
            return scenes;
        }

        /// <summary>
        /// 文件夹拷贝
        /// </summary>
        /// <param name="sourcePath">源目录</param>
        /// <param name="destPath">目的目录</param>
        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] {destPath, Path.GetFileName(c)});
                    //目标目录不存在则创建
                    try
                    {
                        FileInfo fi = new FileInfo(c);
                        if (fi.Attributes != FileAttributes.Hidden && !fi.Name.Contains(".meta"))
                        {
                            var parent = Directory.GetParent(destFile).ToString();
                            if (!Directory.Exists(parent))
                                Directory.CreateDirectory(parent);

                            File.Copy(c, destFile, true);
                            Debug.Log($"文件 -- {c} -- 拷贝成功");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("拷贝文件失败：" + destFile + " -->error:" + ex.Message);
                    }
                });

                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] {destPath, Path.GetFileName(c)});
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });
            }
        }
    }
}