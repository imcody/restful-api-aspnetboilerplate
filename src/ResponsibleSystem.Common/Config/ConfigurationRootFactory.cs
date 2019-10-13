using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace ResponsibleSystem.Common.Config
{
    public class ConfigurationRootFactory<T> : IConfigFactory<T>
    where T : class, IConfig, new()
    {
        private readonly IConfigurationRoot _config;

        public ConfigurationRootFactory(IConfigurationRoot config)
        {
            _config = config;
        }

        public T GetConfig()
        {
            var config = (T)Activator.CreateInstance(typeof(T));

            var basePath =
                typeof(T)
                .GetCustomAttribute<ConfigBasePathAttribute>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var propertyKey = string.IsNullOrWhiteSpace(basePath?.Path) 
                    ? property.Name 
                    : $"{basePath?.Path}:{property.Name}";

                var value = _config[propertyKey];
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = _config.GetConnectionString(property.Name);
                }

                if (!String.IsNullOrWhiteSpace(value))
                {
                    switch (property.PropertyType.FullName)
                    {
                        case "System.Boolean":
                            property.SetValue(config, bool.Parse(value));
                            break;

                        case "System.Int32":
                            property.SetValue(config, int.Parse(value));
                            break;

                        case "System.Int64":
                            property.SetValue(config, long.Parse(value));
                            break;

                        case "System.Single":
                            property.SetValue(config, float.Parse(value));
                            break;

                        case "System.Double":
                            property.SetValue(config, double.Parse(value));
                            break;

                        case "System.Decimal":
                            property.SetValue(config, decimal.Parse(value));
                            break;

                        default:
                            property.SetValue(config, value);
                            break;
                    }
                }
            }
            return config;
        }

    }
}
