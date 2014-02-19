using System;
using System.Collections.Generic;
using System.Linq;

namespace DTS.Framework.DomainDefinition
{
    public class Group
    {
        internal Group(Domain domain, string name)
        {
            Domain = domain;
            Name = name;
            Entities = new List<Entity>();
        }

        public Domain Domain { get; private set; }

        public string Name { get; private set; }

        public string FullName { get { return String.Format("{0}.{1}", Domain.Name, Name); } }

        public List<Entity> Entities { get; private set; }

        public Entity AddEntity<T>(string name, string plural = null, DefaultId defaultId = DefaultId.No)
        {
            return AddEntity(typeof(T), name, plural, defaultId);
        }

        public Entity AddEntity(string name, string plural = null)
        {
            return AddEntity(null, name, plural);
        }

        private Entity AddEntity(Type type, string name, string plural = null, DefaultId? defaultId = null)
        {
            if (plural == null)
            {
                plural = GetPlural(name);
            }

            Entity entity = new Entity(this, name, plural);

            defaultId = defaultId ?? Domain.DomainOptions.DefaultId;

            if (defaultId != DefaultId.No)
            {
                AddDefaultIdProperty(defaultId.Value, type, entity);
            }

            Entities.Add(entity);

            return entity;
        }

        private static string GetPlural(string name)
        {
            return name.EndsWith("y")
                ? name.Substring(0, name.Length - 1) + "ies"
                : name + "s";
        }

        private void AddDefaultIdProperty(DefaultId defaultId, Type type, Entity entity)
        {
            string name = String.Format(Domain.DomainOptions.DefaultIdNameFormat, entity.Name);

            type = type ?? Domain.DomainOptions.DefaultIdType;

            IDataType dataType = Domain.DataTypes.FirstOrDefault(item => item.Type == type 
                && item.IsAuto == (defaultId == DefaultId.Auto));

            if (dataType == null)
            {
                throw new DomainDefinitionException(DomainDefinitionExceptionType.DataTypeNotFound, "Failed to find Default Id DataType for {0} on entity {1}", type.Name, entity.Name);
            }

            entity.Value(name, dataType).SetIdentifier(name);
        }
    }
}
