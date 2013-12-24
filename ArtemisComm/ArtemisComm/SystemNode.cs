using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class SystemNode
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(SystemNode));   

        public static SystemNode[] GetNodes(byte[] byteArray)
        {
            List<SystemNode> retVal = new List<SystemNode>();
            int index = 0;
            do
            {
                SystemNode nd = new SystemNode(byteArray, index);
                index += nd.DataLength;
                retVal.Add(nd);

            } while (byteArray[index] != 255);
            return retVal.ToArray();
        }
        public SystemNode()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   

        }
        public SystemNode(byte[] byteArray)
        {
            Initialize(byteArray, 0);
           
        }
        public SystemNode(byte[] byteArray, int index)
        {
            Initialize(byteArray, index);

        }
        public int DataLength
        {
            get
            {
                int retVal = 0;
                if (X != null)
                {
                    retVal++;
                }
                if (Y != null)
                {
                    retVal++;
                }
                if (Z != null)
                {
                    retVal++;
                }
                if (Damage != null)
                {
                    retVal++;
                }
                return retVal;
            }
        }
        void Initialize(byte[] byteArray, int index)
        {
            if (byteArray != null)
            {
                bool isDone = false;
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }

                if (byteArray[index] == 255)
                {
                    isDone = true;
                }
                if (index < byteArray.Length && !isDone )
                {
                    X = byteArray[index];
                    if (_log.IsInfoEnabled) { _log.InfoFormat("X={0}", X); }
                    if (byteArray[index+ 1] == 255)
                    {
                        isDone = true;
                    }
                }


                if (index + 1 < byteArray.Length && !isDone)
                {
                    Y = byteArray[index + 1];
                    if (_log.IsInfoEnabled) { _log.InfoFormat("Y={0}", Y); }
                    if (byteArray[index + 2] == 255)
                    {
                        isDone = true;
                    }
                }
                if (index + 2 < byteArray.Length && !isDone)
                {
                    Z = byteArray[index + 2];
                    if (_log.IsInfoEnabled) { _log.InfoFormat("Z={0}", Z); }
                    if (byteArray[index + 3] == 255)
                    {
                        isDone = true;
                    }
                }
                if (index + 7 <= byteArray.Length && !isDone)
                {
                    Damage = BitConverter.ToSingle(byteArray, index + 3);
                    if (_log.IsInfoEnabled) { _log.InfoFormat("Damage={0}", Damage); }
                }
                else
                {
                    isDone = true; 
                }
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
        }
        //This contains a list of system nodes, terminated with 0xff. Each system node is formatted as follows:

        //X coordinate (byte)
        //Y coordinate (byte)
        //Z coordinate (byte)
        //Damage (float)
        //DAMCON team status (array)
        public byte? X { get; set; }
        public byte? Y { get; set; }
        public byte? Z { get; set; }

        public float? Damage { get; set; }

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
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            if (X != null)
            {
                retVal.Add(X.Value);
            }
            if (Y != null)
            {
                retVal.Add(Y.Value);
            }
            if (Z != null)
            {
                retVal.Add(Z.Value);
            }
            if (Damage != null)
            {
                retVal.AddRange(BitConverter.GetBytes(Damage.Value));
            }
            return retVal.ToArray();
        }
    }
}
