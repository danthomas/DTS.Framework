using System;

namespace DTS.Framework.DomainDefinition
{
    public class Property
    {
        public Property(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }
        public Type Type { get; set; }
    }
}