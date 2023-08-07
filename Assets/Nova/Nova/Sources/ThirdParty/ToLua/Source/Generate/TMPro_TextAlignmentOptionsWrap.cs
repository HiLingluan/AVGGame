﻿// This source code was auto-generated by ToLua#, do not modify it
using System;
using LuaInterface;

public class TMPro_TextAlignmentOptionsWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(TMPro.TextAlignmentOptions));
		L.RegVar("TopLeft", get_TopLeft, null);
		L.RegVar("Top", get_Top, null);
		L.RegVar("TopRight", get_TopRight, null);
		L.RegVar("TopJustified", get_TopJustified, null);
		L.RegVar("TopFlush", get_TopFlush, null);
		L.RegVar("TopGeoAligned", get_TopGeoAligned, null);
		L.RegVar("Left", get_Left, null);
		L.RegVar("Center", get_Center, null);
		L.RegVar("Right", get_Right, null);
		L.RegVar("Justified", get_Justified, null);
		L.RegVar("Flush", get_Flush, null);
		L.RegVar("CenterGeoAligned", get_CenterGeoAligned, null);
		L.RegVar("BottomLeft", get_BottomLeft, null);
		L.RegVar("Bottom", get_Bottom, null);
		L.RegVar("BottomRight", get_BottomRight, null);
		L.RegVar("BottomJustified", get_BottomJustified, null);
		L.RegVar("BottomFlush", get_BottomFlush, null);
		L.RegVar("BottomGeoAligned", get_BottomGeoAligned, null);
		L.RegVar("BaselineLeft", get_BaselineLeft, null);
		L.RegVar("Baseline", get_Baseline, null);
		L.RegVar("BaselineRight", get_BaselineRight, null);
		L.RegVar("BaselineJustified", get_BaselineJustified, null);
		L.RegVar("BaselineFlush", get_BaselineFlush, null);
		L.RegVar("BaselineGeoAligned", get_BaselineGeoAligned, null);
		L.RegVar("MidlineLeft", get_MidlineLeft, null);
		L.RegVar("Midline", get_Midline, null);
		L.RegVar("MidlineRight", get_MidlineRight, null);
		L.RegVar("MidlineJustified", get_MidlineJustified, null);
		L.RegVar("MidlineFlush", get_MidlineFlush, null);
		L.RegVar("MidlineGeoAligned", get_MidlineGeoAligned, null);
		L.RegVar("CaplineLeft", get_CaplineLeft, null);
		L.RegVar("Capline", get_Capline, null);
		L.RegVar("CaplineRight", get_CaplineRight, null);
		L.RegVar("CaplineJustified", get_CaplineJustified, null);
		L.RegVar("CaplineFlush", get_CaplineFlush, null);
		L.RegVar("CaplineGeoAligned", get_CaplineGeoAligned, null);
		L.RegVar("Converted", get_Converted, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
		TypeTraits<TMPro.TextAlignmentOptions>.Check = CheckType;
		StackTraits<TMPro.TextAlignmentOptions>.Push = Push;
	}

	static void Push(IntPtr L, TMPro.TextAlignmentOptions arg)
	{
		ToLua.Push(L, arg);
	}

	static bool CheckType(IntPtr L, int pos)
	{
		return TypeChecker.CheckEnumType(typeof(TMPro.TextAlignmentOptions), L, pos);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TopLeft(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.TopLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Top(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Top);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TopRight(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.TopRight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TopJustified(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.TopJustified);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TopFlush(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.TopFlush);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TopGeoAligned(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.TopGeoAligned);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Left(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Left);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Center(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Center);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Right(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Right);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Justified(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Justified);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Flush(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Flush);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CenterGeoAligned(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.CenterGeoAligned);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BottomLeft(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.BottomLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Bottom(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Bottom);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BottomRight(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.BottomRight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BottomJustified(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.BottomJustified);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BottomFlush(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.BottomFlush);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BottomGeoAligned(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.BottomGeoAligned);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BaselineLeft(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.BaselineLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Baseline(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Baseline);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BaselineRight(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.BaselineRight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BaselineJustified(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.BaselineJustified);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BaselineFlush(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.BaselineFlush);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BaselineGeoAligned(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.BaselineGeoAligned);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MidlineLeft(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.MidlineLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Midline(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Midline);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MidlineRight(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.MidlineRight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MidlineJustified(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.MidlineJustified);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MidlineFlush(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.MidlineFlush);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MidlineGeoAligned(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.MidlineGeoAligned);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CaplineLeft(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.CaplineLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Capline(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Capline);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CaplineRight(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.CaplineRight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CaplineJustified(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.CaplineJustified);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CaplineFlush(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.CaplineFlush);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CaplineGeoAligned(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.CaplineGeoAligned);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Converted(IntPtr L)
	{
		ToLua.Push(L, TMPro.TextAlignmentOptions.Converted);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		TMPro.TextAlignmentOptions o = (TMPro.TextAlignmentOptions)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}
