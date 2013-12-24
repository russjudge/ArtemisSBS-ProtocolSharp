#if LOG4NET
using log4net;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class DamComStatus : IPackage
    {
#if LOG4NET
        static readonly ILog _log = LogManager.GetLogger(typeof(DamComStatus));
#endif
        const int PacketLength = 33;
        public static DamComStatus[] GetDamComTeams(byte[] byteArray, int index)
        {
            List<DamComStatus> retVal = new List<DamComStatus>();
            if (byteArray != null)
            {
                int position = index;
                while (position < byteArray.Length && byteArray[position] != 0xfe)
                {
                    DamComStatus d = new DamComStatus(byteArray, position);
                    retVal.Add(d);
                    position += PacketLength;
                }
            }
            return retVal.ToArray();
        }
        public DamComStatus()
        {
#if LOG4NET

            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
#endif
        }
        void Initialize(byte[] byteArray, int index)
        {
            if (byteArray != null)
            {
#if LOG4NET

                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
#endif
                if (byteArray[index] < 0x0a)
                {

                }
                TeamNumber = Convert.ToByte(byteArray[index] - 0x0a);


                GoalX = BitConverter.ToInt32(byteArray, index + 1);

                CurrentX = BitConverter.ToInt32(byteArray, index + 5);

                GoalY = BitConverter.ToInt32(byteArray, index + 9);
                CurrentY = BitConverter.ToInt32(byteArray, index + 13);


                GoalZ = BitConverter.ToInt32(byteArray, index + 17);

                CurrentZ = BitConverter.ToInt32(byteArray, index + 21);

                Progress = BitConverter.ToSingle(byteArray, index + 25);

                NumberOfTeamMembers = BitConverter.ToInt32(byteArray, index + 29);

#if LOG4NET

                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
#endif
            }
        }
        public DamComStatus(byte[] byteArray)
        {
            Initialize(byteArray, 0);
        }
        public DamComStatus(byte[] byteArray, int index)
        {
            Initialize(byteArray, index);
        }
        //DAMCON team status (array)

        //This contains a list of DAMCON teams, terminated with 0xfe. Each DAMCON team is formatted as follows:

        //Team number (byte, this value minus 0x0a)
        public byte TeamNumber { get; set; }

        public int GoalX { get; set; }
        //Goal X coordinate (int)
        public int CurrentX { get; set; }
        //Current X coordinate (int)
        public int GoalY { get; set; }
        //Goal Y coordinate (int)

        public int CurrentY { get; set; }
        //Current Y coordinate (int)

        public int GoalZ { get; set; }
        //Goal Z coordinate (int)
        public int CurrentZ { get; set; }
        //Current Z coordinate (int)
        public float Progress { get; set; }
        //Progress (float)
        public int NumberOfTeamMembers { get; set; }
        //Number of team members (int)
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.Add(Convert.ToByte(TeamNumber + 0x0a));

            retVal.AddRange(BitConverter.GetBytes(GoalX));

            retVal.AddRange(BitConverter.GetBytes(CurrentX));

            retVal.AddRange(BitConverter.GetBytes(GoalY));

            retVal.AddRange(BitConverter.GetBytes(CurrentY));

            retVal.AddRange(BitConverter.GetBytes(GoalZ));

            retVal.AddRange(BitConverter.GetBytes(CurrentZ));

            retVal.AddRange(BitConverter.GetBytes(Progress));
            retVal.AddRange(BitConverter.GetBytes(NumberOfTeamMembers));
            
            return retVal.ToArray();
        }
    }
}
