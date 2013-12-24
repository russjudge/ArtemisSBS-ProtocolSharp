using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public class ArtemisString 
    {
        public ArtemisString() 
        {

        }

        public ArtemisString(byte[] byteArray)
        {
            Length = BitConverter.ToInt32(byteArray, 0);
            Value = System.Text.ASCIIEncoding.Unicode.GetString(byteArray, 4, Length * 2);
        }
        public ArtemisString(byte[] byteArray, int index)
        {
            Length = BitConverter.ToInt32(byteArray, index);
            Value = System.Text.ASCIIEncoding.Unicode.GetString(byteArray, index + 4, Length * 2);
        }
        public int Length { get; private set; }
        string _value = null;
        
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (value != null)
                {
                    if (_value.EndsWith("\0"))
                    {
                        _value = value.Substring(0, _value.Length - 1);
                    }

                    Length = _value.Length + 1;
                }
                else
                {
                    Length = 0;
                }
            }
        }
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();

            retVal.AddRange(BitConverter.GetBytes(Length));
            retVal.AddRange(System.Text.ASCIIEncoding.Unicode.GetBytes(Value));
            retVal.Add(0);
            retVal.Add(0);
            return retVal.ToArray();
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
