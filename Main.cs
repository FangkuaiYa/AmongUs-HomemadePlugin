using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Il2CppSystem.Collections.Generic;
using Reactor.Patches;
using System;
using System.Linq;
using System.Reflection;
using static FontChange.FontChange;
using UnityEngine;

namespace FontChange;

[BepInPlugin(Id, ModName, VersionString)]
[BepInProcess("Among Us.exe")]

public partial class FontChangePlugin : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);
    public const string ModName = "FontChange";
    public const string Id = "xlh.developer.FontChange";
    // 模组版本
    public const string VersionString = "1.0.0";


    public override void Load()
    {
        AssetLoader.LoadAssets();
        Harmony.PatchAll();
        ReactorVersionShower.TextUpdated += text =>
        {
            text.font = AssetLoader.font;
            text.text = "<color=#00FFFF>感谢使用方块の字体插件</color>";
        };
    }
}
