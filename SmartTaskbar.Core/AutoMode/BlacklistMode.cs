using System;
using SmartTaskbar.Core.Settings;

namespace SmartTaskbar.Core.AutoMode
{
    public class BlacklistMode : IAutoMode
    {
        private readonly UserSettings _userSettings;

        public BlacklistMode(UserSettings userSettings)
        {
            _userSettings = userSettings;
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Ready()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
