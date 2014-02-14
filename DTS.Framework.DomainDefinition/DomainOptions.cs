using System;

namespace DTS.Framework.DomainDefinition
{
    public class DomainOptions
    {
        public DomainOptions()
        {
            AutoPropertyNameFormat = "{0}Id";
            AutoIdProperty = false;
            AutoIdPropertyType = typeof (Int32);
            DefaultLength = 50;
            DefaultPrec = 6;
            DefaultScale = 2;
        }

        public string AutoPropertyNameFormat { get; set; }
        public bool AutoIdProperty { get; set; }
        public Type AutoIdPropertyType { get; set; }
        public short DefaultLength { get; set; }
        public byte DefaultPrec { get; set; }
        public byte DefaultScale { get; set; }
    }
}