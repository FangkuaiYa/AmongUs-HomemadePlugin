using AmongUsUnknownImpostors.Patches;

namespace AmongUsUnknownImpostors.CustomOption
{
    public class CustomToggleOption : CustomOption
    {
        public CustomToggleOption(int id, string color, string name, bool value = true) : base(id, color, name, CustomOptionType.Toggle, value)
        {
            Format = val => (bool)val ? Language.GetString("option.value.on") : Language.GetString("option.value.off");
        }

        public bool Get()
        {
            return (bool)Value;
        }

        public void Toggle()
        {
            Set(!Get());
        }

        public override void OptionCreated()
        {
            base.OptionCreated();
            var tgl = Setting.Cast<ToggleOption>();
            tgl.CheckMark.enabled = Get();
        }
    }
}