using System;

namespace SmartTaskbar.Core.AutoMode
{
#if DEBUG
    public class DumbMode : IAutoMode
    {
        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Ready()
        {
            throw new NotImplementedException();
        }
    }
#endif

}