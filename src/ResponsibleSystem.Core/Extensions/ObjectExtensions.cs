using AutoMapper;
using Newtonsoft.Json;

namespace ResponsibleSystem.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Proxy to AutoMapper to simplyfy syntax
        /// </summary>
        /// <typeparam name="T">Destination type</typeparam>
        /// <param name="obj">Object to map from</param>
        /// <returns>New object with type T</returns>
        public static T Map<T>(this object obj)
        {
            return Mapper.Map<T>(obj);
        }

        /// <summary>
        /// Proxy to Newtonsoft.Json serialization feature to simplyfy syntax and setup default values
        /// </summary>
        /// <param name="obj">Object to serizalize</param>
        /// <param name="format">Specifies formatting options</param>
        /// <param name="settings">Specifies the JsonSerializer settings</param>
        /// <returns></returns>
        public static string ToJson(this object obj, Formatting format = Formatting.None, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(obj, format,
                settings ?? new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}