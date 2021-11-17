using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Properties;

namespace SmartTaskbar
{
    internal class SystemTray : ApplicationContext
    {
        private readonly ToolStripMenuItem _about;
        private readonly ToolStripMenuItem _animation;
        private readonly ToolStripMenuItem _autoDisplay;
        private readonly ToolStripMenuItem _autoSize;
        private readonly ContextMenuStrip _contextMenuStrip;
        private readonly ToolStripMenuItem _exit;
        private readonly NotifyIcon _notifyIcon;
        private readonly ToolStripMenuItem _smallIcon;

        private readonly UserSettings _userSettings = new UserSettings();

        public SystemTray()
        {
            #region Initialization

            var resource = new ResourceCulture();
            var font = new Font("Segoe UI", 10.5F);
            _about = new ToolStripMenuItem
            {
                Text = resource.GetString("tray_about"),
                Font = font
            };
            _smallIcon = new ToolStripMenuItem
            {
                Text = resource.GetString("tray_smallIcon"),
                Font = font
            };
            _animation = new ToolStripMenuItem
            {
                Text = resource.GetString("tray_animation"),
                Font = font
            };
            _autoSize = new ToolStripMenuItem
            {
                Text = resource.GetString("tray_auto_size"),
                Font = font
            };
            _autoDisplay = new ToolStripMenuItem
            {
                Text = resource.GetString("tray_auto_display"),
                Font = font
            };
            _exit = new ToolStripMenuItem
            {
                Text = resource.GetString("tray_exit"),
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
                new ToolStripSeparator(),
                _autoDisplay,
                _autoSize,
                new ToolStripSeparator(),
                _exit
            });

            _notifyIcon = new NotifyIcon
            {
                ContextMenuStrip = _contextMenuStrip,
                Text = @"SmartTaskbar v1.3.0",
                Icon = Fun.IsLightTheme() ? Resources.Logo_Black : Resources.Logo_White,
                Visible = true
            };

            #endregion

            #region Load Event

            _about.Click += AboutOnClick;

            _smallIcon.Click += SmallIconOnClick;

            _animation.Click += AnimationOnClick;

            _autoSize.Click += AutoSizeOnClick;

            _autoDisplay.Click += AutoDisplayOnClick;

            _exit.Click += ExitOnClick;

            _notifyIcon.MouseClick += NotifyIconOnMouseClick;

            _notifyIcon.MouseDoubleClick += NotifyIconOnMouseDoubleClick;

            #endregion

            #region Load Settings

            switch (_userSettings.AutoModeType)
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

            #endregion
        }

        private void NotifyIconOnMouseDoubleClick(object s, MouseEventArgs e)
        {
            _userSettings.AutoModeType = AutoModeType.None;
            Fun.ChangeAutoHide();
        }

        private void NotifyIconOnMouseClick(object s, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            _animation.Checked = Fun.IsEnableTaskbarAnimation();

            _smallIcon.Checked = Fun.IsUseSmallIcon();
        }

        private void ExitOnClick(object s, EventArgs e)
        {
            // todo
            _notifyIcon.Dispose();
            Application.Exit();
        }

        private void AutoDisplayOnClick(object s, EventArgs e)
        {
            _userSettings.AutoModeType = _autoDisplay.Checked ? AutoModeType.None : AutoModeType.Display;
        }

        private void AutoSizeOnClick(object s, EventArgs e)
        {
            _userSettings.AutoModeType = _autoSize.Checked ? AutoModeType.None : AutoModeType.Size;
        }

        private void AnimationOnClick(object s, EventArgs e) { _animation.Checked = Fun.ChangeTaskbarAnimation(); }

        private void SmallIconOnClick(object s, EventArgs e) { Fun.ChangeIconSize(); }

        private void AboutOnClick(object s, EventArgs e)
        {
            Process.Start(@"https://github.com/ChanpleCai/SmartTaskbar");
        }
    }
}
