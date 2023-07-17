using Loxodon.Framework;
using System;
using UnityEngine;
using XLua;
namespace Game
{

    public class LuaLooper : MonoBehaviour
    {

        [CSharpCallLua]
        public delegate void Arg2FloatRetVoid(float a, float b);

        [CSharpCallLua]
        public delegate void Arg1FloatRetVoid(float a);

        public Arg2FloatRetVoid luaUpdate
        {
            get;
            private set;
        }

        public Action luaLateUpdate
        {
            get;
            private set;
        }

        public Arg1FloatRetVoid luaFixedUpdate
        {
            get;
            private set;
        }

        void Awake()
        {
            GameObject.DontDestroyOnLoad(gameObject);
        }

        public void Init()
        {
            LuaEnv luaEnv = GameEntry.Lua.luaEnv;
            luaUpdate = luaEnv.Global.Get<Arg2FloatRetVoid>("Update");
            luaLateUpdate = luaEnv.Global.Get<Action>("LateUpdate");
            luaFixedUpdate = luaEnv.Global.Get<Arg1FloatRetVoid>("FixUpdate");
        }

        void Update()
        {
            luaUpdate?.Invoke(Time.deltaTime, Time.unscaledDeltaTime);
        }

        void LateUpdate()
        {
            luaLateUpdate?.Invoke();
        }

        void FixedUpdate()
        {
            luaFixedUpdate?.Invoke(Time.fixedDeltaTime);
        }

        void OnDestroy()
        {
            luaUpdate = null;
            luaLateUpdate = null;
            luaFixedUpdate = null;
        }
    }
}
