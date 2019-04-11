using System.Windows.Forms;

namespace SmartTaskbar
{
    internal class MsgFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                default:
                    return false;
            }
        }
    }
}