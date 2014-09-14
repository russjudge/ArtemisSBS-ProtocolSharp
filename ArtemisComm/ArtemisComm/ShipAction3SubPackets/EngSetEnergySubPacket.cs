using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public class EngSetEnergySubPacket : ShipAction3
    {
        public static Packet GetPacket(ShipSystem system, float value)
        {
            EngSetEnergySubPacket esesp = new EngSetEnergySubPacket(system, value);
            ShipAction3Packet sap3 = new ShipAction3Packet(esesp);
            Packet retVal = new Packet(sap3);
            return retVal;
        }

        public EngSetEnergySubPacket(ShipSystem system, float value)
            : base(ShipAction3SubPacketType.EngSetEnergySubPacket, value, 0.0F)
        {
            System = system;
           
        }
        public EngSetEnergySubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public float Value
        {
            get
            {
                return Value1;
            }
            set
            {
                Value1 = value;
            }
        }
        [ArtemisExcluded]
        public ShipSystem System
        {
            get
            {
                return (ShipSystem)BitConverter.ToInt32(BitConverter.GetBytes(Value2), 0);
                
            }
            set
            {
                Value2 = BitConverter.ToSingle(BitConverter.GetBytes((int)value), 0);
            }
        }


       
        
    }
}