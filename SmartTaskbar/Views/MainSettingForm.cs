using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Model;

namespace SmartTaskbar.Views
{
    public partial class MainSettingForm : Form
    {
        private readonly CoreInvoker _coreInvoker;
        private readonly IContainer _container;

        public MainSettingForm(IContainer container, CoreInvoker coreInvoker)
        {
            _container = container;
            _coreInvoker = coreInvoker;
            InitializeComponent();

            VisibleChanged += (s, e) =>
            {
                if (Visible) Activate();
            };
            Activated += (s, e) => ChangeTheme();
            Deactivate += (s, e) => Hide();

            #region Initialization

            #region Text

            groupBoxState0.Text = coreInvoker.GetText("SettTaskbarState0");
            groupBoxState1.Text = coreInvoker.GetText("SettTaskbarState1");
            groupBoxState2.Text = coreInvoker.GetText("SettTaskbarState2");

            groupBoxTransparentMode0.Text =
                groupBoxTransparentMode1.Text =
                    groupBoxTransparentMode2.Text =
                        coreInvoker.GetText("SettTransparentMode");

            checkBoxIsAutoHide0.Text =
                checkBoxIsAutoHide1.Text =
                    checkBoxIsAutoHide2.Text =
                        coreInvoker.GetText("SettIsAutoHide");

            checkBoxHideTaskbar0.Text =
                checkBoxHideTaskbar1.Text =
                    checkBoxHideTaskbar2.Text =
                        coreInvoker.GetText("SettHideTaskbar");

            checkBoxIconSize0.Text =
                checkBoxIconSize1.Text =
                    checkBoxIconSize2.Text =
                        coreInvoker.GetText("SettIconSize");

            radioButtonDisable0.Text =
                radioButtonDisable1.Text =
                    radioButtonDisable2.Text =
                        coreInvoker.GetText("SettDisable");

            radioButtonTransparent0.Text =
                radioButtonTransparent1.Text =
                    radioButtonTransparent2.Text =
                        coreInvoker.GetText("SettTransparent");

            radioButtonBlur0.Text =
                radioButtonBlur1.Text =
                    radioButtonBlur2.Text =
                        coreInvoker.GetText("SettBlur");

            #endregion

            #region Event

            checkBoxIsAutoHide0.CheckedChanged += (s, e) =>
            {
                _coreInvoker.UserSettings.ResetState.IsAutoHide = checkBoxIsAutoHide0.Checked;
                _coreInvoker.SaveUserSettings();
            };

            checkBoxIsAutoHide1.CheckedChanged += (s, e) =>
            {
                _coreInvoker.UserSettings.ReadyState.IsAutoHide = checkBoxIsAutoHide1.Checked;
                _coreInvoker.SaveUserSettings();
            };

            checkBoxIsAutoHide2.CheckedChanged += (s, e) =>
            {
                _coreInvoker.UserSettings.TargetState.IsAutoHide = checkBoxIsAutoHide2.Checked;
                _coreInvoker.SaveUserSettings();
            };

            checkBoxHideTaskbar0.CheckedChanged += (s, e) =>
            {
                _coreInvoker.UserSettings.ResetState.HideTaskbarCompletely = checkBoxHideTaskbar0.Checked;
                _coreInvoker.SaveUserSettings();
            };

            checkBoxHideTaskbar1.CheckedChanged += (s, e) =>
            {
                _coreInvoker.UserSettings.ReadyState.HideTaskbarCompletely = checkBoxHideTaskbar1.Checked;
                _coreInvoker.SaveUserSettings();
            };

            checkBoxHideTaskbar2.CheckedChanged += (s, e) =>
            {
                _coreInvoker.UserSettings.TargetState.HideTaskbarCompletely = checkBoxHideTaskbar2.Checked;
                _coreInvoker.SaveUserSettings();
            };

            checkBoxIconSize0.CheckedChanged += (s, e) =>
            {
                _coreInvoker.UserSettings.ResetState.IconSize = checkBoxIconSize0.Checked ? Constant.IconSmall : Constant.IconLarge;
                _coreInvoker.SaveUserSettings();
            };

            checkBoxIconSize1.CheckedChanged += (s, e) =>
            {
                _coreInvoker.UserSettings.ReadyState.IconSize = checkBoxIconSize1.Checked ? Constant.IconSmall : Constant.IconLarge;
                _coreInvoker.SaveUserSettings();
            };

            checkBoxIconSize2.CheckedChanged += (s, e) =>
            {
                _coreInvoker.UserSettings.TargetState.IconSize = checkBoxIconSize2.Checked ? Constant.IconSmall : Constant.IconLarge;
                _coreInvoker.SaveUserSettings();
            };

            radioButtonDisable0.CheckedChanged += (s, e) =>
            {
                if (!radioButtonDisable0.Checked) return;

                _coreInvoker.UserSettings.ResetState.TransparentMode = TransparentModeType.Disable;
                _coreInvoker.SaveUserSettings();
            };

            radioButtonDisable1.CheckedChanged += (s, e) =>
            {
                if (!radioButtonDisable1.Checked) return;

                _coreInvoker.UserSettings.ReadyState.TransparentMode = TransparentModeType.Disable;
                _coreInvoker.SaveUserSettings();
            };

            radioButtonDisable2.CheckedChanged += (s, e) =>
            {
                if (!radioButtonDisable2.Checked) return;

                _coreInvoker.UserSettings.TargetState.TransparentMode = TransparentModeType.Disable;
                _coreInvoker.SaveUserSettings();
            };

            radioButtonTransparent0.CheckedChanged += (s, e) =>
            {
                if (!radioButtonTransparent0.Checked) return;

                _coreInvoker.UserSettings.ResetState.TransparentMode = TransparentModeType.Transparent;
                _coreInvoker.SaveUserSettings();
            };

            radioButtonTransparent1.CheckedChanged += (s, e) =>
            {
                if (!radioButtonTransparent1.Checked) return;

                _coreInvoker.UserSettings.ReadyState.TransparentMode = TransparentModeType.Transparent;
                _coreInvoker.SaveUserSettings();
            };

            radioButtonTransparent2.CheckedChanged += (s, e) =>
            {
                if (!radioButtonTransparent2.Checked) return;

                _coreInvoker.UserSettings.TargetState.TransparentMode = TransparentModeType.Transparent;
                _coreInvoker.SaveUserSettings();
            };

            radioButtonBlur0.CheckedChanged += (s, e) =>
            {
                if (!radioButtonBlur0.Checked) return;

                _coreInvoker.UserSettings.ResetState.TransparentMode = TransparentModeType.Blur;
                _coreInvoker.SaveUserSettings();
            };

            radioButtonBlur1.CheckedChanged += (s, e) =>
            {
                if (!radioButtonBlur1.Checked) return;

                _coreInvoker.UserSettings.ReadyState.TransparentMode = TransparentModeType.Blur;
                _coreInvoker.SaveUserSettings();
            };

            radioButtonBlur2.CheckedChanged += (s, e) =>
            {
                if (!radioButtonBlur2.Checked) return;

                _coreInvoker.UserSettings.TargetState.TransparentMode = TransparentModeType.Blur;
                _coreInvoker.SaveUserSettings();
            };

            #endregion

            LoadSettings();

            #endregion
        }

        private void LoadSettings()
        {
            checkBoxIsAutoHide0.Checked = _coreInvoker.UserSettings.ResetState.IsAutoHide;
            checkBoxHideTaskbar0.Checked = _coreInvoker.UserSettings.ResetState.HideTaskbarCompletely;
            checkBoxIconSize0.Checked = _coreInvoker.UserSettings.ResetState.IconSize == Constant.IconSmall;

            switch (_coreInvoker.UserSettings.ResetState.TransparentMode)
            {
                case TransparentModeType.Disable:
                    radioButtonDisable0.Checked = true;
                    break;
                case TransparentModeType.Transparent:
                    radioButtonTransparent0.Checked = true;
                    break;
                case TransparentModeType.Blur:
                    radioButtonBlur0.Checked = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            checkBoxIsAutoHide1.Checked = _coreInvoker.UserSettings.ReadyState.IsAutoHide;
            checkBoxHideTaskbar1.Checked = _coreInvoker.UserSettings.ReadyState.HideTaskbarCompletely;
            checkBoxIconSize1.Checked = _coreInvoker.UserSettings.ReadyState.IconSize == Constant.IconSmall;

            switch (_coreInvoker.UserSettings.ReadyState.TransparentMode)
            {
                case TransparentModeType.Disable:
                    radioButtonDisable1.Checked = true;
                    break;
                case TransparentModeType.Transparent:
                    radioButtonTransparent1.Checked = true;
                    break;
                case TransparentModeType.Blur:
                    radioButtonBlur1.Checked = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            checkBoxIsAutoHide2.Checked = _coreInvoker.UserSettings.TargetState.IsAutoHide;
            checkBoxHideTaskbar2.Checked = _coreInvoker.UserSettings.TargetState.HideTaskbarCompletely;
            checkBoxIconSize2.Checked = _coreInvoker.UserSettings.TargetState.IconSize == Constant.IconSmall;

            switch (_coreInvoker.UserSettings.TargetState.TransparentMode)
            {
                case TransparentModeType.Disable:
                    radioButtonDisable2.Checked = true;
                    break;
                case TransparentModeType.Transparent:
                    radioButtonTransparent2.Checked = true;
                    break;
                case TransparentModeType.Blur:
                    radioButtonBlur2.Checked = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void ChangeTheme()
        {
            Icon = _coreInvoker.GetIcon();

            var islight = InvokeMethods.IsLightTheme();

            BackColor = islight ? Color.FromArgb(238, 238, 238) : Color.FromArgb(43, 43, 43);
            ForeColor = islight ? Color.Black : Color.White;

            // todo 

        }
    }
}