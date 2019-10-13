using System;
using System.Configuration;
using System.Reflection;

namespace ResponsibleSystem.Common.Config
{
    public class AppConfigJobConfigFactory<T> : IConfigFactory<T>
        where T : IConfig
    {
        public T GetConfig()
        {
            var config = (T)Activator.CreateInstance(typeof(T));
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var value = ConfigurationManager.AppSettings[property.Name];
                if (String.IsNullOrWhiteSpace(value))
                {
                    var connectionString = ConfigurationManager.ConnectionStrings[property.Name];
                    if (connectionString != null)
                        value = connectionString.ConnectionString;
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
