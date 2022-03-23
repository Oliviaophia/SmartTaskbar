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
        private readonly ToolStripMenuItem _about;
        private readonly ToolStripMenuItem _animation;
        private readonly ToolStripMenuItem _autoDisplay;
        private readonly ToolStripMenuItem _autoSize;
        private readonly Container _container = new Container();
        private readonly ContextMenuStrip _contextMenuStrip;
        private readonly ToolStripMenuItem _exit;
        private readonly NotifyIcon _notifyIcon;
        private readonly ToolStripMenuItem _pauseInTabletMode;
        private readonly ToolStripMenuItem _reverseDisplayModeBehavior;
        private readonly ToolStripMenuItem _reverseSizeModeBehavior;
        private readonly ToolStripMenuItem _showTaskbarWhenExit;
        private readonly ToolStripMenuItem _smallIcon;

        private readonly Engine _engine;

        public SystemTray()
        {
            _engine = new Engine(_container);

            #region Initialization

            var resource = new ResourceCulture();
            var font = new Font("Segoe UI", 10.5F);
            _about = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.TrayAbout),
                Font = font
            };
            _smallIcon = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.TraySmallIcon),
                Font = font
            };
            _animation = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.TrayAnimation),
                Font = font
            };
            _pauseInTabletMode = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.TrayPauseInTabletMode),
                Font = font
            };
            _reverseDisplayModeBehavior = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.TrayReverseDisplayModeBehavior),
                Font = font
            };
            _reverseSizeModeBehavior = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.TrayReverseSizeModeBehavior),
                Font = font
            };
            _autoDisplay = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.TrayAutoDisplay),
                Font = font
            };
            _autoSize = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.TrayAutoSize),
                Font = font
            };
            _showTaskbarWhenExit = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.TrayShowBarOnExit),
                Font = font
            };
            _exit = new ToolStripMenuItem
            {
                Text = resource.GetString(LangName.TrayExit),
                Font = font
            };
            _contextMenuStrip = new ContextMenuStrip
            {
                Renderer = new Win10Renderer()
            };

            _contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                _about,
                new ToolStripSeparator(),
                _smallIcon,
                _animation,
                _pauseInTabletMode,
                new ToolStripSeparator(),
                _reverseDisplayModeBehavior,
                _reverseSizeModeBehavior,
                new ToolStripSeparator(),
                _autoDisplay,
                _autoSize,
                new ToolStripSeparator(),
                _showTaskbarWhenExit,
                _exit
            });

            _notifyIcon = new NotifyIcon(_container)
            {
                ContextMenuStrip = _contextMenuStrip,
                Text = @"SmartTaskbar v1.4.0",
                Icon = Fun.IsLightTheme() ? Resources.Logo_Black : Resources.Logo_White,
                Visible = true
            };

            #endregion

            #region Load Event

            _about.Click += AboutOnClick;

            _smallIcon.Click += SmallIconOnClick;

            _animation.Click += AnimationOnClick;

            _pauseInTabletMode.Click += PauseInTabletModeOnClick;

            _reverseDisplayModeBehavior.Click += ReverseDisplayModeBehaviorOnClick;

            _reverseSizeModeBehavior.Click += ReverseSizeModeBehaviorOnClick;

            _autoSize.Click += AutoSizeOnClick;

            _autoDisplay.Click += AutoDisplayOnClick;

            _showTaskbarWhenExit.Click += ShowTaskbarWhenExitOnClick;

            _exit.Click += ExitOnClick;

            _notifyIcon.MouseClick += NotifyIconOnMouseClick;

            _notifyIcon.MouseDoubleClick += NotifyIconOnMouseDoubleClick;

            Fun.UISettings.ColorValuesChanged += UISettingsOnColorValuesChanged;

            #endregion
        }

        private void ShowTaskbarWhenExitOnClick(object sender, EventArgs e)
            => UserSettings.ShowTaskbarWhenExit = !_showTaskbarWhenExit.Checked;

        private void ReverseSizeModeBehaviorOnClick(object sender, EventArgs e)
            => UserSettings.ReverseSizeModeBehavior = !_reverseSizeModeBehavior.Checked;

        private void ReverseDisplayModeBehaviorOnClick(object sender, EventArgs e)
            => UserSettings.ReverseDisplayModeBehavior = !_reverseDisplayModeBehavior.Checked;

        private void PauseInTabletModeOnClick(object sender, EventArgs e)
            => UserSettings.PauseInTabletMode = !_pauseInTabletMode.Checked;

        private void UISettingsOnColorValuesChanged(UISettings sender, object args)
            => _notifyIcon.Icon = Fun.IsLightTheme() ? Resources.Logo_Black : Resources.Logo_White;

        private void NotifyIconOnMouseDoubleClick(object s, MouseEventArgs e)
        {
            UserSettings.AutoModeType = AutoModeType.None;
            Fun.ChangeAutoHide();
        }

        private void NotifyIconOnMouseClick(object s, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            _animation.Checked = Fun.IsEnableTaskbarAnimation();

            _smallIcon.Checked = Fun.IsUseSmallIcon();

            _pauseInTabletMode.Checked = UserSettings.PauseInTabletMode;

            _reverseSizeModeBehavior.Checked = UserSettings.ReverseSizeModeBehavior;

            _reverseDisplayModeBehavior.Checked = UserSettings.ReverseDisplayModeBehavior;

            _showTaskbarWhenExit.Checked = UserSettings.ShowTaskbarWhenExit;

            switch (UserSettings.AutoModeType)
            {
                case AutoModeType.Display:
                    _autoDisplay.Checked = true;
                    _autoSize.Checked = false;
                    break;
                case AutoModeType.Size:
                    _autoDisplay.Checked = false;
                    _autoSize.Checked = true;
                    break;
                case AutoModeType.None:
                    _autoDisplay.Checked = _autoSize.Checked = false;
                    break;
            }
        }

        private void ExitOnClick(object s, EventArgs e)
        {
            if (UserSettings.ShowTaskbarWhenExit)
                Fun.CancelAutoHide();
            _container?.Dispose();
            Application.Exit();
        }

        private void AutoDisplayOnClick(object s, EventArgs e)
        {
            UserSettings.AutoModeType = _autoDisplay.Checked ? AutoModeType.None : AutoModeType.Display;
        }

        private void AutoSizeOnClick(object s, EventArgs e)
        {
            UserSettings.AutoModeType = _autoSize.Checked ? AutoModeType.None : AutoModeType.Size;
        }

        private void AnimationOnClick(object s, EventArgs e) { _animation.Checked = Fun.ChangeTaskbarAnimation(); }

        private void SmallIconOnClick(object s, EventArgs e) { Fun.ChangeIconSize(); }

        private void AboutOnClick(object s, EventArgs e)
        {
            Process.Start(@"https://github.com/ChanpleCai/SmartTaskbar");
        }
    }
}
