//=======================================================
// 作者：范佳林
// 描述：
// 时间：2020-08-13
//=======================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Builds
{
    public class Command
    {
        public string Key { get { return Find("-key"); } }
        public string Export { get { return Find("-export"); } }
        public string Team { get { return Find("-team"); } }
        public string Profile { get { return Find("-profile"); } }
        public bool IsRebuild { get { return Find("-isRebuild") == "true"; } }
        public string Build { get { return Find("-build"); } }
        public string Version { get { return Find("-appversion"); } }
        public string Config { get { return Find("-config"); } }
        public string Shell { get { return Find("-shell"); } }
        public string Symbols { get { return Find("-symbols"); } }
        public string Bunlde { get { return Find("-Bunlde"); } }
        public string Channel { get { return Find("-channel"); } }
        public bool App { get { return Find("-app") == "true"; } }
        public string Signing { get { return Find("-signing"); } }
        public bool Profiler { get { return Find("-profiler") == "true"; } }
        public string BundleVersion { get { return Find("-bundleVersion"); } }
        public bool HotUpdate  { get { return Find("-hotupdate") == "true"; } }

        public string AndroidStudio { get { return Find("-androidstudio"); } }
        public string Jenkins { get { return Find("-jenkins"); } }
        public string version_code { get { return Find("-code"); } }
        public string version_name { get { return Find("-name"); } }
        public string store_file { get { return Find("-strorefile"); } }
        public string store_password { get { return Find("-strorepassword"); } }
        public string key_alias { get { return Find("-alias"); } }
        public string key_password { get { return Find("-keypassword"); } }
        public string apk_out_path { get { return Find("-apkoutpath"); } }
        public string apk_name { get { return Find("-apkname"); } }
        public string update_log { get { return Find("-updateLog"); } }
        public bool show_log { get { return Find("-showLog") == "true"; } }
        /// <summary>
        /// 1:测试服    2:审核服    3:正式服
        /// </summary>
        public string BuildPlatform { get { return Find("-buildPlatform"); } }
        //---------------------------------------------------------------------------------
        private string[] names = { "-key", "-export" , "-team" , "-profile" , "-build" ,"-isRebuild",
            "-appversion", "-config", "-shell", "-symbols", "-Bunlde","-app","-signing",
        "-androidstudio","-jenkins","-code","-name","-strorefile","-strorepassword","-alias","-keypassword",
        "-apkoutpath","-apkname","-channel","-profiler","-bundleVersion","-hotupdate","-buildPlatform","-updateLog", "-showLog"};
        private Dictionary<string, string> args = new Dictionary<string, string>();
        public Command(string[] args)
        {
            Action<string, int, string[]> InitKey = delegate (string command, int index, string[] param)
             {
                 if (param[index] == command)
                 {
                     this.args.Add(command, param[index + 1]);
                 }
             };
            for (int i = 0; i < args.Length; ++i)
            {
                foreach (string name in names)
                {
                    InitKey(name, i, args);
                }
            }
            if (this.args.ContainsKey("-symbols"))
            {
                string str = this.args["-symbols"];
                str = str.Replace(":", ";");
                this.args["-symbols"] = str;
            }
        }
        public string Find(string key)
        {
            string data = string.Empty;
            args.TryGetValue(key, out data);
            return data;
        }
        public void Add(string key, string param)
        {
            if (args.ContainsKey(key))
                return;
            args.Add(key, param);
        }

    }
}