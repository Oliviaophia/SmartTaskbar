using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskbar.Core.AutoMode
{
    interface IAutoMode
    {
        void Run();

        void Reset();
    }
}
