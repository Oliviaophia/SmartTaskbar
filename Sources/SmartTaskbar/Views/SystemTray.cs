using Windows.System;

namespace SmartTaskbar
{
    internal class SystemTray : ApplicationContext
    {
        private readonly NotifyIcon notifyIcon;
        private readonly ContextMenuStrip contextMenuStrip;
        private readonly ToolStripMenuItem about;
        private readonly ToolStripMenuItem animation;
        private readonly ToolStripMenuItem auto_display;
        private readonly ToolStripMenuItem exit;

        private readonly Engine engine = new Engine();

        public SystemTray()
        {
            #region Initialization

            var resource = new ResourceCulture();
            var font = new Font("Segoe UI", 9F);
            about = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(about)),
                Font = font
            };
            animation = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(animation)),
                Font = font
            };
            auto_display = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(auto_display)),
                Font = font
            };
            exit = new ToolStripMenuItem
            {
                Text = resource.GetString(nameof(exit)),
                Font = font
            };
            contextMenuStrip = new ContextMenuStrip();

            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                about,
                animation,
                new ToolStripSeparator(),
                auto_display,
                new ToolStripSeparator(),
                exit
            });

            notifyIcon = new NotifyIcon
            {
                ContextMenuStrip = contextMenuStrip,
                Text = @"SmartTaskbar v1.2.0",
                Icon = UIInfo.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White,
                Visible = true
            };

            #endregion 

            #region Load Event

            about.Click += async (s, e) => await Launcher.LaunchUriAsync(new Uri(@"https://github.com/ChanpleCai/SmartTaskbar/releases"));

            UserSettings.Default.PropertyChanged += (s, e) =>
            {
                UserSettings.Default.Save();
                switch ((AutoModeType)UserSettings.Default.TaskbarState)
                {
                    case AutoModeType.Display:
                        auto_display.Checked = true;
                        AutoHideHelper.SetAutoHide(AutoHideHelper.NotAutoHide());
                        break;
                    case AutoModeType.None:
                        auto_display.Checked = false;
                        break;
                }
            };

            animation.Click += (s, e) => animation.Checked = Animation.ChangeTaskbarAnimation();

            auto_display.Click += (s, e) => UserSettings.Default.TaskbarState = auto_display.Checked ? (int)AutoModeType.None : (int)AutoModeType.Display;

            exit.Click += (s, e) =>
            {
                PostMessageHelper.HideTaskbar();
                AutoHideHelper.CancelAutoHide();
                notifyIcon?.Dispose();
                engine?.Dispose();
                Application.Exit();
            };

            notifyIcon.MouseClick += (s, e) =>
            {
                if (e.Button != MouseButtons.Right)
                    return;

                animation.Checked = Animation.GetTaskbarAnimation();
            };

            notifyIcon.MouseDoubleClick += (s, e) =>
            {
                UserSettings.Default.TaskbarState = (int)AutoModeType.None;
                AutoHideHelper.SetAutoHide(AutoHideHelper.NotAutoHide());
            };

            UIInfo.Settings.ColorValuesChanged += (s, e) =>
            {
                notifyIcon.Icon = UIInfo.IsLightTheme() ? IconResource.Logo_Black : IconResource.Logo_White;
            };

            #endregion

            #region Load Settings

            if (UserSettings.Default.TaskbarState == -1)
            {
                //Run the software for the first time
                UserSettings.Default.TaskbarState = (int)AutoModeType.Display;
            }
            else
            {
                switch ((AutoModeType)UserSettings.Default.TaskbarState)
                {
                    case AutoModeType.Display:
                        auto_display.Checked = true;
                        break;
                    case AutoModeType.None:
                        auto_display.Checked = false;
                        break;
                }
            }

            #endregion
        }
    }
}
