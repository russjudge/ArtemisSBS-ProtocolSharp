using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class BasePacket : IPackage
    {
        public BasePacket()
        {

        }
        public BasePacket(byte[] byteArray)
        {
            LoadProperties(byteArray, this, 0);

        }
        public BasePacket(byte[] byteArray, int index)
        {
            LoadProperties(byteArray, this, index);

        }

        int LoadProperties(byte[] byteArray, object obj, int index)
        {
            int position = index;
            int retVal = position;
            foreach (PropertyInfo pi in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (pi.PropertyType == typeof(IPackage) || pi.PropertyType.IsSubclassOf(typeof(VariablePackage)))
                {
                    
                    object o2 = null;
                    //TODO: create new object, using bytearray.
                    retVal = LoadProperties(byteArray, o2, position);
                    
                }
                else
                {
                   
                        if (pi.PropertyType.IsArray)
                        {
                            //if (_log.IsInfoEnabled) { _log.InfoFormat("-- property {0} is an array - processing...", pi.Name); }
                            //GetPropertyInformation(, objx, propertyName.PadLeft(depth, '-') + "." + pi.Name, depth + 1);

                            pi.PropertyType.GetElementType();
                            //TODO: load each element in array.
                            

                        }
                        else
                        {
                            //if (_log.IsInfoEnabled) { _log.InfoFormat("-- Adding {0} to list", pi.Name); }
                            string propType = pi.PropertyType.ToString();
                            if (pi.PropertyType.IsEnum)
                            {

                                //TODO: load enum type correctly (enumCast)BitConverter.ToInt32(bytearray, position)
                                propType += "(" + pi.PropertyType.GetEnumUnderlyingType().ToString() + ")";
                            }
                            else
                            {
                                //TODO: (CAST)BitConverter.ToInt32(bytearray, position)
                            }
                            
                        }
                    
                }
            }
            return retVal;
        }

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            //TODO: iterate through properties.
            return retVal.ToArray();
        }
    }
}
