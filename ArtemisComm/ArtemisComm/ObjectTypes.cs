using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public enum ObjectTypes : byte
    {
        Unknown = 0,
        PlayerShip = 1,
        WeaponsBridgeStation = 2,
        EngineeringBridgeStation = 3,
        OtherShip = 4,
        SpaceStation = 5,
        Mine = 6,
        Anomaly = 7,
        Unused = 8,
        Nebula = 9,
        Torpedo = 10,
        BlackHole = 11,
        Asteroid = 12,
        GenericMesh = 13,
        Monster = 14,
        Whale = 15
        //Object type
        //This enumeration has changed significantly in Artemis 2.0 and is still being evaluated. It has not yet been confirmed that these values are accurate.

        //0x00: unknown
        //0x01: player ship
        //0x02: weapons bridge station
        //0x03: engineering bridge station
        //0x04: other ship (enemy or civilian)
        //0x05: space station
        //0x06: mine?
        //0x07: anomaly?
        //0x08: unused?
        //0x09: nebula?
        //0x0a: torpedo?
        //0x0b: black hole?
        //0x0c: asteroid?
        //0x0d: generic mesh? (unsure what this is for)
        //0x0e: monster?
        //0x0f: whale?
    }
}
