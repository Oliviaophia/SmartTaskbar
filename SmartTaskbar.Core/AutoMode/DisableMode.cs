using SmartTaskbar.Core.Helpers;
using SmartTaskbar.Core.Settings;

namespace SmartTaskbar.Core.AutoMode
{
    public class DisableMode : IAutoMode
    {
        private readonly UserSettings _userSettings;

        public DisableMode(UserSettings userSettings)
        {
            _userSettings = userSettings;
            Reset();
        }

        public void Run() { Variable.Taskbars.MaintainBarState(_userSettings.ResetState); }

        public void Ready() { Variable.Taskbars.SetBarState(_userSettings.ResetState); }

        public void Reset()
        {
            Variable.Taskbars.ResetTaskbars();
            Ready();
        }
    }
}
