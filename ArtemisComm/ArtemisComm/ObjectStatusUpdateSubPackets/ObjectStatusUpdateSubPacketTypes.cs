using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public enum ObjectStatusUpdateSubPacketTypes : byte
    {
        UnknownSubPacket = 0x00,
        MainPlayerUpdateSubPacket = 0x01,
        WeapPlayerUpdateSubPacket = 0x02,
        EngPlayerUpdateSubPacket = 0x03,
        NpcUpdateSubPacket = 0x04,
        StationSubPacket = 0x05,
        MineUpdateSubPacket = 0x06,
        AnomalyUpdateSubPacket = 0x07,

        NebulaUpdateSubPacket = 0x09,


        TorpedoUpdateSubPacket = 0x0a,
        BlackHoleUpdateSubPacket = 0x0b,

        AsteroidUpdateSubPacket = 0x0c,
        GenericMeshSubPacket = 0x0d,
        MonsterUpdateSubPacket = 0x0e,
        WhaleUpdateSubPacket = 0x0f,

        DroneUpdateSubPacket = 0x10,

        //OtherShipUpdateSubPacket = 0x06,


    }
}
