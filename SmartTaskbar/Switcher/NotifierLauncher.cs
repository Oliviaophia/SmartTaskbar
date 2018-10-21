using System;
using System.Diagnostics;
using System.IO;
using static SmartTaskbar.SafeNativeMethods;


namespace SmartTaskbar
{
    internal class NotifierLauncher : IDisposable
    {
        private readonly Process notifier = new Process();
        /// <summary>
        /// Startup process
        /// </summary>
        public NotifierLauncher()
        {
            notifier.StartInfo.FileName = Path.Combine(Directory.GetCurrentDirectory(), Environment.Is64BitOperatingSystem ? "x64" : "x86", "TaskbarNotifier");
            notifier.StartInfo.Arguments = Process.GetCurrentProcess().Threads[0].Id.ToString();
            notifier.Start();
            AddProcess(notifier.Handle);
        }
        /// <summary>
        /// Shutdown process
        /// </summary>
        public void Stop()
        {
            if (notifier.HasExited) return;

            notifier.Kill();
            notifier.WaitForExit();
        }
        /// <summary>
        /// Restart process, if it is terminated unexpectedly
        /// </summary>
        public void Resume()
        {
            if (!notifier.HasExited) return;

            notifier.Start();
            AddProcess(notifier.Handle);
        }
        /// <summary>
        /// Dispose notifier
        /// </summary>
        public void Dispose()
        {
            notifier.Dispose();
        }
    }
}
