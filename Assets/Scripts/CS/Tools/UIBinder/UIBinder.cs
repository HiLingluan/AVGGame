using System;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;
using Game.UI;

namespace Game.Editor
{
    public class UIBinder : MonoBehaviour
    {
#if UNITY_EDITOR

        [Header("文件夹")]
        public string DirName;

        [Header("类名")]
        public string className;

        public VariableArray jVariableArray;

        public void ExportLua()
        {
            StringBuilder property = new StringBuilder();
            StringBuilder componentVar = new StringBuilder();
            foreach (var item in jVariableArray.variables)
            {
                object value = item.GetValue();
                string propertyName = item.Name;

                if (value == null || string.IsNullOrEmpty(propertyName))
                {
                    continue;
                }
                //GetString(propertyName, value, item.Note, item.ValueType, property, componentVar);
            }
            SaveFile(GetLuaScript(property.ToString(), componentVar.ToString()));
            AssetDatabase.Refresh();
        }

        private string GetLuaScript(string a1, string b1)
        {
            var lua =
    @"---@class {3}
local {0} = class('{3}');
local CSUtil = CS.CSUtil
{1}
function {0}:ctor(transform)
    self.transform = transform;
{2}
end

return {0}";
            lua = string.Format(lua, className, a1, b1, GetDirClassName());
            return lua;
        }
        private string GetDirClassName()
        {
            if (!string.IsNullOrEmpty(DirName))
            {
                return string.Format("{0}.{1}", DirName, className);

            }
            return className;
        }


        private string GetString(string propertyName, object value, string comment, Type t, StringBuilder psb, StringBuilder csb)
        {
            var str = "";
            // psb.AppendFormat("{0}.{1} = nil;---{2}\n",childType.Name,propertyName,comment);
            if (t == typeof(int) || t == typeof(uint) || t == typeof(float) || t == typeof(long))
            {
                csb.AppendFormat("    self.{0} = {1};---{2}\n", propertyName, value, comment);
                Debug.LogFormat("abc.{0}={1}", propertyName, value);
            }
            else if (t == typeof(string))
            {
                csb.AppendFormat("    self.{0} = \"{1}\";---{2}\n", propertyName, value, comment);
                Debug.LogFormat("abc.{0}=\"{1}\"\n", propertyName, value);
            }
            else if (t == typeof(GameObject))
            {
                var go = value as GameObject;
                var path = getPath(go.transform);
                csb.AppendFormat("    self.{0} = {1}.gameObject;---{2}\n", propertyName, path, comment);
                //csb.AppendFormat("    self.{0} = CSUtil.Find(self.transform,{1},{2});---{3}\n", propertyName, path,"GameObject",comment);
            }
            else if (t == typeof(CanvasGroup) || t == typeof(Animator))
            {
                var cm = value as Component;
                var path = getPathStr(cm.transform);
                csb.AppendFormat("    self.{0} = CSUtil.Find(self.transform,\"{1}\",typeof(CS.{2}));---{3}\n", propertyName, path, t.ToString(), comment);
            }
            else if (value is Component)
            {
                var cm = value as Component;

                if (!(value is Transform))
                {
                    var path = getPathStr(cm.transform);
                    csb.AppendFormat("    self.{0} = CSUtil.Find(self.transform,\"{1}\",\"{2}\");---{3}\n", propertyName, path, t.ToString(), comment);
                }
                else
                {
                    var path = getPath(cm.transform);
                    csb.AppendFormat("    self.{0} = {1};---{2}\n", propertyName, path, comment);
                }
                //csb.AppendFormat("    self.{0} = {1};---{2}\n",propertyName,path,comment);

            }
            else if (t == typeof(GameObject[]))
            {
                var cms = value as GameObject[];
                var path = "{";
                foreach (var cm in cms)
                {
                    path += "\n    " + getPath(cm.transform) + ".gameObject,";
                }
                path += "\n    }";
                csb.AppendFormat("    self.{0} = {1};---{2}\n", propertyName, path, comment);
            }
            else if (t.BaseType == typeof(Array))
            {
                var cms = value as Component[];
                var path = "{";
                foreach (var cm in cms)
                {
                    if (!(cm is Transform))
                    {
                        string pathStr = getPathStr(cm.transform);
                        //path = string.Format("{0}:GetComponent(\"{1}\")", path, cm.GetType().ToString());
                        path += string.Format("\n    CSUtil.Find(self.transform,\"{0}\",\"{1}\")", pathStr, cm.GetType().ToString());
                    }
                    else
                    {
                        path += "\n    " + getPath(cm.transform);
                    }
                    path += ",";
                }
                path += "}";
                csb.AppendFormat("    self.{0} = {1};---{2}\n", propertyName, path, comment);
            }
            return str;
        }


        private string getPath(Transform go)
        {
            var path = go.name;
            var parent = go;
            while (true)
            {
                parent = parent.parent;
                if (parent == null || parent == gameObject.transform) break;
                path = parent.name + "/" + path;
            }
            path = string.Format("CSUtil.FindTransform(self.transform,\"{0}\")", path);
            return path;
        }

        private string getPathStr(Transform go)
        {
            var path = go.name;
            var parent = go;
            while (true)
            {
                parent = parent.parent;
                if (parent == null || parent == gameObject.transform) break;
                path = parent.name + "/" + path;
            }
            return path;
        }

        private void SaveFile(string str)
        {
            string filePath;
            if (!string.IsNullOrEmpty(DirName))
            {
                string dir = string.Format("{0}{1}", Application.dataPath + "/GameMain/Scripts/Lua/Binder/", DirName);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                filePath = string.Format("{0}{1}/{2}.lua.txt", Application.dataPath + "/GameMain/Scripts/Lua/Binder/", DirName, className);
            }
            else
            {
                filePath = string.Format("{0}{1}.lua.txt", Application.dataPath + "/GameMain/Scripts/Lua/Binder/", className);
            }
            var utf8 = new System.Text.UTF8Encoding(false);
            File.WriteAllText(filePath, str, utf8);
            Debug.Log("导出lua文件成功===========" + filePath);
        }
#endif
    }
}
