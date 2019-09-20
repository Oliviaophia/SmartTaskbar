using System;
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

        private static readonly Lazy<CultureResource> LazyInstance =
            new Lazy<CultureResource>(() => new CultureResource(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static CultureResource Instance => LazyInstance.Value;

        private CultureResource()
        {
            LanguageChange();
        }

        public static void LanguageChange()
        {
            switch (InvokeMethods.UserConfig.Language)
            {
                case Core.Settings.Language.Auto:
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
                case Core.Settings.Language.EnUs:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    break;
                case Core.Settings.Language.ZhCn:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetText(string name) => _resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
    }
}