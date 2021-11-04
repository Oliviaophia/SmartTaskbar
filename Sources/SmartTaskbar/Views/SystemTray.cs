using System.Diagnostics;
using Windows.UI.ViewManagement;

namespace SmartTaskbar;

internal class SystemTray : ApplicationContext
{
    private readonly ToolStripMenuItem _centerAlignment;
    private readonly ToolStripMenuItem _animationInBar;
    private readonly ToolStripMenuItem _autoMode;

    private readonly Engine _engine = new();
    private readonly NotifyIcon _notifyIcon;
    private readonly ToolStripMenuItem _showBarOnExit;
    private readonly UserSettings _userSettings = new();

    public SystemTray()
    {
        #region Initialization

        var resource = new ResourceCulture();
        var font = new Font("Segoe UI", 12F);
        var padding = new Padding(4, 2, 4, 2);
        _animationInBar = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_animation"),
            Font = font,
            Margin = padding
        };
        _showBarOnExit = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_showBarOnExit"),
            Font = font,
            Margin = padding
        };
        _centerAlignment = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_centerAlignment"),
            Font = font,
            Margin = padding
        };
        _autoMode = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_auto"),
            Font = font,
            Margin = padding
        };
        var exit = new ToolStripMenuItem
        {
            Text = resource.GetString("tray_exit"),
            Font = font,
            Margin = padding
        };
        var contextMenuStrip = new ContextMenuStrip
        {
            Renderer = new Win11Renderer()
        };

        contextMenuStrip.Items.AddRange(new ToolStripItem[]
        {
            _animationInBar,
            _centerAlignment,
            _showBarOnExit,
            new ToolStripSeparator(),
            _autoMode,
            new ToolStripSeparator(),
            exit
        });

        _notifyIcon = new NotifyIcon
        {
            ContextMenuStrip = contextMenuStrip,
            Text = $"{Application.ProductName} 1.2.7",
            Icon = UISettingsHelper.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White,
            Visible = true
        };

        #endregion

        #region Load Event

        _animationInBar.Click += OnAnimationInBarOnClick;

        _showBarOnExit.Click += OnShowBarOnExitOnClick;

        _centerAlignment.Click += AlignLeftWhenLeft_Click;

        _autoMode.Click += OnAutoModeOnClick;

        exit.Click += OnExitOnClick;

        _notifyIcon.MouseClick += OnNotifyIconOnMouseClick;

        _notifyIcon.MouseDoubleClick += OnNotifyIconOnMouseDoubleClick;

        UISettingsHelper.Settings.ColorValuesChanged += OnSettingsOnColorValuesChanged;

        Application.ApplicationExit += Application_ApplicationExit;

        _userSettings.OnAutoModeTypePropertyChanged += OnAutoModeTypePropertyChanged;

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
    private void OnSettingsOnColorValuesChanged(UISettings s, object e)
        => _notifyIcon.Icon = UISettingsHelper.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White;

    private void OnNotifyIconOnMouseDoubleClick(object? s, MouseEventArgs e)
    {
        _userSettings.AutoModeType = AutoModeType.None;
        AutoHideHelper.ChangeAutoHide();
    }

    private void OnNotifyIconOnMouseClick(object? s, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right) return;

        _animationInBar.Checked = AnimationHelper.GetTaskbarAnimation();
        _showBarOnExit.Checked = UserSettings.ShowTaskbarWhenExit;
        _centerAlignment.Checked = UISettingsHelper.IsCenterAlignment();
        _notifyIcon.ContextMenuStrip.Show(Cursor.Position.X - 30,
                                          TaskbarHelper.InitTaskbar().Rect.top
                                            - _notifyIcon.ContextMenuStrip.Height
                                          - 20);
    }

    private void OnExitOnClick(object? s, EventArgs e)
    {
        TaskbarHelper.InitTaskbar().HideTaskbar();
        _notifyIcon.Dispose();
        _engine.Dispose();
        if (UserSettings.ShowTaskbarWhenExit) AutoHideHelper.CancelAutoHide();
        Application.Exit();
    }

    private void AlignLeftWhenLeft_Click(object? sender, EventArgs e)
        => _centerAlignment.Checked = UISettingsHelper.ChangeAlignment(); 

    private void OnShowBarOnExitOnClick(object? s, EventArgs e)
        => UserSettings.ShowTaskbarWhenExit = !_showBarOnExit.Checked;

    private void OnAutoModeOnClick(object? s, EventArgs e)
        => _userSettings.AutoModeType = _autoMode.Checked ? AutoModeType.None : AutoModeType.Auto;

    private void OnAnimationInBarOnClick(object? s, EventArgs e)
        => _animationInBar.Checked = AnimationHelper.ChangeTaskbarAnimation();

    private void OnAutoModeTypePropertyChanged(object? s, AutoModeType e)
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
