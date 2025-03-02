namespace AmongUsUnknownImpostors.CustomOption
{
    public class CustomHeaderOption : CustomOption
    {
        public CustomHeaderOption(int id, string color, string name) : base(id, color, name, CustomOptionType.Header, 0)
        {
        }

        public override void OptionCreated()
        {
            base.OptionCreated();
            Setting.Cast<ToggleOption>().TitleText.text = Name;
        }
    }
}