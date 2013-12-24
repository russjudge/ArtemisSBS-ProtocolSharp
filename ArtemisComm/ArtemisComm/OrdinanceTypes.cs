using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public enum OrdinanceTypes : byte
    {
        HomingMissile = 0x00,
        NukeMissile = 0x01,
        Mine = 0x02,
        EMP = 0x03
    }
}
