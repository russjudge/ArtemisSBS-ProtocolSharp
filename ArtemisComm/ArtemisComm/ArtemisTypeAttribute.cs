using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    [System.AttributeUsage(AttributeTargets.Property)]
    internal sealed class ArtemisTypeAttribute : System.Attribute
    {
        public ArtemisTypeAttribute(Type t)
        {
            ArtemisProtocolType = t;
        }
        public Type ArtemisProtocolType { get; private set; }
    }
}
