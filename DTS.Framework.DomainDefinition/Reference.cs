namespace DTS.Framework.DomainDefinition
{
    public class Reference : Property
    {
        internal Reference(Entity entity, string name, Entity referencedEntity, bool isNullable)
            : base(entity, name, isNullable, false)
        {
            ReferencedEntity = referencedEntity;
        }

        public Entity ReferencedEntity { get; private set; }

        public override IDataType DataType
        {
            get { return ReferencedEntity.IdentityValue.DataType; }
        }

        public override string SqlServerType
        {
            get { return ReferencedEntity.IdentityValue.SqlServerType; }
        }
    }
}