using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class EngPlayerUpdateSubPacket : VariablePackage
    {

        public EngPlayerUpdateSubPacket()
            : base()
        {

        }
        public EngPlayerUpdateSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }

        public float? BeamHeat { get; set; }
        public float? TorpedoHeat { get; set; }
        public float? SensorsHeat { get; set; }
        public float? ManeuveringHeat { get; set; }
        public float? ImpulseHeat { get; set; }
        public float? DriveHeat { get; set; }
        public float? ForeShieldsHeat { get; set; }
        public float? AftShieldsHeat { get; set; }

        public float? BeamEnergy { get; set; }
        public float? TorpedoEnergy { get; set; }
        public float? SensorsEnergy { get; set; }
        public float? ManeuverEnergy { get; set; }
        public float? ImpulseEnergy { get; set; }
        public float? DriveEnergy { get; set; }
        public float? ForeShieldsEnergy { get; set; }
        public float? AftShieldsEnergy { get; set; }


        public byte? BeamCoolant { get; set; }
        public byte? TorpedoCoolant { get; set; }
        public byte? SensorsCoolant { get; set; }
        public byte? ManeuverCoolant { get; set; }
        public byte? ImpulseCoolant { get; set; }
        public byte? DriveCoolant { get; set; }
        public byte? ForeShieldsCoolant { get; set; }
        public byte? AftShieldsCoolant { get; set; }



        public int? BeamUnknown { get; set; }
        public int? TorpedoUnknown { get; set; }
        public int? SensorsUnknown { get; set; }
        public int? ManeuverUnknown { get; set; }
        public int? ImpulseUnknown { get; set; }
        public int? DriveUnknown { get; set; }
        public int? ForeShieldsUnknown { get; set; }
        public int? AftShieldsUnknown { get; set; }



    }
}
