using HarmonyLib;
using UnityEngine;

namespace AmongUsUnknownImpostors.Patches
{
    class LightSabotageFix
    {
        [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.CalculateLightRadius))]
        public static class ShipStatus_CalculateLightRadius
        {
            public static bool Prefix(ShipStatus __instance, NetworkedPlayerInfo player, ref float __result)
            {
                if (!CustomGameOptionsData.unkImpostor.Get()) return true;
                if (player == null || player.IsDead)
                {
                    __result = __instance.MaxLightRadius;
                    return false;
                }

                SwitchSystem switchSystem = __instance.Systems[SystemTypes.Electrical].Cast<SwitchSystem>();
                float num = (float) switchSystem.Value / 255f;
                if (player.Role.IsImpostor)
                {
                    __result = Mathf.Lerp(__instance.MinLightRadius, __instance.MaxLightRadius, num) *
                               Mathf.Lerp(CustomGameOptionsData.impoVision.Get(), GameOptionsManager.Instance.currentNormalGameOptions.ImpostorLightMod, num);
                    return false;
                }

                __result = Mathf.Lerp(__instance.MinLightRadius, __instance.MaxLightRadius, num) *
                           GameOptionsManager.Instance.currentNormalGameOptions.CrewLightMod;
                return false;
            }
        }
    }
}