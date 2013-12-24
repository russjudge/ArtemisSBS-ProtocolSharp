using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public enum ShipAction2SubPacketTypes : uint
    {
        ConvertTorpedoSubPacket = 0x03,
        EngSendDamconSubPacket = 0x04,
        EngSetCoolantSubPacket = 0x00,
        LoadTubeSubPacket = 0x02,
    }
}
