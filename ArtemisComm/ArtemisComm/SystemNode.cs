using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class SystemNode
    {
        public static ReadOnlyCollection<SystemNode> GetNodes(Stream stream, int index)
        {
            List<SystemNode> retVal = new List<SystemNode>();
            byte endCheck = 0;
            do
            {
                SystemNode nd = new SystemNode(stream, index);
                index += nd.DataLength;
                retVal.Add(nd);
                if (index < stream.Length)
                {
                    stream.Position = index;
                    endCheck = Convert.ToByte(stream.ReadByte());
                }
            } while (endCheck != 255 && index < stream.Length);
            return new ReadOnlyCollection<SystemNode>(retVal);
        }


        //public static SystemNode[] GetNodes(byte[] byteArray)
        //{
        //    List<SystemNode> retVal = new List<SystemNode>();
        //    int index = 0;
        //    do
        //    {
        //        SystemNode nd = new SystemNode(byteArray, index);
        //        index += nd.DataLength;
        //        retVal.Add(nd);

        //    } while (byteArray[index] != 255);
        //    return retVal.ToArray();
        //}
        public SystemNode()
        {

        }
        //public SystemNode(byte[] byteArray)
        //{
        //    Initialize(byteArray, 0);
           
        //}
        public SystemNode(Stream stream, int index)
        {
            Initialize(stream, index);

        }

        [ArtemisExcluded]
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
                    retVal += 4;
                }
                return retVal;
            }
        }
        void Initialize(Stream stream, int index)
        {
            if (stream != null)
            {
                bool isDone = false;
                stream.Position = index;
                if (Convert.ToByte(stream.ReadByte()) == 255)
                {
                    isDone = true;
                    return;
                }
                stream.Position = index++;
                if (index < stream.Length && !isDone )
                {
                    X = Convert.ToByte(stream.ReadByte());

                    if (Convert.ToByte(stream.ReadByte()) == 255)
                    {
                        isDone = true;
                        return;
                    }
                    stream.Position = ++index;
                }


                if (index < stream.Length && !isDone)
                {
                    Y = Convert.ToByte(stream.ReadByte());

                    if (Convert.ToByte(stream.ReadByte()) == 255)
                    {
                        isDone = true;
                        return;
                    }
                    stream.Position = ++index;
                }
                if (index < stream.Length && !isDone)
                {
                    Z = Convert.ToByte(stream.ReadByte());

                    if (Convert.ToByte(stream.ReadByte()) == 255)
                    {
                        isDone = true;
                        return;
                    }
                    stream.Position = ++index;
                }
                if (index < stream.Length - 4 && !isDone)
                {
                    byte[] buffer = new byte[4];
                   
                        stream.Read(buffer, 0, 4);
                   
                    Damage = BitConverter.ToSingle(buffer, 0);
                }
                else
                {
                    isDone = true; 
                }
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
        //public byte[] GetBytes()
        //{
        //    List<byte> retVal = new List<byte>();
        //    if (X != null)
        //    {
        //        retVal.Add(X.Value);
        //    }
        //    if (Y != null)
        //    {
        //        retVal.Add(Y.Value);
        //    }
        //    if (Z != null)
        //    {
        //        retVal.Add(Z.Value);
        //    }
        //    if (Damage != null)
        //    {
        //        retVal.AddRange(BitConverter.GetBytes(Damage.Value));
        //    }
            
        //    return retVal.ToArray();
        //}

    }
}
