using System.Diagnostics;
using System.Text;
using static SmartTaskbar.SafeNativeMethods;
using Timer = System.Threading.Timer;

namespace SmartTaskbar;

internal class Engine : IDisposable
{
    private static readonly Timer Timer = new(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
    private static int _counter;
    private static TaskbarInfo Taskbar = TaskbarHelper.InitTaskbar();
    private static HashSet<IntPtr> _cachedIntPtr;

    private const int Capacity = 256;
    private static readonly StringBuilder Sb = new(Capacity);

    static Engine()
    {
        _cachedIntPtr = new HashSet<IntPtr> { Taskbar.Handle };
    }

    public void Dispose()
    {
        Timer.Change(Timeout.Infinite, Timeout.Infinite);
        Timer.Dispose();
    }

    private static void TimerCallback(object? state)
    {
        // There is a certain probability that values will be incorrect after a long, long, long time of operation.
        // Reset every minute to avoid.
        if (_counter == 480)
        {
            #region Reset
            // Reinitialize taskbar information.
            Taskbar = TaskbarHelper.InitTaskbar();
            // Reinitialize the cache IntPtr.
            _cachedIntPtr = new HashSet<IntPtr> { Taskbar.Handle };
            // Set the taskbar to Auto-Hide.
            AutoHideHelper.SetAutoHide();

            #endregion
            // Reset counter.
            _counter = 0;
        }

        #region Run

        if (Taskbar.IsMouseOverTaskbar())
        {
            return;
        }
        else
        {
             // todo
        }

        var foregroundHandle = GetForegroundWindow();

        if (_cachedIntPtr.Contains(foregroundHandle))
        {
            Taskbar.ShowTaskar();
            return;
        }

        _ = Sb.Clear();
        _ = GetClassName(foregroundHandle, Sb, Capacity);

        var name = Sb.ToString();
        Debug.WriteLine(name);
        switch (name)
        {
            // Determine whether it is a desktop.
            case "WorkerW":
            case "Progman":
                // If true, add to the cache list.
                _cachedIntPtr.Add(foregroundHandle);
                Taskbar.ShowTaskar();
                return;
            // On some devices, opening the search and start menu can cause the taskbar to hide. Seems to be related to third-party software.
            case "Windows.UI.Core.CoreWindow":
                // do nothing just return?
                // todo
                return;
        }

        // Get foreground window Re
        _ = GetWindowRect(foregroundHandle, out var rect);
        if (rect.left < Taskbar.Rect.right
               && rect.right > Taskbar.Rect.left
               && rect.top < Taskbar.Rect.bottom
               && rect.bottom > Taskbar.Rect.top)
            Taskbar.HideTaskbar();
        else
            Taskbar.ShowTaskar();
         
        #endregion

        ++_counter;
    }

    /// <summary>
    ///     Turn off the timer, Pause auto mode
    /// </summary>
    public static void Stop() => Timer.Change(Timeout.Infinite, Timeout.Infinite);

    /// <summary>
    ///     Start the timer, start the auto mode
    /// </summary>
    public static void Start()
    {
        // Make sure the taskbar has been automatically hidden, otherwise it will not work
        AutoHideHelper.SetAutoHide();
        // 125 milliseconds is a balance between user-acceptable perception and system call time
        Timer.Change(125, 125);
    }
}
