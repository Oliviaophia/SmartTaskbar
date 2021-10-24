using System.Globalization;
using System.Reflection;
using System.Resources;

namespace SmartTaskbar;

internal class ResourceCulture
{
    private readonly CultureInfo _cultureInfo = new("en-US");

    private readonly ResourceManager _resourceManager =
        new("SmartTaskbar.Languages.Resource", Assembly.GetExecutingAssembly());

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
                Thread.CurrentThread.CurrentUICulture = _cultureInfo;
                break;
        }
    }

    public string GetString(string name)
    {
        try { return _resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture) ?? ""; }
        catch { return _resourceManager.GetString(name, _cultureInfo) ?? ""; }
    }
}
