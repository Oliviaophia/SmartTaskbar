using System;
using System.ComponentModel;
using System.Drawing;
using System.Reactive.Disposables;
using System.Windows.Forms;
using ReactiveUI;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Properties;

namespace SmartTaskbar.Views
{
    internal class Tray : ApplicationContext, IViewFor<AppViewModel>, ISupportsActivation
    {
        private readonly Container _container;
        private readonly ToolStripMenuItem _settings;
        private readonly ToolStripMenuItem _exit;
        private readonly ContextMenuStrip _contextMenuStrip;
        private readonly NotifyIcon _notifyIcon;

        public Tray(Container container)
        {
            #region Initialization

            _container = container;
            var font = new Font("Segoe UI", 9F);

            _settings = new ToolStripMenuItem
            {
                Text = ViewModel.TraySettings,
                Font = font
            };

            _exit = new ToolStripMenuItem
            {
                Text = ViewModel.TrayExit,
                Font = font
            };

            _contextMenuStrip = new ContextMenuStrip(_container)
            {
                Renderer = new TrayRenderer()
            };

            _contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                _settings,
                new ToolStripSeparator(),
                _exit
            });

            _notifyIcon = new NotifyIcon(_container)
            {
                ContextMenuStrip = _contextMenuStrip,
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

                this.OneWayBind(ViewModel, model => model.TraySettings,
                        view => view._settings.Text)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel, model => model.TrayExit,
                        view => view._exit.Text)
                    .DisposeWith(disposables);
            });

            #endregion
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static void Settings_Click(object sender, EventArgs e)
        {
            SettingForm.Instance.Visible = !SettingForm.Instance.Visible;
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AppViewModel)value;
        }

        public AppViewModel ViewModel { get; set; } = AppViewModel.Instance;

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

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
                    ViewModel.IconStyle = IconStyle.Blue;
                    return Resources.Logo_Blue;
            }
        }
    }
}