using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetStationSubPacket : ShipAction
    {
        //**CONFIRMED
        public static Packet GetPacket(StationType station, bool isSelected)
        {

            SetStationSubPacket sstp = new SetStationSubPacket(station, isSelected);
            ShipActionPacket sap = new ShipActionPacket(sstp);

            return new Packet(sap);

        }
       

        public SetStationSubPacket(StationType station, bool isSelected) : base (ShipActionSubPacketType.SetStationSubPacket, (int)station)
        {
            IsSelected = isSelected;
            Station = station;
        }
        public SetStationSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public StationType Station
        {
            get
            {
                return (StationType)Value;
            }
            set
            {
                Value = (int)value;
            }
        }


        [ArtemisType(typeof(int))]
        public bool IsSelected { get; set; }



        
    }
}