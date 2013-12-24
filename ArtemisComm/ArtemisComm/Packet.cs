using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class Packet
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(Packet));
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


        public static int GetLength(byte[] byteArray, int startIndex)
        {
            return BitConverter.ToInt32(byteArray, startIndex + 4);
        }
        

        public Packet(byte[] byteArray)
        {
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.Info("~~~~~~~~~~~~~~~~Starting Packet Creation.~~~~~~~~~~~~~~~~~~~~~~~~"); }
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--{2} bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray), byteArray.Length.ToString()); }


                ID = BitConverter.ToUInt32(byteArray, 0); //Len = 4

                if (ID != Connector.StandardID)
                {
                    if (ThrowWhenInvalid)
                    {
                        throw new InvalidPacketException();
                    }
                    else
                    {
                        return;
                    }
                }

                if (_log.IsInfoEnabled) { _log.InfoFormat("ID={0}", ID.ToString()); }
                Length = BitConverter.ToInt32(byteArray, 4);//Len = 4
                if (_log.IsInfoEnabled) { _log.InfoFormat("Length={0}", Length.ToString()); }
                Origin = (OriginType)BitConverter.ToInt32(byteArray, 8);//Len = 4
                if (_log.IsInfoEnabled) { _log.InfoFormat("Origin={0}", Origin.ToString()); }
                Unknown = BitConverter.ToInt32(byteArray, 12);//Len = 4
                if (_log.IsInfoEnabled) { _log.InfoFormat("Unknown={0}", Unknown.ToString()); }
                PayloadLength = BitConverter.ToInt32(byteArray, 16);//Len = 4
                if (_log.IsInfoEnabled) { _log.InfoFormat("RemainingPacketLength={0}", PayloadLength.ToString()); }
                PacketType = (PacketTypes)BitConverter.ToUInt32(byteArray, 20);//Len = 4
                if (_log.IsInfoEnabled) { _log.InfoFormat("PacketType={0}", PacketType.ToString()); }
                List<byte> newArray = new List<byte>();
                int ln = byteArray.Length;
                if (ln > Length)
                {
                    ln = Length;
                }
                for (int i = HeaderLength; i < ln; i++)
                {
                    newArray.Add(byteArray[i]);
                }
                Payload = newArray.ToArray();

                _package = GetPackage(Payload);
                int packetLength = 0;
                if (_package != null)
                {
                    byte[] packetBytes = _package.GetBytes();
                    packetLength = packetBytes.Length;
                }
                if (packetLength + HeaderLength != Length && ThrowWhenInvalid)
                {
                    throw new InvalidPacketException();
                }

                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--{2} Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes()), this.GetBytes().Length); }
                if (_log.IsInfoEnabled) { _log.Info("~~~~~~~~~~~~~~~~Packet Creation Ended.~~~~~~~~~~~~~~~~~~~~~~~~"); }
            }
        }
        public Packet(IPackage package)
        {
            ID = Convert.ToUInt32(Connector.StandardID);
            Origin = OriginType.Client;
            Package = package;
        }


        IPackage GetPackage(byte[] byteArray)
        {
            IPackage retVal = null;
            try
            {
                object[] parms = { byteArray };
                Type[] constructorSignature = { typeof(byte[]) };

                Type t = Type.GetType(typeof(Packet).Namespace + "." + this.PacketType.ToString());


                if (t != null)
                {

                    ConstructorInfo constructor = t.GetConstructor(constructorSignature);
                    object obj = constructor.Invoke(parms);
                    retVal = obj as IPackage;

                }
            }
            catch (OverflowException ex)
            {
                //Receiving this means the type is not correct.
            }
            catch (Exception ex)
            {
                //Receiving this probably means a type is not correct--but could mean something else.
            }
            return retVal;
        }

        public uint ID { get; private set; }

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
        public int Unknown { get; set; }
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
        public PacketTypes PacketType { get; private set; }
        /// <summary>
        /// Sets the origin.  Origin is not set if the Packet type is not among the defined list--this is to assume that it is working with a newer version of Artemis
        /// than originally written for.
        /// </summary>
        void SetOrigin()
        {
            
            if (Package is AudioCommandPacket
                || Package is CommsOutgoingPacket
                || Package is ShipActionPacket
                || Package is ShipAction2Packet
                || Package is ShipAction3Packet)
            {
                Origin = OriginType.Client;
            }
            if (Package is CommsIncomingPacket
                || Package is DestroyObjectPacket
                || Package is EngGridUpdatePacket
                || Package is IncomingAudioPacket
                || Package is GameMessagePacket
                || Package is ObjectStatusUpdatePacket
                || Package is StationStatusPacket
                || Package is VersionPacket
                || Package is WelcomePacket
                || Package is GameStartPacket
                || Package is Unknown2Packet)
            {
                Origin = OriginType.Server;
            }

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
                    Payload = value.GetBytes();
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
                        this.PacketType = (PacketTypes)Enum.Parse(typeof(PacketTypes), Payloadtype);
                    }
                    catch
                    {
                        this.PacketType = PacketTypes.InvalidPacket;
                    }
                    Length = Payload.Length + HeaderLength;

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
       

        public void OverrideSetPacketType(PacketTypes packetType)
        {
            PacketType = packetType;
        }

        private byte[] Payload { get; set; }


        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();

            retVal.AddRange(BitConverter.GetBytes(ID));
            retVal.AddRange(BitConverter.GetBytes(Length));
            retVal.AddRange(BitConverter.GetBytes((int)Origin));

            retVal.AddRange(BitConverter.GetBytes(Unknown));
            retVal.AddRange(BitConverter.GetBytes(PayloadLength));

            retVal.AddRange(BitConverter.GetBytes((uint)PacketType));
            retVal.AddRange(Payload);
            return retVal.ToArray();
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