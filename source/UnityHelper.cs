using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace PeasOption;

public static class UnityHelper
{

	public static Dictionary<string, Sprite> CachedSprites = new();

	public static Sprite loadSpriteFromResources(string path, float pixelsPerUnit)
	{
		try
		{
			if (CachedSprites.TryGetValue(path + pixelsPerUnit, out var sprite)) return sprite;
			Texture2D texture = loadTextureFromResources(path);
			sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
			sprite.hideFlags |= HideFlags.HideAndDontSave | HideFlags.DontSaveInEditor;
			return CachedSprites[path + pixelsPerUnit] = sprite;
		}
		catch
		{
			System.Console.WriteLine("Error loading sprite from path: " + path);
		}
		return null;
	}

	public static Sprite loadSpriteFromResource(Texture2D texture, float pixelsPerUnit, Rect textureRect, Vector2 pivot)
	{
		return Sprite.Create(texture, textureRect, pivot, pixelsPerUnit);
	}

	public static unsafe Texture2D loadTextureFromResources(string path)
	{
		try
		{
			Texture2D texture = new(2, 2, TextureFormat.ARGB32, true);
			Assembly assembly = Assembly.GetExecutingAssembly();
			Stream stream = assembly.GetManifestResourceStream(path);
			var length = stream.Length;
			var byteTexture = new Il2CppStructArray<byte>(length);
			stream.Read(new Span<byte>(IntPtr.Add(byteTexture.Pointer, IntPtr.Size * 4).ToPointer(), (int)length));
			if (path.Contains("HorseHats"))
			{
				byteTexture = new Il2CppStructArray<byte>(byteTexture.Reverse().ToArray());
			}
			ImageConversion.LoadImage(texture, byteTexture, false);
			return texture;
		}
		catch
		{
			System.Console.WriteLine("Error loading texture from resources: " + path);
		}
		return null;
	}

	public static Texture2D loadTextureFromDisk(string path)
	{
		if (File.Exists(path))
		{
			Texture2D texture = new(2, 2, TextureFormat.ARGB32, true);
			var byteTexture = Il2CppSystem.IO.File.ReadAllBytes(path);
			ImageConversion.LoadImage(texture, byteTexture, false);
			return texture;
		}
		return null;
	}
}
