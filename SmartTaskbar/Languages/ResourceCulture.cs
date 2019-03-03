using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace SmartTaskbar.Languages
{
    internal class ResourceCulture
    {
        private static readonly Lazy<ResourceCulture> Instance = new Lazy<ResourceCulture>(() => new ResourceCulture());

        internal static ResourceCulture Get => Instance.Value;

        private readonly ResourceManager resourceManager  = new ResourceManager("SmartTaskbar.Languages.Resource", Assembly.GetExecutingAssembly());

        private readonly CultureInfo cultureInfo = new CultureInfo("en-US");

        private ResourceCulture()
        {
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                case "en-US":
                case "ru-RU":
                case "uk-UA":
                    break;
                default:
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                    break;
            }
        }

        internal string GetString(string name)
        {
            try
            {
                return resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
            }
            catch 
            {
                return resourceManager.GetString(name, cultureInfo);
            }
        }
    }
}