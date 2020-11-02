using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Models;
using SmartTaskbar.Properties;

namespace SmartTaskbar.Views
{
    public partial class MainContextMenu : Form
    {
        private readonly IContainer _container;
        private readonly CoreInvoker _coreInvoker;

        public MainContextMenu(IContainer container, CoreInvoker coreInvoker)
        {
            InitializeComponent();

            _container = container;
            _coreInvoker = coreInvoker;

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

            exitMenuButton.Text = coreInvoker.GetText("TrayExit");
            exitMenuButton.Image = Resources.Empty;
            exitMenuButton.Click += (s, e) => Application.Exit();

            stopButton.Text = coreInvoker.GetText("TrayStop");
            stopButton.Click += (s, e) =>
            {
                _coreInvoker.UserSettings.ModeType = AutoModeType.Disable;
                _coreInvoker.ModeSwitch.LoadSetting();
                SetAutoModeTypeIcon();
                _coreInvoker.SaveUserSettings();
            };

            AllowlistButton.Text = coreInvoker.GetText("TrayAllowlistMode");
            AllowlistButton.Click += (s, e) =>
            {
                _coreInvoker.UserSettings.ModeType = AutoModeType.AllowlistMode;
                _coreInvoker.ModeSwitch.LoadSetting();
                SetAutoModeTypeIcon();
                _coreInvoker.SaveUserSettings();
            };

            BlockListButton.Text = coreInvoker.GetText("TrayBlockListMode");
            BlockListButton.Click += (s, e) =>
            {
                _coreInvoker.UserSettings.ModeType = AutoModeType.BlockListMode;
                _coreInvoker.ModeSwitch.LoadSetting();
                SetAutoModeTypeIcon();
                _coreInvoker.SaveUserSettings();
            };

            foreButton.Text = coreInvoker.GetText("TrayAutoMode2");
            foreButton.Click += (s, e) =>
            {
                _coreInvoker.UserSettings.ModeType = AutoModeType.ForegroundMode;
                _coreInvoker.ModeSwitch.LoadSetting();
                SetAutoModeTypeIcon();
                _coreInvoker.SaveUserSettings();
            };

            apiButton.Text = coreInvoker.GetText("TrayAutoMode1");
            apiButton.Click += (s, e) =>
            {
                _coreInvoker.UserSettings.ModeType = AutoModeType.AutoHideApiMode;
                _coreInvoker.ModeSwitch.LoadSetting();
                SetAutoModeTypeIcon();
                _coreInvoker.SaveUserSettings();
            };

            settingsButton.Text = coreInvoker.GetText("TraySettings");

            aboutButton.Text = coreInvoker.GetText("TrayAbout");
            aboutButton.Image = Resources.Empty;
            aboutButton.Click += (s, e) => Process.Start("https://github.com/ChanpleCai/SmartTaskbar/releases");

            #endregion
        }

        #region Helper

        private void ChangeTheme()
        {
            var islight = InvokeMethods.IsLightTheme();

            BackColor = islight ? ViewColor.DarkBackColor : ViewColor.LightBackColor;
            ForeColor = islight ? Color.Black : Color.White;

            settingsButton.Image = islight ? Resources.Setting_Black : Resources.Setting_White;

            SetAutoModeTypeIcon();
        }

        private void SetAutoModeTypeIcon()
        {
            var islight = InvokeMethods.IsLightTheme();
            switch (_coreInvoker.UserSettings.ModeType)
            {
                case AutoModeType.Disable:
                    stopButton.Image = islight ? Resources.Pause_Black : Resources.Pause_White;
                    AllowlistButton.Image =
                        BlockListButton.Image =
                            foreButton.Image =
                                apiButton.Image =
                                    Resources.Empty;
                    break;
                case AutoModeType.AutoHideApiMode:
                    apiButton.Image = islight ? Resources.Run_Black : Resources.Run_White;
                    AllowlistButton.Image =
                        BlockListButton.Image =
                            foreButton.Image =
                                stopButton.Image =
                                    Resources.Empty;
                    break;
                case AutoModeType.ForegroundMode:
                    foreButton.Image = islight ? Resources.Run_Black : Resources.Run_White;
                    AllowlistButton.Image =
                        BlockListButton.Image =
                            apiButton.Image =
                                stopButton.Image =
                                    Resources.Empty;
                    break;
                case AutoModeType.BlockListMode:
                    BlockListButton.Image = islight ? Resources.Run_Black : Resources.Run_White;
                    AllowlistButton.Image =
                        apiButton.Image =
                            foreButton.Image =
                                stopButton.Image =
                                    Resources.Empty;
                    break;
                case AutoModeType.AllowlistMode:
                    AllowlistButton.Image = islight ? Resources.Run_Black : Resources.Run_White;
                    apiButton.Image =
                        BlockListButton.Image =
                            foreButton.Image =
                                stopButton.Image =
                                    Resources.Empty;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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

            // todo For some reason the taskbar will cover other windows
            Left = mouse.X + Width < workArea.Right ? mouse.X :
                mouse.X < workArea.Right            ? mouse.X - Width : workArea.Right - Width - Offset;
            Top = mouse.Y + Height < workArea.Bottom ? mouse.Y :
                mouse.Y < workArea.Bottom            ? mouse.Y - Height : workArea.Bottom - Height - Offset;
        }

        /// <summary>
        ///     Add shadow
        ///     https://stackoverflow.com/a/16495142
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int csDropshadow = 0x20000;
                var cp = base.CreateParams;
                cp.ClassStyle |= csDropshadow;
                return cp;
            }
        }

        #endregion
    }
}
