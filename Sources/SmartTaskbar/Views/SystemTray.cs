using Windows.System;

namespace SmartTaskbar;

internal class SystemTray : ApplicationContext
{
    private readonly ToolStripMenuItem _about;
    private readonly ToolStripMenuItem _animation;
    private readonly ToolStripMenuItem _auto;
    private readonly ContextMenuStrip _contextMenuStrip;

    private readonly Engine _engine = new();
    private readonly ToolStripMenuItem _exit;
    private readonly NotifyIcon _notifyIcon;

    public SystemTray()
    {
        #region Initialization

        var resource = new ResourceCulture();
        var font = new Font("Segoe UI", 9F);
        _about = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_about"),
            Font = font
        };
        _animation = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_animation"),
            Font = font
        };
        _auto = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_auto"),
            Font = font
        };
        _exit = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_exit"),
            Font = font
        };
        _contextMenuStrip = new ContextMenuStrip();

        _contextMenuStrip.Items.AddRange(new ToolStripItem[]
        {
            _about,
            _animation,
            new ToolStripSeparator(),
            _auto,
            new ToolStripSeparator(),
            _exit
        });

        _notifyIcon = new NotifyIcon
        {
            ContextMenuStrip = _contextMenuStrip,
            Text = @"SmartTaskbar v1.2.0",
            Icon = UiInfo.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White,
            Visible = true
        };

        #endregion

        #region Load Event

        _about.Click += async (s, e)
            => await Launcher.LaunchUriAsync(new Uri(@"https://github.com/ChanpleCai/SmartTaskbar/releases"));

        UserSettings.Default.PropertyChanged += (s, e) =>
        {
            UserSettings.Default.Save();
            switch ((AutoModeType) UserSettings.Default.TaskbarState)
            {
                case AutoModeType.Display:
                    _auto.Checked = true;
                    AutoHideHelper.SetAutoHide();
                    _engine.Start();
                    break;
                case AutoModeType.None:
                    _auto.Checked = false;
                    _engine.Stop();
                    break;
            }
        };

        _animation.Click += (s, e) => _animation.Checked = Animation.ChangeTaskbarAnimation();

        _auto.Click += (s, e) =>
        {
            if (_auto.Checked)
                UserSettings.Default.TaskbarState = (int) AutoModeType.None;
            else
                UserSettings.Default.TaskbarState = (int) AutoModeType.Display;
        };

        _exit.Click += (s, e) =>
        {
            TaskbarHelper.Taskbar.HideTaskbar();
            AutoHideHelper.CancelAutoHide();
            _notifyIcon.Dispose();
            _engine.Dispose();
            Application.Exit();
        };

        _notifyIcon.MouseClick += (s, e) =>
        {
            if (e.Button != MouseButtons.Right)
                return;

            _animation.Checked = Animation.GetTaskbarAnimation();
        };

        _notifyIcon.MouseDoubleClick += (s, e) =>
        {
            UserSettings.Default.TaskbarState = (int) AutoModeType.None;
            AutoHideHelper.SetAutoHide(AutoHideHelper.NotAutoHide());
        };

        UiInfo.Settings.ColorValuesChanged += (s, e) =>
        {
            _notifyIcon.Icon = UiInfo.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White;
        };

        #endregion

        #region Load Settings

        if (UserSettings.Default.TaskbarState == -1)
            //Run the software for the first time
            UserSettings.Default.TaskbarState = (int) AutoModeType.Display;
        else
            switch ((AutoModeType) UserSettings.Default.TaskbarState)
            {
                case AutoModeType.Display:
                    _auto.Checked = true;
                    _engine.Start();
                    break;
                case AutoModeType.None:
                    _auto.Checked = false;
                    break;
            }

        #endregion
    }
}
