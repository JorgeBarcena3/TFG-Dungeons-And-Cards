using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Tools.Interfaces
{
    abstract class IInfoUIElement<T>
    {
        public abstract void fillInfo(T info);
        

    }
}
