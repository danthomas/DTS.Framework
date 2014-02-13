using System;
using System.Collections.Generic;
using System.Linq;

namespace DTS.Framework.DomainDefinition
{
    public class Entity
    {
        public Entity(string name)
        {
            Name = name;
            Properties = new List<Property>();
        }

        public string Name { get; set; }
        public List<Property> Properties { get; set; }

        public Entity AddProperty(string name, Type type)
        {
            Property property = new Property(name, type);

            Properties.Add(property);

            return this;
        }

        public Entity SetIdentifier(string name)
        {
            IdentityProperty = Properties.Single(item => item.Name == name);

            return this;
        }

        public Property IdentityProperty { get; set; }
    }
}
