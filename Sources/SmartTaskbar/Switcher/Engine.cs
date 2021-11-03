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
    private static IntPtr _desktopHandle;

    private const int Capacity = 256;
    private static readonly StringBuilder Sb = new(Capacity);

    static Engine()
    {
        _cachedIntPtr = new HashSet<IntPtr> { Taskbar.TaskbarHandle };
        _desktopHandle = GetDesktopWindow();
    }

    public void Dispose()
    {
        Timer.Change(Timeout.Infinite, Timeout.Infinite);
        Timer.Dispose();
    }

    private static void TimerCallback(object? state)
    {
        if (_counter == 480)
        {
            #region Reset

            Taskbar = TaskbarHelper.InitTaskbar();
            _cachedIntPtr = new HashSet<IntPtr> { Taskbar.TaskbarHandle };
            _desktopHandle = GetDesktopWindow();
            AutoHideHelper.SetAutoHide();

            #endregion
            _counter = 0;
        }

        #region Run

        if (IsMouseOverTaskbar()) return;

        var foregroundHandle = GetForegroundWindow();

        if (foregroundHandle.IsWindowInvisible()) return;

        if (_cachedIntPtr.Contains(foregroundHandle))
        {
            Taskbar.ShowTaskar();
            return;
        }

        _ = Sb.Clear();
        _ = GetClassName(foregroundHandle, Sb, Capacity);

        switch (Sb.ToString())
        {
            case "Progman":
            case "WorkerW":
                _cachedIntPtr.Add(foregroundHandle);
                Taskbar.ShowTaskar();
                return;
        }

        _ = GetWindowRect(foregroundHandle, out var rect);
        if (rect.left < Taskbar.TaskbarRectangle.right
               && rect.right > Taskbar.TaskbarRectangle.left
               && rect.top < Taskbar.TaskbarRectangle.bottom
               && rect.bottom > Taskbar.TaskbarRectangle.top)
            Taskbar.HideTaskbar();
        else
            Taskbar.ShowTaskar();

        #endregion

        ++_counter;
    }

    public static void Stop()
    {
        Timer.Change(Timeout.Infinite, Timeout.Infinite);
    }

    public static void Start()
    {
        AutoHideHelper.SetAutoHide();
        Timer.Change(125, 125);
    }

    private const uint GaParent = 1;

    private static IntPtr _lastHandle;
    private static IntPtr _currentHandle;
    private static bool _lastResult;
    private static bool IsMouseOverTaskbar()
    {
        _ = GetCursorPos(out var point);
        _currentHandle = WindowFromPoint(point);
        if (_lastHandle == _currentHandle) return _lastResult;

        if (point.y < Taskbar.MonitorRectangle.Top ||
            point.x > Taskbar.MonitorRectangle.Right ||
            point.x < Taskbar.MonitorRectangle.Left ||
            point.y > Taskbar.MonitorRectangle.Bottom) 
            return _lastResult = false;

        _lastHandle = _currentHandle;
        while (_currentHandle != _desktopHandle)
        {
            if (Taskbar.TaskbarHandle == _currentHandle) return _lastResult = true;

            _currentHandle = GetAncestor(_currentHandle, GaParent);
        }

        return _lastResult = false;
    }
}
