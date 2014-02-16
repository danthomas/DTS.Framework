using System;

namespace DTS.Framework.DomainDefinition
{
    public abstract class DataType<T>
    {
        protected DataType(string name, string clrName, string cSharpName, string sqlServerName, bool hasLength, bool hasPrec, bool hasScale)
        {
            Name = name;
            ClrName = clrName;
            CSharpName = cSharpName;
            SqlServerName = sqlServerName;
            Type = typeof(T);
            HasLength = hasLength;
            HasPrec = hasPrec;
            HasScale = hasScale;
        }

        public string Name { get; set; }

        public string ClrName { get; set; }

        public string CSharpName { get; set; }
        public string SqlServerName { get; set; }

        public Type Type { get; set; }

        public bool HasLength { get; set; }

        public bool HasPrec { get; set; }

        public bool HasScale { get; set; }
    }

    internal class BooleanDataType : DataType<Boolean>
    {
        internal BooleanDataType()
            : base("Bool", "Boolean", "bool", "bit", false, false, false)
        {
        }

        public BooleanDataType(string name) 
            : base(name, "Boolean", "bool", "bit", false, false, false)
        {
        }
    }

    internal class ByteDataType : DataType<Byte>
    {
        internal ByteDataType()
            : base("TinyInt", "Byte", "byte", "tinyint", false, false, false)
        {
        }

        public ByteDataType(string name)
            : base(name, "Byte", "byte", "tinyint", false, false, false)
        {
        }
    }

    internal class Int16DataType : DataType<Int16>
    {
        internal Int16DataType()
            : base("SmallInt", "Int16", "short", "smallint", false, false, false)
        {
        }

        public Int16DataType(string name)
            : base(name, "Int16", "short", "smallint", false, false, false)
        {
        }
    }

    internal class Int32DataType : DataType<Int32>
    {
        internal Int32DataType()
            : base("Int", "Int32", "int", "int", false, false, false)
        {
        }

        public Int32DataType(string name)
            : base(name, "Int32", "int", "int", false, false, false)
        {
        }
    }

    internal class Int64DataType : DataType<Int64>
    {
        internal Int64DataType()
            : base("BigInt", "Int64", "long", "big", false, false, false)
        {
        }

        public Int64DataType(string name)
            : base(name, "Int64", "long", "big", false, false, false)
        {
        }
    }

    internal class StringDataType : DataType<String>
    {
        internal StringDataType()
            : base("String", "String", "string", "varchar", true, false, false)
        {
        }

        public StringDataType(string name)
            : base(name, "String", "string", "varchar", true, false, false)
        {
        }
    }

    internal class DateDataType : DataType<DateTime>
    {
        internal DateDataType()
            : base("Date", "DateTime", "DateTime", "date", false, false, false)
        {
        }

        public DateDataType(string name)
            : base(name, "DateTime", "DateTime", "date", false, false, false)
        {
        }
    }

    internal class DateTimeDataType : DataType<DateTime>
    {
        internal DateTimeDataType()
            : base("DateTime", "DateTime", "DateTime", "datetime", false, false, false)
        {
        }

        public DateTimeDataType(string name)
            : base(name, "DateTime", "DateTime", "datetime", false, false, false)
        {
        }
    }

    internal class TimeSpanDataType : DataType<TimeSpan>
    {
        internal TimeSpanDataType()
            : base("TimeSpan", "TimeSpan", "TimeSpan", "time", false, false, false)
        {
        }

        public TimeSpanDataType(string name)
            : base(name, "TimeSpan", "TimeSpan", "time", false, false, false)
        {
        }
    }

    internal class DecimalDataType : DataType<Decimal>
    {
        internal DecimalDataType()
            : base("Decimal", "Decimal", "decimal", "decimal", false, true, true)
        {
        }

        public DecimalDataType(string name)
            : base(name, "Decimal", "decimal", "decimal", false, true, true)
        {
        }
    }
}
