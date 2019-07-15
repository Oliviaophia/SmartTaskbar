using System.Windows.Forms;
using SmartTaskbar.Core;

namespace SmartTaskbar
{
    internal class MsgFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case Constant.MsgSettings:

                default:
                    return false;
            }
        }
    }
}