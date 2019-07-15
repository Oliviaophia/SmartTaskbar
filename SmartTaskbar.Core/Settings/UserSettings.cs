using System;
using System.Collections.Generic;
using SmartTaskbar.Core.Helpers;

namespace SmartTaskbar.Core.Settings
{
    [Serializable]
    public class UserSettings
    {
        /// <summary>
        ///     SmartTaskbar Icon Style
        /// </summary>
        public IconStyle IconStyle { get; set; }

        /// <summary>
        ///     Taskbar default state
        /// </summary>
        public TaskbarState DefaultState { get; set; }

        /// <summary>
        ///     Taskbar Auto Mode Type
        /// </summary>
        public AutoModeType ModeType { get; set; }

        /// <summary>
        ///     Is Taskbar in Auto Mode
        /// </summary>
        public bool InAutoMode => ModeType != AutoModeType.Disable;

        /// <summary>
        ///     Application blacklist
        /// </summary>
        public HashSet<string> Blacklist { get; set; }

        /// <summary>
        ///     Default Taskbar state in blacklist mode
        /// </summary>
        public TaskbarState BlistDefaultState { get; set; }

        /// <summary>
        ///     Target Taskbar state in blacklist mode
        /// </summary>
        public TaskbarState BlistTargetState { get; set; }

        /// <summary>
        ///     Application whitelist
        /// </summary>
        public HashSet<string> Whitelist { get; set; }

        /// <summary>
        ///     Default Taskbar state in whitelist mode
        /// </summary>
        public TaskbarState WlistDefaultState { get; set; }

        /// <summary>
        ///     Target Taskbar state in whitelist mode
        /// </summary>
        public TaskbarState WlistTargetState { get; set; }

        /// <summary>
        ///     Taskbar Transparent Type
        /// </summary>
        public TransparentModeType TransparentType { get; set; }

        /// <summary>
        ///     Is Taskbars in Transparent Mode
        /// </summary>
        public bool InTransparentMode => TransparentType != TransparentModeType.Disabled;

        /// <summary>
        ///     Is Taskbars using small Icon Button
        /// </summary>
        public bool IsSmallButton => ButtonSize.GetIconSize() == Constant.IconSmall;

        /// <summary>
        ///     Whether to completely hide the taskbar when hiding taskbar
        /// </summary>
        public bool HideTaskbarCompletely { get; set; }

        /// <summary>
        ///     reset taskbar state to default in tablet mode
        /// </summary>
        public bool DisabledOnTabletMode { get; set; }

        /// <summary>
        ///     Display Language
        /// </summary>
        public Language Language { get; set; }
    }
}