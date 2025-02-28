using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace FontChange;

[BepInPlugin(Id, ModName, VersionString)]
[BepInProcess("Among Us.exe")]

public partial class FontChangePlugin : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);
    public const string ModName = "FontChange";
    public const string Id = "xlh.developer.FontChange";
    // 模组版本
    public const string VersionString = "1.1.0";


    public override void Load()
    {
        AssetLoader.LoadAssets();
        Harmony.PatchAll();
    }
}
