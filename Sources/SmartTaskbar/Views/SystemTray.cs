using System.ComponentModel;
using System.Diagnostics;
using Windows.System;
using Windows.UI.ViewManagement;
using SmartTaskbar.Languages;

namespace SmartTaskbar
{
    internal class SystemTray : ApplicationContext
    {
        private const int TrayTolerance = 4;
        private readonly ToolStripMenuItem _animationInBar;
        private readonly ToolStripMenuItem _autoMode;

        private readonly Container _container = new();
        private readonly ContextMenuStrip _contextMenuStrip;

        private readonly Engine _engine;
        private readonly ToolStripMenuItem _exit;
        private readonly NotifyIcon _notifyIcon;
        private readonly ResourceCulture _resourceCulture = new();
        private readonly ToolStripMenuItem _showBarOnExit;

        public SystemTray()
        {
            #region Initialization

            _engine = new Engine(_container);

            var font = new Font("Segoe UI", 10.5F);

            var about = new ToolStripMenuItem(_resourceCulture.GetString(LangName.About))
            {
                Font = font
            };
            _animationInBar = new ToolStripMenuItem(_resourceCulture.GetString(LangName.Animation))
            {
                Font = font
            };
            _showBarOnExit = new ToolStripMenuItem(_resourceCulture.GetString(LangName.ShowBarOnExit))
            {
                Font = font
            };
            _autoMode = new ToolStripMenuItem(_resourceCulture.GetString(LangName.Auto))
            {
                Font = font
            };
            _exit = new ToolStripMenuItem(_resourceCulture.GetString(LangName.Exit))
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
                _animationInBar,
                new ToolStripSeparator(),
                _autoMode,
                new ToolStripSeparator(),
                _showBarOnExit,
                _exit
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

            _exit.Click += ExitOnClick;

            _notifyIcon.MouseClick += NotifyIconOnMouseClick;

            _notifyIcon.MouseDoubleClick += NotifyIconOnMouseDoubleClick;

            Fun.UiSettings.ColorValuesChanged += UISettingsOnColorValuesChanged;

            Application.ApplicationExit += Application_ApplicationExit;

            #endregion
        }

        private void AboutOnClick(object? sender, EventArgs e)
            => _ = Launcher.LaunchUriAsync(new Uri("https://github.com/ChanpleCai/SmartTaskbar"));

        private void UISettingsOnColorValuesChanged(UISettings s, object e)
            => _notifyIcon.Icon = Fun.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White;

        private void NotifyIconOnMouseDoubleClick(object? s, MouseEventArgs e)
        {
            UserSettings.AutoModeType = AutoModeType.None;

            Fun.ChangeAutoHide();
            HideBar();
        }

        private void NotifyIconOnMouseClick(object? s, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            _animationInBar.Checked = Fun.IsEnableTaskbarAnimation();
            _showBarOnExit.Checked = UserSettings.ShowTaskbarWhenExit;
            _autoMode.Checked = UserSettings.AutoModeType == AutoModeType.Auto;

            ShowMenu();

            Fun.SetForegroundWindow(_contextMenuStrip.Handle);
        }

        private void ShowMenu()
        {
            var taskbar = TaskbarHelper.InitTaskbar();

            if (taskbar.Handle == IntPtr.Zero)
                return;

            switch (taskbar.Position)
            {
                case TaskbarPosition.Bottom:
                    if (Cursor.Position.X + _contextMenuStrip.Width > Screen.PrimaryScreen.Bounds.Right)
                        _contextMenuStrip.Show(
                            Screen.PrimaryScreen.Bounds.Right - _contextMenuStrip.Width - TrayTolerance,
                            taskbar.Rect.top - _contextMenuStrip.Height - TrayTolerance);
                    else
                        _contextMenuStrip.Show(Cursor.Position.X - TrayTolerance,
                                               taskbar.Rect.top - _contextMenuStrip.Height - TrayTolerance);
                    break;
                case TaskbarPosition.Left:
                    if (Cursor.Position.Y + _contextMenuStrip.Height > Screen.PrimaryScreen.Bounds.Bottom)
                        _contextMenuStrip.Show(taskbar.Rect.right + TrayTolerance,
                                               Screen.PrimaryScreen.Bounds.Bottom
                                               - _contextMenuStrip.Height
                                               - TrayTolerance);
                    else
                        _contextMenuStrip.Show(taskbar.Rect.right + TrayTolerance,
                                               Cursor.Position.Y - TrayTolerance);
                    break;
                case TaskbarPosition.Right:
                    if (Cursor.Position.Y + _contextMenuStrip.Height > Screen.PrimaryScreen.Bounds.Bottom)
                        _contextMenuStrip.Show(taskbar.Rect.left - TrayTolerance - _contextMenuStrip.Width,
                                               Screen.PrimaryScreen.Bounds.Bottom
                                               - _contextMenuStrip.Height
                                               - TrayTolerance);
                    else
                        _contextMenuStrip.Show(taskbar.Rect.left - TrayTolerance - _contextMenuStrip.Width,
                                               Cursor.Position.Y - TrayTolerance);
                    break;
                case TaskbarPosition.Top:
                    if (Cursor.Position.X + _contextMenuStrip.Width > Screen.PrimaryScreen.Bounds.Right)
                        _contextMenuStrip.Show(
                            Screen.PrimaryScreen.Bounds.Right - _contextMenuStrip.Width - TrayTolerance,
                            taskbar.Rect.bottom + TrayTolerance);
                    else
                        _contextMenuStrip.Show(Cursor.Position.X - TrayTolerance,
                                               taskbar.Rect.bottom + TrayTolerance);
                    break;
            }
        }

        private static void HideBar()
        {
            if (Fun.IsNotAutoHide())
                return;

            var taskbar = TaskbarHelper.InitTaskbar();

            if (taskbar.Handle != IntPtr.Zero)
                taskbar.HideTaskbar();
        }

        private void ExitOnClick(object? s, EventArgs e)
        {
            if (UserSettings.ShowTaskbarWhenExit)
                Fun.CancelAutoHide();
            else
                HideBar();
            _container?.Dispose();
            Application.Exit();
        }

        private void ShowBarOnExitOnClick(object? s, EventArgs e)
            => UserSettings.ShowTaskbarWhenExit = !_showBarOnExit.Checked;

        private void AutoModeOnClick(object? s, EventArgs e)
        {
            if (_autoMode.Checked)
            {
                UserSettings.AutoModeType = AutoModeType.None;
                HideBar();
            }
            else { UserSettings.AutoModeType = AutoModeType.Auto; }
        }

        private void AnimationInBarOnClick(object? s, EventArgs e)
            => _animationInBar.Checked = Fun.ChangeTaskbarAnimation();

        private static async void Application_ApplicationExit(object? sender, EventArgs e)
        {
            // Weird bug.
            await Task.Delay(500);
            Process.GetCurrentProcess().Kill();
        }
    }
}
