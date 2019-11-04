using System.Reactive.Disposables;
using System.Windows.Forms;
using ReactiveUI;
using SmartTaskbar.Model;
using SmartTaskbar.ViewModels;

namespace SmartTaskbar.Views
{
    public partial class SettingForm : Form, IViewFor<SettingFormViewModel>
    {
        public SettingForm(CoreInvoker taskbarController)
        {
            InitializeComponent();

            #region Reactive Binding

            ViewModel = new SettingFormViewModel(taskbarController);

            this.WhenActivated(disposables =>
            {
                #region Icon

                this.OneWayBind(ViewModel,
                        m => m.IconStyle,
                        v => v.Icon,
                        vmToViewConverterOverride: new IconStyleIconConverter())
                    .DisposeWith(disposables);

                #endregion

                #region Text

                #region AutoModeGroupBox

                this.OneWayBind(ViewModel,
                        m => m.SettingModeText,
                        v => v.groupBox_AutoMode.Text)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                        m => m.SettingDisableText,
                        v => v.radioButtonDisableMode.Text)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                        m => m.SettingForegroundModeText,
                        v => v.radioButtonForegroundMode.Text)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                        m => m.SettingBlacklistModeText,
                        v => v.radioButtonBlacklistMode.Text)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                        m => m.SettingWhitelistModeText,
                        v => v.radioButtonWhitelistMode.Text)
                    .DisposeWith(disposables);

                #endregion

                #endregion

                #region AutoModeGroupBox

                this.Bind(ViewModel,
                    m => m.IsSettingDisable,
                    v => v.radioButtonDisableMode.Checked);

                this.Bind(ViewModel,
                    m => m.IsSettingForegroundMode,
                    v => v.radioButtonForegroundMode.Checked);

                this.Bind(ViewModel,
                    m => m.IsSettingBlacklistMode,
                    v => v.radioButtonBlacklistMode.Checked);

                this.Bind(ViewModel,
                    m => m.IsSettingWhitelistMode,
                    v => v.radioButtonWhitelistMode.Checked);

                #endregion
            });

            #endregion
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (SettingFormViewModel) value;
        }

        public SettingFormViewModel ViewModel { get; set; }
    }
}