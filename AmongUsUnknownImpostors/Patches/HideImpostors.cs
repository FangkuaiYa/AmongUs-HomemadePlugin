using AmongUs.GameOptions;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AmongUsUnknownImpostors.Patches
{
    internal class HideImpostors
    {
        [HarmonyPatch(typeof(ChatController), nameof(ChatController.AddChat))]
        public static class ChatController_AddChat
        {
            public static void Postfix(ChatController __instance, PlayerControl sourcePlayer)
            {
                if (!CustomGameOptionsData.unkImpostor.Get()) return;
                
                if (!sourcePlayer || !PlayerControl.LocalPlayer)
                {
                    return;
                }

                NetworkedPlayerInfo data = PlayerControl.LocalPlayer.Data;
                NetworkedPlayerInfo data2 = sourcePlayer.Data;
                if (data2 == null || data == null || (data2.IsDead && !data.IsDead))
                {
                    return;
                }

                var activeChildren = __instance.chatBubblePool.activeChildren;

                ChatBubble chatBubble = activeChildren[activeChildren.Count - 1].Cast<ChatBubble>();

                if (data2.Role.IsImpostor && data2.Object != PlayerControl.LocalPlayer)
                {
                    chatBubble.NameText.color = Color.white;
                    chatBubble.ColorBlindName.color = Color.white;
                }
            }
        }

        //Patch that makes impostors along in intro cutscene
        [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.BeginImpostor))]
        public static class IntroCutscene_BeginImpostor
        {
            public static void Prefix(IntroCutscene __instance, Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam)
            {
                if (!CustomGameOptionsData.unkImpostor.Get()) return;
                yourTeam.Clear();
                yourTeam.Add(PlayerControl.LocalPlayer);
            }
        }

        //Patch that hide other impostors in Meeting HUD
        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
        public static class MeetingHud_Start
        {
            static void Postfix(MeetingHud __instance)
            {
                if (!CustomGameOptionsData.unkImpostor.Get()) return;
                
                if (PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data.Role.IsImpostor)
                {
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                    {
                        if (player.Data.Role.IsImpostor && player != PlayerControl.LocalPlayer)
                        {
                            setPlayerNameColor(player, Color.white);
                        }
                    }
                }
            }
            static void setPlayerNameColor(PlayerControl p, Color color)
            {
                if (MeetingHud.Instance != null)
                    foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates)
                        if (player.NameText != null && p.PlayerId == player.TargetPlayerId)
                            player.NameText.color = color;
            }
        }

        //Patch that fixes Kill button between impostors
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
        public static class PlayerControlFixedUpdatePatch
        {
            public static void Postfix(PlayerControl __instance)
            {
                if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started || GameOptionsManager.Instance.currentGameOptions.GameMode == GameModes.HideNSeek || !CustomGameOptionsData.unkImpostor.Get()) return;
                impostorSetTarget();
            }
            static void impostorSetTarget()
            {
                if (!PlayerControl.LocalPlayer.Data.Role.IsImpostor || !PlayerControl.LocalPlayer.CanMove || PlayerControl.LocalPlayer.Data.IsDead)
                {
                    DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(null);
                    return;
                }

                PlayerControl target = null;
                target = setTarget(false, true);

                DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(target); // Includes setPlayerOutline(target, Palette.ImpstorRed);
            }

            static PlayerControl setTarget(bool onlyCrewmates = false, bool targetPlayersInVents = false, List<PlayerControl> untargetablePlayers = null, PlayerControl targetingPlayer = null)
            {
                PlayerControl result = null;
                float num = AmongUs.GameOptions.GameOptionsData.KillDistances[Mathf.Clamp(GameOptionsManager.Instance.currentNormalGameOptions.KillDistance, 0, 2)];
                if (!ShipStatus.Instance) return result;
                if (targetingPlayer == null) targetingPlayer = PlayerControl.LocalPlayer;
                if (targetingPlayer.Data.IsDead) return result;

                untargetablePlayers ??= new List<PlayerControl>();

                Vector2 truePosition = targetingPlayer.GetTruePosition();
                foreach (var playerInfo in GameData.Instance.AllPlayers.ToArray())
                {
                    if (!playerInfo.Disconnected && playerInfo.PlayerId != targetingPlayer.PlayerId && !playerInfo.IsDead && (!onlyCrewmates || !playerInfo.Role.IsImpostor))
                    {
                        PlayerControl @object = playerInfo.Object;
                        if (untargetablePlayers != null && untargetablePlayers.Any(x => x == @object))
                        {
                            // if that player is not targetable: skip check
                            continue;
                        }

                        if (@object && (!@object.inVent || targetPlayersInVents))
                        {
                            Vector2 vector = @object.GetTruePosition() - truePosition;
                            float magnitude = vector.magnitude;
                            if (magnitude <= num && !PhysicsHelpers.AnyNonTriggersBetween(truePosition, vector.normalized, magnitude, Constants.ShipAndObjectsMask))
                            {
                                result = @object;
                                num = magnitude;
                            }
                        }
                    }
                }
                return result;
            }
        }

        //Patch that sets player name color when impostors are chosen
        [HarmonyPatch(typeof(RoleManager), nameof(RoleManager.SelectRoles))]
        public static class PlayerControl_RpcSetInfected
        {
            public static void Postfix(PlayerControl __instance)
            {
                if (!CustomGameOptionsData.unkImpostor.Get()) return;
                var infected = GameData.Instance.AllPlayers.ToArray().Where(o => o.Role.IsImpostor).ToArray();
                for (int j = 0; j < infected.Length; j++)
                {
                    NetworkedPlayerInfo playerById2 = infected[j];
                    if (playerById2 != null && playerById2.Object != PlayerControl.LocalPlayer)
                    {
                        playerById2.Object.cosmetics.nameText.color = Color.white;
                    }
                }
            }
        }
    }
}