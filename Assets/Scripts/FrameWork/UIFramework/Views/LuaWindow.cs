using System;
using UnityEngine;
using XLua;

namespace Game.UI
{
    [LuaCallCSharp]
    public class LuaWindowData
    {
        public LuaTable param;
    }

    [LuaCallCSharp]
    public class LuaWindow
    {
        private string name;

        public ScriptReference script;
        public VariableArray variables;

        protected LuaTable scriptEnv;
        protected LuaTable metatable;
        protected Action<LuaWindow> onCreate;
        protected Action<LuaWindow> onShow;
        protected Action<LuaWindow> onHide;
        protected Action<LuaWindow> onDismiss;
        protected Action<LuaWindow,object> onSetParam;
        
        private bool initialized = false;

        //LuaWindowData windowData;
        public virtual LuaTable GetMetatable()
        {
            return this.metatable;
        }

        protected virtual void Initialize()
        {
            if (initialized)
                return;

            initialized = true;

            var luaEnv = GameEntry.Lua.luaEnv;
            scriptEnv = luaEnv.NewTable();

            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            scriptEnv.Set("target", this);
            string scriptText = string.Format("local cls = require(\"{0}\");return extends(target,cls);", script.GetFullFileName());
            object[] result = luaEnv.DoString(scriptText, string.Format("{0}({1})", "LuaWindow", this.name), scriptEnv);

            if (result.Length != 1 || !(result[0] is LuaTable))
                throw new Exception("");

            metatable = (LuaTable)result[0];

            if (variables != null && variables.Variables != null)
            {
                foreach (var variable in variables.Variables)
                {
                    var name = variable.Name.Trim();
                    if (string.IsNullOrEmpty(name))
                        continue;

                    metatable.Set(name, variable.GetValue());
                }
            }

            onCreate = metatable.Get<Action<LuaWindow>>("onCreate");
            onShow = metatable.Get<Action<LuaWindow>>("onShow");
            onHide = metatable.Get<Action<LuaWindow>>("onHide");
            onDismiss = metatable.Get<Action<LuaWindow>>("onDismiss");
        }
        
        public void Create()
        {

        }

        protected  void OnCreate()
        {
            if (!initialized)
                Initialize();

            if (onCreate != null)
                onCreate(this);
        }
        public void Show()
        {

        }

        protected  void OnShow()
        {
            if (onShow != null)
                onShow(this);
        }

        public void Hide()
        {

        }
        protected void OnHide()
        {
            if (onHide != null)
                onHide(this);
            
            //GameEntry.JUI.CloseWindow(this,null);
        }

        public void Dismiss()
        {

        }

        protected void OnDismiss()
        {
            if (onDismiss != null)
                onDismiss(this);

            onCreate = null;
            onShow = null;
            onHide = null;
            onDismiss = null;

            if (metatable != null)
            {
                metatable.Dispose();
                metatable = null;
            }

            if (scriptEnv != null)
            {
                scriptEnv.Dispose();
                scriptEnv = null;
            }
        }

        public void OnRecycle()
        {
            OnDismiss();
        }
    }
}