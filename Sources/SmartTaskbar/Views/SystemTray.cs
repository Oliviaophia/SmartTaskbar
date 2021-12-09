using System.ComponentModel;
using System.Diagnostics;
using Windows.System;
using Windows.UI.ViewManagement;
using SmartTaskbar.Languages;

namespace SmartTaskbar;

internal class SystemTray : ApplicationContext
{
    private readonly ToolStripMenuItem _debugItem;
    private readonly ToolStripMenuItem _animationInBar;
    private readonly ToolStripMenuItem _autoMode;

    private readonly Container _container = new();
    private readonly ContextMenuStrip _contextMenuStrip;

    private readonly Engine _engine;
    private readonly ResourceCulture _resourceCulture = new();
    private readonly NotifyIcon _notifyIcon;
    private readonly ToolStripMenuItem _showBarOnExit;
    private readonly UserSettings _userSettings = new();

    public SystemTray()
    {
        #region Initialization

        _engine = new Engine(_container, _userSettings);

        var font = new Font("Segoe UI", 10.5F);

        var about = new ToolStripMenuItem(_resourceCulture.GetString(LanguagName.TrayAbout))
        {
            Font = font
        };
        _debugItem = new ToolStripMenuItem(_resourceCulture.GetString(LanguagName.TrayDebug))
        {
            Font = font
        };
        _animationInBar = new ToolStripMenuItem(_resourceCulture.GetString(LanguagName.TrayAnimation))
        {
            Font = font
        };
        _showBarOnExit = new ToolStripMenuItem(_resourceCulture.GetString(LanguagName.TrayShowBarOnExit))
        {
            Font = font
        };
        _autoMode = new ToolStripMenuItem(_resourceCulture.GetString(LanguagName.TrayAuto))
        {
            Font = font
        };
        var exit = new ToolStripMenuItem(_resourceCulture.GetString(LanguagName.TrayExit))
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
            _debugItem,
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

        _debugItem.Click += DebugItem_Click;

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
                break;
            case AutoModeType.None:
                _autoMode.Checked = false;
                break;
        }

        #endregion
    }

    private void DebugItem_Click(object? sender, EventArgs e)
    {
        if (_engine.LastHiddenHandle == IntPtr.Zero)
        {
            MessageBox.Show(_resourceCulture.GetString(LanguagName.ShowNoFindInfo),
                            _resourceCulture.GetString(LanguagName.TrayDebug),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            return;
        }

        _ = Fun.GetWindowThreadProcessId(_engine.LastHiddenHandle, out var processId);

        if (processId == 0)
        {
            MessageBox.Show(_resourceCulture.GetString(LanguagName.ShowNoFindInfo),
                            _resourceCulture.GetString(LanguagName.TrayDebug),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            return;
        }

        var process = Process.GetProcessById(processId);

        MessageBox.Show($@"{process.MainWindowTitle} {process.ProcessName}",
                        _resourceCulture.GetString(LanguagName.TrayDebug),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
    }

    private void AboutOnClick(object? sender, EventArgs e)
        => _ = Launcher.LaunchUriAsync(new Uri("https://github.com/ChanpleCai/SmartTaskbar"));

    private void UISettingsOnColorValuesChanged(UISettings s, object e)
        => _notifyIcon.Icon = Fun.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White;

    private void NotifyIconOnMouseDoubleClick(object? s, MouseEventArgs e)
    {
        _userSettings.AutoModeType = AutoModeType.None;
        Fun.ChangeAutoHide();
        TaskbarHelper.InitTaskbar()?.HideTaskbar();
    }

    private void NotifyIconOnMouseClick(object? s, MouseEventArgs e)
    {
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
        TaskbarHelper.InitTaskbar()?.HideTaskbar();
        if (UserSettings.ShowTaskbarWhenExit) Fun.CancelAutoHide();
        Application.Exit();
    }

    private void ShowBarOnExitOnClick(object? s, EventArgs e)
        => UserSettings.ShowTaskbarWhenExit = !_showBarOnExit.Checked;

    private void AutoModeOnClick(object? s, EventArgs e)
    {
        _userSettings.AutoModeType = _autoMode.Checked ? AutoModeType.None : AutoModeType.Auto;
        TaskbarHelper.InitTaskbar()?.HideTaskbar();
    }

    private void AnimationInBarOnClick(object? s, EventArgs e)
        => _animationInBar.Checked = Fun.ChangeTaskbarAnimation();

    private void AutoModeTypePropertyChanged(object? s, AutoModeType e)
    {
        switch (e)
        {
            case AutoModeType.Auto:
                _autoMode.Checked = true;
                break;
            case AutoModeType.None:
                _autoMode.Checked = false;
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
