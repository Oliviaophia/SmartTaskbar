using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace SmartTaskbar
{
    class ResourceCulture
    {
        private ResourceManager resourceManager;

        public ResourceCulture()
        {
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                    break;
                case "en-US":
                    break;
                case "de-DE":
                    break;
                default:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    break;
            }
            resourceManager = new ResourceManager("SmartTaskbar.Resource", Assembly.GetExecutingAssembly());
        }

        public string GetString(string name) => resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
    }
}
