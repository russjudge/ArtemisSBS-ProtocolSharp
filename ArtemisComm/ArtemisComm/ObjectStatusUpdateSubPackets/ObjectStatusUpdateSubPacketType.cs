using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public enum ObjectStatusUpdateSubPacketType : byte
    {
        DroneUpdateSubPacket = 0x10,
        EngPlayerUpdateSubPacket = 0x03,
        GenericMeshSubPacket = 0x0d,

        MineUpdateSubPacket = 0x06,
        AnomalyUpdateSubPacket = 0x07,
        TorpedoUpdateSubPacket = 0x0a,
        BlackHoleUpdateSubPacket = 0x0b,
        AsteroidUpdateSubPacket = 0x0c,
        MonsterUpdateSubPacket = 0x0e,

        
        MainPlayerUpdateSubPacket = 0x01,
        NebulaUpdateSubPacket = 0x09,
        NpcUpdateSubPacket = 0x04,
        StationSubPacket = 0x05,
        WeapPlayerUpdateSubPacket = 0x02,
        WhaleUpdateSubPacket = 0x0f,




        //OtherShipUpdateSubPacket = 0x06,

        UnknownSubPacket = 0x00,

    }
}
