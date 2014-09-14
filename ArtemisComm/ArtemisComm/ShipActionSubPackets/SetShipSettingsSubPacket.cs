using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetShipSettingsSubPacket : ShipAction
    {
        public static Packet GetPacket(DriveType drive, int shipType, int unknown, string shipName)
        {
            SetShipSettingsSubPacket ssp = new SetShipSettingsSubPacket(drive, shipType, unknown, shipName);
            ShipActionPacket sap = new ShipActionPacket(ssp);
            Packet p = new Packet(sap);
            return p;
        }
        public SetShipSettingsSubPacket(DriveType drive, int shipType, int unknown, string shipName) : base( ShipActionSubPacketType.SetShipSettingsSubPacket, (int)drive)
        {
           
            ShipType = shipType;
            Unknown = unknown;
            ShipName = new ArtemisString(shipName);
        }
        public SetShipSettingsSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        //ef:be:ad:de:38:00:00:00:02:00:00:00:00:00:00:00:24:00:00:00:
        //3c:1d:82:4c:
        //16:00:00:00:
        //01:00:00:00:
        //00:00:00:00:
        //01:00:00:00:
        //06:00:00:00:
        //44:00:
        //69:00:
        //61:00:
        //6e:00:
        //61:00:
        //00:00
        [ArtemisExcluded]
        public DriveType Drive 
        {
            get
            {
                return (DriveType)Value;
            }
            set
            {
                Value = (int)value;
            }
        }
        public int ShipType { get; set; }
        public int Unknown { get; set; }
       
        public ArtemisString ShipName{get;set;}


    
        
    }
}