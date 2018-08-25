using System.Windows.Forms;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar
{
    internal class MsgFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case MSG_MAX:
                    IsMax = true;
                    switch ((AutoModeType)Properties.Settings.Default.TaskbarState)
                    {
                        case AutoModeType.Display:
                            Hide();
                            break;
                        case AutoModeType.Size:
                            SetIconSize(SmallIcon);
                            break;
                        case AutoModeType.None:
                            break;
                    }
                    return true;
                case MSG_UNMAX:
                    IsMax = false;
                    switch ((AutoModeType)Properties.Settings.Default.TaskbarState)
                    {
                        case AutoModeType.Display:
                            Show();
                            break;
                        case AutoModeType.Size:
                            SetIconSize(BigIcon);
                            break;
                        case AutoModeType.None:
                            break;
                    }
                    return true;
                default:
                    return false;
            }
        }
    }
}