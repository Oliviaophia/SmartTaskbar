using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Windows.UI.ViewManagement;
using SmartTaskbar.Properties;

namespace SmartTaskbar
{
    internal class SystemTray : ApplicationContext
    {
        private const int TrayTolerance = 4;
        private readonly ToolStripMenuItem _about;
        private readonly ToolStripMenuItem _animation;
        private readonly ToolStripMenuItem _autoMode;
        private readonly Container _container = new Container();
        private readonly ContextMenuStrip _contextMenuStrip;

        private readonly Engine _engine;
        private readonly ToolStripMenuItem _exit;
        private readonly NotifyIcon _notifyIcon;
        private readonly ToolStripMenuItem _showTaskbarWhenExit;

        public SystemTray()
        {
            _engine = new Engine(_container);

            #region Initialization

            var resource = new ResourceCulture();
            var font = new Font("Segoe UI", 10.5F);
            _about = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.About),
                Font = font
            };
            _animation = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.Animation),
                Font = font
            };
            _autoMode = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.Auto),
                Font = font
            };
            _showTaskbarWhenExit = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.ShowBarOnExit),
                Font = font
            };
            _exit = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.Exit),
                Font = font
            };
            _contextMenuStrip = new ContextMenuStrip
            {
                Renderer = new Win10Renderer()
            };

            _contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                _about,
                _animation,
                new ToolStripSeparator(),
                _autoMode,
                new ToolStripSeparator(),
                _showTaskbarWhenExit,
                _exit
            });

            _notifyIcon = new NotifyIcon(_container)
            {
                Text = @"SmartTaskbar v1.4.3",
                Icon = Fun.IsLightTheme() ? Resources.Logo_Black : Resources.Logo_White,
                Visible = true
            };

            #endregion

            #region Load Event

            _about.Click += AboutOnClick;

            _animation.Click += AnimationOnClick;

            _autoMode.Click += AutoModeOnClick;

            _showTaskbarWhenExit.Click += ShowTaskbarWhenExitOnClick;

            _exit.Click += ExitOnClick;

            _notifyIcon.MouseClick += NotifyIconOnMouseClick;

            _notifyIcon.MouseDoubleClick += NotifyIconOnMouseDoubleClick;

            Fun.UiSettings.ColorValuesChanged += UISettingsOnColorValuesChanged;

            #endregion
        }

        private void ShowTaskbarWhenExitOnClick(object sender, EventArgs e)
            => UserSettings.ShowTaskbarWhenExit = !_showTaskbarWhenExit.Checked;

        private void UISettingsOnColorValuesChanged(UISettings sender, object args)
            => _notifyIcon.Icon = Fun.IsLightTheme() ? Resources.Logo_Black : Resources.Logo_White;

        private void NotifyIconOnMouseDoubleClick(object s, MouseEventArgs e)
        {
            UserSettings.AutoModeType = AutoModeType.None;
            Fun.ChangeAutoHide();
            HideBar();
            HookHelper.ReleaseHook();
        }

        private static void HideBar()
        {
            if (Fun.IsNotAutoHide())
                return;

            var taskbar = TaskbarHelper.InitTaskbar();

            if (taskbar.Handle != IntPtr.Zero)
                taskbar.HideTaskbar();
        }

        private void NotifyIconOnMouseClick(object s, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            _animation.Checked = Fun.IsEnableTaskbarAnimation();

            _showTaskbarWhenExit.Checked = UserSettings.ShowTaskbarWhenExit;

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

        private void ExitOnClick(object s, EventArgs e)
        {
            if (UserSettings.ShowTaskbarWhenExit)
                Fun.CancelAutoHide();
            else
                HideBar();
            _container?.Dispose();
            HookHelper.ReleaseHook();
            Application.Exit();
        }

        private void AutoModeOnClick(object s, EventArgs e)
        {
            if (_autoMode.Checked)
            {
                UserSettings.AutoModeType = AutoModeType.None;
                HideBar();
                HookHelper.ReleaseHook();
            }
            else { UserSettings.AutoModeType = AutoModeType.Auto; }
        }

        private void AnimationOnClick(object s, EventArgs e)
            => _animation.Checked = Fun.ChangeTaskbarAnimation();

        private static void AboutOnClick(object s, EventArgs e)
            => Process.Start(@"https://github.com/ChanpleCai/SmartTaskbar");
    }
}
