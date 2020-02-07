using SmartTaskbar.Core.Settings;

namespace SmartTaskbar.Core.Helpers
{
    internal static class BarState
    {
        public static void SetState(this TaskbarState state)
        {
            AutoHide.SetAutoHide(state.IsAutoHide);
            ButtonSize.SetIconSize(state.IconSize);
        }

        public static TaskbarState GetDefault() =>
            new TaskbarState
            {
                IsAutoHide = !AutoHide.NotAutoHide(),
                IconSize = ButtonSize.GetIconSize()
            };
    }
}