using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Views;

namespace SmartTaskbar
{
    internal class MsgFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case Constant.MsgSettings:
                    SettingForm.Instance.Show();
                    return true;
                default:
                    return false;
            }
        }
    }
}