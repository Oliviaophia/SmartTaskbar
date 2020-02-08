using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Model;

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

            Activated += (s, e) =>
            {
                ChangeTheme();
                SetPosition();
            };
            Deactivate += (s, e) => Hide();

            #region Initialization

            exitMenuButton.Text = coreInvoker.GetText("TrayExit");
            exitMenuButton.Image = Properties.Resources.Empty;
            exitMenuButton.Click += (s, e) => Application.Exit();

            disableButton.Text = coreInvoker.GetText("TrayDisable");
            disableButton.Image = Properties.Resources.Empty;

            autoButton.Text = coreInvoker.GetText("TrayForegroundMode");
            autoButton.Image = Properties.Resources.Empty;

            WhitelistButton.Text = coreInvoker.GetText("TrayWhitelistMode");
            WhitelistButton.Image = Properties.Resources.Empty;

            BlacklistButton.Text = coreInvoker.GetText("TrayBlacklistMode");
            BlacklistButton.Image = Properties.Resources.Empty;

            #endregion
        }

        #region Helper

        private void ChangeTheme()
        {
            var islight = InvokeMethods.IsLightTheme();

            BackColor = islight ? Color.FromArgb(238,238,238) : Color.FromArgb(43, 43, 43);
            ForeColor = islight ? Color.Black : Color.White;
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