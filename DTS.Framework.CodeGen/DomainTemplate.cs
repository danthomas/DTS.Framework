using System;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    public abstract class DomainTemplate : ITemplate
    {
        public Domain Domain { get; set; }

        public abstract string Generate();

        public abstract string RelativeFilePath { get; }

        public void ForEachGroup(Action<Group, bool> action)
        {
            Extensions.ForEachGroup(this, action);
        }

        public void ForEachEntity(Action<Entity, bool> action)
        {
            Extensions.ForEachEntity(this, action);
        }
    }
}