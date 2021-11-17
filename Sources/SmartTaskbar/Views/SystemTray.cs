using System.ComponentModel;
using System.Diagnostics;
using Windows.System;
using Windows.UI.ViewManagement;

namespace SmartTaskbar;

internal class SystemTray : ApplicationContext
{
    private readonly ToolStripMenuItem _animationInBar;
    private readonly ToolStripMenuItem _autoMode;

    private readonly Container _container = new();
    private readonly ContextMenuStrip _contextMenuStrip;

    private readonly Engine _engine = new();
    private readonly NotifyIcon _notifyIcon;
    private readonly ToolStripMenuItem _showBarOnExit;
    private readonly UserSettings _userSettings = new();

    public SystemTray()
    {
        #region Initialization

        var font = new Font("Segoe UI", 10.5F);

        var resource = new ResourceCulture();

        var about = new ToolStripMenuItem(resource.GetString("tray_about"))
        {
            Font = font
        };
        _animationInBar = new ToolStripMenuItem(resource.GetString("tray_animation"))
        {
            Font = font
        };
        _showBarOnExit = new ToolStripMenuItem(resource.GetString("tray_showBarOnExit"))
        {
            Font = font
        };
        _autoMode = new ToolStripMenuItem(resource.GetString("tray_auto"))
        {
            Font = font
        };
        var exit = new ToolStripMenuItem(resource.GetString("tray_exit"))
        {
            Font = font
        };

        _contextMenuStrip = new ContextMenuStrip(_container)
        {
            Renderer = new Win11Renderer()
        };

        _contextMenuStrip.Items.AddRange(new ToolStripItem[]
        {
            about,
            new ToolStripSeparator(),
            _animationInBar,
            _showBarOnExit,
            new ToolStripSeparator(),
            _autoMode,
            new ToolStripSeparator(),
            exit
        });

        _notifyIcon = new NotifyIcon(_container)
        {
            Text = Application.ProductName,
            Icon = Fun.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White,
            Visible = true
        };

        #endregion

        #region Load Event

        about.Click += AboutOnClick;

        _animationInBar.Click += AnimationInBarOnClick;

        _showBarOnExit.Click += ShowBarOnExitOnClick;

        _autoMode.Click += AutoModeOnClick;

        exit.Click += ExitOnClick;

        _notifyIcon.MouseClick += NotifyIconOnMouseClick;

        _notifyIcon.MouseDoubleClick += NotifyIconOnMouseDoubleClick;

        Fun.UISettings.ColorValuesChanged += UISettingsOnColorValuesChanged;

        Application.ApplicationExit += Application_ApplicationExit;

        _userSettings.OnAutoModeTypePropertyChanged += AutoModeTypePropertyChanged;

        #endregion

        #region Load Settings

        switch (_userSettings.AutoModeType)
        {
            case AutoModeType.Auto:
                _autoMode.Checked = true;
                Engine.Start();
                break;
            case AutoModeType.None:
                _autoMode.Checked = false;
                break;
        }

        #endregion
    }

    private void AboutOnClick(object? sender, EventArgs e)
        => _ = Launcher.LaunchUriAsync(new Uri("https://github.com/ChanpleCai/SmartTaskbar"));

    private void UISettingsOnColorValuesChanged(UISettings s, object e)
        => _notifyIcon.Icon = Fun.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White;

    private void NotifyIconOnMouseDoubleClick(object? s, MouseEventArgs e)
    {
        _userSettings.AutoModeType = AutoModeType.None;
        Fun.ChangeAutoHide();
    }

    private void NotifyIconOnMouseClick(object? s, MouseEventArgs e)
    {
        // When the explorer.exe is killed, the system timer will automatically pause.
        // It is very difficult to tell when it starts again,
        // so when the mouse moves over the icon, perform a restart operation.
        if (_userSettings.AutoModeType == AutoModeType.Auto)
            Engine.Start();

        if (e.Button != MouseButtons.Right) return;

        _animationInBar.Checked = Fun.IsEnableTaskbarAnimation();
        _showBarOnExit.Checked = UserSettings.ShowTaskbarWhenExit;

        var taskbar = TaskbarHelper.InitTaskbar();

        var y = taskbar?.Rect.top ?? Cursor.Position.Y;

        _contextMenuStrip.Show(Cursor.Position.X - 30,
                               y - _contextMenuStrip.Height - 20);
        Fun.SetForegroundWindow(_contextMenuStrip.Handle);
    }

    private void ExitOnClick(object? s, EventArgs e)
    {
        _container.Dispose();
        _engine.Dispose();
        TaskbarHelper.InitTaskbar()?.HideTaskbar();
        if (UserSettings.ShowTaskbarWhenExit) Fun.CancelAutoHide();
        Application.Exit();
    }

    private void ShowBarOnExitOnClick(object? s, EventArgs e)
        => UserSettings.ShowTaskbarWhenExit = !_showBarOnExit.Checked;

    private void AutoModeOnClick(object? s, EventArgs e)
        => _userSettings.AutoModeType = _autoMode.Checked ? AutoModeType.None : AutoModeType.Auto;

    private void AnimationInBarOnClick(object? s, EventArgs e)
        => _animationInBar.Checked = Fun.ChangeTaskbarAnimation();

    private void AutoModeTypePropertyChanged(object? s, AutoModeType e)
    {
        switch (e)
        {
            case AutoModeType.Auto:
                _autoMode.Checked = true;
                Engine.Start();
                break;
            case AutoModeType.None:
                _autoMode.Checked = false;
                Engine.Stop();
                break;
        }
    }

    private static async void Application_ApplicationExit(object? sender, EventArgs e)
    {
        // Weird bug.
        await Task.Delay(500);
        Process.GetCurrentProcess().Kill();
    }
}
