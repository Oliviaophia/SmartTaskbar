using System;
using System.ComponentModel;
using System.Drawing;
using System.Reactive.Disposables;
using System.Windows.Forms;
using ReactiveUI;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Model;
using SmartTaskbar.Properties;
using SmartTaskbar.ViewModels;

namespace SmartTaskbar.Views
{
    internal class Tray : Form, IViewFor<TrayViewModel>
    {
        private static SettingForm _settingForm;
        private readonly CoreInvoker _autotaskbarController;
        private readonly ToolStripMenuItem _exit;
        private readonly NotifyIcon _notifyIcon;
        private readonly ToolStripMenuItem _settings;

        public Tray(IContainer container, CoreInvoker autotaskbarController)
        {
            _autotaskbarController = autotaskbarController;

            #region Initialization

            ViewModel = new TrayViewModel(autotaskbarController);
            var font = new Font("Segoe UI", 9F);

            _settings = new ToolStripMenuItem
            {
                Text = ViewModel.TraySettingsText,
                Font = font
            };

            _exit = new ToolStripMenuItem
            {
                Text = ViewModel.TrayExitText,
                Font = font
            };

            var contextMenuStrip = new ContextMenuStrip(container)
            {
                Renderer = new TrayRenderer()
            };

            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                _settings,
                new ToolStripSeparator(),
                _exit
            });

            _notifyIcon = new NotifyIcon(container)
            {
                ContextMenuStrip = contextMenuStrip,
                Text = Application.ProductName,
                Icon = GetIcon(),
                Visible = true
            };

            _settings.Click += Settings_Click;
            _exit.Click += Exit_Click;
            _notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;

            #endregion

            #region Reactive Binding

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, model => model.IconStyle,
                        view => view._notifyIcon.Icon,
                        vmToViewConverterOverride: new IconStyleIconConverter())
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel, model => model.TraySettingsText,
                        view => view._settings.Text)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel, model => model.TrayExitText,
                        view => view._exit.Text)
                    .DisposeWith(disposables);
            });

            #endregion
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TrayViewModel)value;
        }

        public TrayViewModel ViewModel { get; set; }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            ShowSettingForm();
        }

        public void ShowSettingForm()
        {
            if (_settingForm == null || _settingForm.IsDisposed)
                _settingForm = new SettingForm(_autotaskbarController);

            _settingForm.Show();
        }

        private Icon GetIcon()
        {
            switch (ViewModel.IconStyle)
            {
                case IconStyle.Black:
                    return Resources.Logo_Black;
                case IconStyle.Blue:
                    return Resources.Logo_Blue;
                case IconStyle.Pink:
                    return Resources.Logo_Pink;
                case IconStyle.White:
                    return Resources.Logo_White;
                case IconStyle.Auto:
                    return InvokeMethods.IsLightTheme()
                        ? Resources.Logo_Black
                        : Resources.Logo_White;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}