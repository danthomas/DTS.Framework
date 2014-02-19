using System;

namespace DTS.Framework.DomainDefinition
{
    public class DomainOptions
    {
        public DomainOptions()
        {
            DefaultId = DefaultId.No;
            DefaultIdNameFormat = "{0}Id";
            DefaultIdType = typeof(Int32);
        }

        public DefaultId DefaultId { get; set; }

        public string DefaultIdNameFormat { get; set; }

        public Type DefaultIdType { get; set; }
    }
}