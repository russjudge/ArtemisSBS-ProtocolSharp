using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class ShipActionPacket : BasePacket
    {
        public ShipActionPacket(IPackage subpacket)
            : base()
        {
            SubPacket = subpacket;
        }

        public ShipActionPacket(Stream stream, int index)
            : base()
        {

            if (stream != null)
            {
                if (stream.CanSeek)
                {
                    stream.Position = index;
                }
                if (index < stream.Length - 3)
                {
                    SubPacketType = (ShipActionSubPackets.ShipActionSubPacketType)stream.ToInt32();
                }
                if (index < stream.Length - 4)
                {
                    SubPacketData = stream.GetMemoryStream(index + 4);
                    _subPacket = GetSubPacket(SubPacketData);
                }
                else
                {
                    _subPacket = null;
                    SubPacketData = null;
                }
            }
        }


        IPackage GetSubPacket(Stream stream)
        {
            IPackage retVal = null;
            object[] parms = { stream, 0 };
            Type[] constructorSignature = { typeof(Stream), typeof(int) };

            Type t = Type.GetType(typeof(ShipActionSubPackets.ShipActionSubPacketType).Namespace + "." + this.SubPacketType.ToString());


            if (t != null)
            {
                ConstructorInfo constructor = t.GetConstructor(constructorSignature);
                object obj = constructor.Invoke(parms);
                retVal = obj as IPackage;
            }

            return retVal;
        }


        public ShipActionSubPackets.ShipActionSubPacketType SubPacketType { get; set; }


        IPackage _subPacket = null;

        public IPackage SubPacket
        {
            get { return _subPacket; }
            private set
            {
                _subPacket = value;
                if (value != null)
                {
                    SubPacketData = _subPacket.GetRawData();
                    string tp = _subPacket.GetType().Name;
                    SubPacketType = (ShipActionSubPackets.ShipActionSubPacketType)Enum.Parse(typeof(ShipActionSubPackets.ShipActionSubPacketType), tp);
                }
                else
                {
                    SubPacketData = new MemoryStream();
                    SubPacketType = (ShipActionSubPackets.ShipActionSubPacketType)int.MaxValue;
                }



            }
        }

        [ArtemisExcluded]
        public MemoryStream SubPacketData { get; private set; }



        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }

    }
}
