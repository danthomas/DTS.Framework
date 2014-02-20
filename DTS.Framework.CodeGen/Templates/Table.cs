using System;

namespace DTS.Framework.CodeGen.Templates
{
    public abstract class Table : EntityTemplate
    {
        public override string RelativeFilePath
        {
            get
            {
                return String.Format(@"{0}.SqlServer\Schemas\{1}\Tables\{2}.g.sql",
                    Entity.Group.Domain.Name,
                    Entity.Group.Name,
                    Entity.Name);
            }
        }
    }
}
