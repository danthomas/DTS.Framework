namespace DTS.Framework.DomainDefinition
{
    public class Value : Property
    {
        private readonly DataType _dataType;

        internal Value(Entity entity, string name, DataType dataType, int length, byte prec, byte scale, bool nullable)
            : base(entity, name, nullable)
        {
            _dataType = dataType;
            Length = length;
            Prec = prec;
            Scale = scale;
            Nullable = nullable;
        }

        public override DataType DataType { get { return _dataType; } }

        public int Length { get; set; }

        public byte Prec { get; set; }

        public byte Scale { get; set; }
    }
}