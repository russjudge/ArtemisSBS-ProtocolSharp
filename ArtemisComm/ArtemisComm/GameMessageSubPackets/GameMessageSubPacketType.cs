using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public enum GameMessageSubPacketType
    {
        //For Master packet type: 0Xf754c8fe - fe c8 54 f7 (GameMessagePacket)
        AllShipSettingsSubPacket = 15,  //0x0f
        DMXMessageSubPacket = 16,       //0x10
        GameTextMessageSubPacket = 10,  //0x0a
        GameOverSubPacket = 6,          //documentation lists this as GameMessagePacket, included with type 10. TODO: Update documentation.
        GameOverReasonSubPacket = 20,   //0x14  - Didn't see--hyothesis: only seen if Client is MainScreen.  Possible on non-solo missions, also.
        GameOverStatsSubPacket = 21,    //0x15  - Didn't see--hyothesis: only seen if Client is MainScreen.  Possible on non-solo missions, also.
        SimulationEndSubPacket = 0,          //Documentation lists this as GameStartPacket--but never comes at GameStart.
        //Observed: at simulation end, solo mission.  
        //Hypothesis-- this is "Simulation End"--the page that shows stats.

        JumpStartSubPacket = 12,        //0x0c
        JumpCompleteSubPacket = 13,     //0x0d

        KeyCaptureToggleSubPacket = 17, //0x11
        PlayerShipDamageSubPacket = 5,
        SkyboxSubPacket = 9,
        SoundEffectSubPacket = 3,

        
        Unknown3SubPacket = 7,
        KeepAliveSubPacket = 8,
        

        /*
        ... GameStartPacket (subtype 0x00)
... JumpStatusPacket (subtypes 0x0c and 0x0d)
... KeyCaptureTogglePacket (subtype 0x11)
... PlayerShipDamagePacket (subtype 0x05)
... SkyboxPacket (subtype 0x09)
... SoundEffectPacket (subtype 0x03)
    */
    }
}
