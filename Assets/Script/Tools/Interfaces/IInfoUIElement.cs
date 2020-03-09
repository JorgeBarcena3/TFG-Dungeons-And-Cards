using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Tools.Interfaces
{
   public abstract class IInfoUIElement<T> : MonoBehaviour
    {
        public abstract void fillInfo(T info);
        

    }
}
