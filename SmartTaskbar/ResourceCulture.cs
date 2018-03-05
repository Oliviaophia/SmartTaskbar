using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace SmartTaskbar
{
    class ResourceCulture
    {
        private ResourceManager resourceManager;

        public ResourceCulture()
        {
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case string str when str.StartsWith("zh"):
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                    break;
                case string str when str.StartsWith("de"):
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
                    break;
                default:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    break;
            }
            resourceManager = new ResourceManager("SmartTaskbar.Languages.Resource", Assembly.GetExecutingAssembly());
        }

        public string GetString(string name) => resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
    }
}
