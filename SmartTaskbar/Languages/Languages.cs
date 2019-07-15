using System;
using System.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SmartTaskbar.Languages
{
    internal class Language : ReactiveObject
    {
        private static readonly Lazy<Language> LazyInstance =
            new Lazy<Language>(() => new Language(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static Language Instance => LazyInstance.Value;

        private static readonly CultureResource Resource = new CultureResource();

        private Language()
        {
            GetCultureResource();
        }

        private void GetCultureResource()
        {
            TraySettings = Resource.GetString(nameof(TraySettings));
            TrayExit = Resource.GetString(nameof(TrayExit));
        }

        [Reactive] public string TraySettings { get; set; }

        [Reactive] public string TrayExit { get; set; }
    }
}