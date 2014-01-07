
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class VariablePackage : IPackage
    {

        MemoryStream _rawData = null;
        public MemoryStream GetRawData()
        {
            if (_rawData == null)
            {
                _rawData = this.SetRawData();
            }
            return _rawData;
        }

        public VariablePackage(Stream stream, int index)
        {
            Initialize(stream, index);
        }
        static Collection<Exception> ProcessVariableData(PropertyInfo[] propertyList, object obj, Stream sourceData, int index, bool[] IncludedFields)
        {
            int position = index;
            Collection<Exception> errors = new Collection<Exception>();
            if (propertyList != null && obj != null && sourceData != null && IncludedFields != null)
            {

                using (MemoryStream dataStream = sourceData.GetMemoryStream(index))
                {
                    dataStream.Position = 0;

                    for (int i = 0; i < IncludedFields.Length; i++)
                    {

                        if (IncludedFields[i])
                        {
                            try
                            {
                                

                                position += Utility.LoadProperty(obj, dataStream, propertyList[i], errors);

                                
                            }
                            catch (Exception ex)
                            {
                                errors.Add(ex);
                            }
                        }

                    }
                }
            }
            return errors;
        }

        public static bool[] ProcessBitFlags(int propertyCount, MemoryStream stream, int index)
        {
            if (stream != null)
            {
                stream.Position = index;

                List<bool> IncludedList = new List<bool>();


                int k = 1;
              
                int FlagSize = 1;

                byte wrkByte = Convert.ToByte(stream.ReadByte());

                for (int i = 0; i < propertyCount; i++)
                {
                    //1, 2, 4, 8, 16, 32, 64, 128
                    bool test = ((wrkByte & k) == k);
                    IncludedList.Add(test);
                    k *= 2;
                    if (k > 128)
                    {
                        k = 1;
                        wrkByte = Convert.ToByte(stream.ReadByte());
                        FlagSize++;
                    }

                }

                return IncludedList.ToArray();
            }
            else
            {
                return null;
            }
        }
        
        void Initialize(Stream stream, int index)
        {
            if (stream != null)
            {
                if (stream.CanRead)
                {
                    stream.Position = index;
                }
                _rawData = stream.GetMemoryStream(index);
                if (stream.CanRead)
                {
                    stream.Position = index;
                }
                _rawData.Position = 0;
                ID = _rawData.ToInt32();
                

                List<PropertyInfo> propertyList = new List<PropertyInfo>();

                Type t = this.GetType();
                PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in properties)
                {
                    bool skip = false;
                    foreach (System.Attribute attrib in prop.GetCustomAttributes(true))
                    {
                        if (attrib is ArtemisExcludedAttribute)
                        {
                            skip = true;
                            break;
                        }
                    }
                    if (!skip)
                    {
                        if (prop.Name != "ID" && prop.Name != "IncludedFields")
                        {
                            propertyList.Add(prop);
                        }
                    }
                }

                IncludedFields = new ReadOnlyCollection<bool>(ProcessBitFlags(propertyList.Count, _rawData, 4));
                int flagsize = (propertyList.Count - 1)/ 8 + 1;

                int position = 4 + flagsize;

                errors = ProcessVariableData(propertyList.ToArray(), this, _rawData, position, IncludedFields.ToArray<bool>());
                
            }
            
        }
       

        public int ID { get; set; }
        [ArtemisExcluded]
        public int Length
        {
            get
            {
                if (_rawData != null)
                {
                    return Convert.ToInt32(_rawData.Length);
                }
                else
                {
                    return 0;
                }
            }
        }

        [ArtemisExcluded]
        public ReadOnlyCollection<bool> IncludedFields { get; private set; }

        //public byte[] GetBytes()
        //{
        //    List<byte> byteArray = new List<byte>();

        //    byteArray.AddRange(BitConverter.GetBytes(ID));



        //    List<byte> values = new List<byte>();
        //    List<PropertyInfo> propertyList = new List<PropertyInfo>();
        //    PropertyInfo[] properties = this.GetType().GetProperties();
        //    foreach (PropertyInfo prop in properties)
        //    {
        //        if (prop.Name != "ID" && prop.Name != "IncludedFields")
        //        {
        //            propertyList.Add(prop);
        //        }
        //    }
        //    byte position = 1;
        //    byte workByte = 0;
        //    for (int i = 0; i < IncludedFields.Count; i++)
        //    {

        //        if (IncludedFields[i])
        //        {

        //            workByte += position;
        //            PropertyInfo prop = propertyList[i];
        //            object obj = prop.GetValue(this, null);
        //            if (obj != null)
        //            {
        //                if (prop.PropertyType == typeof(int?))
        //                {
        //                    values.AddRange(BitConverter.GetBytes((int)obj));

        //                }
        //                if (prop.PropertyType == typeof(ArtemisString))
        //                {
        //                    ArtemisString item = ((ArtemisString)obj);
        //                    if (item != null)
        //                    {
        //                        values.AddRange(item.GetBytes());
        //                    }
        //                    else
        //                    {
        //                        for (int j = 0; j < 4; j++)
        //                        {
        //                            values.Add(0);
        //                        }
        //                    }

        //                }
        //                if (prop.PropertyType == typeof(float?))
        //                {

        //                    values.AddRange(BitConverter.GetBytes((float)obj));
        //                }
        //                if (prop.PropertyType == typeof(byte?) || prop.PropertyType == typeof(OrdinanceType?))
        //                {
        //                    values.Add((byte)obj);

        //                }
        //                if (prop.PropertyType == typeof(short?))
        //                {
        //                    values.AddRange(BitConverter.GetBytes((short)obj));
        //                }
        //                if (prop.PropertyType == typeof(long?))
        //                {
        //                    values.AddRange(BitConverter.GetBytes((long)obj));
        //                }
        //            }
                    
        //        }
        //        position *= 2;
        //        if (position > 128 || position == 0)
        //        {
        //            byteArray.Add(workByte);

        //            position = 1;
        //            workByte = 0;
        //        }
        //    }
        //    byteArray.AddRange(values);
        //    return byteArray.ToArray();
        //}

        public OriginType GetValidOrigin()
        {
            return OriginType.Server; 
        }

        Collection<Exception> errors = new Collection<Exception>();
        public ReadOnlyCollection<Exception> GetErrors()
        {
            return new ReadOnlyCollection<Exception>(errors);
        }
        #region Dispose

        bool _isDisposed = false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (!_isDisposed)
                {

                    this.DisposeProperties();
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
