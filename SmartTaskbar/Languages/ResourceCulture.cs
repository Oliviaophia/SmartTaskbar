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

        private readonly CultureInfo _cultureInfo = new CultureInfo("en-US");

        private readonly ResourceManager _resourceManager =
            new ResourceManager("SmartTaskbar.Languages.Resource", Assembly.GetExecutingAssembly());

        private ResourceCulture()
        {
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "en-US":
                case "zh-CN":
                    // Temporarily removed
                    //case "ru-RU":
                    //case "uk-UA":
                    break;
                default:
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;
                    break;
            }
        }

        internal static ResourceCulture Get => Instance.Value;

        internal string GetString(string name)
        {
            try
            {
                return _resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
            }
            catch
            {
                return _resourceManager.GetString(name, _cultureInfo);
            }
        }
    }
}