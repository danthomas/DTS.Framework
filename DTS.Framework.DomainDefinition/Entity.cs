using System;
using System.Collections.Generic;
using System.Linq;

namespace DTS.Framework.DomainDefinition
{
    public class Entity
    {
        internal Entity(Group @group, string name, string plural)
        {
            Group = group;
            Name = name;
            Plural = plural;
            Properties = new List<Property>();
        }

        public Group Group { get; private set; }

        public string Name { get; private set; }

        public string FullName { get { return String.Format("{0}.{1}.{2}", Group.Domain.Name, Group.Name, Name); } }

        public string Plural { get; set; }

        public List<Property> Properties { get; private set; }

        public IEnumerable<Value> Values { get { return Properties.OfType<Value>(); } }

        public Entity Value<T>(string name, bool nullable = false)
        {
            return Value(name, typeof(T), 0, 0, 0, nullable);
        }

        public Entity Value<T>(string name, short length, bool nullable = false)
        {
            return Value(name, typeof(T), length, 0, 0, nullable);
        }

        public Entity Value<T>(string name, byte prec, byte scale, bool nullable = false)
        {
            return Value(name, typeof(T), 0, prec, scale, nullable);
        }

        public Entity Value(string name, Type type, bool nullable = false)
        {
            return Value(name, type, 0, 0, 0, nullable);
        }

        public Entity Value(string name, Type type, int length, bool nullable = false)
        {
            return Value(name, type, length, 0, 0, nullable);
        }

        public Entity Value(string name, Type type, byte prec, byte scale, bool nullable = false)
        {
            return Value(name, type, 0, prec, scale, nullable);
        }

        public Entity Value(string name, Type type, int length, byte prec, byte scale, bool nullable = false)
        {
            DataType dataType = Group.Domain.DataTypes.First(item => item.Type == type);

            if (dataType.HasLength && length == 0)
            {
                length = Group.Domain.DomainOptions.DefaultLength;
            }

            if (dataType.HasPrec && prec == 0)
            {
                prec = Group.Domain.DomainOptions.DefaultPrec;
            }

            if (dataType.HasScale && scale == 0)
            {
                scale = Group.Domain.DomainOptions.DefaultScale;
            }

            Value value = new Value(this, name, dataType, length, prec, scale, nullable);

            Properties.Add(value);

            return this;
        }

        public Entity Reference(Entity entity, bool nullable = false)
        {
            return Reference(entity.Name, entity, nullable);
        }

        public Entity Reference(string name, Entity entity, bool nullable = false)
        {
            Reference reference = new Reference(this, name, entity, nullable);

            Properties.Add(reference);

            return this;
        }

        public Entity SetIdentifier(string name)
        {
            if (IdentityValue != null)
            {
                throw new DomainDefinitionException(DomainDefinitionExceptionType.IdentifierAlreadySet, "Identifier already set.");
            }

            Value value = Properties.OfType<Value>().SingleOrDefault(item => item.Name == name);

            if (value == null)
            {
                throw new DomainDefinitionException(DomainDefinitionExceptionType.PropertyNotFound, "Property {0} not found.", name);
            }

            IdentityValue = value;

            return this;
        }

        public Value IdentityValue { get; set; }
    }
}
