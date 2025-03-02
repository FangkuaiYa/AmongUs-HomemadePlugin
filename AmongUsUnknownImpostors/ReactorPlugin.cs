using BepInEx.Unity.IL2CPP.Utils;
using Il2CppInterop.Runtime.Injection;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AmongUsUnknownImpostors.Reactor;

public static class Reactor
{
	public static void Destroy(this UnityEngine.Object obj)
	{
		UnityEngine.Object.Destroy(obj);
	}

	public static void DestroyImmediate(this UnityEngine.Object obj)
	{
		UnityEngine.Object.DestroyImmediate(obj);
	}

	public static void SetSize(this RectTransform rectTransform, float width, float height)
	{
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
	}
}

public static class Coroutines
{
	[RegisterInIl2Cpp]
	internal class Component : MonoBehaviour
	{
		internal static Component Instance { get; set; }

		public Component(IntPtr ptr) : base(ptr)
		{
		}

		private void Awake()
		{
			Instance = this;
		}

		private void OnDestroy()
		{
			Instance = null;
		}
	}

	private static readonly ConditionalWeakTable<IEnumerator, Coroutine> _ourCoroutineStore = new();

	[return: NotNullIfNotNull("routine")]
	public static IEnumerator Start(IEnumerator routine)
	{
		if (routine != null)
		{
			_ourCoroutineStore.AddOrUpdate(routine, Component.Instance!.StartCoroutine(routine));
		}

		return routine;
	}

	public static void Stop(IEnumerator enumerator)
	{
		if (enumerator != null && _ourCoroutineStore.TryGetValue(enumerator, out var routine))
		{
			Component.Instance!.StopCoroutine(routine);
		}
	}
}
[AttributeUsage(AttributeTargets.Class)]
public class RegisterInIl2CppAttribute : Attribute
{
	public Type[] Interfaces { get; }

	public RegisterInIl2CppAttribute()
	{
		Interfaces = Type.EmptyTypes;
	}

	public RegisterInIl2CppAttribute(params Type[] interfaces)
	{
		Interfaces = interfaces;
	}

	[Obsolete("You don't need to call this anymore", true)]
	public static void Register()
	{
	}

	private static void Register(Type type, Type[] interfaces)
	{
		var baseTypeAttribute = type.BaseType?.GetCustomAttribute<RegisterInIl2CppAttribute>();
		if (baseTypeAttribute != null)
		{
			Register(type.BaseType!, baseTypeAttribute.Interfaces);
		}

		if (ClassInjector.IsTypeRegisteredInIl2Cpp(type))
		{
			return;
		}

		try
		{
			ClassInjector.RegisterTypeInIl2Cpp(type, new RegisterTypeOptions { Interfaces = interfaces });
		}
		catch { }
	}

	public static void Register(Assembly assembly)
	{
		foreach (var type in assembly.GetTypes())
		{
			var attribute = type.GetCustomAttribute<RegisterInIl2CppAttribute>();
			if (attribute != null)
			{
				Register(type, attribute.Interfaces);
			}
		}
	}
}
