using ArtemisComm.ShipAction2SubPackets;
using ArtemisComm.ShipAction3SubPackets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class ShipAction2Packet : BasePacket
    {
        public ShipAction2Packet(IPackage subPacket) : base()
        {
            SubPacket = subPacket;
        }
        public ShipAction2Packet(Stream stream, int index) : base()
        {
            try
            {
                if (stream != null)
                {
                    if (stream.CanSeek)
                    {
                        stream.Position = index;
                    }
                    if (index < stream.Length - 3)
                    {
                        SubPacketType = (ShipAction2SubPacketType)stream.ToInt32();
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
            catch (Exception ex)
            {
                AddError(ex);
            }
        }


        IPackage GetSubPacket(Stream stream)
        {
            IPackage retVal = null;
            object[] parms = { stream, 0 };
            Type[] constructorSignature = { typeof(Stream), typeof(int) };

            Type t = Type.GetType(typeof(ShipAction2SubPacketType).Namespace + "." + this.SubPacketType.ToString());


            if (t != null)
            {
                ConstructorInfo constructor = t.GetConstructor(constructorSignature);
                object obj = constructor.Invoke(parms);
                retVal = obj as IPackage;
            }

            return retVal;
        }


        public ShipAction2SubPacketType SubPacketType { get; set; }


        IPackage _subPacket = null;

        public IPackage SubPacket
        {
            get { return _subPacket; }
            set
            {
                _subPacket = value;
                if (value != null)
                {
                    SubPacketData = _subPacket.GetRawData();
                    string tp = _subPacket.GetType().Name;
                    SubPacketType = (ShipAction2SubPacketType)Enum.Parse(typeof(ShipAction2SubPacketType), tp);
                }
                else
                {
                    SubPacketData = new MemoryStream();
                    SubPacketType = (ShipAction2SubPacketType)int.MaxValue;
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
