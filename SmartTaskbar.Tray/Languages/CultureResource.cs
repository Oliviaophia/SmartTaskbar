using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace SmartTaskbar.Tray.Languages
{
    public class CultureResource
    {
        private readonly ResourceManager _resourceManager =
            new ResourceManager("SmartTaskbar.Tray.Languages.Resource", Assembly.GetExecutingAssembly());

        public CultureResource() { LanguageChange(); }

        public void LanguageChange()
        {
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "zh-CN":
                case "en-US":
                case "de-DE":
                    break;
                default:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    break;
            }
        }

        public string GetText(string name)
            => _resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture);
    }
}
