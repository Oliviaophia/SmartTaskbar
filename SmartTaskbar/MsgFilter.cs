using System.Windows.Forms;
using SmartTaskbar.Core;
using SmartTaskbar.Views;

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
                    _mainController.ShowSettingForm();
                    return true;
                default:
                    return false;
            }
        }
    }
}