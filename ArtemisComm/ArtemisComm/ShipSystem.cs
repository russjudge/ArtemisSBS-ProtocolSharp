using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public enum ShipSystem
    {
        Beams = 0x00,
        Torpedoes = 0x01,
        Sensors = 0x02,
        Maneuvering = 0x03,
        Impulse = 0x04,
        WarpJumpDrive = 0x05,
        ForeShields = 0x06,
        AftShields = 0x07
    }
}
