﻿using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar;

internal static class PostMessageHelper
{
    private const uint BarFlag = 0x05D1;

    internal static void HideTaskbar(this TaskbarInfo taskbar)
        => PostMessage(taskbar.TaskbarHandle,
                       BarFlag,
                       IntPtr.Zero,
                       IntPtr.Zero);


    internal static void ShowTaskar(this TaskbarInfo taskbar)
        => PostMessage(
            taskbar.TaskbarHandle,
            BarFlag,
            (IntPtr) 1,
            taskbar.MonitorHandle);
}
