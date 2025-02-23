using HarmonyLib;
using InnerNet;
using TMPro;
using UnityEngine;

namespace FontChange;

[HarmonyPatch]
public static class FontChange
{
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTrackerPatch
    {
        public static Sprite commsdown;
        private static void Postfix(PingTracker __instance)
        {
            if (!__instance.GetComponentInChildren<SpriteRenderer>() && AmongUsClient.Instance.Ping > 300)
            {
                var spriteObject = new GameObject("WIFI Sprite");
                spriteObject.AddComponent<SpriteRenderer>().sprite = commsdown;
                spriteObject.transform.parent = __instance.transform;
                spriteObject.transform.localPosition = new Vector3(-1f, -0.3f, -1);
                spriteObject.transform.localScale *= 0.72f;
            }
            AspectPosition position = __instance.GetComponent<AspectPosition>();
            __instance.text.font = AssetLoader.font;
            if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
            {
                __instance.text.alignment = TextAlignmentOptions.Top;
                position.Alignment = AspectPosition.EdgeAlignments.Top;
                __instance.text.text = $"{__instance.text.text}\n<color=#00FFFF>>>方块の插件<<</color>\n";
                position.DistanceFromEdge = new Vector3(1.5f, 0.11f, 0);
            }
            else
            {
                position.Alignment = AspectPosition.EdgeAlignments.LeftTop;
                __instance.text.alignment = TextAlignmentOptions.TopLeft;
                __instance.text.text =
                    $"{__instance.text.text}\n<color=#00FFFF>>>方块の插件<<</color>\nfangkuai.fun";
                position.DistanceFromEdge = new Vector3(0.5f, 0.11f);
            }
        }
    }
    [HarmonyPatch(typeof(GameSettingMenu), nameof(GameSettingMenu.ChangeTab))]
    internal class GameOptionsMenuChangeTabPatch
    {
        public static void Postfix(GameSettingMenu __instance, int tabNum, bool previewOnly)
        {
            __instance.MenuDescriptionText.font = AssetLoader.font;
            __instance.GameSettingsTab.numberOptionOrigin.ValueText.font = AssetLoader.font;
            __instance.GameSettingsTab.stringOptionOrigin.ValueText.font = AssetLoader.font;
            __instance.GameSettingsTab.numberOptionOrigin.TitleText.font = AssetLoader.font;
            __instance.GameSettingsTab.playerOptionOrigin.TitleText.font = AssetLoader.font;
            __instance.GameSettingsTab.stringOptionOrigin.TitleText.font = AssetLoader.font;
            __instance.GameSettingsTab.categoryHeaderOrigin.Title.font = AssetLoader.font;
            __instance.GameSettingsTab.checkboxOrigin.TitleText.font = AssetLoader.font;
            __instance.PresetsTab.AlternateRulesText.font = AssetLoader.font;
            __instance.PresetsTab.PresetDescriptionText.font = AssetLoader.font;
            __instance.PresetsTab.StandardRulesText.font = AssetLoader.font;
            __instance.GameSettingsButton.buttonText.font = AssetLoader.font;
            __instance.GamePresetsButton.buttonText.font = AssetLoader.font;
            __instance.RoleSettingsButton.buttonText.font = AssetLoader.font;
            __instance.RoleSettingsTab.roleTitleText.font = AssetLoader.font;
            __instance.RoleSettingsTab.roleHeaderText.font = AssetLoader.font;
            __instance.RoleSettingsTab.roleDescriptionText.font = AssetLoader.font;
            __instance.RoleSettingsTab.advHeader.Title.font = AssetLoader.font;
            __instance.RoleSettingsTab.AllButton.buttonText.font = AssetLoader.font;
            __instance.RoleSettingsTab.numberOptionOrigin.ValueText.font = AssetLoader.font;
            __instance.RoleSettingsTab.stringOptionOrigin.ValueText.font = AssetLoader.font;
            __instance.RoleSettingsTab.numberOptionOrigin.TitleText.font = AssetLoader.font;
            __instance.RoleSettingsTab.categoryHeaderEditRoleOrigin.Title.font = AssetLoader.font;
            __instance.RoleSettingsTab.stringOptionOrigin.TitleText.font = AssetLoader.font;
            __instance.RoleSettingsTab.checkboxOrigin.TitleText.font = AssetLoader.font;
            __instance.RoleSettingsTab.roleOptionSettingOrigin.titleText.font = AssetLoader.font;
        }
    }
    [HarmonyPatch(typeof(LobbyViewSettingsPane), nameof(LobbyViewSettingsPane.ChangeTab))]
    internal class LobbyViewSettingsPaneChangeTabPatch
    {
        public static void Postfix(LobbyViewSettingsPane __instance, StringNames category)
        {
            __instance.gameModeText.font = AssetLoader.font;

            __instance.rolesTabButton.buttonText.font = AssetLoader.font;

            __instance.categoryHeaderRoleOrigin.Title.font = AssetLoader.font;

            __instance.categoryHeaderOrigin.Title.font = AssetLoader.font;

            __instance.infoPanelOrigin.titleText.font = AssetLoader.font;
            __instance.infoPanelOrigin.settingText.font = AssetLoader.font;

            __instance.infoPanelRoleOrigin.titleText.font = AssetLoader.font;
            __instance.infoPanelRoleOrigin.titleText.font = AssetLoader.font;
            __instance.infoPanelRoleOrigin.settingText.font = AssetLoader.font;
            __instance.infoPanelRoleOrigin.chanceText.font = AssetLoader.font;

            __instance.taskTabButton.buttonText.font = AssetLoader.font;
        }
    }
    [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Update))]
    public class GameStartManagerUpdatePatch
    {
        public static void Postfix(GameStartManager __instance)
        {
            __instance.GameStartText.font = AssetLoader.font;
            __instance.GameStartTextClient.font = AssetLoader.font;
            __instance.privatePublicPanelText.font = AssetLoader.font;
            __instance.RulesPresetText.font = AssetLoader.font;

            __instance.ClientViewButton.buttonText.font = AssetLoader.font;
            __instance.HostViewButton.buttonText.font = AssetLoader.font;
            __instance.HostPrivateButton.buttonText.font = AssetLoader.font;
            __instance.HostPublicButton.buttonText.font = AssetLoader.font;
            __instance.HostViewButton.buttonText.font = AssetLoader.font;
            __instance.StartButton.buttonText.font = AssetLoader.font;
            __instance.EditButton.buttonText.font = AssetLoader.font;
            __instance.GameRoomNameCode.font = AssetLoader.font;
            __instance.PlayerCounter.font = AssetLoader.font;
            __instance.HostInfoPanel.hostLabel.font = AssetLoader.font;
            __instance.HostInfoPanel.playerName.font = AssetLoader.font;
        }
    }
    [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.CoBegin))]
    class IntroCutsceneCoBeginPatch
    {
        private static void Postfix(IntroCutscene __instance, ref Il2CppSystem.Collections.IEnumerator __result)
        {
            SoundManager.Instance.StopSound(LobbyBehaviourStartPatch.MapTheme);
            __instance.HideAndSeekTimerText.font = AssetLoader.font;
            __instance.ImpostorText.font = AssetLoader.font;
            __instance.RoleBlurbText.font = AssetLoader.font;
            __instance.RoleText.font = AssetLoader.font;
            __instance.YouAreText.font = AssetLoader.font;
            __instance.ImpostorName.font = AssetLoader.font;
            __instance.ImpostorTitle.font = AssetLoader.font;
            __instance.TeamTitle.font = AssetLoader.font;
        }
    }
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public static class PlayerControlFixedUpdatePatch
    {
        public static void Postfix(PlayerControl __instance)
        {
            DestroyableSingleton<HudManager>.Instance.TaskPanel.taskText.font = AssetLoader.font;
            DestroyableSingleton<HudManager>.Instance.TaskPanel.taskText.color = Color.black;
            DestroyableSingleton<HudManager>.Instance.roomTracker.text.font = AssetLoader.font;
            DestroyableSingleton<HudManager>.Instance.roomTracker.text.color = Color.black;
        }
    }
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.Update))]
    public static class PlayerChatPatch
    {
        public static void Postfix(ChatController __instance)
        {
            __instance.chatNotification.chatText.font = AssetLoader.font;
            __instance.chatNotification.playerNameText.font = AssetLoader.font;
            __instance.chatNotification.playerColorText.font = AssetLoader.font;

            __instance.chatNotification.chatText.color = Color.black;
            __instance.chatNotification.playerNameText.color = Color.black;
            __instance.chatNotification.playerColorText.color = Color.black;

            __instance.freeChatField.charCountText.font = AssetLoader.font;
            __instance.quickChatField.text.font = AssetLoader.font;
            __instance.quickChatField.placeholderText.font = AssetLoader.font;
            __instance.quickChatField.warningText.font = AssetLoader.font;

            __instance.freeChatField.charCountText.color = Color.black;
            __instance.quickChatField.text.color = Color.black;
            __instance.quickChatField.placeholderText.color = Color.black;
            __instance.quickChatField.warningText.color = Color.black;

            __instance.sendRateMessageText.font = AssetLoader.font; ;
            __instance.sendRateMessageText.color = Color.black;
        }
    }
    [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.Start))]
    public class LobbyBehaviourStartPatch
    {
        public static AudioClip MapTheme;

        public static void Postfix(LobbyBehaviour __instance)
        {
            __instance.MapTheme = null;
            SoundManager.Instance.PlaySound(MapTheme, true, 0.7f);
        }
    }
    [HarmonyPatch(typeof(MMOnlineManager), nameof(MMOnlineManager.Start))]
    public class MMOnlineManagerStartPatch
    {
        private static void Prefix(MMOnlineManager __instance)
        {
            SoundManager.Instance.StopNamedSound("MainBG");
            SoundManager.Instance.StopSound(LobbyBehaviourStartPatch.MapTheme);
        }
    }

    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public class MainMenuManagerStartPatch
    {
        public static AudioClip MainBG;

        private static void Prefix(MainMenuManager __instance)
        {
            SoundManager.Instance.StopSound(LobbyBehaviourStartPatch.MapTheme);
            
            SoundManager.Instance.StopNamedSound("MainBG");
            SoundManager.Instance.PlaySound(MainBG, true, 1f);

            __instance.announcementPopUp.AnnouncementBodyText.font = AssetLoader.font;
            __instance.announcementPopUp.SubTitle.font = AssetLoader.font;
            __instance.announcementPopUp.DateString.font = AssetLoader.font;

            __instance.accountCTAButton.buttonText.font = AssetLoader.font;
            __instance.freePlayButton.buttonText.font = AssetLoader.font;
            __instance.howToPlayButton.buttonText.font = AssetLoader.font;
            __instance.inventoryButton.buttonText.font = AssetLoader.font;
            __instance.newsButton.buttonText.font = AssetLoader.font;
            __instance.myAccountButton.buttonText.font = AssetLoader.font;
            __instance.playLocalButton.buttonText.font = AssetLoader.font;
            __instance.PlayOnlineButton.buttonText.font = AssetLoader.font;
            __instance.settingsButton.buttonText.font = AssetLoader.font;
            __instance.shopButton.buttonText.font = AssetLoader.font;
            __instance.playButton.buttonText.font = AssetLoader.font;
        }
    }
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    public class EndGameManagerSetUpPatch
    {
        public static void Postfix(EndGameManager __instance)
        {
            __instance.WinText.font = AssetLoader.font;
        }
    }
}