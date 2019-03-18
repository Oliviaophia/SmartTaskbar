using System;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core
{
    public static class InvokeMethods
    {
        #region PostThreadMessage

        public static void BringOutSettingsWindow(int id) =>
            PostThreadMessage(id, Constant.MsgSettings, IntPtr.Zero, IntPtr.Zero);

        #endregion
    }
}
