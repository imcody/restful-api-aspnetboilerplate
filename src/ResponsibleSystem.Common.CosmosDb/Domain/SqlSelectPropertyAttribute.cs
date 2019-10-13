using System;

namespace ResponsibleSystem.Common.CosmosDb.Domain
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlSelectPropertyAttribute : Attribute
    {
        public string Name { get; set; }
        public bool UseInnerProps { get; set; }

        public SqlSelectPropertyAttribute(string name = "", bool useInnerProps = false)
        {
            Name = name;
            UseInnerProps = useInnerProps;
        }
    }
}