using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using SmartTaskbar.Engines;
using SmartTaskbar.Models;
using SmartTaskbar.PlatformInvoke;
using SmartTaskbar.Tray.Languages;

namespace SmartTaskbar.Tray.Views
{
    public partial class MainContextMenu : Form
    {
        private readonly IContainer _container;
        private readonly UserConfigEngine _userConfigEngine;
        private readonly CultureResource _cultureResource;

        public MainContextMenu(IContainer container, UserConfigEngine userConfigEngine, CultureResource cultureResource)
        {
            InitializeComponent();

            _container = container;
            _userConfigEngine = userConfigEngine;
            _cultureResource = cultureResource;

            VisibleChanged += (s, e) =>
            {
                if (Visible) Activate();
            };
            Activated += (s, e) =>
            {
                ChangeTheme();
                SetPosition();
            };
            Deactivate += (s, e) => Hide();

            #region Initialization

            exitMenuButton.Text = cultureResource.GetText("TrayExit");
            exitMenuButton.Image = IconResources.Empty;
            exitMenuButton.Click += (s, e) => Application.Exit();

            stopButton.Text = cultureResource.GetText("TrayStop");
            stopButton.Click += (s, e) => { SetAutoModeTypeIcon(AutoModeType.Disable); };

            WhitelistButton.Text = cultureResource.GetText("TrayWhitelistMode");
            WhitelistButton.Click += (s, e) => { SetAutoModeTypeIcon(AutoModeType.WhitelistMode); };

            BlacklistButton.Text = cultureResource.GetText("TrayBlacklistMode");
            BlacklistButton.Click += (s, e) => { SetAutoModeTypeIcon(AutoModeType.BlacklistMode); };

            foreButton.Text = cultureResource.GetText("TrayAutoMode2");
            foreButton.Click += (s, e) => { SetAutoModeTypeIcon(AutoModeType.ForegroundMode); };

            apiButton.Text = cultureResource.GetText("TrayAutoMode1");
            apiButton.Click += (s, e) => { SetAutoModeTypeIcon(AutoModeType.AutoHideApiMode); };

            settingsButton.Text = cultureResource.GetText("TraySettings");

            aboutButton.Text = cultureResource.GetText("TrayAbout");
            aboutButton.Image = IconResources.Empty;
            aboutButton.Click += (s, e) => Process.Start("https://github.com/ChanpleCai/SmartTaskbar/releases");

            #endregion
        }

        #region Helper

        private void ChangeTheme()
        {
            BackColor = UIInfo.Background;
            ForeColor = UIInfo.ForeGround;

            settingsButton.Image = UIInfo.IsLightTheme() ? IconResources.Setting_Black : IconResources.Setting_White;

            //todo

            //SetAutoModeTypeIcon();
        }

        private void SetAutoModeTypeIcon(AutoModeType type)
        {
            switch (type)
            {
                case AutoModeType.Disable:
                    stopButton.Image = UIInfo.IsLightTheme() ? IconResources.Pause_Black : IconResources.Pause_White;
                    WhitelistButton.Image =
                        BlacklistButton.Image =
                            foreButton.Image =
                                apiButton.Image =
                                    IconResources.Empty;
                    break;
                case AutoModeType.AutoHideApiMode:
                    apiButton.Image = UIInfo.IsLightTheme() ? IconResources.Run_Black : IconResources.Run_White;
                    WhitelistButton.Image =
                        BlacklistButton.Image =
                            foreButton.Image =
                                stopButton.Image =
                                    IconResources.Empty;
                    break;
                case AutoModeType.ForegroundMode:
                    foreButton.Image = UIInfo.IsLightTheme() ? IconResources.Run_Black : IconResources.Run_White;
                    WhitelistButton.Image =
                        BlacklistButton.Image =
                            apiButton.Image =
                                stopButton.Image =
                                    IconResources.Empty;
                    break;
                case AutoModeType.BlacklistMode:
                    BlacklistButton.Image = UIInfo.IsLightTheme() ? IconResources.Run_Black : IconResources.Run_White;
                    WhitelistButton.Image =
                        apiButton.Image =
                            foreButton.Image =
                                stopButton.Image =
                                    IconResources.Empty;
                    break;
                case AutoModeType.WhitelistMode:
                    WhitelistButton.Image = UIInfo.IsLightTheme() ? IconResources.Run_Black : IconResources.Run_White;
                    apiButton.Image =
                        BlacklistButton.Image =
                            foreButton.Image =
                                stopButton.Image =
                                    IconResources.Empty;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private const int Offset = 5;

        /// <summary>
        ///     Simulate where the menu appears
        /// </summary>
        private void SetPosition()
        {
            var mouse = MousePosition;

            var workArea = Screen.GetWorkingArea(mouse);

            // todo for unknown reason the Taskbar may cover other windows
            Left = mouse.X + Width < workArea.Right ? mouse.X :
                mouse.X < workArea.Right ? mouse.X - Width : workArea.Right - Width - Offset;
            Top = mouse.Y + Height < workArea.Bottom ? mouse.Y :
                mouse.Y < workArea.Bottom ? mouse.Y - Height : workArea.Bottom - Height - Offset;
        }

        /// <summary>
        ///     Add shadow
        ///     https://stackoverflow.com/a/16495142
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int csDropShadow = 0x20000;
                var cp = base.CreateParams;
                cp.ClassStyle |= csDropShadow;
                return cp;
            }
        }

        #endregion
    }
}