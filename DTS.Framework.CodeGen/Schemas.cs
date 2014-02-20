using System;

namespace DTS.Framework.CodeGen
{
    public abstract class Schemas : DomainTemplate
    {
        public override string RelativeFilePath
        {
            get
            {
                return String.Format(@"{0}.SqlServer\Schemas\Schemas.g.sql", Domain.Name);
            }
        }
    }
}
