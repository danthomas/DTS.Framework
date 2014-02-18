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

        public Entity AddEntity<T>(string name)
        {
            return AddEntity<T>(name, name + "s");
        }

        public Entity AddEntity<T>(string name, string plural)
        {
            Entity entity = new Entity(this, name, plural);

            AddAutoIdProperty(typeof(T), entity);

            Entities.Add(entity);

            return entity;
        }

        public Entity AddEntity(string name)
        {
            return AddEntity(name, name + "s");
        }

        public Entity AddEntity(string name, string plural)
        {
            Entity entity = new Entity(this, name, plural);

            if (Domain.DomainOptions.AutoIdProperty)
            {
                AddAutoIdProperty(Domain.DomainOptions.AutoIdPropertyType, entity);
            }

            Entities.Add(entity);

            return entity;
        }

        private void AddAutoIdProperty(Type type, Entity entity)
        {
            string name = String.Format(Domain.DomainOptions.AutoPropertyNameFormat, entity.Name);
            IDataType dataType = Domain.DataTypes.First(item => item.Type == type && item.IsAuto);
            entity.Value(name, dataType).SetIdentifier(name);
        }
    }
}
