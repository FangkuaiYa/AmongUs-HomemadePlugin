using HarmonyLib;

namespace PeasOption;

[HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
public static class PingTracker_Update
{
	public static void Postfix(PingTracker __instance)
	{
		__instance.text.text +=
		"\n<color=#EA0000>PeasOption</color>";
	}
}

