using System;
using SmartTaskbar.Core.Settings;

namespace SmartTaskbar.Core.AutoMode
{
    public class WhitelistMode : IAutoMode
    {
        private readonly UserSettings _userSettings;

        public WhitelistMode(UserSettings userSettings)
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
