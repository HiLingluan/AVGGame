﻿// This source code was auto-generated by ToLua#, do not modify it
using System;
using LuaInterface;

public class Nova_LazyComputableAnimationProperty_float_floatWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Nova.LazyComputableAnimationProperty<float,float>), typeof(System.Object), "LazyComputableAnimationProperty_float_float");
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("value", get_value, set_value);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_value(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Nova.LazyComputableAnimationProperty<float,float> obj = (Nova.LazyComputableAnimationProperty<float,float>)o;
			float ret = obj.value;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index value on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_value(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Nova.LazyComputableAnimationProperty<float,float> obj = (Nova.LazyComputableAnimationProperty<float,float>)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.value = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index value on a nil value");
		}
	}
}

