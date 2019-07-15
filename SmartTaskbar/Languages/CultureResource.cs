using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using SmartTaskbar.Core;

namespace SmartTaskbar.Languages
{
    internal class CultureResource
    {
        private readonly ResourceManager _resourceManager =
            new ResourceManager("SmartTaskbar.Languages.Resource", Assembly.GetExecutingAssembly());

        public CultureResource()
        {
            void SetThreadCulture()
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "zh-CN":
                    case "en-US":
                        break;
                    default:
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                        break;
                }
            }

            switch (InvokeMethods.UserConfig.Language)
            {
                case Core.Settings.Language.Auto:
                    SetThreadCulture();
                    break;
                case Core.Settings.Language.EnUs:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    break;
                case Core.Settings.Language.ZhCn:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                    break;
                default:
                    InvokeMethods.UserConfig.Language = Core.Settings.Language.Auto;
                    SetThreadCulture();
                    break;
            }
        }

        public string GetString(string name) => _resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
    }
}