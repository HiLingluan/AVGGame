﻿// This source code was auto-generated by ToLua#, do not modify it
using System;
using LuaInterface;

public class Nova_RawImageControllerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Nova.RawImageController), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("GetRestoreData", GetRestoreData);
		L.RegFunction("Restore", Restore);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("luaGlobalName", get_luaGlobalName, set_luaGlobalName);
		L.RegVar("material", null, set_material);
		L.RegVar("sharedMaterial", get_sharedMaterial, null);
		L.RegVar("restorableName", get_restorableName, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRestoreData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Nova.RawImageController obj = (Nova.RawImageController)ToLua.CheckObject<Nova.RawImageController>(L, 1);
			Nova.IRestoreData o = obj.GetRestoreData();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Restore(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Nova.RawImageController obj = (Nova.RawImageController)ToLua.CheckObject<Nova.RawImageController>(L, 1);
			Nova.IRestoreData arg0 = (Nova.IRestoreData)ToLua.CheckObject<Nova.IRestoreData>(L, 2);
			obj.Restore(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_luaGlobalName(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Nova.RawImageController obj = (Nova.RawImageController)o;
			string ret = obj.luaGlobalName;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaGlobalName on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sharedMaterial(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Nova.RawImageController obj = (Nova.RawImageController)o;
			UnityEngine.Material ret = obj.sharedMaterial;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index sharedMaterial on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_restorableName(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Nova.RawImageController obj = (Nova.RawImageController)o;
			string ret = obj.restorableName;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index restorableName on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_luaGlobalName(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Nova.RawImageController obj = (Nova.RawImageController)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.luaGlobalName = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index luaGlobalName on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_material(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Nova.RawImageController obj = (Nova.RawImageController)o;
			UnityEngine.Material arg0 = (UnityEngine.Material)ToLua.CheckObject<UnityEngine.Material>(L, 2);
			obj.material = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index material on a nil value");
		}
	}
}
