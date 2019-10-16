using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using SmartTaskbar.Core.Settings;
using SmartTaskbar.Model;

namespace SmartTaskbar.Languages
{
    internal class CultureResource
    {
        private readonly CoreInvoker _coreInvoker;

        private readonly ResourceManager _resourceManager =
            new ResourceManager("SmartTaskbar.Languages.Resource", Assembly.GetExecutingAssembly());

        public CultureResource(CoreInvoker coreInvoker)
        {
            _coreInvoker = coreInvoker;
            LanguageChange();
        }

        public void LanguageChange()
        {
            switch (_coreInvoker.UserSettings.Language)
            {
                case Language.Auto:
                    switch (Thread.CurrentThread.CurrentUICulture.Name)
                    {
                        case "zh-CN":
                        case "en-US":
                            break;
                        default:
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                            break;
                    }

                    break;
                case Language.EnUs:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    break;
                case Language.ZhCn:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetText(string name) => _resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
    }
}