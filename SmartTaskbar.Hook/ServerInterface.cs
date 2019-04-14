using System;

namespace SmartTaskbar.Hook
{
    public class ServerInterface : MarshalByRefObject
    {
        private static bool _flag;

        public void Ping()
        {
            _flag = !_flag;
        }
    }
}