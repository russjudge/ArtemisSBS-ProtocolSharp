using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class ShipActionPacket : IPackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(ShipActionPacket));
        public ShipActionPacket(IPackage subpacket)
        {
            SubPacket = subpacket;
        }
     
        public ShipActionPacket(byte[] byteArray)
        {
            
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
                if (byteArray.Length > 3)
                {
                    SubPacketType = (ShipActionSubPackets.ShipActionSubPacketTypes)BitConverter.ToUInt32(byteArray, 0);
                }
                if (byteArray.Length > 4)
                {
                    List<byte> bytes = new List<byte>();
                    for (int i = 4; i < byteArray.Length; i++)
                    {
                        bytes.Add(byteArray[i]);
                    }
                    SubPacketData = bytes.ToArray();
                    _subPacket = GetSubPacket(SubPacketData);
                }
                else
                {
                    _subPacket = null;
                    SubPacketData = null;
                }
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
        }


        IPackage GetSubPacket(byte[] byteArray)
        {
            IPackage retVal = null;
            object[] parms = { byteArray };
            Type[] constructorSignature = { typeof(byte[]) };

            Type t = Type.GetType(typeof(ShipActionSubPackets.ShipActionSubPacketTypes).Namespace + "." + this.SubPacketType.ToString());


            if (t != null)
            {
                ConstructorInfo constructor = t.GetConstructor(constructorSignature);
                object obj = constructor.Invoke(parms);
                retVal = obj as IPackage;
            }

            return retVal;
        }


        public ShipActionSubPackets.ShipActionSubPacketTypes SubPacketType { get; set; }


        IPackage _subPacket = null;

        public IPackage SubPacket
        {
            get { return _subPacket; }
            private set
            {
                _subPacket = value;
                if (value != null)
                {
                    SubPacketData = _subPacket.GetBytes();
                    string tp = _subPacket.GetType().Name;
                    SubPacketType = (ShipActionSubPackets.ShipActionSubPacketTypes)Enum.Parse(typeof(ShipActionSubPackets.ShipActionSubPacketTypes), tp);
                }
                else
                {
                    SubPacketData = new byte[0];
                    SubPacketType = (ShipActionSubPackets.ShipActionSubPacketTypes)int.MaxValue;
                }



            }
        }

        //Message type (int)
        public byte[] SubPacketData { get; set; }



        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes((uint)SubPacketType));

            retVal.AddRange(SubPacketData);
            return retVal.ToArray();
        }
    }
}
