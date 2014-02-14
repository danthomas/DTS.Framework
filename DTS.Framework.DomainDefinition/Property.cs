namespace DTS.Framework.DomainDefinition
{
    public abstract class Property
    {
        protected Property(Entity entity, string name, bool nullable)
        {
            Entity = entity;
            Name = name;
            Nullable = nullable;
        }

        public Entity Entity { get; private set; }

        public string Name { get; private set; }

        public abstract DataType DataType { get; }

        public bool Nullable { get; set; }
    }
}