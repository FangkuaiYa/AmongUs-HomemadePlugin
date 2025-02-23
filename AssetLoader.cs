using System.IO;
using System.Reflection;
using Il2CppInterop.Runtime;
using TMPro;
using UnityEngine;

namespace FontChange
{
    public static class AssetLoader
    {
        private static readonly Assembly dll = Assembly.GetExecutingAssembly();
        private static bool flag = false;
        public static GameObject foxTask;
        public static TMPro.TMP_FontAsset font;

        public static void LoadAssets()
        {
            if (flag) return;
            flag = true;
            LoadFontAssets();
        }
        private static void LoadFontAssets()
        {
            var resourceTestAssetBundleStream = dll.GetManifestResourceStream("FontChange.AssetBundles.fontasset");
            var assetBundleBundle = AssetBundle.LoadFromMemory(resourceTestAssetBundleStream.ReadFully());
            font = assetBundleBundle.LoadAsset<TMP_FontAsset>("JingNanBoBoHei.asset").DontUnload();
            FontChange.LobbyBehaviourStartPatch.MapTheme = assetBundleBundle.LoadAsset<AudioClip>("MapTheme.mp3").DontUnload();
            FontChange.MainMenuManagerStartPatch.MainBG = assetBundleBundle.LoadAsset<AudioClip>("MainBG.mp3").DontUnload();
            FontChange.PingTrackerPatch.commsdown = assetBundleBundle.LoadAsset<Sprite>("commsdown.png").DontUnload();
        }

        public static byte[] ReadFully(this Stream input)
        {
            using var ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }

#nullable enable
        public static T? LoadAsset<T>(this AssetBundle assetBundle, string name) where T : UnityEngine.Object
        {
            return assetBundle.LoadAsset(name, Il2CppType.Of<T>())?.Cast<T>();
        }
#nullable disable
        public static T DontUnload<T>(this T obj) where T : Object
        {
            obj.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            return obj;
        }
    }


}
