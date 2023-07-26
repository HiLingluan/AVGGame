﻿// This source code was auto-generated by ToLua#, do not modify it
using System;
using LuaInterface;

public class UnityEngine_Texture2DWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.Texture2D), typeof(UnityEngine.Texture));
		L.RegFunction("Compress", Compress);
		L.RegFunction("ClearRequestedMipmapLevel", ClearRequestedMipmapLevel);
		L.RegFunction("IsRequestedMipmapLevelLoaded", IsRequestedMipmapLevelLoaded);
		L.RegFunction("ClearMinimumMipmapLevel", ClearMinimumMipmapLevel);
		L.RegFunction("UpdateExternalTexture", UpdateExternalTexture);
		L.RegFunction("GetRawTextureData", GetRawTextureData);
		L.RegFunction("GetPixels", GetPixels);
		L.RegFunction("GetPixels32", GetPixels32);
		L.RegFunction("PackTextures", PackTextures);
		L.RegFunction("CreateExternalTexture", CreateExternalTexture);
		L.RegFunction("SetPixel", SetPixel);
		L.RegFunction("SetPixels", SetPixels);
		L.RegFunction("GetPixel", GetPixel);
		L.RegFunction("GetPixelBilinear", GetPixelBilinear);
		L.RegFunction("LoadRawTextureData", LoadRawTextureData);
		L.RegFunction("Apply", Apply);
		L.RegFunction("Reinitialize", Reinitialize);
		L.RegFunction("ReadPixels", ReadPixels);
		L.RegFunction("GenerateAtlas", GenerateAtlas);
		L.RegFunction("SetPixels32", SetPixels32);
		L.RegFunction("New", _CreateUnityEngine_Texture2D);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("format", get_format, null);
		L.RegVar("whiteTexture", get_whiteTexture, null);
		L.RegVar("blackTexture", get_blackTexture, null);
		L.RegVar("redTexture", get_redTexture, null);
		L.RegVar("grayTexture", get_grayTexture, null);
		L.RegVar("linearGrayTexture", get_linearGrayTexture, null);
		L.RegVar("normalTexture", get_normalTexture, null);
		L.RegVar("isReadable", get_isReadable, null);
		L.RegVar("vtOnly", get_vtOnly, null);
		L.RegVar("streamingMipmaps", get_streamingMipmaps, null);
		L.RegVar("streamingMipmapsPriority", get_streamingMipmapsPriority, null);
		L.RegVar("requestedMipmapLevel", get_requestedMipmapLevel, set_requestedMipmapLevel);
		L.RegVar("minimumMipmapLevel", get_minimumMipmapLevel, set_minimumMipmapLevel);
		L.RegVar("calculatedMipmapLevel", get_calculatedMipmapLevel, null);
		L.RegVar("desiredMipmapLevel", get_desiredMipmapLevel, null);
		L.RegVar("loadingMipmapLevel", get_loadingMipmapLevel, null);
		L.RegVar("loadedMipmapLevel", get_loadedMipmapLevel, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_Texture2D(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
				UnityEngine.Texture2D obj = new UnityEngine.Texture2D(arg0, arg1);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 4 && TypeChecker.CheckTypes<UnityEngine.TextureFormat, bool>(L, 3))
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
				UnityEngine.TextureFormat arg2 = (UnityEngine.TextureFormat)ToLua.ToObject(L, 3);
				bool arg3 = LuaDLL.lua_toboolean(L, 4);
				UnityEngine.Texture2D obj = new UnityEngine.Texture2D(arg0, arg1, arg2, arg3);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 4 && TypeChecker.CheckTypes<UnityEngine.Experimental.Rendering.DefaultFormat, UnityEngine.Experimental.Rendering.TextureCreationFlags>(L, 3))
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
				UnityEngine.Experimental.Rendering.DefaultFormat arg2 = (UnityEngine.Experimental.Rendering.DefaultFormat)ToLua.ToObject(L, 3);
				UnityEngine.Experimental.Rendering.TextureCreationFlags arg3 = (UnityEngine.Experimental.Rendering.TextureCreationFlags)ToLua.ToObject(L, 4);
				UnityEngine.Texture2D obj = new UnityEngine.Texture2D(arg0, arg1, arg2, arg3);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 4 && TypeChecker.CheckTypes<UnityEngine.Experimental.Rendering.GraphicsFormat, UnityEngine.Experimental.Rendering.TextureCreationFlags>(L, 3))
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
				UnityEngine.Experimental.Rendering.GraphicsFormat arg2 = (UnityEngine.Experimental.Rendering.GraphicsFormat)ToLua.ToObject(L, 3);
				UnityEngine.Experimental.Rendering.TextureCreationFlags arg3 = (UnityEngine.Experimental.Rendering.TextureCreationFlags)ToLua.ToObject(L, 4);
				UnityEngine.Texture2D obj = new UnityEngine.Texture2D(arg0, arg1, arg2, arg3);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 5 && TypeChecker.CheckTypes<UnityEngine.TextureFormat, int, bool>(L, 3))
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
				UnityEngine.TextureFormat arg2 = (UnityEngine.TextureFormat)ToLua.ToObject(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				bool arg4 = LuaDLL.lua_toboolean(L, 5);
				UnityEngine.Texture2D obj = new UnityEngine.Texture2D(arg0, arg1, arg2, arg3, arg4);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 5 && TypeChecker.CheckTypes<UnityEngine.TextureFormat, bool, bool>(L, 3))
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
				UnityEngine.TextureFormat arg2 = (UnityEngine.TextureFormat)ToLua.ToObject(L, 3);
				bool arg3 = LuaDLL.lua_toboolean(L, 4);
				bool arg4 = LuaDLL.lua_toboolean(L, 5);
				UnityEngine.Texture2D obj = new UnityEngine.Texture2D(arg0, arg1, arg2, arg3, arg4);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else if (count == 5 && TypeChecker.CheckTypes<UnityEngine.Experimental.Rendering.GraphicsFormat, int, UnityEngine.Experimental.Rendering.TextureCreationFlags>(L, 3))
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
				UnityEngine.Experimental.Rendering.GraphicsFormat arg2 = (UnityEngine.Experimental.Rendering.GraphicsFormat)ToLua.ToObject(L, 3);
				int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.Experimental.Rendering.TextureCreationFlags arg4 = (UnityEngine.Experimental.Rendering.TextureCreationFlags)ToLua.ToObject(L, 5);
				UnityEngine.Texture2D obj = new UnityEngine.Texture2D(arg0, arg1, arg2, arg3, arg4);
				ToLua.PushSealed(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Texture2D.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Compress(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.Compress(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearRequestedMipmapLevel(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
			obj.ClearRequestedMipmapLevel();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsRequestedMipmapLevelLoaded(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
			bool o = obj.IsRequestedMipmapLevelLoaded();
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearMinimumMipmapLevel(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
			obj.ClearMinimumMipmapLevel();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateExternalTexture(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
			System.IntPtr arg0 = ToLua.CheckIntPtr(L, 2);
			obj.UpdateExternalTexture(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRawTextureData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
			byte[] o = obj.GetRawTextureData();
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPixels(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Color[] o = obj.GetPixels();
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				UnityEngine.Color[] o = obj.GetPixels(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
				UnityEngine.Color[] o = obj.GetPixels(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 6)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
				int arg4 = (int)LuaDLL.luaL_checknumber(L, 6);
				UnityEngine.Color[] o = obj.GetPixels(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.GetPixels");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPixels32(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Color32[] o = obj.GetPixels32();
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				UnityEngine.Color32[] o = obj.GetPixels32(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.GetPixels32");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PackTextures(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Texture2D[] arg0 = ToLua.CheckObjectArray<UnityEngine.Texture2D>(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				UnityEngine.Rect[] o = obj.PackTextures(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Texture2D[] arg0 = ToLua.CheckObjectArray<UnityEngine.Texture2D>(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				UnityEngine.Rect[] o = obj.PackTextures(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Texture2D[] arg0 = ToLua.CheckObjectArray<UnityEngine.Texture2D>(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				bool arg3 = LuaDLL.luaL_checkboolean(L, 5);
				UnityEngine.Rect[] o = obj.PackTextures(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.PackTextures");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateExternalTexture(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 6);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
			UnityEngine.TextureFormat arg2 = (UnityEngine.TextureFormat)ToLua.CheckObject(L, 3, typeof(UnityEngine.TextureFormat));
			bool arg3 = LuaDLL.luaL_checkboolean(L, 4);
			bool arg4 = LuaDLL.luaL_checkboolean(L, 5);
			System.IntPtr arg5 = ToLua.CheckIntPtr(L, 6);
			UnityEngine.Texture2D o = UnityEngine.Texture2D.CreateExternalTexture(arg0, arg1, arg2, arg3, arg4, arg5);
			ToLua.PushSealed(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPixel(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 4)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				UnityEngine.Color arg2 = ToLua.ToColor(L, 4);
				obj.SetPixel(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 5)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				UnityEngine.Color arg2 = ToLua.ToColor(L, 4);
				int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
				obj.SetPixel(arg0, arg1, arg2, arg3);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.SetPixel");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPixels(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Color[] arg0 = ToLua.CheckStructArray<UnityEngine.Color>(L, 2);
				obj.SetPixels(arg0);
				return 0;
			}
			else if (count == 3)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Color[] arg0 = ToLua.CheckStructArray<UnityEngine.Color>(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				obj.SetPixels(arg0, arg1);
				return 0;
			}
			else if (count == 6)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
				UnityEngine.Color[] arg4 = ToLua.CheckStructArray<UnityEngine.Color>(L, 6);
				obj.SetPixels(arg0, arg1, arg2, arg3, arg4);
				return 0;
			}
			else if (count == 7)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
				UnityEngine.Color[] arg4 = ToLua.CheckStructArray<UnityEngine.Color>(L, 6);
				int arg5 = (int)LuaDLL.luaL_checknumber(L, 7);
				obj.SetPixels(arg0, arg1, arg2, arg3, arg4, arg5);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.SetPixels");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPixel(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				UnityEngine.Color o = obj.GetPixel(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				UnityEngine.Color o = obj.GetPixel(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.GetPixel");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPixelBilinear(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
				float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
				UnityEngine.Color o = obj.GetPixelBilinear(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
				float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				UnityEngine.Color o = obj.GetPixelBilinear(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.GetPixelBilinear");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadRawTextureData(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				byte[] arg0 = ToLua.CheckByteBuffer(L, 2);
				obj.LoadRawTextureData(arg0);
				return 0;
			}
			else if (count == 3)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				System.IntPtr arg0 = ToLua.CheckIntPtr(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				obj.LoadRawTextureData(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.LoadRawTextureData");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Apply(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				obj.Apply();
				return 0;
			}
			else if (count == 2)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
				obj.Apply(arg0);
				return 0;
			}
			else if (count == 3)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
				bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
				obj.Apply(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.Apply");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Reinitialize(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				bool o = obj.Reinitialize(arg0, arg1);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 5 && TypeChecker.CheckTypes<UnityEngine.TextureFormat, bool>(L, 4))
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				UnityEngine.TextureFormat arg2 = (UnityEngine.TextureFormat)ToLua.ToObject(L, 4);
				bool arg3 = LuaDLL.lua_toboolean(L, 5);
				bool o = obj.Reinitialize(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 5 && TypeChecker.CheckTypes<UnityEngine.Experimental.Rendering.GraphicsFormat, bool>(L, 4))
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				UnityEngine.Experimental.Rendering.GraphicsFormat arg2 = (UnityEngine.Experimental.Rendering.GraphicsFormat)ToLua.ToObject(L, 4);
				bool arg3 = LuaDLL.lua_toboolean(L, 5);
				bool o = obj.Reinitialize(arg0, arg1, arg2, arg3);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.Reinitialize");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadPixels(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 4)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Rect arg0 = StackTraits<UnityEngine.Rect>.Check(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				obj.ReadPixels(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 5)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Rect arg0 = StackTraits<UnityEngine.Rect>.Check(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				bool arg3 = LuaDLL.luaL_checkboolean(L, 5);
				obj.ReadPixels(arg0, arg1, arg2, arg3);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.ReadPixels");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GenerateAtlas(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			UnityEngine.Vector2[] arg0 = ToLua.CheckStructArray<UnityEngine.Vector2>(L, 1);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
			int arg2 = (int)LuaDLL.luaL_checknumber(L, 3);
			System.Collections.Generic.List<UnityEngine.Rect> arg3 = (System.Collections.Generic.List<UnityEngine.Rect>)ToLua.CheckObject(L, 4, typeof(System.Collections.Generic.List<UnityEngine.Rect>));
			bool o = UnityEngine.Texture2D.GenerateAtlas(arg0, arg1, arg2, arg3);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPixels32(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Color32[] arg0 = ToLua.CheckStructArray<UnityEngine.Color32>(L, 2);
				obj.SetPixels32(arg0);
				return 0;
			}
			else if (count == 3)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				UnityEngine.Color32[] arg0 = ToLua.CheckStructArray<UnityEngine.Color32>(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				obj.SetPixels32(arg0, arg1);
				return 0;
			}
			else if (count == 6)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
				UnityEngine.Color32[] arg4 = ToLua.CheckStructArray<UnityEngine.Color32>(L, 6);
				obj.SetPixels32(arg0, arg1, arg2, arg3, arg4);
				return 0;
			}
			else if (count == 7)
			{
				UnityEngine.Texture2D obj = (UnityEngine.Texture2D)ToLua.CheckObject(L, 1, typeof(UnityEngine.Texture2D));
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 4);
				int arg3 = (int)LuaDLL.luaL_checknumber(L, 5);
				UnityEngine.Color32[] arg4 = ToLua.CheckStructArray<UnityEngine.Color32>(L, 6);
				int arg5 = (int)LuaDLL.luaL_checknumber(L, 7);
				obj.SetPixels32(arg0, arg1, arg2, arg3, arg4, arg5);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Texture2D.SetPixels32");
			}
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
	static int get_format(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			UnityEngine.TextureFormat ret = obj.format;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index format on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_whiteTexture(IntPtr L)
	{
		try
		{
			ToLua.PushSealed(L, UnityEngine.Texture2D.whiteTexture);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_blackTexture(IntPtr L)
	{
		try
		{
			ToLua.PushSealed(L, UnityEngine.Texture2D.blackTexture);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_redTexture(IntPtr L)
	{
		try
		{
			ToLua.PushSealed(L, UnityEngine.Texture2D.redTexture);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_grayTexture(IntPtr L)
	{
		try
		{
			ToLua.PushSealed(L, UnityEngine.Texture2D.grayTexture);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_linearGrayTexture(IntPtr L)
	{
		try
		{
			ToLua.PushSealed(L, UnityEngine.Texture2D.linearGrayTexture);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_normalTexture(IntPtr L)
	{
		try
		{
			ToLua.PushSealed(L, UnityEngine.Texture2D.normalTexture);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isReadable(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			bool ret = obj.isReadable;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index isReadable on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_vtOnly(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			bool ret = obj.vtOnly;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index vtOnly on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_streamingMipmaps(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			bool ret = obj.streamingMipmaps;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index streamingMipmaps on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_streamingMipmapsPriority(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			int ret = obj.streamingMipmapsPriority;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index streamingMipmapsPriority on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_requestedMipmapLevel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			int ret = obj.requestedMipmapLevel;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index requestedMipmapLevel on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minimumMipmapLevel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			int ret = obj.minimumMipmapLevel;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index minimumMipmapLevel on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_calculatedMipmapLevel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			int ret = obj.calculatedMipmapLevel;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index calculatedMipmapLevel on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_desiredMipmapLevel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			int ret = obj.desiredMipmapLevel;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index desiredMipmapLevel on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loadingMipmapLevel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			int ret = obj.loadingMipmapLevel;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index loadingMipmapLevel on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loadedMipmapLevel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			int ret = obj.loadedMipmapLevel;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index loadedMipmapLevel on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_requestedMipmapLevel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.requestedMipmapLevel = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index requestedMipmapLevel on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_minimumMipmapLevel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.Texture2D obj = (UnityEngine.Texture2D)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.minimumMipmapLevel = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index minimumMipmapLevel on a nil value");
		}
	}
}

