using GameFramework;
using GameFramework.FileSystem;
using GameFramework.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;
using XLua;

namespace Game
{
    public class LuaComponent : GameFrameworkComponent
    {
        public LuaEnv luaEnv;
        float lastGCTime = 0;
        float gcInterval = 1;

        [HideInInspector]
        public LuaLooper luaLooper;
        static string luaFileHeadPath = "Assets/GameMain/Scripts/Lua/{0}.lua.txt";
        bool isRun = false;

        public void Run()
        {
            Log.Debug("LuaManger Init:" + System.DateTime.Now.ToString());
            luaEnv = new LuaEnv();

            if (GameEntry.Base.EditorResourceMode)
            {
                luaEnv.AddLoader(EditorLoader);
#if UNITY_EDITOR && EDITOR_HOTRELOAD//放一些有关Lua端调试的代码
                luaEnv.DoString("require('EditorMain')");
#endif
            }
            else
            {
                luaEnv.AddLoader(CustomLoader);
            }
            luaEnv.DoString("require('Main')");
            InitLuaLooper();
        }
        
        byte[] EditorLoader(ref string filePath,ref int length)
        {
            string realFilePath = filePath.Replace('.', '/');
            string realPath = Utility.Text.Format(luaFileHeadPath, realFilePath);
            string path = Application.dataPath.Replace("/Assets","/")+ realPath;
            if (!File.Exists(path))
            {
                return null;
            }
            byte[] content = File.ReadAllBytes(path);
            //Log.Info("EditorLoader============={0}", path);
            filePath = path;
            length = content.Length;
            return content;
        }
                      
        byte[] readBuffer = new byte[1024*512];
        byte[] CustomLoader(ref string filePath, ref int length)
        {
            string realFilePath = filePath.Replace('.', '/');
            string realPath = Utility.Text.Format(luaFileHeadPath, realFilePath);
            length = GameEntry.Resource.LoadBinaryFromFileSystem(realPath, readBuffer);
            return readBuffer;
        }

        void InitLuaLooper()
        {
            lastGCTime = Time.time;
            isRun = true;

            if (luaLooper == null)
            {
                GameObject loop = new GameObject("LuaLooper");
                luaLooper = loop.AddComponent<LuaLooper>();
                luaLooper.Init();
                loop.transform.SetParent(transform);
            }
        }
        void Update()
        {
            if (isRun)
            {
                if (Time.time - lastGCTime > gcInterval)
                {
                    luaEnv.Tick();
                    lastGCTime = Time.time;
                }
            }
        }

        public void Dispose()
        {
            isRun = false;
            if (luaEnv != null)
            {
                luaEnv.Dispose();
                luaEnv = null;
            }

            if (luaLooper != null)
            {
                GameObject.Destroy(luaLooper.gameObject);
            }
            luaLooper = null;
        }
    }
}