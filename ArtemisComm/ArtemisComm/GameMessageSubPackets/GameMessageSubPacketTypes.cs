using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public enum GameMessageSubPacketTypes
    {
        GameEndSubPacket = 6,  //documentation lists this as GameMessagePacket, included with type 10. TODO: Update documentation.
        EndSimulationSubPacket = 0,  //Documentation lists this as GameStartPacket--but never comes at GameStart.
        KeepAliveSubPacket = 8,
        GameTypeSubPacket = 9,
        GameTextMessageSubPacket = 10,
        JumpStartSubPacket = 12,
        JumpCompleteSubPacket = 13,
        AllShipSettingsSubPacket = 15,

    }
}
