using System.Runtime.CompilerServices;
using SmartTaskbar.Core.UserConfig;

namespace SmartTaskbar.Core.Helpers
{
    internal static class BarState
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetState(this TaskbarState state)
        {
            AutoHide.SetAutoHide(state.IsAutoHide);
            ButtonSize.SetIconSize(state.IconSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TaskbarState GetDefault() =>
            new TaskbarState
            {
                IsAutoHide = !AutoHide.NotAutoHide(),
                IconSize = ButtonSize.GetIconSize()
            };
    }
}