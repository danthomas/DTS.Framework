namespace DTS.Framework.DomainDefinition
{
    public class Reference : Property
    {
        internal Reference(Entity entity, string name, Entity referencedEntity, bool nullable)
            : base(entity, name, nullable)
        {
            ReferencedEntity = referencedEntity;
        }

        public Entity ReferencedEntity { get; private set; }

        public override DataType DataType
        {
            get { return ReferencedEntity.IdentityValue.DataType; }
        }
    }
}