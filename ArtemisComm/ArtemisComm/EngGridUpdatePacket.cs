using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class EngGridUpdatePacket : IPackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(EngGridUpdatePacket));
        public EngGridUpdatePacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   

        }
        public EngGridUpdatePacket(byte[] byteArray)
        {
            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
            Systems = SystemNode.GetNodes(byteArray);
            int index = 0;
            foreach (SystemNode node in Systems)
            {
                index += node.DataLength;
            }
            
            DamageControlTeams = DamComStatus.GetDamComTeams(byteArray, ++index);

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
        }
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            foreach (SystemNode node in Systems)
            {
                retVal.AddRange(node.GetBytes());
            }
            retVal.Add(0xff);

            foreach (DamComStatus stat in DamageControlTeams)
            {
                retVal.AddRange(stat.GetBytes());
            }

            retVal.Add(0xfe);

            return retVal.ToArray();
        }

        public SystemNode[] Systems { get; set; }

        public DamComStatus[] DamageControlTeams { get; set; }
        
        //System grid status (array)

        //This contains a list of system nodes, terminated with 0xff. Each system node is formatted as follows:

        //X coordinate (byte)
        //Y coordinate (byte)
        //Z coordinate (byte)
        //Damage (float)
        //DAMCON team status (array)

        //This contains a list of DAMCON teams, terminated with 0xfe. Each DAMCON team is formatted as follows:

        //Team number (byte, this value minus 0x0a)
        //Goal X coordinate (int)
        //Current X coordinate (int)
        //Goal Y coordinate (int)
        //Current Y coordinate (int)
        //Goal Z coordinate (int)
        //Current Z coordinate (int)
        //Progress (float)
        //Number of team members (int)
    }
}
