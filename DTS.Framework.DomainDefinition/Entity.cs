using System;
using System.Collections.Generic;
using System.Linq;

namespace DTS.Framework.DomainDefinition
{
    public class Entity
    {
        public Entity(Group group, string name)
        {
            Group = group;
            Name = name;
            Properties = new List<Property>();
        }

        public Group Group { get; private set; }

        public string Name { get; private set; }

        public List<Property> Properties { get; private set; }

        public Entity AddProperty<T>(string name)
        {
            return AddProperty(name, typeof (T), 0, 0, 0);
        }

        public Entity AddProperty<T>(string name, short length)
        {
            return AddProperty(name, typeof(T), length, 0, 0);
        }

        public Entity AddProperty<T>(string name, byte prec, byte scale)
        {
            return AddProperty(name, typeof(T), 0, prec, scale);
        }

        public Entity AddProperty(string name, Type type)
        {
            return AddProperty(name, type, 0, 0, 0);
        }

        public Entity AddProperty(string name, Type type, int length)
        {
            return AddProperty(name, type, length, 0, 0);
        }

        public Entity AddProperty(string name, Type type, byte prec, byte scale)
        {
            return AddProperty(name, type, 0, prec, scale);
        }

        public Entity AddProperty(Entity entity)
        {
            return AddProperty(entity.Name, entity);
        }

        public Entity AddProperty(string name, Entity entity)
        {
            Property property = new Property(this, name, entity);

            Properties.Add(property);

            return this;
        }

        public Entity AddProperty(string name, Type type, int length, byte prec, byte scale)
        {
            DataType dataType = Group.Domain.DataTypes.Single(item => item.Type == type);

            Property property = new Property(this, name, dataType, length, prec, scale);

            Properties.Add(property);

            return this;
        }

        public Entity SetIdentifier(string name)
        {
            if (IdentityProperty != null)
            {
                throw new DomainDefinitionException(DomainDefinitionExceptionType.IdentifierAlreadySet, "Identifier already set.");
            }

            Property property = Properties.SingleOrDefault(item => item.Name == name);

            if (property == null)
            {
                throw new DomainDefinitionException(DomainDefinitionExceptionType.PropertyNotFound, "Property {0} not found.", name);
            }

            IdentityProperty = property;

            return this;
        }

        public Property IdentityProperty { get; set; }

        public Entity AddMany(Entity entity)
        {

            return this;
        }
    }
}
