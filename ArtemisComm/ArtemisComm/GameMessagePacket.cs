using ArtemisComm.GameMessageSubPackets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class GameMessagePacket : IPackage
    {

        MemoryStream _rawData = null;
        public MemoryStream GetRawData()
        {
            if (_rawData == null)
            {
                _rawData = this.SetRawData();
            }
            return _rawData;
        }
        // GameStartSubPacket = 0,
        //GameOverSubPacket = 6,
        //Unknown1SubPacket = 8,
        //Unknown2SubPacket = 9,
        //GameTextMessageSubPacket = 10,
        //JumpStartSubPacket = 12,
        //JumpCompleteSubPacket = 13,
        //AllShipSettingsSubPacket = 15,
        public GameMessagePacket(Stream stream, int index)
        {
            try
            {
                if (stream != null)
                {
                    if (stream.CanSeek)
                    {
                        stream.Position = index;
                    }
                    if (stream.Length > 3)
                    {
                        SubPacketType = (GameMessageSubPacketType)stream.ToInt32();
                       
                    }
                    if (stream.Length >= 4)
                    {
                        SubPacketData = stream.GetMemoryStream(index + 4);

                     
                        _subPacket = GetSubPacket(stream, index + 4);
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
                errors.Add(ex);
            }
        }
        //IPackage GetSubPacket(byte[] byteArray)
        //{
        //    IPackage retVal = null;
        //    object[] parms = { byteArray };
        //    Type[] constructorSignature = { typeof(byte[]) };

        //    Type t = Type.GetType(typeof(GameMessageSubPacketType).Namespace + "." + this.SubPacketType.ToString());


        //    if (t != null)
        //    {
        //        ConstructorInfo constructor = t.GetConstructor(constructorSignature);
        //        object obj = constructor.Invoke(parms);
        //        retVal = obj as IPackage;
        //    }

        //    return retVal;
        //}
        IPackage GetSubPacket(Stream stream, int index)
        {
            IPackage retVal = null;
            try
            {
                if (stream.CanSeek)
                {
                    stream.Position = index;
                }

                object[] parms = { stream, index };
                Type[] constructorSignature = { typeof(Stream), typeof(int) };

                Type t = Type.GetType(typeof(GameMessageSubPacketType).Namespace + "." + this.SubPacketType.ToString());


                if (t != null)
                {
                    ConstructorInfo constructor = t.GetConstructor(constructorSignature);
                    object obj = constructor.Invoke(parms);
                    retVal = obj as IPackage;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
            return retVal;
        }


        public GameMessageSubPacketType SubPacketType { get; private set; }


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
                    SubPacketType = (GameMessageSubPacketType)Enum.Parse(typeof(GameMessageSubPacketType), tp);
                }
                else
                {
                    SubPacketData = new MemoryStream();
                    SubPacketType = (GameMessageSubPacketType)int.MaxValue;
                }
                


            }
        }

        //Message type (int)
        public MemoryStream SubPacketData { get; private set; }

        //public byte[] GetBytes()
        //{
        //    List<byte> retVal = new List<byte>();

        //    retVal.AddRange(BitConverter.GetBytes((int)SubPacketType));
        //    if (SubPacketData != null)
        //    {
        //        retVal.AddRange(SubPacketData);
        //    }
        //    return retVal.ToArray();
        //}

        public OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        List<Exception> errors = new List<Exception>();
        public ReadOnlyCollection<Exception> GetErrors()
        {
            return new ReadOnlyCollection<Exception>(errors);
        }
        #region Dispose

        bool _isDisposed = false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (!_isDisposed)
                {

                    this.DisposeProperties();
                    _isDisposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
