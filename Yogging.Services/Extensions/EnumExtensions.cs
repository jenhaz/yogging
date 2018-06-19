using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Yogging.Services.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enu)
        {
            DisplayAttribute attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Name : enu.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("Type {0} is not an enum", type));
            }

            // Get the enum field.
            FieldInfo field = type.GetField(value.ToString());
            return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
        }
    }
}