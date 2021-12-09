using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace SmartTaskbar
{
    internal class ResourceCulture
    {
        private readonly ResourceManager _resourceManager =
            new ResourceManager("SmartTaskbar.Languages.Resource", Assembly.GetExecutingAssembly());

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
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    break;
            }
        }

        public string GetString(string name)
        {
            try { return _resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture); }
            catch { return ""; }
        }
    }
}
