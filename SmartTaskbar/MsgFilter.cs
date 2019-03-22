using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Views;

namespace SmartTaskbar
{
    internal class MsgFilter : IMessageFilter
    {
        /// <summary>
        ///     Processing messages
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
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