using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Game.Binder
{
    public class PanelBinder : MonoBehaviour
    {
        /// <summary>
        /// 要导出lua文件夹名称
        /// </summary>
        private string _directoryName;

        [HideInInspector]
        public Type childType;
        public PanelBinder(Type childType,string directoryName)
        {
            _directoryName = directoryName;
            this.childType = childType;
        }

#if UNITY_EDITOR
        public void ExportLua() 
        { 
            StringBuilder property = new StringBuilder();
            StringBuilder componentVar = new StringBuilder();
            foreach(FieldInfo field in childType.GetFields())
            {
                foreach (Attribute attr in field.GetCustomAttributes(true))
                {
                    var a = attr as ExportLuaAttribute;
                    if (null != a)
                    {
                        var ft = field.FieldType;
                        var v = field.GetValue(this);
                        if(v != null)
                        {
                            getString(field.Name,v,a.comment,ft,property,componentVar);
                        }
                    }
                }
            }
            saveFile(getLuaScript(property.ToString(),componentVar.ToString()));
            AssetDatabase.Refresh();
        }

        private string getLuaScript(string a1,string b1)
        {
            var lua = 
@"---@class {0}
local {0} = class('{0}');
local CSUtil = CS.CSUtil
{1}
function {0}:ctor(transform)
    self.transform = transform;
{2}
end

return {0}";
            lua = string.Format(lua,childType.Name,a1,b1);
            return lua;
        }

        private string getString(string propertyName,object value,string comment,Type t,StringBuilder psb,StringBuilder csb)
        {
            var str = "";
            // psb.AppendFormat("{0}.{1} = nil;---{2}\n",childType.Name,propertyName,comment);
            if(t == typeof(int) || t == typeof(uint) || t == typeof(float) || t == typeof(long))
            {
                csb.AppendFormat("    self.{0} = {1};---{2}\n",propertyName,value,comment);
                Debug.LogFormat("abc.{0}={1}",propertyName,value);
            }else if(t == typeof(string))
            {
                csb.AppendFormat("    self.{0} = \"{1}\";---{2}\n",propertyName,value,comment);
                Debug.LogFormat("abc.{0}=\"{1}\"\n",propertyName,value);
            }else if(t == typeof(GameObject))
            {
                var go = value as GameObject;
                var path = getPath(go.transform);
                csb.AppendFormat("    self.{0} = {1}.gameObject;---{2}\n",propertyName,path,comment);
                //csb.AppendFormat("    self.{0} = CSUtil.Find(self.transform,{1},{2});---{3}\n", propertyName, path,"GameObject",comment);
            }
            else if(value is Component)
            {
                var cm = value as Component;
               
                if(!(value is Transform))
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
            else if(t == typeof(GameObject[]))
            {
                var cms = value as GameObject[];
                var path = "{";
                foreach (var cm in cms)
                {
                    path += "\n    " + getPath(cm.transform) + ".gameObject,";
                }
                path += "\n    }";
                csb.AppendFormat("    self.{0} = {1};---{2}\n",propertyName,path,comment);
            }else if(t.BaseType == typeof(Array))
            {
                var cms = value as Component[];
                var path = "{";
                foreach (var cm in cms)
                {
                    if(!(cm is Transform))
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
                csb.AppendFormat("    self.{0} = {1};---{2}\n",propertyName,path,comment);
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

        private void saveFile(string str)
        {
            var direct = string.Format("{0}/JMain/Lua/Binder/{1}/", Application.dataPath,_directoryName);
            if (!Directory.Exists(direct)) Directory.CreateDirectory(direct);
            var filePath = string.Format("{0}{1}.lua.txt",direct,childType.Name);
            var utf8 = new System.Text.UTF8Encoding(false);
            File.WriteAllText(filePath,str,utf8);
        }
#endif
    }
}