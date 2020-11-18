using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Windows.System;
using SmartTaskbar.Engines;
using SmartTaskbar.Models;
using SmartTaskbar.PlatformInvoke;
using SmartTaskbar.UI.Languages;
using SmartTaskbar.UI.ViewModels;

namespace SmartTaskbar.UI.Views
{
    public partial class MainContextMenu : Form
    {
        private readonly IContainer _container;
        private readonly CultureResource _cultureResource;
        private readonly MainContextMenuViewModel _mainContextMenuViewModel;
        private readonly UserConfigEngine _userConfigEngine;
        private readonly Lazy<MainSettingForm> _mainSettingForm;

        public MainContextMenu(IContainer container, UserConfigEngine userConfigEngine, CultureResource cultureResource)
        {
            InitializeComponent();

            _container = container;
            _userConfigEngine = userConfigEngine;
            _mainContextMenuViewModel = userConfigEngine.InitViewModel<MainContextMenuViewModel>();
            _cultureResource = cultureResource;
            _mainSettingForm =
                new Lazy<MainSettingForm>(() => new MainSettingForm(), LazyThreadSafetyMode.ExecutionAndPublication);

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
            stopButton.Click += (s, e) => { SetAutoModeType(AutoModeType.Disable); };

            AllowlistButton.Text = cultureResource.GetText("TrayAllowlistMode");
            AllowlistButton.Click += (s, e) => { SetAutoModeType(AutoModeType.AllowlistMode); };

            BlockListButton.Text = cultureResource.GetText("TrayBlockListMode");
            BlockListButton.Click += (s, e) => { SetAutoModeType(AutoModeType.BlockListMode); };

            foreButton.Text = cultureResource.GetText("TrayAutoMode2");
            foreButton.Click += (s, e) => { SetAutoModeType(AutoModeType.ForegroundMode); };

            apiButton.Text = cultureResource.GetText("TrayAutoMode1");
            apiButton.Click += (s, e) => { SetAutoModeType(AutoModeType.AutoHideApiMode); };

            settingsButton.Text = cultureResource.GetText("TraySettings");
            settingsButton.Click += (s, e) => { _mainSettingForm.Value.Show(); };

            aboutButton.Text = cultureResource.GetText("TrayAbout");
            aboutButton.Image = IconResources.Empty;
            aboutButton.Click += (s, e)
                => _ = Launcher.LaunchUriAsync(new Uri("https://github.com/ChanpleCai/SmartTaskbar/releases"));

            #endregion
        }

        #region Helper

        private void ChangeTheme()
        {
            BackColor = UIInfo.Background;
            ForeColor = UIInfo.ForeGround;

            settingsButton.Image = UIInfo.IsWhiteBackground ? IconResources.Setting_Black : IconResources.Setting_White;

            //todo

            LoadAutoModeTypeIcon(_mainContextMenuViewModel.AutoModeType);
        }

        private void SetAutoModeType(AutoModeType type)
        {
            _userConfigEngine.Update(x => x.AutoModeType = type);

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
                case AutoModeType.AllowlistMode:
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

            // todo for unknown reason the Taskbar may cover other windows
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
                const int csDropShadow = 0x20000;
                var cp = base.CreateParams;
                cp.ClassStyle |= csDropShadow;
                return cp;
            }
        }

        #endregion
    }
}
