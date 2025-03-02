using HarmonyLib;
using InnerNet;
using TMPro;
using UnityEngine;

namespace AmongUsUnknownImpostors.Patches
{
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTrackerPatch
    {
        private static void Postfix(PingTracker __instance)
        {
            var position = __instance.GetComponent<AspectPosition>();
            if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
            {
                __instance.text.alignment = TextAlignmentOptions.Top;
                position.Alignment = AspectPosition.EdgeAlignments.Top;
                __instance.text.text = $"<color=#FF0000>AmongUsUnknownImpostors v{UnknownImpostorsPlugin.VersionString}</color>\n{__instance.text.text}";
                position.DistanceFromEdge = new Vector3(1.5f, 0.11f, 0);
            }
            else
            {
                position.Alignment = AspectPosition.EdgeAlignments.LeftTop;
                __instance.text.alignment = TextAlignmentOptions.TopLeft;
                __instance.text.text = $"<color=#FF0000>AmongUsUnknownImpostors v{UnknownImpostorsPlugin.VersionString}</color>\nBy <color=#00FFFF>Fangkuai</color>\n{__instance.text.text}";
                position.DistanceFromEdge = new Vector3(0.5f, 0.11f);
            }
        }
    }
}