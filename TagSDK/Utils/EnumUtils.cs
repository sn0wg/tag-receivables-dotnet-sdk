using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace TagSDK.Utils
{
    static class EnumUtils
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    EnumMemberAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(EnumMemberAttribute)) as EnumMemberAttribute;
                    if (attr != null)
                    {
                        return attr.Value;
                    }
                }
            }
            return null;
        }
    }
}
