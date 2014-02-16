using System;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    public partial class Class : IEntityTemplate
    {
        public Entity Entity { get; set; }

        public string RelativeFilePath
        {
            get { return String.Format(@"{0}.Domain\{1}\{2}.g.cs", 
                Entity.Group.Domain.Name, 
                Entity.Group.Name, 
                Entity.Name); }
        }
    }
}
