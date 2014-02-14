using System;

namespace DTS.Framework.DomainDefinition
{
    public abstract class DataType
    {
        protected DataType(string name, string clrName, string cSharpName, Type type, bool hasLength, bool hasPrec, bool hasScale)
        {
            Name = name;
            ClrName = clrName;
            CSharpName = cSharpName;
            Type = type;
            HasLength = hasLength;
            HasPrec = hasPrec;
            HasScale = hasScale;
        }

        public string Name { get; set; }

        public string ClrName { get; set; }

        public string CSharpName { get; set; }

        public Type Type { get; set; }

        public bool HasLength { get; set; }

        public bool HasPrec { get; set; }

        public bool HasScale { get; set; }
    }

    internal class BooleanDataType : DataType
    {
        internal BooleanDataType()
            : base("Bool", "Boolean", "bool", typeof(Boolean), false, false, false)
        {
        }

        public BooleanDataType(string name) 
            : base(name, "Boolean", "bool", typeof(Boolean), false, false, false)
        {
        }
    }

    internal class ByteDataType : DataType
    {
        internal ByteDataType()
            : base("TinyInt", "Byte", "byte", typeof(Byte), false, false, false)
        {
        }

        public ByteDataType(string name)
            : base(name, "Byte", "byte", typeof(Byte), false, false, false)
        {
        }
    }

    internal class Int16DataType : DataType
    {
        internal Int16DataType()
            : base("SmallInt", "Int16", "short", typeof(Int16), false, false, false)
        {
        }

        public Int16DataType(string name)
            : base(name, "Int16", "short", typeof(Int16), false, false, false)
        {
        }
    }

    internal class Int32DataType : DataType
    {
        internal Int32DataType()
            : base("Int", "Int32", "int", typeof(Int32), false, false, false)
        {
        }

        public Int32DataType(string name)
            : base(name, "Int32", "int", typeof(Int32), false, false, false)
        {
        }
    }

    internal class Int64DataType : DataType
    {
        internal Int64DataType()
            : base("BigInt", "Int64", "long", typeof(Int64), false, false, false)
        {
        }

        public Int64DataType(string name)
            : base(name, "Int64", "long", typeof(Int64), false, false, false)
        {
        }
    }

    internal class StringDataType : DataType
    {
        internal StringDataType()
            : base("String", "String", "string", typeof(String), true, false, false)
        {
        }

        public StringDataType(string name)
            : base(name, "String", "string", typeof(String), true, false, false)
        {
        }
    }

    internal class DateDataType : DataType
    {
        internal DateDataType()
            : base("Date", "DateTime", "DateTime", typeof(DateTime), false, false, false)
        {
        }

        public DateDataType(string name)
            : base(name, "DateTime", "DateTime", typeof(DateTime), false, false, false)
        {
        }
    }

    internal class DateTimeDataType : DataType
    {
        internal DateTimeDataType()
            : base("DateTime", "DateTime", "DateTime", typeof(DateTime), false, false, false)
        {
        }

        public DateTimeDataType(string name)
            : base(name, "DateTime", "DateTime", typeof(DateTime), false, false, false)
        {
        }
    }

    internal class TimeSpanDataType : DataType
    {
        internal TimeSpanDataType()
            : base("TimeSpan", "TimeSpan", "TimeSpan", typeof(TimeSpan), false, false, false)
        {
        }

        public TimeSpanDataType(string name)
            : base(name, "TimeSpan", "TimeSpan", typeof(TimeSpan), false, false, false)
        {
        }
    }

    internal class DecimalDataType : DataType
    {
        internal DecimalDataType()
            : base("Decimal", "Decimal", "decimal", typeof(Decimal), false, true, true)
        {
        }

        public DecimalDataType(string name)
            : base(name, "Decimal", "decimal", typeof(Decimal), false, true, true)
        {
        }
    }
}
