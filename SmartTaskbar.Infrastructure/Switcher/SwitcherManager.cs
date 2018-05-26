using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskbar.Infrastructure.Switcher
{
    class SwitcherManager
    {
        private static readonly Lazy<SwitcherManager> lazy = new Lazy<SwitcherManager>(() => new SwitcherManager());

        public static SwitcherManager Instance => lazy.Value;

        private ISwitcher switcher;

        public SwitcherManager()
        {

        }

        public void RunDefaultAutoMode(bool isWin10)
        {
            switcher?.Close();
            if (isWin10)
                switcher = new AutoModeWin10.DefaultAutoMode();
            else
                switcher = new AutoMode.DefaultAutoMode();
            switcher.Start();
        }

        public void RunWhitelistAutoMode(bool isWin10)
        {
            switcher?.Close();
        }

        public void RunBlacklistAutoMode(bool isWin10)
        {
            switcher?.Close();
        }

    }
}
