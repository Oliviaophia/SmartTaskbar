using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartTaskbar.Infrastructure.Languages
{
    public class ResourceCulture
    {
        private static readonly Lazy<ResourceCulture> lazy = new Lazy<ResourceCulture>(() => new ResourceCulture());

        public static ResourceCulture CultureInstance => lazy.Value;

        private ResourceManager resourceManager = new ResourceManager("SmartTaskbar.Infrastructure.Languages.Resource", Assembly.GetExecutingAssembly());

        private ResourceCulture()
        {

        }

        public string GetString(string label) => resourceManager.GetString(label, Thread.CurrentThread.CurrentUICulture);

    }
}
