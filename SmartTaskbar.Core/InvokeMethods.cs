using System;
using SmartTaskbar.Core.Helpers;
using static SmartTaskbar.Core.SafeNativeMethods;

namespace SmartTaskbar.Core
{
    public static class InvokeMethods
    {
        #region PostThreadMessage

        public static void BringOutSettingsWindow(int id) =>
            PostThreadMessage(id, Constant.MsgSettings, IntPtr.Zero, IntPtr.Zero);

        #endregion

        #region UpdateTaskbarList

        public static void UpdateTaskbarList() => taskbars.UpdateTaskbarList();

        #endregion
    }
}