using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskbar.Infrastructure.Switcher
{
    public class TaskbarSwitcher
    {
        //http://csharpindepth.com/Articles/General/Singleton.aspx
        private static readonly Lazy<TaskbarSwitcher> lazy = new Lazy<TaskbarSwitcher>(() => new TaskbarSwitcher());
        private SafeNativeMethods.APPBARDATA msgData = SafeNativeMethods.APPBARDATA.New();

        public static TaskbarSwitcher SwitcherInstance => lazy.Value;
        private ISwitcher switcher;


        private TaskbarSwitcher()
        {
            
        }

        public bool IsTaskbarAutoHide => SafeNativeMethods.IsTaskbarAutoHide(ref msgData);

        public void ShowTaskbar()
        {
            CloseSwitcher();
            SafeNativeMethods.ShowTaskbar(ref msgData);
        }

        public void HideTaskbar()
        {
            CloseSwitcher();
            SafeNativeMethods.HideTaskbar(ref msgData);
        }

        public void DefaultMode(bool isWin10)
        {
            CloseSwitcher();
            if (isWin10)
                switcher = new AutoModeWin10.DefaultMode();
            else
                switcher = new AutoMode.DefaultMode();
            switcher.Start();
        }

        public void CustomMode(bool isWin10)
        {
            CloseSwitcher();
        }


        public void CloseSwitcher() => switcher?.Close();
    }
}
