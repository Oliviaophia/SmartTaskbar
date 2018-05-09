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

        public static TaskbarSwitcher SwitcherInstance => lazy.Value;

        private TaskbarSwitcher()
        {

        }

        public void SwitchTaskbar()
        {
            //SafeNativeMethods.SwitchTaskbar(ref msgData);
        }

        public bool IsTaskbarAutoHide()
        {
            //SafeNativeMethods.IsTaskbarAutoHide(ref msgData);
            return true;
        }

        public void ShowTaskbar()
        {
            //SafeNativeMethods.ShowTaskbar(ref msgData);
        }

        public void HideTaskbar()
        {
            //SafeNativeMethods.HideTaskbar(ref msgData);
        }

        public void DefaultAutoMode() => SwitcherManager.Instance.RunDefaultAutoMode();

        public void WhitelistAutoMode() => SwitcherManager.Instance.RunWhitelistAutoMode();

        public void BlacklistAutoMode() => SwitcherManager.Instance.RunBlacklistAutoMode();

    }
}
