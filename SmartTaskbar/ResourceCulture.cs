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
            if (!Thread.CurrentThread.CurrentUICulture.Name.Equals("zh-CN"))
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            resourceManager = new ResourceManager("SmartTaskbar.Resource", Assembly.GetExecutingAssembly());
        }

        public string GetString(string name) => resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
    }
}
