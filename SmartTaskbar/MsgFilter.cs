using System.Windows.Forms;
using SmartTaskbar.Core;

namespace SmartTaskbar
{
    internal class MsgFilter : IMessageFilter
    {
        private readonly MainController _mainController;

        public MsgFilter(MainController mainController)
        {
            _mainController = mainController;
        }

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case Constant.MsgSettings:
                    _mainController.ShowSettingWindow();
                    return true;
                default:
                    return false;
            }
        }
    }
}