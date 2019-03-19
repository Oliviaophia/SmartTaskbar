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

        #region UpdateCache

        public static void UpdateCache()
        {
            taskbars.UpdateTaskbarList();
            cacheName.UpdateCacheName();
        }

        #endregion
    }
}