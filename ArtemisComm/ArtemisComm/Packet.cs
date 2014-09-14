using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    //Matches to Artemis Packet Protocol: Common Packet Structure
    public class Packet : IDisposable
    {
        /// <summary>
        /// Gets or sets a value indicating whether throw an exception if the packet is invalid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [throw when invalid]; otherwise, <c>false</c>.
        /// </value>
        public static bool ThrowWhenInvalid { get; set; }
        /// <summary>
        /// Gets the current active Artemis version.  This is intended for use globally for being able to code for differences in versions.
        /// 
        /// As of 12/7/2013, only version 2.0 is available and supported.
        /// </summary>
        /// <value>
        /// The current active Artemis version.
        /// </value>
        public static float CurrentActiveArtemisVersion { get; internal set; }

        /// <summary>
        /// The standard unique identifier.  If the ID on the Packet is not this value--there is a problem.
        /// </summary>
       
        //six header fields, all integers:
        public const int HeaderLength = 6*4;


        internal static int GetLength(byte[] byteArray, int startIndex)
        {
            if (startIndex < int.MaxValue - 4)
            {
                int position = startIndex + 4;
                return BitConverter.ToInt32(byteArray, position);
            }
            else
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }
        }
        //[ArtemisExcluded]
        //public string HexValue { get; private set; }



        MemoryStream _rawData = null;
        [ArtemisExcluded]
        public MemoryStream GetRawData()
        {

            if (_rawData == null)
            {
                SetRawData();
            }

            return _rawData.GetMemoryStream(0);

        }
        static string FormatIt(byte[] buffer)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in buffer)
            {
                sb.Append(b.ToString("x").PadLeft(2, '0'));
                sb.Append(":");
            }
            return sb.ToString();
        }
        public Packet(Stream stream)
        {
            if (stream != null)
            {
                if (stream.CanSeek)
                {
                    stream.Position = 0;
                }
#if DEBUG
                HexFormattedData = FormatIt(stream.GetMemoryStream(0).ToArray());
                if (stream.CanSeek)
                {
                    stream.Position = 0;
                }
#endif
                _rawData = stream.GetMemoryStream(0);

                if (ThrowWhenInvalid && _rawData.Length < HeaderLength)
                {
                    throw new InvalidPacketException();
                }
                else
                {
                    if (_rawData.Length > 3) ID = _rawData.ToInt32();
                    if (ThrowWhenInvalid && ID != Connector.StandardID)
                    {
                        throw new InvalidPacketException();
                    }
                    else
                    {
                        if (_rawData.Length > 7) Length = _rawData.ToInt32();
                        if (_rawData.Length > 11) Origin = (OriginType)_rawData.ToInt32();
                        if (_rawData.Length > 15) Padding = _rawData.ToInt32();
                        if (_rawData.Length > 19) PayloadLength = _rawData.ToInt32();
                        if (_rawData.Length > 23) PacketType = (PacketType)_rawData.ToInt32();


                        int ln = Convert.ToInt32(_rawData.Length);
                        if (ln > Length)
                        {
                            ln = Length;
                        }

                        Payload = _rawData.GetMemoryStream(HeaderLength);

                        _package = BuildPackage(_rawData, HeaderLength, Type.GetType(typeof(Packet).Namespace + "." + this.PacketType.ToString()));
                                //GetPackage(byteArray);
                        int packetLength = 0;
                        if (_package != null)
                        {
                            if (_package.GetErrors().Count > 0)
                            {
                                ConversionFailed = true;
                                
                            }
                            packetLength = Convert.ToInt32(Payload.Length);
                            if (Length - HeaderLength != packetLength)
                            {
                                if (ThrowWhenInvalid)
                                {
                                    throw new InvalidPacketException();
                                }
                                else
                                {
                                    ConversionException = new InvalidPacketException();
                                    ConversionFailed = true;
                                }
                            }
                        }
                        else
                        {
                            //Unknown package found!!
                        }
                    }
                }
            }

        }
#if DEBUG

        public string HexFormattedData
        {
            get;
            private set;
        }
#endif
        public Packet(IPackage package)
        {
            ID = Convert.ToInt32(Connector.StandardID);
            Origin = OriginType.Client;
            Package = package;
        }

        [ArtemisExcluded]
        public Exception ConversionException { get; private set; }

        [ArtemisExcluded]
        public bool ConversionFailed { get; private set; }

        IPackage BuildPackage(MemoryStream stream, int index, Type packageType)
        {
            try
            {

                return GetPackage(stream, index, packageType);
            }
            catch (OverflowException ex)
            {
                if (ThrowWhenInvalid)
                {
                    throw new InvalidPacketException(ex);
                }
                else
                {
                    //Receiving this means the type is not correct.
                    ConversionException = ex;
                    ConversionFailed = true;
                }
            }
            catch (Exception ex)
            {
                if (ThrowWhenInvalid)
                {
                    throw new InvalidPacketException(ex);
                }
                else
                {
                    //Receiving this probably means a type is not correct--but could mean something else.
                    ConversionException = ex;
                    ConversionFailed = true;
                }
            }
            return null;
        }
        internal static IPackage GetPackage(MemoryStream stream, int index, Type packageType)
        {
            IPackage retVal = null;

            object[] parms = { stream, index };
            Type[] constructorSignature = { typeof(MemoryStream), typeof(int) };
            if (packageType != null)
            {
                ConstructorInfo constructor = packageType.GetConstructor(constructorSignature);
                object obj = constructor.Invoke(parms);
                retVal = obj as IPackage;
            }

            return retVal;
        }


        //@@@@
        public int ID { get; private set; }

        public int Length { get; private set; }
        public OriginType Origin { get; private set; }
        /// <summary>
        ///  sets the origin.  Origin is normally not accessible since it can be deduced from the Packet type.
        ///  However, this allows setting the origin for reverse-engineering the protocol AND for future-compatibility:
        ///  if a new packet is built into a later version of Artemis, this library might not need updated to continue to support it.
        /// </summary>
        /// <param name="origin">The origin.</param>
        public void OverrideSetOrigin(OriginType origin)
        {
            Origin = origin;
        }
        public int Padding { get; set; }
        /// <summary>
        /// Gets the length of the payload.
        /// </summary>
        /// <value>
        /// The length of the payload.
        /// </value>
        public int PayloadLength { get; private set; }


        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        /// <value>
        /// The type of the packet.
        /// </value>
        public PacketType PacketType { get; private set; }
        public void OverrideSetPacketType(int packetType)
        {
            PacketType = (PacketType)packetType;
        }
        /// <summary>
        /// Sets the origin.  Origin is not set if the Packet type is not among the defined list--this is to assume that it is working with a newer version of Artemis
        /// than originally written for.
        /// </summary>
        void SetOrigin()
        {
            if (Package != null)
            {
                Origin = Package.GetValidOrigin();
            }
            //if (Package is AudioCommandPacket
            //    || Package is CommsOutgoingPacket
            //    || Package is ShipActionPacket
            //    || Package is ShipAction2Packet
            //    || Package is ShipAction3Packet)
            //{
            //    Origin = OriginType.Client;
            //}
            //if (Package is CommsIncomingPacket
            //    || Package is DestroyObjectPacket
            //    || Package is EngGridUpdatePacket
            //    || Package is IncomingAudioPacket
            //    || Package is GameMessagePacket
            //    || Package is ObjectStatusUpdatePacket
            //    || Package is StationStatusPacket
            //    || Package is VersionPacket
            //    || Package is WelcomePacket
            //    || Package is GameStartPacket
            //    || Package is Unknown2Packet)
            //{
            //    Origin = OriginType.Server;
            //}

        }
        IPackage _package;
        public IPackage Package
        {
            get
            {
                return _package;
            }
            private set
            {
                _package = value;

                if (value != null)
                {
                    Payload = value.GetRawData();
                    
                    //Payload = new ReadOnlyCollection<byte>(value.GetBytes());
                    SetOrigin();
                }
                else
                {
                    Payload = null;
                }
                if (Payload != null)
                {
                    string Payloadtype = _package.GetType().ToString();
                    int i = Payloadtype.LastIndexOf('.');
                    if (i > -1)
                    {
                        Payloadtype = Payloadtype.Substring(i + 1);
                    }
                    try
                    {
                        this.PacketType = (PacketType)Enum.Parse(typeof(PacketType), Payloadtype);
                    }
                    catch (Exception ex)
                    {
                        
                        ConversionException = ex;
                        this.PacketType = PacketType.InvalidPacket;
                    }
                    Length = Convert.ToInt32(Payload.Length) + HeaderLength;

                }
                else
                {
                    Length = HeaderLength;
                }
                PayloadLength = Length - HeaderLength + 4;
            }
        }

        //This is supposedly a bit field, although it's not certain what exactly is stored here.
        //It seems that this field can be safely ignored for all incoming packets. 
        //Most outgoing packets set this to 0x0c, but there are some exceptions:

        //0x10: EngSetEnergyPacket, HelmJumpPacket, SetStationPacket
        //0x18: CommsOutgoingPacket, ConvertTorpedoPacket, EngSendDamconPacket, EngSetCoolantPacket, LoadTubePacket
        //0x26: SetShipSettingsPacket
       

        public void OverrideSetPacketType(PacketType packetType)
        {
            PacketType = packetType;
        }

        public MemoryStream Payload { get; private set; }

        void SetRawData()
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                ms.Write(ID);
                ms.Write(Length);
                ms.Write(Origin);
                ms.Write(Padding);
                ms.Write(PayloadLength);
                ms.Write(PacketType);
                if (Package != null)
                {
                    Package.GetRawData().WriteTo(ms);

                }
                else
                {
                    Payload.WriteTo(ms);

                }
                _rawData = ms;
                ms = null;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Dispose();
                }
            }
        }
        
        bool _isDisposed = false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (!_isDisposed)
                {
                    if (_rawData != null)
                    {
                        _rawData.Dispose();
                    }
                    if (Payload != null)
                    {
                        Payload.Dispose();
                    }
                    if (Package != null)
                    {
                        Package.Dispose();
                    }
                    _isDisposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
//Header (int)

//The first four bytes constitute a header which identifies the packet as an Artemis packet. This value should always be 0xdeadbeef.

//Packet length (int)

//Gives the total length of the packet in bytes.

//Origin (int)

//Indicates whether this packet originates from the server (0x01) or the client (0x02).

//Unknown (int)

//The Artemis 1.7 API asserts that this int is always 0x00. It is unknown at this time what it's for.

//Flags (int)

//This is supposedly a bit field, although it's not certain what exactly is stored here. It seems that this field can be safely ignored for all incoming packets. Most outgoing packets set this to 0x0c, but there are some exceptions:

//0x10: EngSetEnergyPacket, HelmJumpPacket, SetStationPacket
//0x18: CommsOutgoingPacket, ConvertTorpedoPacket, EngSendDamconPacket, EngSetCoolantPacket, LoadTubePacket
//0x26: SetShipSettingsPacket
//Packet type (int)

//This value identifies the type of this packet. Different packet types communicate different information in their payloads. See: Packet Types

//Payload (compound)

//The remaining bytes in the packet contain the payload. Its format will vary depending on the packet type.