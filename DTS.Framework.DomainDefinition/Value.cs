using System;

namespace DTS.Framework.DomainDefinition
{
    public class Value : Property
    {
        private readonly IDataType _dataType;

        internal Value(Entity entity, string name, IDataType dataType, int maxMaxLength, int minLength, byte prec, byte scale, bool isNullable, bool isAuto)
            : base(entity, name, isNullable, isAuto)
        {
            _dataType = dataType;
            MaxLength = maxMaxLength;
            MinLength = minLength;
            Prec = prec;
            Scale = scale;
        }

        public override IDataType DataType { get { return _dataType; } }

        public int MaxLength { get; set; }

        public int MinLength { get; set; }

        public byte Prec { get; set; }

        public byte Scale { get; set; }

        public override string SqlServerType
        {
            get
            {
                string ret;

                if (DataType.HasLength)
                {
                    ret =  String.Format(@"{0}({1})", DataType.SqlServerName, MaxLength);
                }
                else if (DataType.HasPrec && DataType.HasScale)
                {
                    ret =  String.Format(@"{0}({1}, {2})", DataType.SqlServerName, Prec, Scale);
                }
                else if (DataType.HasPrec)
                {
                    ret =  String.Format(@"{0}({1})", DataType.SqlServerName, Prec);
                }
                else if (DataType.HasScale)
                {
                    ret =  String.Format(@"{0}({1})", DataType.SqlServerName, Scale);
                }
                else
                {
                    ret =  DataType.SqlServerName;
                }

                return ret;
            }
        }
    }
}