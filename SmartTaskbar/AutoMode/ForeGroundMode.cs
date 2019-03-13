using System;
using System.Windows.Forms;
using static SmartTaskbar.Core.InvokeMethods;

namespace SmartTaskbar.AutoMode
{
    internal class ForeGroundMode : IAutoMode
    {
        public ForeGroundMode()
        {
            
        }

        public void Run()
        {
            InvokeForeGroundMode();
        }
    }
}
