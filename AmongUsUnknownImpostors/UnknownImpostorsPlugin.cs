using AmongUsUnknownImpostors.Patches;
using AmongUsUnknownImpostors.Reactor;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace AmongUsUnknownImpostors
{
    [BepInPlugin(Id, "AmongUsUnknownImpostors", VersionString)]
    [BepInProcess("Among Us.exe")]

    public class UnknownImpostorsPlugin : BasePlugin
    {
        public const string Id = "com.fangkuai.amongusunkimpostor";
        public const string VersionString = "2.0.0";

        public Harmony Harmony { get; } = new Harmony(Id);

        public override void Load()
        {
            Language.Load();
            CustomGameOptionsData.LoadAll();
            Harmony.PatchAll();
            AddComponent<Coroutines.Component>();
        }
    }
}
