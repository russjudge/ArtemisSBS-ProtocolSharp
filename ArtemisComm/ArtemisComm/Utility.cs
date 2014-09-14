using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public static void CopyTo(this Stream sourceStream, Stream targetStream, int sourceIndex, long length)
        {
            if (targetStream != null)
            {
                long longlength = length;
                byte[] buffer = GetBuffer(sourceStream, sourceIndex);
                if (buffer.Length < longlength)
                {
                    longlength = buffer.Length;
                }

                int ln = 0;


                //Okay, I'm nuts to do this--but I guess I like to plan for all contingencies.
                do
                {
                    if (longlength < int.MaxValue)
                    {
                        ln = Convert.ToInt32(longlength);
                        longlength = 0;
                    }
                    else
                    {
                        ln = int.MaxValue;
                        longlength -= int.MaxValue;
                    }
                    targetStream.Write(buffer, 0, ln);
                } while (longlength > 0);
            }

        }
        public static MemoryStream GetMemoryStream(this Stream stream, int index)
        {
            MemoryStream retVal = null;
            if (stream != null)
            {
                
                MemoryStream wrkStream = null;
                try
                {
                    wrkStream = new MemoryStream();

                    stream.CopyTo(wrkStream, index, stream.Length - index);
                    retVal = wrkStream;
                    retVal.Position = 0;
                    wrkStream = null;
                }
                finally
                {
                    if (wrkStream != null)
                    {
                        wrkStream.Dispose();
                    }
                }
            }
            return retVal;
        }
        public static byte[] GetBuffer(Stream stream, int index)
        {

            byte[] buffer = null;
            if (stream != null)
            {
                buffer = new byte[stream.Length - index];
                //Setting position to ensure at beginning.
                long currentPosition = 0;


                if (stream.CanSeek)
                {
                    currentPosition = stream.Position;
                    stream.Position = index;

                }

                int bytesRead = 0;
                int totalRead = 0;
                do
                {
                    bytesRead = stream.Read(buffer, totalRead, buffer.Length - totalRead);
                    totalRead += bytesRead;
                } while (bytesRead > 0 && totalRead < buffer.Length);

                if (stream.CanSeek)
                {
                    stream.Position = currentPosition;
                }
            }
            return buffer;
        }

        public static int ToInt32(this Stream me)
        {
            if (me != null)
            {
                byte[] buffer = new byte[4];
              
                me.Read(buffer, 0, 4);
               
                return BitConverter.ToInt32(buffer, 0);
            }
            else
            {
                return 0;
            }
        }
        public static short ToInt16(this Stream me)
        {
            if (me != null)
            {
                byte[] buffer = new byte[2];
                
                me.Read(buffer, 0, 2);
                return BitConverter.ToInt16(buffer, 0);
            }
            else
            {
                return 0;
            }
        }
        public static long ToInt64(this Stream me)
        {
            if (me != null)
            {
                byte[] buffer = new byte[8];

                me.Read(buffer, 0, 8);
                return BitConverter.ToInt64(buffer, 0);
            }
            else
            {
                return 0;
            }
        }
        public static float ToSingle(this Stream me)
        {
            if (me != null)
            {
                byte[] buffer = new byte[4];

                me.Read(buffer, 0, 4);
                return BitConverter.ToSingle(buffer, 0);
            }
            else
            {
                return 0;
            }
        }
        
        public static void Write(this MemoryStream me, Enum enumeration)
        {
            if (enumeration != null && me != null)
            {
                if (Enum.GetUnderlyingType(enumeration.GetType()) == typeof(byte))
                {
                    me.WriteByte(Convert.ToByte(enumeration, CultureInfo.InvariantCulture));
                }
                if (Enum.GetUnderlyingType(enumeration.GetType()) == typeof(short))
                {
                    me.Write(Convert.ToInt16(enumeration, CultureInfo.InvariantCulture));
                }

                if (Enum.GetUnderlyingType(enumeration.GetType()) == typeof(int))
                {
                    me.Write(Convert.ToInt32(enumeration, CultureInfo.InvariantCulture));
                }

                if (Enum.GetUnderlyingType(enumeration.GetType()) == typeof(long))
                {
                    me.Write(Convert.ToInt64(enumeration, CultureInfo.InvariantCulture));
                }
            }
           
        }

        public static void Write(this MemoryStream me, byte? data)
        {
            if (me != null && data != null) me.WriteByte(data.Value);
        }
        public static void Write(this MemoryStream me, short? data)
        {
            if (me != null && data != null) me.Write(BitConverter.GetBytes(data.Value), 0, 2);
        }
        public static void Write(this MemoryStream me, int? data)
        {
            if (me != null && data != null) me.Write(BitConverter.GetBytes(data.Value), 0, 4);
        }
        public static void Write(this MemoryStream me, float? data)
        {
            if (me != null && data != null) me.Write(BitConverter.GetBytes(data.Value), 0, 4);
        }
        public static void Write(this MemoryStream me, long? data)
        {
            if (me != null && data != null) me.Write(BitConverter.GetBytes(data.Value), 0, 8);
        }
        public static void Write(this MemoryStream me, ArtemisString data)
        {
            if (me != null && data != null) me.Write(data.GetRawData().GetBuffer(), 0, data.Length);
        }




        public static MemoryStream SetRawData(this IPackage package)
        {
            MemoryStream stream = null;
            MemoryStream returnStream = null;
            try
            {
                stream = new MemoryStream();
                if (package != null)
                {
                    foreach (PropertyInfo pi in package.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        ArtemisTypeAttribute artyAttrib = null;
                        bool exclude = false;
                        foreach (object a in pi.GetCustomAttributes(false))
                        {
                            if (a.GetType() == typeof(ArtemisExcludedAttribute))
                            {
                                exclude = true;
                            }
                            if (artyAttrib == null)
                            {
                                artyAttrib = a as ArtemisTypeAttribute;
                            }
                        }
                        if (!exclude)
                        {
                            Type castType = pi.PropertyType;
                            Type ArtyType = castType;
                            if (artyAttrib != null)
                            {
                                ArtyType = artyAttrib.ArtemisProtocolType;
                            }
                            if (ArtyType.IsEnum)
                            {
                                ArtyType = ArtyType.GetEnumUnderlyingType();
                            }
                            if (castType == typeof(IPackage))
                            {
                                IPackage pck = (IPackage)pi.GetValue(package, null);
                                pck.GetRawData().WriteTo(stream);
                            }
                            else
                            {
                                if (castType.IsEnum)
                                {
                                    castType = castType.GetEnumUnderlyingType();
                                }
                                
                                object item = null;
                                if (castType == typeof(byte))
                                {
                                    item = (byte)pi.GetValue(package, null);
                                }
                                else if (castType == typeof(bool))
                                {
                                    item = (bool)pi.GetValue(package, null);
                                }
                                else if (castType == typeof(short))
                                {
                                    item = (short)pi.GetValue(package, null);
                                }
                                else if (castType == typeof(int))
                                {
                                    item = (int)pi.GetValue(package, null);
                                }
                                else if (castType == typeof(float))
                                {
                                    item = (float)pi.GetValue(package, null);
                                }
                                else if (castType == typeof(long))
                                {
                                    item = (long)pi.GetValue(package, null);
                                }
                                else if (castType == typeof(ArtemisString))
                                {
                                    item = (ArtemisString)pi.GetValue(package, null);
                                }
                                else if (castType == typeof(string))
                                {
                                    item = (string)pi.GetValue(package, null);
                                }
                                WriteToStream(ArtyType, item, stream);
                            }
                        }
                    }
                }
                returnStream = stream;
                stream = null;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }
            return returnStream;
        }

        static void WriteToStream(Type targetType, object item, MemoryStream stream)
        {
            if (targetType == typeof(byte))
            {
                stream.Write(Convert.ToByte(item, CultureInfo.InvariantCulture));
            }
            if (targetType == typeof(short))
            {
                stream.Write(Convert.ToInt16(item, CultureInfo.InvariantCulture));
            }
            if (targetType == typeof(int))
            {
                stream.Write(Convert.ToInt32(item, CultureInfo.InvariantCulture));
            }
            if (targetType == typeof(float))
            {
                stream.Write(Convert.ToSingle(item, CultureInfo.InvariantCulture));
            }
            if (targetType == typeof(long))
            {
                stream.Write(Convert.ToInt64(item, CultureInfo.InvariantCulture));
            }
            if (targetType == typeof(string))
            {
                byte[] ar = System.Text.ASCIIEncoding.ASCII.GetBytes((string)item);
                stream.Write(ar, 0, ar.Length);
            }
            if (targetType == typeof(ArtemisString))
            {
                ArtemisString artyString = item as ArtemisString;
                stream.Write(artyString);
            }
        }
        public static void DisposeProperties(this IDisposable disposableObject)
        {
            if (disposableObject != null)
            {
                foreach (PropertyInfo prop in disposableObject.GetType().GetProperties())
                {
                    if (prop.PropertyType.GetInterface("IDisposable") != null)
                    {
                        IDisposable dispose = prop.GetValue(disposableObject, null) as IDisposable;
                        if (dispose != null)
                        {
                            dispose.Dispose();
                        }
                        ICollection<IDisposable> collection = prop.GetValue(disposableObject, null) as ICollection<IDisposable>;
                        if (collection != null)
                        {
                            foreach (IDisposable disposer in collection)
                            {
                                if (disposer != null)
                                {
                                    disposer.Dispose();
                                }
                            }
                        }
                    }
                }
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static int LoadProperty(object baseObject, MemoryStream stream, PropertyInfo prop, Collection<Exception> errors)
        {
            int retVal = 0;
            if (stream != null)
            {
                 retVal = Convert.ToInt32(stream.Position);
                try
                {
                    bool exclude = false;
                    ArtemisTypeAttribute artyType = null;
                    if (prop != null)
                    {
                        foreach (object a in prop.GetCustomAttributes(false))
                        {
                            if (a.GetType() == typeof(ArtemisExcludedAttribute))
                            {
                                exclude = true;
                            }
                            if (artyType == null)
                            {
                                artyType = a as ArtemisTypeAttribute;
                            }

                        }

                        if (!exclude)
                        {
                            Type CastType = prop.PropertyType;
                            if (artyType != null)
                            {
                                CastType = artyType.ArtemisProtocolType;
                            }
                            Type toType = prop.PropertyType;

                            if (prop.PropertyType == typeof(IPackage))
                            {
                                object[] parms = { stream, retVal };
                                Type[] constructorSignature = { typeof(MemoryStream), typeof(int) };

                                ConstructorInfo constructor = prop.PropertyType.GetConstructor(constructorSignature);
                                object obj = constructor.Invoke(parms);

                                prop.SetValue(baseObject, obj, null);
                                IPackage pck = obj as IPackage;
                                if (pck != null)
                                {
                                    retVal = Convert.ToInt32(pck.GetRawData().Length);
                                }
                            }
                            else
                            {

                                if (prop.PropertyType.IsEnum)
                                {
                                    toType = prop.PropertyType.GetEnumUnderlyingType();
                                }

                                object intermediateObject = null;
                                if (stream != null)
                                {

                                    KeyValuePair<int, object> item = GetIntermediateType(CastType, retVal, stream);

                                    intermediateObject = item.Value;

                                    retVal = item.Key; //length.

                                    if (CastType == typeof(string))
                                    {
                                        if (retVal < stream.Length)
                                        {
                                            byte[] buffer = new byte[stream.Length - stream.Position];
                                            stream.Read(buffer, 0, Convert.ToInt32(stream.Length - stream.Position));
                                            //WelcomePacket
                                            string itemString = System.Text.ASCIIEncoding.ASCII.GetString(buffer, 0, buffer.Length);
                                            prop.SetValue(baseObject, itemString, null);
                                            retVal = itemString.Length;
                                        }
                                    }


                                    SetProperty(toType, baseObject, intermediateObject, prop);

                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (errors != null)
                    {
                        errors.Add(e);
                    }
                }
            }
            return retVal;
        }

        static KeyValuePair<int, object> GetIntermediateType(Type CastType, int index, Stream stream)
        {
            int retVal = index;
            object intermediateObject = null;
            Type cTp = CastType;
            if (cTp.IsEnum)
            {
             
                
                cTp = Enum.GetUnderlyingType(cTp);
            }
            if (cTp == typeof(byte) || cTp == typeof(byte?))
            {

                if (retVal < stream.Length)
                {
                    intermediateObject = Convert.ToByte(stream.ReadByte());
                    retVal = 1;
                }
            }
            if (cTp == typeof(short) || cTp == typeof(short?))
            {

                if (retVal < stream.Length - 1)
                {
                    intermediateObject = stream.ToInt16();
                    retVal = 2;
                }
            }
            if (cTp == typeof(int) || cTp == typeof(int?))
            {

                if (retVal < stream.Length - 3)
                {

                    intermediateObject = stream.ToInt32();
                    retVal = 4;
                }
            }
            if (cTp == typeof(float) || cTp == typeof(float?))
            {

                if (retVal < stream.Length - 3)
                {

                    intermediateObject = stream.ToSingle();
                    retVal = 4;
                }
            }
            if (cTp == typeof(long) || cTp == typeof(long?))
            {

                if (retVal < stream.Length - 7)
                {

                    intermediateObject = stream.ToInt64();
                    retVal = 8;
                }
            }
            if (CastType == typeof(ArtemisString))
            {
                if (retVal < stream.Length - 3)
                {
                    ArtemisString artyString = new ArtemisString(stream, index);
                    intermediateObject = artyString;
                    retVal = artyString.Length * 2 + 4;
                }
            }
            return new KeyValuePair<int,object>(retVal, intermediateObject);
        }
        static void SetProperty(Type toType, object baseObject, object intermediateObject, PropertyInfo prop)
        {
            if (toType == typeof(string))
            {
                prop.SetValue(baseObject, intermediateObject as string, null);
            }
            if (toType == typeof(ArtemisString))
            {
                prop.SetValue(baseObject, intermediateObject as ArtemisString, null);
            }
            if (toType == typeof(bool) || toType == typeof(bool?))
            {
                prop.SetValue(baseObject, Convert.ToBoolean(intermediateObject, CultureInfo.InvariantCulture), null);
            }
            if (toType == typeof(byte) || toType == typeof(byte?))
            {
                if (prop.PropertyType.IsEnum)
                {
                    prop.SetValue(baseObject, Enum.ToObject(prop.PropertyType, Convert.ToByte(intermediateObject, CultureInfo.InvariantCulture)), null);
                }
                else
                {
                    prop.SetValue(baseObject, Convert.ToByte(intermediateObject, CultureInfo.InvariantCulture), null);
                }
            }
            if (toType == typeof(short) || toType == typeof(short?))
            {
                if (prop.PropertyType.IsEnum)
                {
                    prop.SetValue(baseObject, Enum.ToObject(prop.PropertyType, Convert.ToInt16(intermediateObject, CultureInfo.InvariantCulture)), null);
                }
                else
                {
                    prop.SetValue(baseObject, Convert.ToInt16(intermediateObject, CultureInfo.InvariantCulture), null);
                }
            }
            if (toType == typeof(int) || toType == typeof(int?))
            {
                if (prop.PropertyType.IsEnum)
                {
                    prop.SetValue(baseObject, Enum.ToObject(prop.PropertyType, Convert.ToInt32(intermediateObject, CultureInfo.InvariantCulture)), null);
                }
                else
                {
                    prop.SetValue(baseObject, Convert.ToInt32(intermediateObject, CultureInfo.InvariantCulture), null);
                }
            }
            if (toType == typeof(float) || toType == typeof(float?))
            {
                prop.SetValue(baseObject, Convert.ToSingle(intermediateObject, CultureInfo.InvariantCulture), null);
            }
            if (toType == typeof(long) || toType == typeof(long?))
            {
                if (prop.PropertyType.IsEnum)
                {
                    prop.SetValue(baseObject, Enum.ToObject(prop.PropertyType, Convert.ToInt64(intermediateObject, CultureInfo.InvariantCulture)), null);
                }
                else
                {
                    prop.SetValue(baseObject, Convert.ToInt64(intermediateObject, CultureInfo.InvariantCulture), null);
                }
            }
        }
        public static int LoadProperties(object baseObject, Stream stream, int index, Collection<Exception> errors)
        {

            int retVal = 0;
            if (baseObject == null || stream == null || errors == null)
            {

            }
            else
            {
                if (index < stream.Length)
                {

                    using (MemoryStream RawData = stream.GetMemoryStream(index))
                    {

                        RawData.Position = 0;
                        foreach (PropertyInfo prop in baseObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            //@@@@@@@
                            bool exclude = false;
                            foreach (object a in prop.GetCustomAttributes(false))
                            {
                                if (a.GetType() == typeof(ArtemisExcludedAttribute))
                                {
                                    exclude = true;
                                    break;
                                }
                               

                            }
                            if (!exclude)
                            {
                                retVal += LoadProperty(baseObject, RawData, prop, errors);
                            }
                            long i = RawData.Position;
                        }
                        if (stream.CanSeek)
                        {
                            stream.Position = retVal + index;
                        }
                    }

                }
            }
            return retVal;
        }
    }
}
