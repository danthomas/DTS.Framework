using System;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    public partial class Table : IEntityTemplate
    {
        public string RelativeFilePath
        {
            get
            {
                return String.Format(@"{0}.SqlServer\Schemas\{1}\Tables\{2}.g.sql",
                    Entity.Group.Domain.Name,
                    Entity.Group.Name,
                    Entity.Name);
            }
        }

        public Entity Entity { get; set; }
    }
}
