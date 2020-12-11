using System;
using System.Threading;
using System.Windows.Forms;
using Windows.System;
using Windows.UI.ViewManagement;
using SmartTaskbar.Engines;
using SmartTaskbar.Models;
using SmartTaskbar.PlatformInvoke;
using SmartTaskbar.UI.Languages;

namespace SmartTaskbar.UI.Views
{
    public partial class MainContextMenu : Form
    {
        private readonly AutoModeWorker _autoModeWorker;
        private readonly CultureResource _cultureResource;
        private readonly Lazy<MainSettingForm> _mainSettingForm;
        private readonly UserConfigEngine<MainViewModel> _userConfigEngine;

        public MainContextMenu(UserConfigEngine<MainViewModel> userConfigEngine,
                               CultureResource                 cultureResource,
                               AutoModeWorker                  autoModeWorker)
        {
            InitializeComponent();

            _userConfigEngine = userConfigEngine;
            _cultureResource = cultureResource;
            _autoModeWorker = autoModeWorker;
            _mainSettingForm =
                new Lazy<MainSettingForm>(() => new MainSettingForm(userConfigEngine, cultureResource),
                                          LazyThreadSafetyMode.ExecutionAndPublication);

            #region Events

            VisibleChanged += MainContextMenu_VisibleChanged;
            Activated += MainContextMenu_Activated;
            Deactivate += MainContextMenu_Deactivate;
            UIInfo.Settings.ColorValuesChanged += Settings_ColorValuesChanged;

            exitMenuButton.Click += OnExitMenuButtonOnClick;
            stopButton.Click += OnStopButtonOnClick;
            AllowlistButton.Click += OnAllowlistButtonOnClick;
            BlockListButton.Click += OnBlockListButtonOnClick;
            foreButton.Click += OnForeButtonOnClick;
            apiButton.Click += OnApiButtonOnClick;
            settingsButton.Click += OnSettingsButtonOnClick;
            aboutButton.Click += OnAboutButtonOnClick;

            #endregion

            #region Initialization

            UpdateTheme();
            UpdateText();

            aboutButton.Image = IconResources.Empty;
            exitMenuButton.Image = IconResources.Empty;

            #endregion
        }

        #region Events

        private void OnAboutButtonOnClick(object? s, EventArgs e)
        {
            _ = Launcher.LaunchUriAsync(new Uri("https://github.com/ChanpleCai/SmartTaskbar/releases"));
        }

        private void OnSettingsButtonOnClick(object? s, EventArgs e) { _mainSettingForm.Value.BringUp(); }

        private void OnApiButtonOnClick(object? s, EventArgs e) { SetAutoModeType(AutoModeType.AutoHideApiMode); }

        private void OnForeButtonOnClick(object? s, EventArgs e) { SetAutoModeType(AutoModeType.ForegroundMode); }

        private void OnBlockListButtonOnClick(object? s, EventArgs e) { SetAutoModeType(AutoModeType.BlockListMode); }

        private void OnAllowlistButtonOnClick(object? s, EventArgs e) { SetAutoModeType(AutoModeType.AllowListMode); }

        private void OnStopButtonOnClick(object? s, EventArgs e) { SetAutoModeType(AutoModeType.Disable); }

        private void OnExitMenuButtonOnClick(object? s, EventArgs e)
        {
            _autoModeWorker.ResetTaskbarState();
            Application.Exit();
        }

        private void Settings_ColorValuesChanged(UISettings sender, object? args)
        {
            if (InvokeRequired)
                BeginInvoke(new MethodInvoker(UpdateTheme));
            else
                UpdateTheme();
        }

        private void MainContextMenu_Deactivate(object? sender, EventArgs e)
            => Hide();

        private void MainContextMenu_Activated(object? sender, EventArgs e)
            => SetPosition();

        private void MainContextMenu_VisibleChanged(object? sender, EventArgs e)
        {
            if (Visible) Activate();
        }

        #endregion

        #region Helper

        private void UpdateText()
        {
            exitMenuButton.Text = _cultureResource.GetText("TrayExit");
            stopButton.Text = _cultureResource.GetText("TrayStop");
            AllowlistButton.Text = _cultureResource.GetText("TrayAllowlistMode");
            BlockListButton.Text = _cultureResource.GetText("TrayBlockListMode");
            foreButton.Text = _cultureResource.GetText("TrayAutoMode2");
            apiButton.Text = _cultureResource.GetText("TrayAutoMode1");
            settingsButton.Text = _cultureResource.GetText("TraySettings");
            aboutButton.Text = _cultureResource.GetText("TrayAbout");
        }

        private void UpdateTheme()
        {
            BackColor = UIInfo.Background;
            ForeColor = UIInfo.ForeGround;

            settingsButton.Image = UIInfo.IsWhiteBackground ? IconResources.Setting_Black : IconResources.Setting_White;

            //todo

            LoadAutoModeTypeIcon(_userConfigEngine.ViewModel.AutoModeType);
        }

        private void SetAutoModeType(AutoModeType type)
        {
            _ = _userConfigEngine.Update(x => x with
                                                  {
                                                  AutoModeType = type
                                                  } as MainViewModel
                                              ?? throw new InvalidOperationException("UserConfiguration is null")
            );

            LoadAutoModeTypeIcon(type);
        }

        private void LoadAutoModeTypeIcon(AutoModeType type)
        {
            switch (type)
            {
                case AutoModeType.Disable:
                    stopButton.Image = UIInfo.IsWhiteBackground ? IconResources.Pause_Black : IconResources.Pause_White;
                    AllowlistButton.Image =
                        BlockListButton.Image =
                            foreButton.Image =
                                apiButton.Image =
                                    IconResources.Empty;
                    break;
                case AutoModeType.AutoHideApiMode:
                    apiButton.Image = UIInfo.IsWhiteBackground ? IconResources.Run_Black : IconResources.Run_White;
                    AllowlistButton.Image =
                        BlockListButton.Image =
                            foreButton.Image =
                                stopButton.Image =
                                    IconResources.Empty;
                    break;
                case AutoModeType.ForegroundMode:
                    foreButton.Image = UIInfo.IsWhiteBackground ? IconResources.Run_Black : IconResources.Run_White;
                    AllowlistButton.Image =
                        BlockListButton.Image =
                            apiButton.Image =
                                stopButton.Image =
                                    IconResources.Empty;
                    break;
                case AutoModeType.BlockListMode:
                    BlockListButton.Image =
                        UIInfo.IsWhiteBackground ? IconResources.Run_Black : IconResources.Run_White;
                    AllowlistButton.Image =
                        apiButton.Image =
                            foreButton.Image =
                                stopButton.Image =
                                    IconResources.Empty;
                    break;
                case AutoModeType.AllowListMode:
                    AllowlistButton.Image =
                        UIInfo.IsWhiteBackground ? IconResources.Run_Black : IconResources.Run_White;
                    apiButton.Image =
                        BlockListButton.Image =
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

            Left = mouse.X + Width < workArea.Right
                ? workArea.Left < mouse.X
                    ? mouse.X
                    : workArea.Left + Offset
                : mouse.X < workArea.Right
                    ? mouse.X - Width
                    : workArea.Right - Width - Offset;
            Top = mouse.Y + Height < workArea.Bottom
                ? workArea.Top < mouse.Y
                    ? mouse.Y
                    : workArea.Top + Offset
                : mouse.Y < workArea.Bottom
                    ? mouse.Y - Height
                    : workArea.Bottom - Height - Offset;
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
