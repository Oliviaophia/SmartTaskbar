using System;
using System.Collections.Generic;

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
        ///     Taskbar Auto Mode Type
        /// </summary>
        public AutoModeType ModeType { get; set; }

        /// <summary>
        ///     Taskbar state when Disable Auto Function
        /// </summary>
        public TaskbarState ResetState { get; set; }

        /// <summary>
        ///     Default Taskbar state in AutoMode
        /// </summary>
        public TaskbarState ReadyState { get; set; }

        /// <summary>
        ///     Changed Taskbar state in AutoMode
        /// </summary>
        public TaskbarState TargetState { get; set; }

        /// <summary>
        ///     Application blockList
        /// </summary>
        public HashSet<string> BlockList { get; set; }

        /// <summary>
        ///     Application allowlist
        /// </summary>
        public HashSet<string> Allowlist { get; set; }

        ///// <summary>
        /////     reset taskbar state to default in tablet mode [only work on UWP API]
        ///// </summary>
        //public bool DisableOnTabletMode { get; set; }

        /// <summary>
        ///     Center all icons in the Taskbar
        /// </summary>
        public bool IconCentered { get; set; }

        /// <summary>
        ///     Display Language
        /// </summary>
        public Language Language { get; set; }
    }
}
