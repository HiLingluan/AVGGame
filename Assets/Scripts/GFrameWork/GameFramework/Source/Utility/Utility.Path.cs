//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace GameFramework
{
    public static partial class Utility
    {
        /// <summary>
        /// 路径相关的实用函数。
        /// </summary>
        public static class Path
        {
            /// <summary>
            /// 获取规范的路径。
            /// </summary>
            /// <param name="path">要规范的路径。</param>
            /// <returns>规范的路径。</returns>
            public static string GetRegularPath(string path)
            {
                if (path == null)
                {
                    return null;
                }

                return path.Replace('\\', '/');
            }

            /// <summary>
            /// 获取远程格式的路径（带有file:// 或 http:// 前缀）。
            /// </summary>
            /// <param name="path">原始路径。</param>
            /// <returns>远程格式路径。</returns>
            public static string GetRemotePath(string path)
            {
                string regularPath = GetRegularPath(path);
                if (regularPath == null)
                {
                    return null;
                }

                return regularPath.Contains("://") ? regularPath : ("file:///" + regularPath).Replace("file:////", "file:///");
            }

            /// <summary>
            /// 移除空文件夹。
            /// </summary>
            /// <param name="directoryName">要处理的文件夹名称。</param>
            /// <returns>是否移除空文件夹成功。</returns>
            public static bool RemoveEmptyDirectory(string directoryName)
            {
                if (string.IsNullOrEmpty(directoryName))
                {
                    throw new GameFrameworkException("Directory name is invalid.");
                }

                try
                {
                    if (!Directory.Exists(directoryName))
                    {
                        return false;
                    }

                    // 不使用 SearchOption.AllDirectories，以便于在可能产生异常的环境下删除尽可能多的目录
                    string[] subDirectoryNames = Directory.GetDirectories(directoryName, "*");
                    int subDirectoryCount = subDirectoryNames.Length;
                    foreach (string subDirectoryName in subDirectoryNames)
                    {
                        if (RemoveEmptyDirectory(subDirectoryName))
                        {
                            subDirectoryCount--;
                        }
                    }

                    if (subDirectoryCount > 0)
                    {
                        return false;
                    }

                    if (Directory.GetFiles(directoryName, "*").Length > 0)
                    {
                        return false;
                    }

                    Directory.Delete(directoryName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static void CopyFolder(string sourceFolder, string destFolder)
            {
                try
                {
                    if (!Directory.Exists(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                    }
                    string[] files = Directory.GetFiles(sourceFolder);
                    foreach (string file in files)
                    {
                        string name = System.IO.Path.GetFileName(file);
                        string dest = System.IO.Path.Combine(destFolder, name);
                        File.Copy(file, dest);
                    }

                    string[] folders = Directory.GetDirectories(sourceFolder);
                    foreach (string folder in folders)
                    {
                        string name = System.IO.Path.GetFileName(folder);
                        string dest = System.IO.Path.Combine(destFolder, name);
                        CopyFolder(folder, dest);
                    }

                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError(e.Message);
                }

            }
            
            public static string GetFilePathWithoutExtension(string path)
            {
                int index = path.IndexOf('.');
                if (index < 0)
                    return path;
                return path.Substring(0, index);
            }
            
            public static string GetCompressedFileName(string url)
            {
                url = Regex.Replace(url, @"^jar:file:///", "");
                return url.Substring(0, url.LastIndexOf("!"));
            }
        }
    }
}
