using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Schema;

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
            Uniques = new List<Unique>();
        }

        public Group Group { get; private set; }

        public string Name { get; private set; }

        public string FullName { get { return String.Format("{0}.{1}.{2}", Group.Domain.Name, Group.Name, Name); } }

        public string Plural { get; set; }

        public List<Property> Properties { get; private set; }

        public IEnumerable<Value> Values { get { return Properties.OfType<Value>(); } }

        public IEnumerable<Reference> References { get { return Properties.OfType<Reference>(); } }

        public Entity Value<T>(string name, short maxLength = 0, short minLength = 0, byte prec = 0, byte scale = 0, bool nullable = false, bool isUnique = false, bool isAuto = false)
        {
            return Value(name, Group.Domain.DataTypes.First(item => item.Type == typeof(T)), maxLength, minLength, prec, scale, nullable, isUnique, isAuto);
        }

        public Entity Value(string name, Type type, short maxLength = 0, short minLength = 0, byte prec = 0, byte scale = 0, bool nullable = false, bool isUnique = false, bool isAuto = false)
        {
            return Value(name, Group.Domain.DataTypes.First(item => item.Type == type), maxLength, minLength, prec, scale, nullable, isUnique, isAuto);
        }

        public Entity Value(string name, string dataTypeName, short maxLength = 0, short minLength = 0, byte prec = 0, byte scale = 0, bool nullable = false, bool isUnique = false, bool isAuto = false)
        {
            return Value(name, Group.Domain.DataTypes.First(item => item.Name == dataTypeName), maxLength, minLength, prec, scale, nullable, isUnique, isAuto);
        }

        public Entity Value(string name, IDataType dataType, int maxLength = 0, int minLength = 0, byte prec = 0, byte scale = 0, bool isNullable = false, bool isUnique = false, bool isAuto = false)
        {
            if (dataType.IsAuto)
            {
                isAuto = true;
            }

            if (maxLength == 0 && dataType.MaxLength > 0)
            {
                maxLength = dataType.MaxLength;
            }

            if (minLength == 0 && dataType.MinLength > 0)
            {
                minLength = dataType.MinLength;
            }

            Value value = new Value(this, name, dataType, maxLength, minLength, prec, scale, isNullable, isAuto);

            if (isUnique)
            {
                Unique unique = new Unique(value);

                Uniques.Add(unique);
            }

            Properties.Add(value);

            return this;
        }

        public List<Unique> Uniques { get; set; }

        public Entity Reference(Entity entity, bool nullable = false)
        {
            return Reference(entity.IdentityValue.Name, entity, nullable);
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

        public Entity Unique(params string[] propertyNames)
        {
            Property[] properties = Properties.Where(item => propertyNames.Contains(item.Name)).ToArray();

            if (propertyNames.Length != properties.Length)
            {
                throw new DomainDefinitionException(DomainDefinitionExceptionType.PropertyNotFound, "Failed to find property");
            }

            Uniques.Add(new Unique(properties));

            return this;
        }
    }
}
