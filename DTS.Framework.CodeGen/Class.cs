using System;

namespace DTS.Framework.CodeGen
{
    public abstract class Class : EntityTemplate
    {
        public override string RelativeFilePath
        {
            get
            {
                return String.Format(@"{0}.Domain\{1}\{2}.g.cs",
                    Entity.Group.Domain.Name,
                    Entity.Group.Name,
                    Entity.Name);
            }
        }
    }
} ;
