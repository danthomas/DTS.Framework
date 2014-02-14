using System;
using System.Collections.Generic;

namespace DTS.Framework.DomainDefinition
{
    public class Group
    {
        public Group(Domain domain, string name)
        {
            Domain = domain;
            Name = name;
            Entities = new List<Entity>();
        }

        public Domain Domain { get; private set; }

        public string Name { get; private set; }

        public List<Entity> Entities { get; private set; }

        public Entity AddEntity<T>(string name) where T : struct
        {
            Entity entity = new Entity(this, name);

            AddAutoIdProperty(typeof(T), entity);

            Entities.Add(entity);

            return entity;
        }

        public Entity AddEntity(string name)
        {
            Entity entity = new Entity(this, name);

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

            entity.AddProperty(name, type).SetIdentifier(name);
        }
    }
}
