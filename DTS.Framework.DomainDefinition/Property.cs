namespace DTS.Framework.DomainDefinition
{
    public class Property
    {
        public Property(Entity entity, string name, DataType dataType, int length, byte prec, byte scale)
        {
            Entity = entity;
            Name = name;
            DataType = dataType;
            Length = length;
            Prec = prec;
            Scale = scale;
        }

        public Property(Entity entity, string name, Entity referncedEntity)
        {
            Entity = entity;
            Name = name;
            DataType = referncedEntity.IdentityProperty.DataType;
            ReferencedEntity = referncedEntity;
        }

        public Entity Entity { get; private set; }

        public string Name { get; private set; }

        public DataType DataType { get; private set; }

        public int Length { get; set; }

        public byte Prec { get; set; }
        
        public byte Scale { get; set; }

        public Entity ReferencedEntity { get; private set; }
    }
}