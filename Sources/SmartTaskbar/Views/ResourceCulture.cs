using System.Globalization;
using System.Reflection;
using System.Resources;

namespace SmartTaskbar;

/// <summary>
///     Language management
/// </summary>
internal class ResourceCulture
{
    private readonly ResourceManager _resourceManager =
        new("SmartTaskbar.Languages.Resource", Assembly.GetExecutingAssembly());

    public ResourceCulture()
    {
        // I feel that there is no need to add an option for language selection
        // If there is a new language translation, just add it below
        switch (Thread.CurrentThread.CurrentUICulture.Name)
        {
            case "zh-CN":
            case "en-US":
                break;
            default:
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                break;
        }
    }

    /// <summary>
    ///     Get the corresponding translation based on the name, default en-US
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetString(string name)
    {
        try { return _resourceManager.GetString(name, Thread.CurrentThread.CurrentUICulture) ?? ""; }
        catch { return ""; }
    }
}
