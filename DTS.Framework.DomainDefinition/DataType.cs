using System;

namespace DTS.Framework.DomainDefinition
{
    public interface IDataType
    {
        string Name { get; }
        string ClrName { get; }
        string CSharpName { get; }
        string SqlServerName { get; }
        Type Type { get; }
        bool HasLength { get; }
        bool HasPrec { get; }
        bool HasScale { get; }
        int MaxLength { get; }
        int MinLength { get; }
        bool IsAuto { get; }
    }

    public abstract class DataType<T> : IDataType
    {
        internal DataType(string name, string clrName, string cSharpName, string sqlServerName, bool hasLength, int maxLength, int minLength, bool hasPrec, byte prec, bool hasScale, byte scale, bool isAuto)
        {
            Name = name;
            ClrName = clrName;
            CSharpName = cSharpName;
            SqlServerName = sqlServerName;
            Type = typeof(T);
            HasLength = hasLength;
            MaxLength = maxLength;
            MinLength = minLength;
            HasPrec = hasPrec;
            Prec = prec;
            HasScale = hasScale;
            Scale = scale;
            IsAuto = isAuto;
        }

        public string Name { get; private set; }

        public string ClrName { get; private set; }

        public string CSharpName { get; private set; }

        public string SqlServerName { get; private set; }

        public Type Type { get; private set; }

        public bool HasLength { get; private set; }

        public bool HasPrec { get; private set; }

        public byte Prec { get; private set; }

        public bool HasScale { get; private set; }

        public byte Scale { get; private set; }

        public bool IsAuto { get; private set; }

        public int MaxLength { get; private set; }

        public int MinLength { get; private set; }
    }

    internal class BooleanDataType : DataType<Boolean>
    {
        internal BooleanDataType()
            : this("Bool")
        {
        }

        public BooleanDataType(string name)
            : base(name, "Boolean", "bool", "bit", false, 0, 0, false, 0, false, 0, false)
        {
        }
    }

    internal class ByteDataType : DataType<Byte>
    {
        internal ByteDataType()
            : this("TinyInt")
        {
        }

        public ByteDataType(string name)
            : base(name, "Byte", "byte", "tinyint", false, 0, 0, false, 0, false, 0, false)
        {
        }
    }

    internal class Int16DataType : DataType<Int16>
    {
        internal Int16DataType()
            : this("SmallInt", false)
        {
        }

        public Int16DataType(string name, bool isAuto)
            : base(name, "Int16", "short", "smallint", false, 0, 0, false, 0, false, 0, isAuto)
        {
        }
    }

    internal class Int32DataType : DataType<Int32>
    {
        internal Int32DataType()
            : this("Int", false)
        {
        }

        public Int32DataType(string name, bool isAuto)
            : base(name, "Int32", "int", "int", false, 0, 0, false, 0, false, 0, isAuto)
        {
        }
    }

    internal class Int64DataType : DataType<Int64>
    {
        internal Int64DataType()
            : this("BigInt", false)
        {
        }

        public Int64DataType(string name, bool isAuto)
            : base(name, "Int64", "long", "big", false, 0, 0, false, 0, false, 0, isAuto)
        {
        }
    }

    public class StringDataType : DataType<String>
    {
        internal StringDataType()
            : this("String", 50, 0)
        {
        }

        public StringDataType(string name, int maxLength, int minLength)
            : base(name, "String", "string", "varchar", true, maxLength, minLength, false, 0, false, 0, false)
        {
        }
    }

    internal class DateDataType : DataType<DateTime>
    {
        internal DateDataType()
            : this("Date")
        {
        }

        public DateDataType(string name)
            : base(name, "DateTime", "DateTime", "date", false, 0, 0, false, 0, false, 0, false)
        {
        }
    }

    internal class DateTimeDataType : DataType<DateTime>
    {
        internal DateTimeDataType()
            : this("DateTime")
        {
        }

        public DateTimeDataType(string name)
            : base(name, "DateTime", "DateTime", "datetime", false, 0, 0, false, 0, false, 0, true)
        {
        }
    }

    internal class TimeSpanDataType : DataType<TimeSpan>
    {
        internal TimeSpanDataType()
            : this("TimeSpan")
        {
        }

        public TimeSpanDataType(string name)
            : base(name, "TimeSpan", "TimeSpan", "time", false, 0, 0, false, 0, false, 0, false)
        {
        }
    }

    internal class DecimalDataType : DataType<Decimal>
    {
        internal DecimalDataType()
            : this("Decimal", 6, 2)
        {
        }

        public DecimalDataType(string name, byte prec, byte scale)
            : base(name, "Decimal", "decimal", "decimal", false, 0, 0, true, prec, true, scale, false)
        {
        }
    }
}
