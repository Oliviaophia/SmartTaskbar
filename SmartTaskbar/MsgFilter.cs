using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Views;

namespace SmartTaskbar
{
    internal class MsgFilter : IMessageFilter
    {
        /// <inheritdoc />
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case Constant.MsgSettings:
                    SettingsView.Get.ShowView();
                    return true;
                default:
                    return false;
            }
        }
    }
}