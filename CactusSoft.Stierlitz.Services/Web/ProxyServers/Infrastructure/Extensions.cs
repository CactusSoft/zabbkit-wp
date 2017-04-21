using System;
using System.ComponentModel;
using System.Reflection;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure
{
    public static class Extensions
    {
        public static string ToDescriptionString(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            
            return value.ToString();
        }
    }
}
