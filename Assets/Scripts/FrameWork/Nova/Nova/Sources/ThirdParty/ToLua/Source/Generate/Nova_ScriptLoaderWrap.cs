﻿// This source code was auto-generated by ToLua#, do not modify it
using System;
using LuaInterface;

public class Nova_ScriptLoaderWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Nova.ScriptLoader), typeof(System.Object));
		L.RegFunction("Init", Init);
		L.RegFunction("ForceInit", ForceInit);
		L.RegFunction("GetFlowChartGraph", GetFlowChartGraph);
		L.RegFunction("AddDeferredDialogueChunks", AddDeferredDialogueChunks);
		L.RegFunction("RegisterNewNode", RegisterNewNode);
		L.RegFunction("AddLocalizedNode", AddLocalizedNode);
		L.RegFunction("RegisterJump", RegisterJump);
		L.RegFunction("RegisterBranch", RegisterBranch);
		L.RegFunction("AddLocalizedBranch", AddLocalizedBranch);
		L.RegFunction("EndRegisterBranch", EndRegisterBranch);
		L.RegFunction("SetCurrentAsChapter", SetCurrentAsChapter);
		L.RegFunction("SetCurrentAsStart", SetCurrentAsStart);
		L.RegFunction("SetCurrentAsUnlockedStart", SetCurrentAsUnlockedStart);
		L.RegFunction("SetCurrentAsDebug", SetCurrentAsDebug);
		L.RegFunction("SetCurrentAsEnd", SetCurrentAsEnd);
		L.RegFunction("New", _CreateNova_ScriptLoader);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("stateLocale", get_stateLocale, set_stateLocale);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNova_ScriptLoader(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				Nova.ScriptLoader obj = new Nova.ScriptLoader();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: Nova.ScriptLoader.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.Init(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForceInit(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.ForceInit(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFlowChartGraph(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			Nova.FlowChartGraph o = obj.GetFlowChartGraph();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddDeferredDialogueChunks(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			Nova.FlowChartNode arg0 = (Nova.FlowChartNode)ToLua.CheckObject<Nova.FlowChartNode>(L, 2);
			obj.AddDeferredDialogueChunks(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterNewNode(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			obj.RegisterNewNode(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddLocalizedNode(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			obj.AddLocalizedNode(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterJump(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.RegisterJump(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterBranch(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 7);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			string arg2 = ToLua.CheckString(L, 4);
			Nova.BranchImageInformation arg3 = (Nova.BranchImageInformation)ToLua.CheckObject<Nova.BranchImageInformation>(L, 5);
			Nova.BranchMode arg4 = (Nova.BranchMode)ToLua.CheckObject(L, 6, typeof(Nova.BranchMode));
			LuaFunction arg5 = ToLua.CheckLuaFunction(L, 7);
			obj.RegisterBranch(arg0, arg1, arg2, arg3, arg4, arg5);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddLocalizedBranch(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			string arg2 = ToLua.CheckString(L, 4);
			obj.AddLocalizedBranch(arg0, arg1, arg2);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EndRegisterBranch(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			obj.EndRegisterBranch();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCurrentAsChapter(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			obj.SetCurrentAsChapter();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCurrentAsStart(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			obj.SetCurrentAsStart();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCurrentAsUnlockedStart(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			obj.SetCurrentAsUnlockedStart();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCurrentAsDebug(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			obj.SetCurrentAsDebug();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCurrentAsEnd(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)ToLua.CheckObject<Nova.ScriptLoader>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.SetCurrentAsEnd(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stateLocale(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)o;
			UnityEngine.SystemLanguage ret = obj.stateLocale;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index stateLocale on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stateLocale(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Nova.ScriptLoader obj = (Nova.ScriptLoader)o;
			UnityEngine.SystemLanguage arg0 = (UnityEngine.SystemLanguage)ToLua.CheckObject(L, 2, typeof(UnityEngine.SystemLanguage));
			obj.stateLocale = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index stateLocale on a nil value");
		}
	}
}

