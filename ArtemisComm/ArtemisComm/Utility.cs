using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public static class Utility
    {
        public static string BytesToDebugString(byte[] byteArray)
        {
            return BytesToDebugString(byteArray, 0);
        }

        public static string BytesToDebugString(byte[] byteArray, int index)
        {
            return BitConverter.ToString(byteArray, index).Replace("-", ":");
        }
        public static byte[] SafeByteCopy(this byte[] byteArray, int sourceIndex, int length)
        {
            if (byteArray != null)
            {
                return ToList(byteArray, sourceIndex, length).ToArray();
                
            }
            else
            {
                return null;
            }
        }
        public static byte[] SafeByteCopy(this byte[] byteArray)
        {
            if (byteArray != null)
            {
                return SafeByteCopy(byteArray, 0, byteArray.Length);
            }
            else
            {
                return null;
            }
        }
        public static List<byte> ToList(this byte[] byteArray)
        {
            if (byteArray != null)
            {
                return ToList(byteArray, 0, byteArray.Length);
            }
            else
            {
                return null;
            }
        }
        public static List<byte> ToList(this byte[] byteArray, int sourceIndex, int length)
        {
            if (byteArray != null)
            {
                List<byte> retVal = new List<byte>();
                int l = length;
                if (l > byteArray.Length)
                {
                    l = byteArray.Length;
                }
                for (int i = sourceIndex; i < l; i++)
                {
                    retVal.Add(byteArray[i]);
                }

                return retVal;
            }
            else
            {
                return null;
            }
        }
    }
}
