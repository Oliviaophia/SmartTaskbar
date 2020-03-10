using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Model;
using SmartTaskbar.Properties;

namespace SmartTaskbar.Views
{
    public partial class MainContextMenu : Form
    {
        private static MainSettingForm _mainSettingForm;
        private readonly CoreInvoker _coreInvoker;
        private readonly IContainer _container;

        public MainSettingForm MainSettingFormInstance
        {
            get
            {
                if (_mainSettingForm == null || _mainSettingForm.IsDisposed)
                    _mainSettingForm = new MainSettingForm(_container, _coreInvoker);

                return _mainSettingForm;
            }
        }

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

            WhitelistButton.Text = coreInvoker.GetText("TrayWhitelistMode");
            WhitelistButton.Click += (s, e) =>
            {
                _coreInvoker.UserSettings.ModeType = AutoModeType.WhitelistMode;
                _coreInvoker.ModeSwitch.LoadSetting();
                SetAutoModeTypeIcon();
                _coreInvoker.SaveUserSettings();
            };

            BlacklistButton.Text = coreInvoker.GetText("TrayBlacklistMode");
            BlacklistButton.Click += (s, e) =>
            {
                _coreInvoker.UserSettings.ModeType = AutoModeType.BlacklistMode;
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
            settingsButton.Click += (s, e) => MainSettingFormInstance.Show();

            aboutButton.Text = coreInvoker.GetText("TrayAbout");
            aboutButton.Image = Resources.Empty;
            aboutButton.Click += (s, e) => Process.Start("https://github.com/ChanpleCai/SmartTaskbar/releases");

            #endregion
        }

        #region Helper

        private void ChangeTheme()
        {
            var islight = InvokeMethods.IsLightTheme();

            BackColor = islight ? Color.FromArgb(238, 238, 238) : Color.FromArgb(43, 43, 43);
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
                    WhitelistButton.Image =
                        BlacklistButton.Image =
                            foreButton.Image =
                                apiButton.Image =
                                    Resources.Empty;
                    break;
                case AutoModeType.AutoHideApiMode:
                    apiButton.Image = islight ? Resources.Run_Black : Resources.Run_White;
                    WhitelistButton.Image =
                        BlacklistButton.Image =
                            foreButton.Image =
                                stopButton.Image =
                                    Resources.Empty;
                    break;
                case AutoModeType.ForegroundMode:
                    foreButton.Image = islight ? Resources.Run_Black : Resources.Run_White;
                    WhitelistButton.Image =
                        BlacklistButton.Image =
                            apiButton.Image =
                                stopButton.Image =
                                    Resources.Empty;
                    break;
                case AutoModeType.BlacklistMode:
                    BlacklistButton.Image = islight ? Resources.Run_Black : Resources.Run_White;
                    WhitelistButton.Image =
                        apiButton.Image =
                            foreButton.Image =
                                stopButton.Image =
                                    Resources.Empty;
                    break;
                case AutoModeType.WhitelistMode:
                    WhitelistButton.Image = islight ? Resources.Run_Black : Resources.Run_White;
                    apiButton.Image =
                        BlacklistButton.Image =
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
                const int csDropshadow = 0x20000;
                var cp = base.CreateParams;
                cp.ClassStyle |= csDropshadow;
                return cp;
            }
        }

        #endregion
    }
}