using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetStationSubPacket : BasePacket
    {
        //**CONFIRMED
        public static Packet GetPacket(StationType station, bool isSelected)
        {

            SetStationSubPacket sstp = new SetStationSubPacket(station, isSelected);
            ShipActionPacket sap = new ShipActionPacket(sstp);

            return new Packet(sap);

        }
       

        public SetStationSubPacket(StationType station, bool isSelected)
        {
            IsSelected = isSelected;
            Station = station;
        }
        public SetStationSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public StationType Station { get; set; }


        [ArtemisType(typeof(int))]
        public bool IsSelected { get; set; }



        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
        
    }
}