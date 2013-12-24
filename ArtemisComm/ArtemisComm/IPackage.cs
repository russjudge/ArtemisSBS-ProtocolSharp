using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public interface IPackage
    {
        byte[] GetBytes();
    }
}
