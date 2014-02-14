using System;

namespace DTS.Framework.DomainDefinition
{
    public abstract class DataType
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public bool HasLength { get; set; }
        public bool HasPrec { get; set; }
        public bool HasScale { get; set; }

        protected DataType(string name, Type type, bool hasLength, bool hasPrec, bool hasScale)
        {
            Name = name;
            Type = type;
            HasLength = hasLength;
            HasPrec = hasPrec;
            HasScale = hasScale;
        }
    }

    public class BooleanDataType : DataType
    {
        public BooleanDataType(string name) 
            : base(name, typeof(Boolean), false, false, false)
        {
        }
    }

    public class ByteDataType : DataType
    {
        public ByteDataType(string name)
            : base(name, typeof(Byte), false, false, false)
        {
        }
    }

    public class Int16DataType : DataType
    {
        public Int16DataType(string name)
            : base(name, typeof(Int16), false, false, false)
        {
        }
    }

    public class Int32DataType : DataType
    {
        public Int32DataType(string name)
            : base(name, typeof(Int32), false, false, false)
        {
        }
    }

    public class Int64DataType : DataType
    {
        public Int64DataType(string name)
            : base(name, typeof(Int64), false, false, false)
        {
        }
    }

    public class StringDataType : DataType
    {
        public StringDataType(string name)
            : base(name, typeof(String), true, false, false)
        {
        }
    }

    public class DateDataType : DataType
    {
        public DateDataType(string name)
            : base(name, typeof(DateTime), false, false, false)
        {
        }
    }

    public class TimeSpanDataType : DataType
    {
        public TimeSpanDataType(string name)
            : base(name, typeof(TimeSpan), false, false, false)
        {
        }
    }

    public class DecimalDataType : DataType
    {
        public DecimalDataType(string name)
            : base(name, typeof(Decimal), false, true, true)
        {
        }
    }
}
