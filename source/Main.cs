using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using PeasOption.Reactor;

namespace PeasOption;

[BepInPlugin(Id, ModName, VersionString)]
[BepInProcess("Among Us.exe")]

public partial class PeasOption : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);

    public const string ModName = "PeasOption";

    public const string Id = "fangkuai.peasoption.develop";
    // °æ±¾
    public const string VersionString = "1.0.0";

    public override void Load()
    {
        Harmony.PatchAll();
		AddComponent<Coroutines.Component>();
	}
}
