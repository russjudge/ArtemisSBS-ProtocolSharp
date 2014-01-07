using ArtemisComm.ObjectStatusUpdateSubPackets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace ArtemisComm
{
    public class ObjectStatusUpdatePacket : BasePacket
    {
        public ObjectStatusUpdatePacket(Stream stream, int index) : base()
        {
            if (stream != null)
            {
                try
                {
                    if (stream.Length > index + 1)
                    {
                        SubPacketType = (ObjectStatusUpdateSubPacketType)Convert.ToByte(stream.ReadByte());
                    }
                    SubPacketData = stream.GetMemoryStream(index + 1);

                    if (SubPacketData != null)
                    {
                        _subPacket = GetSubPacket(SubPacketData);
                    }
                }
                catch (Exception ex)
                {
                    AddError(ex);
                }
            }
        }
        IPackage GetSubPacket(Stream stream)
        {
            IPackage retVal = null;
            object[] parms = { stream, 0 };
            Type[] constructorSignature = { typeof(Stream), typeof(int) };
            Type t = Type.GetType(typeof(ObjectStatusUpdateSubPacketType).Namespace + "." + this.SubPacketType.ToString());
            if (t != null)
            {
                ConstructorInfo constructor = t.GetConstructor(constructorSignature);
                object obj = constructor.Invoke(parms);
                retVal = obj as IPackage;
            }
            return retVal;
        }

        public ObjectStatusUpdateSubPacketType SubPacketType { get; set; }

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
                    SubPacketType = (ObjectStatusUpdateSubPacketType)Enum.Parse(typeof(ObjectStatusUpdateSubPacketType), tp);
                }
                else
                {
                    SubPacketData = new MemoryStream();
                    SubPacketType = (ObjectStatusUpdateSubPacketType)byte.MaxValue;
                }
            }
        }

        [ArtemisExcluded]
        public MemoryStream SubPacketData { get; private set; }

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
       
    }
}
