using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public enum GameMessageSubPacketType
    {
        Unknown1SubPacket = 0,  //Documentation lists this as GameStartPacket--but never comes at GameStart.
        SoundEffectSubPacket = 3,
        PlayerShipDamageSubPacket = 5,
        GameResetSubPacket = 6,  //documentation lists this as GameMessagePacket, included with type 10. TODO: Update documentation.
        Unknown3SubPacket = 7,
        KeepAliveSubPacket = 8,
        GameTypeSubPacket = 9,
        GameTextMessageSubPacket = 10,
        
        JumpStartSubPacket = 12,
        JumpCompleteSubPacket = 13,
        AllShipSettingsSubPacket = 15,
        KeyCaptureToggleSubPacket = 17,
    }
}
