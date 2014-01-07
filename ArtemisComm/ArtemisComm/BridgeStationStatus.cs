using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32")]
    public enum BridgeStationStatus : byte
    {
        Available = 0,
        InPossesion = 1,
        Unavailable = 2
    }
}
