using System.Diagnostics;
using static SmartTaskbar.SafeNativeMethods;
using Timer = System.Threading.Timer;

namespace SmartTaskbar;

internal class Engine : IDisposable
{
    private static readonly Timer Timer = new(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
    private static int _counter;
    private static TaskbarInfo _taskbar = TaskbarHelper.InitTaskbar();
    private static HashSet<IntPtr> _cachedIntPtr;
    private static bool _isCoreWindow;
    private static IntPtr _coreWindowHandle;

    static Engine()
        => _cachedIntPtr = new HashSet<IntPtr> {_taskbar.Handle};

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
            _taskbar = TaskbarHelper.InitTaskbar();
            // Reinitialize the cache IntPtr.
            _cachedIntPtr = new HashSet<IntPtr> {_taskbar.Handle};
            // Set the taskbar to Auto-Hide.
            AutoHideHelper.SetAutoHide();

            #endregion

            // Reset counter.
            _counter = 0;
        }

        // todo 

        #region Run

        var foregroundHandle = GetForegroundWindow();

        if (_isCoreWindow)
        {
            if (foregroundHandle == _coreWindowHandle)
                return;

            _isCoreWindow = false;
        }

        if (_cachedIntPtr.Contains(foregroundHandle))
        {
            _taskbar.ShowTaskar();
            return;
        }

        #if DEBUG
        var name = foregroundHandle.GetName();
        Debug.WriteLine(name);
        switch (name)
            #else
        switch (foregroundHandle.GetName())
            #endif
        {
            // Determine whether it is a desktop.
            case "WorkerW":
            case "Progman":
                // If true, add to the cache list.
                _cachedIntPtr.Add(foregroundHandle);
                _taskbar.ShowTaskar();
                return;
            // On some devices, opening the search and start menu can cause the taskbar to hide.
            case "Windows.UI.Core.CoreWindow":
            case "CabinetWClass":
                _isCoreWindow = true;
                return;
        }

        if (_taskbar.IsMouseOverTaskbar()) return;

        // Get foreground window Rectange
        _ = GetWindowRect(foregroundHandle, out var rect);
        if (rect.left < _taskbar.Rect.right
            && rect.right > _taskbar.Rect.left
            && rect.top < _taskbar.Rect.bottom
            && rect.bottom > _taskbar.Rect.top)
            _taskbar.HideTaskbar();
        else
            _taskbar.ShowTaskar();

        #endregion

        ++_counter;
    }

    /// <summary>
    ///     Turn off the timer, Pause auto mode
    /// </summary>
    public static void Stop()
        => Timer.Change(Timeout.Infinite, Timeout.Infinite);

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
