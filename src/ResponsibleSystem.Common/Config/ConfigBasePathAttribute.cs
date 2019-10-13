using System;

namespace ResponsibleSystem.Common.Config
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigBasePathAttribute : Attribute
    {
        public string Path { get; set; }

        public ConfigBasePathAttribute(string path = "")
        {
            Path = path;
        }
    }
}
