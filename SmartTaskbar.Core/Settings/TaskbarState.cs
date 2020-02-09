namespace SmartTaskbar.Core.Settings
{
    public class TaskbarState
    {
        public bool IsAutoHide { get; set; }

        public bool HideTaskbarCompletely { get; set; }

        public int IconSize { get; set; }

        public TransparentModeType TransparentMode { get; set; }
    }
}