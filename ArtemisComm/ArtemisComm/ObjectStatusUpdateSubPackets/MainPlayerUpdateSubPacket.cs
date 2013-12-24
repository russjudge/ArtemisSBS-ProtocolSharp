using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class MainPlayerUpdateSubPacket : VariablePackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(MainPlayerUpdateSubPacket));
       public MainPlayerUpdateSubPacket()
            : base()
        {

        }
        public MainPlayerUpdateSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }
        public MainPlayerUpdateSubPacket(byte[] byteArray, int index)
            : base(byteArray, index)
        {

        }
        public int? Unknown { get; set; }
        public float? Impulse { get; set; }
        public float? Rudder { get; set; }
        public float? TopSpeed { get; set; }
        public float? TurnRate { get; set; }
        public byte? AutoBeams { get; set; }
        public byte? Warp { get; set; }
        public float? Energy { get; set; }
        public short? ShieldState { get; set; }
        public int? ShipNumber { get; set; }
        public int? ShipType { get; set; }
        public float? X { get; set; }
        public float? Y { get; set; }
        public float? Z { get; set; }
        public int? Unknown2 { get; set; }
        public int? Unknown3 { get; set; }
        /// <summary>
        /// Gets or sets the heading. pi to negative pi. 0 is south
        /// </summary>
        /// <value>
        /// The heading.
        /// </value>
        public float? Heading { get; set; }
        public float? Velocity { get; set; }
        public short? Unknown4 { get; set; }
        public ArtemisString Name { get; set; }
        public float? ForeShields { get; set; }
        public float? ForeShieldsMax { get; set; }
        public float? AftShields { get; set; }
        public float? AftShieldsMax { get; set; }

        public int? DockingStation { get; set; }
        public byte? RedAlert { get; set; }
        public float? Unknown5 { get; set; }
        public byte? MainScreen { get; set; }
        public byte? BeamFrequency { get; set; }
        public byte? AvailableCoolant { get; set; }
        public int? ScienceTarget { get; set; }
        public int? CaptainTarget { get; set; }
        public byte? DriveType { get; set; }
        public int? ScanningID { get; set; }
        public float? ScanProgress { get; set; }
        public int? Reverse { get; set; }
        public short? Undefined1 { get; set; }
        public int? Undefined2 { get; set; }
        public int? Undefined3 { get; set; }
    }
}
