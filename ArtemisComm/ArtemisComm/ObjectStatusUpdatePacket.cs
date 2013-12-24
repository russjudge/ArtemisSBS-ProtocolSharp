using ArtemisComm.ObjectStatusUpdateSubPackets;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class ObjectStatusUpdatePacket : IPackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(ObjectStatusUpdatePacket));
        public ObjectStatusUpdatePacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   

        }

        public ObjectStatusUpdatePacket(byte[] byteArray)
        {
         
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
                if (byteArray.Length > 1)
                {
                    SubPacketType = (ObjectStatusUpdateSubPacketTypes)byteArray[0];   //BitConverter.ToInt32(byteArray, 0);
                    if (_log.IsInfoEnabled)
                    {
                        _log.InfoFormat("SubPacketType={0}", SubPacketType.ToString());
                    }
                }


                List<byte> bytes = new List<byte>();
                for (int i = 1; i < byteArray.Length; i++)
                {
                    bytes.Add(byteArray[i]);
                }
                SubPacketData = bytes.ToArray();
                _subPacket = GetSubPacket(SubPacketData);
                //if (byteArray.Length > 4)
                //{
                //    List<byte> bytes = new List<byte>();
                //    for (int i = 4; i < byteArray.Length; i++)
                //    {
                //        bytes.Add(byteArray[i]);
                //    }
                //    SubPacketData = bytes.ToArray();
                //    _subPacket = GetSubPacket(SubPacketData);
                //}
                //else
                //{
                //    _subPacket = null;
                //    SubPacketData = null;
                //}
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
            
        }
        IPackage GetSubPacket(byte[] byteArray)
        {
            IPackage retVal = null;
            object[] parms = { byteArray };
            Type[] constructorSignature = { typeof(byte[]) };

            Type t = Type.GetType(typeof(ObjectStatusUpdateSubPacketTypes).Namespace + "." + this.SubPacketType.ToString());


            if (t != null)
            {
                ConstructorInfo constructor = t.GetConstructor(constructorSignature);
                object obj = constructor.Invoke(parms);
                retVal = obj as IPackage;
            }

            return retVal;
        }

        public ObjectStatusUpdateSubPacketTypes SubPacketType { get; set; }

        IPackage _subPacket = null;

        public IPackage SubPacket
        {
            get { return _subPacket; }
            set
            {
                _subPacket = value;
                if (value != null)
                {
                    SubPacketData = _subPacket.GetBytes();
                    string tp = _subPacket.GetType().Name;
                    SubPacketType = (ObjectStatusUpdateSubPacketTypes)Enum.Parse(typeof(ObjectStatusUpdateSubPacketTypes), tp);
                }
                else
                {
                    SubPacketData = new byte[0];
                    SubPacketType = (ObjectStatusUpdateSubPacketTypes)byte.MaxValue;
                }



            }
        }

        //Message type (int)
        public byte[] SubPacketData { get; set; }


        
        
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();

            //retVal.AddRange(BitConverter.GetBytes((int)SubPacketType));
            retVal.Add((byte)SubPacketType);
            if (_log.IsInfoEnabled)
            {
                _log.InfoFormat("~~~~SubPacketType added to bytes: {0}", Utility.BytesToDebugString(retVal.ToArray()));
            }
            if (SubPacketData != null)
            {
                retVal.AddRange(SubPacketData);
                if (_log.IsInfoEnabled)
                {
                    _log.InfoFormat("~~~~####SubPacketData added to bytes: {0}", Utility.BytesToDebugString(retVal.ToArray()));
                }
            }
            return retVal.ToArray();
        }
    }
}
