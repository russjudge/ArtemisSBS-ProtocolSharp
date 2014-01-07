using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public class ArtemisString : IDisposable
    {
        public const int LongestAllowedLength = 4096;  //arbitrary safety feature.  It is unlikely there would be a string with this many characters (2048)
        public ArtemisString() 
        {

        }
        public ArtemisString(string value)
        {
            Value = value;
        }
        //public ArtemisString(byte[] byteArray)
        //{
        //    Length = BitConverter.ToInt32(byteArray, 0);
        //    Value = System.Text.ASCIIEncoding.Unicode.GetString(byteArray, 4, Length * 2);
        //}
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ArtemisString"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ByteArray"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "index+4")]
        public ArtemisString(Stream stream, int index)
        {
            
            if (stream != null && index < stream.Length - 3)
            {
                using (MemoryStream Data = stream.GetMemoryStream(index))
                {
                    Data.Position = 0;

                    Length = Data.ToInt32();
                    if (Length <= LongestAllowedLength)
                    {

                        //Only last character should be a null character (two zeroes together).  If there are two null characters together elsewhere--the parsing is WRONG and needs fixed.
                        bool badString = false;
                        for (int i = index; i < (Length * 2) - 2; i += 2)
                        {
                            byte[] buffer = new byte[2];
                            Data.Read(buffer, 0, 2);
                            if (BitConverter.ToInt16(buffer, 0) == 0)
                            {
                                badString = true;
                                break;
                            }
                        }
                        if (badString)
                        {
                            throw new ParseException("Stream for ArtemisString not correct.");
                        }
                        else
                        {
                            Data.Position = 0;
                            Value = System.Text.ASCIIEncoding.Unicode.GetString(Data.GetBuffer(), 4, Length * 2 - 2);
                            if (Value != "Artemis" && Value != "Intrepid")
                            {

                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {
                        throw new ParseException("Stream for ArtemisString not correct: Length too long.");
                    }
                }
                if (stream.CanSeek)
                {
                    stream.Position = index;
                }

                byte[] buff = new byte[Length * 2 + 4];
                stream.Read(buff, 0, buff.Length);
                _rawData = new MemoryStream(buff);
                if (stream.CanSeek)
                {
                    stream.Position += Length * 2 + 4;
                }
            }

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
                    if (_value.EndsWith("\0", StringComparison.Ordinal))
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
       

        public override string ToString()
        {
            return Value;
        }


        #region RawData
        MemoryStream _rawData = null;
        [ArtemisExcluded]
        public MemoryStream GetRawData()
        {

            if (_rawData == null)
            {
                _rawData = this.SetRawData();
            }
            _rawData.Position = 0;
            return _rawData;

        }
        MemoryStream SetRawData()
        {
            MemoryStream retVal = null;
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream();
                stream.Write(BitConverter.GetBytes(Length), 0, 4);
                byte[] buffer = System.Text.ASCIIEncoding.Unicode.GetBytes(Value);
                stream.Write(buffer, 0, buffer.Length);
                stream.WriteByte(0);
                stream.WriteByte(0);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
            return retVal;
        }

        #endregion


        #region Dispose

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
                    _isDisposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
