using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class VariablePackage : IPackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(VariablePackage));
        public VariablePackage()
        {

        }
        public VariablePackage(byte[] byteArray, int index)
        {
            Initialize(byteArray, index);
        }
        public static void ProcessVariableData(PropertyInfo[] propertyList, object obj, byte[] sourceData, int index, bool[] IncludedFields)
        {
            
            if (propertyList != null && obj != null && sourceData != null && IncludedFields != null)
            {


              //@@@@
                int position = index;
                for (int i = 0; i < IncludedFields.Length; i++)
                {
                    if (position > sourceData.Length)
                    {
                        break;
                    }
                    if (IncludedFields[i])
                    {
                        try
                        {
                            PropertyInfo prop = propertyList[i];
                            if (prop.PropertyType == typeof(bool?))
                            {
                                //incoming must be byte--no way to translate other type at this point.
                                bool? boolVal = Convert.ToBoolean(sourceData[position]);
                                if (_log.IsInfoEnabled && boolVal != null)
                                {
                                    _log.InfoFormat("Property {0} set to {1}", prop.Name, boolVal.Value.ToString());
                                }
                                propertyList[i].SetValue(obj, boolVal, null);
                                position += 1;
                            }
                            if (prop.PropertyType == typeof(int?))
                            {
                                if (position + 4 < sourceData.Length)
                                {
                                    int? intVal = BitConverter.ToInt32(sourceData, position);
                                    if (_log.IsInfoEnabled && intVal != null)
                                    {
                                        _log.InfoFormat("Property {0} set to {1}", prop.Name, intVal.Value.ToString());
                                    }
                                    propertyList[i].SetValue(obj, intVal, null);
                                }
                                position += 4;
                            }
                            if (prop.PropertyType == typeof(ArtemisString))
                            {

                                ArtemisString val = new ArtemisString(sourceData, position);

                                if (_log.IsInfoEnabled && val != null)
                                {
                                    _log.InfoFormat("Property {0} set to {1}", prop.Name, val.Value);
                                }


                                propertyList[i].SetValue(obj, val, null);
                                position += val.Length * 2 + 4;

                            }
                            if (prop.PropertyType == typeof(float?))
                            {
                                if (position + 4 < sourceData.Length)
                                {

                                    float? floatVal = BitConverter.ToSingle(sourceData, position);
                                    propertyList[i].SetValue(obj, floatVal, null);
                                    if (_log.IsInfoEnabled && floatVal != null)
                                    {
                                        _log.InfoFormat("Property {0} set to {1}", prop.Name, floatVal.Value.ToString());
                                    }
                                }
                                position += 4;
                            }
                            if (prop.PropertyType == typeof(byte?))
                            {
                                if (_log.IsInfoEnabled)
                                {
                                    _log.InfoFormat("Property {0} set to {1}", prop.Name, sourceData[position].ToString());
                                }
                                propertyList[i].SetValue(obj, sourceData[position], null);
                                position++;

                            }
                            if (prop.PropertyType == typeof(short?))
                            {
                                if (position + 2 < sourceData.Length)
                                {
                                    short? shortVal = BitConverter.ToInt16(sourceData, position);
                                    if (_log.IsInfoEnabled && shortVal != null)
                                    {
                                        _log.InfoFormat("Property {0} set to {1}", prop.Name, shortVal.Value.ToString());
                                    }
                                    propertyList[i].SetValue(obj, shortVal, null);
                                }
                                position += 2;
                            }
                            if (prop.PropertyType == typeof(long?))
                            {
                                if (position + 8 < sourceData.Length)
                                {
                                    long? longVal = BitConverter.ToInt64(sourceData, position);
                                    if (_log.IsInfoEnabled && longVal != null)
                                    {
                                        _log.InfoFormat("Property {0} set to {1}", prop.Name, longVal.Value.ToString());
                                    }

                                    propertyList[i].SetValue(obj, longVal, null);
                                }
                                position += 8;
                            }

                            if (prop.PropertyType == typeof(OrdinanceTypes?))
                            {
                                OrdinanceTypes? OrdVal = (OrdinanceTypes?)sourceData[position++];
                                if (_log.IsInfoEnabled && OrdVal != null)
                                {
                                    _log.InfoFormat("Property {0} set to {1}", prop.Name, OrdVal.Value.ToString());
                                }

                                propertyList[i].SetValue(obj, OrdVal, null);
                                position++;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (_log.IsWarnEnabled)
                            {
                                _log.Warn("Error getting bytes", ex);
                            }
                        }
                    }
                    else
                    {
                        if (_log.IsInfoEnabled)
                        {
                            _log.InfoFormat("Property {0} not included", propertyList[i].Name);
                        }
                    }
                }
            }
           
        }

        public static bool[] ProcessBitFlags(int propertyCount, byte[] byteArray, int index)
        {
            if (byteArray != null)
            {
                List<bool> IncludedList = new List<bool>();


                int k = 1;
                int j = 0;
                int FlagSize = 1;

                if (_log.IsInfoEnabled)
                {
                    _log.InfoFormat("(c)byte {0} value:{1}", FlagSize.ToString(), byteArray[index + j].ToString());
                }
                for (int i = 0; i < propertyCount; i++)
                {



                    //1, 2, 4, 8, 16, 32, 64, 128

                    bool test = ((byteArray[index + j] & k) == k);

                    IncludedList.Add(test);
                    k *= 2;
                    if (k > 128)
                    {
                        k = 1;
                        j++;
                        FlagSize++;

                        if (_log.IsInfoEnabled)
                        {
                            _log.InfoFormat("(d)byte {0} value:{1} ", FlagSize.ToString(), byteArray[index + j].ToString());
                        }
                    }

                }

                return IncludedList.ToArray();
            }
            else
            {
                return null;
            }
        }
        void Initialize(byte[] byteArray, int index)
        {
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--{2} bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray, index), (byteArray.Length - index).ToString()); }

                if (_log.IsInfoEnabled) { _log.InfoFormat("(a)index={0}", index); }
                ID = BitConverter.ToInt32(byteArray, index);
                

                if (_log.IsInfoEnabled)
                {
                    _log.InfoFormat("ID={0}, index={1}", ID.ToString(), index);
                }


                List<PropertyInfo> propertyList = new List<PropertyInfo>();

                Type t = this.GetType();
                PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in properties)
                {
                    if (prop.Name != "ID" && prop.Name != "IncludedFields")
                    {
                        propertyList.Add(prop);
                    }
                }

                if (_log.IsInfoEnabled) { _log.InfoFormat("(b)index={0}", index); }
                IncludedFields = ProcessBitFlags(propertyList.Count, byteArray, index + 4);
                int flagsize = (propertyList.Count - 1)/ 8 + 1;

                int position = index + 4 + flagsize;

                ProcessVariableData(propertyList.ToArray(), this, byteArray, position, IncludedFields);



                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
            
        }
        public VariablePackage(byte[] byteArray)
        {

            Initialize(byteArray, 0);

        }
        public int ID { get; set; }
        public bool[] IncludedFields { get; set; }

        public byte[] GetBytes()
        {
            List<byte> byteArray = new List<byte>();

            byteArray.AddRange(BitConverter.GetBytes(ID));



            List<byte> values = new List<byte>();
            List<PropertyInfo> propertyList = new List<PropertyInfo>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                if (prop.Name != "ID" && prop.Name != "IncludedFields")
                {
                    propertyList.Add(prop);
                }
            }
            byte position = 1;
            byte workByte = 0;
            for (int i = 0; i < IncludedFields.Length; i++)
            {

                if (IncludedFields[i])
                {
                    if (_log.IsInfoEnabled)
                    {
                        _log.InfoFormat(" Property #{0} included", i.ToString());
                    }
                    workByte += position;
                    PropertyInfo prop = propertyList[i];
                    object obj = prop.GetValue(this, null);
                    if (obj != null)
                    {
                        if (prop.PropertyType == typeof(int?))
                        {
                            values.AddRange(BitConverter.GetBytes((int)obj));

                        }
                        if (prop.PropertyType == typeof(ArtemisString))
                        {
                            ArtemisString item = ((ArtemisString)obj);
                            if (item != null)
                            {
                                values.AddRange(item.GetBytes());
                            }
                            else
                            {
                                for (int j = 0; i < 4; i++)
                                {
                                    values.Add(0);
                                }
                            }

                        }
                        if (prop.PropertyType == typeof(float?))
                        {

                            values.AddRange(BitConverter.GetBytes((float)obj));
                        }
                        if (prop.PropertyType == typeof(byte?) || prop.PropertyType == typeof(OrdinanceTypes?))
                        {
                            values.Add((byte)obj);

                        }
                        if (prop.PropertyType == typeof(short?))
                        {
                            values.AddRange(BitConverter.GetBytes((short)obj));
                        }
                        if (prop.PropertyType == typeof(long?))
                        {
                            values.AddRange(BitConverter.GetBytes((long)obj));
                        }
                    }
                    
                }
                position *= 2;
                if (position > 128 || position == 0)
                {
                    byteArray.Add(workByte);
                    if (_log.IsInfoEnabled)
                    {
                        _log.InfoFormat("**Adding Included byte: {0}", workByte.ToString());
                    }
                    position = 1;
                    workByte = 0;
                }
            }
            byteArray.AddRange(values);
            return byteArray.ToArray();
        }
    }
}
