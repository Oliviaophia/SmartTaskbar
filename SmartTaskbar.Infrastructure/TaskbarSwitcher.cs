using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskbar.Infrastructure
{
    public class TaskbarSwitcher
    {
        //http://csharpindepth.com/Articles/General/Singleton.aspx
        private static readonly Lazy<TaskbarSwitcher> lazy = new Lazy<TaskbarSwitcher>(() => new TaskbarSwitcher());

        public static TaskbarSwitcher Instance => lazy.Value;


        private SafeNativeMethods.APPBARDATA msgData = new SafeNativeMethods.APPBARDATA();

        private TaskbarSwitcher()
        {
            msgData.cbSize = (uint)Marshal.SizeOf(msgData);
        }

        public void SwitchTaskbar() => SafeNativeMethods.SwitchTaskbar(ref msgData);

        public bool IsTaskbarAutoHide() => SafeNativeMethods.IsTaskbarAutoHide(ref msgData);

        public void ShowTaskbar() => SafeNativeMethods.ShowTaskbar(ref msgData);

        public void HideTaskbar() => SafeNativeMethods.HideTaskbar(ref msgData);
    }
}
