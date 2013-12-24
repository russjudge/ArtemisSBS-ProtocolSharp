using ArtemisComm.GameMessageSubPackets;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class GameMessagePacket : IPackage
    {

        //**CONFIRMED
        static readonly ILog _log = LogManager.GetLogger(typeof(GameMessagePacket));
        public GameMessagePacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   

        }
        // GameStartSubPacket = 0,
        //GameOverSubPacket = 6,
        //Unknown1SubPacket = 8,
        //Unknown2SubPacket = 9,
        //GameTextMessageSubPacket = 10,
        //JumpStartSubPacket = 12,
        //JumpCompleteSubPacket = 13,
        //AllShipSettingsSubPacket = 15,
        public GameMessagePacket(byte[] byteArray)
        {
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
                if (byteArray.Length > 3)
                {
                    SubPacketType = (GameMessageSubPacketTypes)BitConverter.ToInt32(byteArray, 0);
                }
                if (byteArray.Length >= 4)
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

            Type t = Type.GetType(typeof(GameMessageSubPacketTypes).Namespace + "." + this.SubPacketType.ToString());


            if (t != null)
            {
                ConstructorInfo constructor = t.GetConstructor(constructorSignature);
                object obj = constructor.Invoke(parms);
                retVal = obj as IPackage;
            }

            return retVal;
        }



        public GameMessageSubPacketTypes SubPacketType { get; private set; }


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
                    SubPacketType = (GameMessageSubPacketTypes)Enum.Parse(typeof(GameMessageSubPacketTypes), tp);
                }
                else
                {
                    SubPacketData = new byte[0];
                    SubPacketType = (GameMessageSubPacketTypes)int.MaxValue;
                }
                


            }
        }

        //Message type (int)
        public byte[] SubPacketData { get; set; }

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();

            retVal.AddRange(BitConverter.GetBytes((int)SubPacketType));
            if (SubPacketData != null)
            {
                retVal.AddRange(SubPacketData);
            }
            return retVal.ToArray();
        }
    }
}
