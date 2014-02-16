using System;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    partial class Schemas : IDomainTemplate
    {
        public Domain Domain { get; set; }

        public string RelativeFilePath
        {
            get
            {
                return String.Format(@"{0}.SqlServer\Schemas\Schemas.g.sql",
                    Domain.Name);
            }
        }
    }
}
