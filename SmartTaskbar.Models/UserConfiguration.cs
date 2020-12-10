namespace SmartTaskbar.Models
{
    public record UserConfiguration
    {
        public IconStyle IconStyle { get; init; } = IconStyle.Auto;

        public AutoModeType AutoModeType { get; init; } = AutoModeType.AutoHideApiMode;

        /// <summary>
        ///     Taskbar state when Disable Auto Function
        /// </summary>
        public TaskbarState ResetState { get; set; } =
            // todo need to init
            new(false, false, IconSize.IconLarge, TransparentModeType.Disable);

        /// <summary>
        ///     Default Taskbar state in AutoMode
        /// </summary>
        public TaskbarState ReadyState { get; set; } =
            new(false, false, IconSize.IconLarge, TransparentModeType.Disable);

        /// <summary>
        ///     Changed Taskbar state in AutoMode
        /// </summary>
        public TaskbarState TargetState { get; set; } =
            new(true, false, IconSize.IconLarge, TransparentModeType.Disable);
    }
}
