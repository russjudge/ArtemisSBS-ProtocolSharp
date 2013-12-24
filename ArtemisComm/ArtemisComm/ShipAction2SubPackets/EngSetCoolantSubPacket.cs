using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class EngSetCoolantSubPacket : IPackage
    {
        public static Packet GetPacket(ShipSystem system, int value)
        {
            EngSetCoolantSubPacket escsp = new EngSetCoolantSubPacket(system, value);
            ShipAction2Packet sap2 = new ShipAction2Packet(escsp);
            return new Packet(sap2);
        }
        public EngSetCoolantSubPacket(ShipSystem system, int value)
        {
            System = system;
            Value = value;
        }
        public EngSetCoolantSubPacket(byte[] byteArray)
        {


            System = (ShipSystem)BitConverter.ToInt32(byteArray, 0);
            Value = BitConverter.ToInt32(byteArray, 4);
            

        }
        public ShipSystem System { get; set; }
        
        public int Value
        {
            get;
            set;
        }
        

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes((int)System));
            retVal.AddRange(BitConverter.GetBytes(Value));
            return retVal.ToArray();
        }
    }
}