using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SmartTaskbarSettings
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //https://stackoverflow.com/questions/93989/prevent-multiple-instances-of-a-given-app-in-net/31827620#31827620
        private static Mutex mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            mutex = new Mutex(true, "{46fcdea3-c43b-4a51-9e9b-000522e84135}", out bool createdNew);
            if (!createdNew)
                Current.Shutdown();
            base.OnStartup(e);
        }
    }
}
