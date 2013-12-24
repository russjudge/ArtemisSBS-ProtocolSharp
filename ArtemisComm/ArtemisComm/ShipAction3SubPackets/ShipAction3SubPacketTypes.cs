using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public enum ShipAction3SubPacketTypes : uint
    {
        EngSetEnergySubPacket = 0x04,
        HelmJumpSubPacket = 0x05,
        HelmSetImpulseSubPacket = 0x00,
        HelmSetSteeringSubPacket = 0x01,
    }
}
