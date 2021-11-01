using System.ComponentModel;
using System.Diagnostics;
using Windows.System;
using Windows.UI.ViewManagement;

namespace SmartTaskbar;

internal class SystemTray : ApplicationContext
{
    private readonly ToolStripMenuItem _animationInBar;
    private readonly ToolStripMenuItem _autoMode;

    private readonly Engine _engine = new();
    private readonly NotifyIcon _notifyIcon;
    private readonly ToolStripMenuItem _showBarOnExit;

    public SystemTray()
    {
        #region Initialization

        var resource = new ResourceCulture();
        var font = new Font("Segoe UI", 9F);
        var about = new ToolStripMenuItem
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
        var exit = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_exit"),
            Font = font
        };
        var contextMenuStrip = new ContextMenuStrip();

        contextMenuStrip.Items.AddRange(new ToolStripItem[]
        {
            about,
            _animationInBar,
            new ToolStripSeparator(),
            _autoMode,
            _showBarOnExit,
            new ToolStripSeparator(),
            exit
        });

        _notifyIcon = new NotifyIcon
        {
            ContextMenuStrip = contextMenuStrip,
            Text = @"SmartTaskbar v1.2.0",
            Icon = UiInfo.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White,
            Visible = true
        };

        #endregion

        #region Load Event

        about.Click += OnAboutOnClick!;

        UserSettings.Default.PropertyChanged += OnDefaultOnPropertyChanged!;

        _animationInBar.Click += OnAnimationInBarOnClick!;

        _autoMode.Click += OnAutoModeOnClick!;

        _showBarOnExit.Click += OnShowBarOnExitOnClick!;

        exit.Click += OnExitOnClick!;

        _notifyIcon.MouseClick += OnNotifyIconOnMouseClick!;

        _notifyIcon.MouseDoubleClick += OnNotifyIconOnMouseDoubleClick!;

        UiInfo.Settings.ColorValuesChanged += OnSettingsOnColorValuesChanged;

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
                    Engine.Start();
                    break;
                case AutoModeType.None:
                    _autoMode.Checked = false;
                    break;
            }

        #endregion
    }

    private void OnSettingsOnColorValuesChanged(UISettings s, object e)
        => _notifyIcon.Icon = UiInfo.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White;

    private static void OnNotifyIconOnMouseDoubleClick(object s, MouseEventArgs e)
    {
        UserSettings.Default.TaskbarState = (int) AutoModeType.None;
        AutoHideHelper.SetAutoHide(AutoHideHelper.NotAutoHide());
    }

    private void OnNotifyIconOnMouseClick(object s, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right) return;

        _animationInBar.Checked = Animation.GetTaskbarAnimation();
        _showBarOnExit.Checked = UserSettings.Default.ShowBarOnExit;
    }

    private void OnExitOnClick(object s, EventArgs e)
    {
        TaskbarHelper.HideTaskbar();
        if (UserSettings.Default.ShowBarOnExit) AutoHideHelper.CancelAutoHide();
        _notifyIcon.Dispose();
        _engine.Dispose();
        Application.Exit();
    }

    private void OnShowBarOnExitOnClick(object s, EventArgs e) { UserSettings.Default.ShowBarOnExit = !_showBarOnExit.Checked; }

    private void OnAutoModeOnClick(object s, EventArgs e)
    {
        if (_autoMode.Checked)
            UserSettings.Default.TaskbarState = (int) AutoModeType.None;
        else
            UserSettings.Default.TaskbarState = (int) AutoModeType.Display;
    }

    private void OnAnimationInBarOnClick(object s, EventArgs e) { _animationInBar.Checked = Animation.ChangeTaskbarAnimation(); }

    private void OnDefaultOnPropertyChanged(object s, PropertyChangedEventArgs e)
    {
        UserSettings.Default.Save();
        switch ((AutoModeType) UserSettings.Default.TaskbarState)
        {
            case AutoModeType.Display:
                _autoMode.Checked = true;
                AutoHideHelper.SetAutoHide();
                Engine.Start();
                break;
            case AutoModeType.None:
                _autoMode.Checked = false;
                Engine.Stop();
                break;
        }
    }

    private static void OnAboutOnClick(object s, EventArgs e) { _ = Launcher.LaunchUriAsync(new Uri(@"https://github.com/ChanpleCai/SmartTaskbar/releases")); }

    private static async void Application_ApplicationExit(object? sender, EventArgs e)
    {
        // Weird bug.
        await Task.Delay(1000);
        Process.GetCurrentProcess().Kill();
    }
}
