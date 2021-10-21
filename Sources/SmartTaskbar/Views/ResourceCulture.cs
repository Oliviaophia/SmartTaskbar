using System.Globalization;
using System.Reflection;
using System.Resources;

namespace SmartTaskbar
{
    internal class ResourceCulture
    {
        private readonly ResourceManager resourceManager = new ResourceManager("SmartTaskbar.Languages.Resource", Assembly.GetExecutingAssembly());

        private readonly CultureInfo cultureInfo = new CultureInfo("en-US");

        public ResourceCulture()
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

        public string GetString(string name)
        {
            try
            {
                return resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture) ?? "";
            }
            catch
            {
                return resourceManager.GetString(name, cultureInfo) ?? "";
            }
        }
    }
}
