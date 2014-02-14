﻿using System;

namespace DTS.Framework.DomainDefinition
{
    public class DomainOptions
    {
        public DomainOptions()
        {
            AutoPropertyNameFormat = "{0}Id";
            AutoIdProperty = false;
            AutoIdPropertyType = typeof(Int32);
            DefaultStringLength = 50;
        }

        public string AutoPropertyNameFormat { get; set; }
        public bool AutoIdProperty { get; set; }
        public Type AutoIdPropertyType { get; set; }
        public short DefaultStringLength { get; set; }
    }
}