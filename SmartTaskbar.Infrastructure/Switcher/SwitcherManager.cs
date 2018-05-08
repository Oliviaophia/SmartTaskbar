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

        public bool IsWin10 { get; } = Environment.OSVersion.Version.Major.ToString() == "10";

        public SwitcherManager()
        {
            
        }

        public void RunDefaultAutoMode()
        {
            switcher?.Close();
            if (IsWin10)
                switcher = new AutoMode.DefaultAutoMode();
            else
                switcher = new AutoModeWin10.DefaultAutoMode();
            switcher.Start();
        }

        public void RunWhitelistAutoMode()
        {
            switcher?.Close();
        }

        public void RunBlacklistAutoMode()
        {
            switcher?.Close();
        }

    }
}
