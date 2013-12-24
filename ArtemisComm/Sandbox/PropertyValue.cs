using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    public class PropertyValue
    {
        public PropertyValue(string propertyName, string value, string objectType, string hexValue)
        {
            PropertyName = propertyName;
            Value = value;
            ObjectType = objectType;
            HexValue = hexValue;
        }
        public string PropertyName { get; private set; }
        public string Value { get; private set; }
        public string ObjectType { get; set; }
        public string HexValue { get; set; }
    }
}
