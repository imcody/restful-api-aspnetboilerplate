using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ResponsibleSystem.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string ToLower(this Enum obj)
        {
            return obj.ToString().ToLower();
        }

        /// <summary>
        ///    Gets the value of an Enum given the description string.   If no match is found, throw an exception.
        /// </summary>
        /// <typeparam name="T">Type of attribute</typeparam>
        /// <param name="description">Description used to match value.</param>
        /// <param name="type">Type of attribute</param>
        /// <returns>Enum value with matching description for type T</returns>
        public static T? GetValueFromDescription<T>(string description, Type type = null) where T: struct
        {
            type = type ?? typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return null;
        }

        /// <summary>
        ///    Given a particular enum value, get the description attribute (if one exists, or return the value as itself if not.
        /// </summary>
        /// <param name="value">Value of enum</param>
        /// <returns>Description (if exists) or value as string (if not)</returns>
        public static string GetDescriptionFromValue(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                return null;
            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static string ToStringWithEnumDescription(this object value)
        {
            var type = value.GetType();
            if (type.IsEnum || type.IsNullableEnum())
                return ((Enum)value).GetDescriptionFromValue();

            return value.ToString();
        }

        public static Dictionary<int, string> GetEnumDictionary(this Type type)
        {
            if (!(type.IsEnum || type.IsNullableEnum()))
                return null;

            var values = Enum.GetValues(type);
            var result = values.Cast<object>().ToDictionary(
                value => (int)value,
                value => ((Enum)value).GetDescriptionFromValue());

            return result;
        }
    }
}
