using System;

namespace DTS.Framework.DomainDefinition
{
    public abstract class Property
    {
        protected Property(Entity entity, string name, bool isNullable, bool isAuto)
        {
            Entity = entity;
            Name = name;
            IsNullable = isNullable;
            IsAuto = isAuto;
        }

        public Entity Entity { get; private set; }

        public string Name { get; private set; }

        public abstract IDataType DataType { get; }

        public bool IsNullable { get; set; }

        public bool IsAuto { get; set; }

        public abstract string SqlServerType { get; }
    }
}