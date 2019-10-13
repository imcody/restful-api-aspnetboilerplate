using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Attribute = System.Attribute;

namespace ResponsibleSystem.Extensions
{
    public static class TypeExtensions
    {
        public static string FullNameWithAssembly(this Type type)
        {
            return string.Format("{0}, {1}", type.FullName, type.Assembly.GetName().Name);
        }

        public static Type GetEnumType(this Type type)
        {
            if (type.IsEnum)
                return type;

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                   type.GetGenericArguments()[0].IsEnum
                ? type.GetGenericArguments()[0]
                : null;
        }

        public static bool IsNullableEnum(this Type type)
        {
            Type underlyingType = Nullable.GetUnderlyingType(type);
            return (underlyingType != null) && underlyingType.IsEnum;
        }

        public static string GetDescription(this Type type, Func<Type, string> noDescriptionCallback)
        {
            DescriptionAttribute attr =
                Attribute.GetCustomAttribute(type, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attr == null) return noDescriptionCallback(type);
            return attr.Description;
        }

        public static string GetDescription(this Type type)
        {
            return type.GetDescription(t => t.Name);
        }

        public static PropertyInfo GetPropertyInfo(this Type type, string propertyName)
        {
            return type.GetProperties().GetPropertyInfo(propertyName);
        }

        public static PropertyInfo GetPropertyInfo(this PropertyInfo[] properties, string propertyName)
        {
            return properties
                .FirstOrDefault(p =>
                    string.Compare(p.Name, propertyName, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }
}
