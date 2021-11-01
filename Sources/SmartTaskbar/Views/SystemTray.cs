using System.Diagnostics;
using Windows.System;

namespace SmartTaskbar;

internal class SystemTray : ApplicationContext
{
    private readonly ToolStripMenuItem _about;
    private readonly ToolStripMenuItem _animationInBar; 
    private readonly ToolStripMenuItem _showBarOnExit;
    private readonly ToolStripMenuItem _autoMode;
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
        _animationInBar = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_animation"),
            Font = font
        };
        _showBarOnExit = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_showBarOnExit"),
            Font = font
        };
        _autoMode = new ToolStripMenuItem
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
            _animationInBar,
            new ToolStripSeparator(),
            _autoMode,
            _showBarOnExit,
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
                    _autoMode.Checked = true;
                    AutoHideHelper.SetAutoHide();
                    _engine.Start();
                    break;
                case AutoModeType.None:
                    _autoMode.Checked = false;
                    _engine.Stop();
                    break;
            }
        };

        _animationInBar.Click += (s, e) => _animationInBar.Checked = Animation.ChangeTaskbarAnimation();

        _autoMode.Click += (s, e) =>
        {
            if (_autoMode.Checked)
                UserSettings.Default.TaskbarState = (int) AutoModeType.None;
            else
                UserSettings.Default.TaskbarState = (int) AutoModeType.Display;
        };

        _showBarOnExit.Click += (s, e) => UserSettings.Default.ShowBarOnExit = !_showBarOnExit.Checked;

        _exit.Click += (s, e) =>
        {
            TaskbarHelper.HideTaskbar();
            if (UserSettings.Default.ShowBarOnExit)
                AutoHideHelper.CancelAutoHide();
            _notifyIcon.Dispose();
            _engine.Dispose();
            Application.Exit();
        };

        _notifyIcon.MouseClick += (s, e) =>
        {
            if (e.Button != MouseButtons.Right)
                return;

            _animationInBar.Checked = Animation.GetTaskbarAnimation();
            _showBarOnExit.Checked = UserSettings.Default.ShowBarOnExit;
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

        Application.ApplicationExit += Application_ApplicationExit;

        #endregion

        #region Load Settings

        if (UserSettings.Default.TaskbarState == -1)
            //Run the software for the first time
            UserSettings.Default.TaskbarState = (int) AutoModeType.Display;
        else
            switch ((AutoModeType) UserSettings.Default.TaskbarState)
            {
                case AutoModeType.Display:
                    _autoMode.Checked = true;
                    _engine.Start();
                    break;
                case AutoModeType.None:
                    _autoMode.Checked = false;
                    break;
            }

        #endregion
    }

    private void Application_ApplicationExit(object? sender, EventArgs e)
    {
        // Weird bug.
        Process.GetCurrentProcess().Kill();
    }
}
