using System;
using AmongUsUnknownImpostors.CustomOption;

namespace AmongUsUnknownImpostors.Patches
{
    public class CustomGameOptionsData
    {
        public static CustomHeaderOption unkImpostorSettings;
        public static CustomToggleOption unkImpostor;
        public static CustomNumberOption impoVision;

        private static Func<object, string> MultiplierFormat { get; } = value => $"{value:0.0#}x";

        public static void LoadAll()
        {
            unkImpostorSettings = new CustomHeaderOption(1, "#F9CF01", "option.settingbutton");
            unkImpostor = new CustomToggleOption(2, "#FF0000", "option.unkImpostor", false);
            impoVision = new CustomNumberOption(3, "#01F93B", "option.impoVision", 0.25f, 0.25f, 5f, 0.25f, MultiplierFormat);
        }
    }
}