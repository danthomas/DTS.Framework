using System;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    public abstract class EntityTemplate : ITemplate
    {
        public Entity Entity { get; set; }

        public abstract string TransformText();

        public abstract string RelativeFilePath { get; }

        public void ForEachProperty(Action<Property, bool> action)
        {
            Extensions.ForEachProperty(this, action);
        }

        public void ForEachValue(Action<Value, bool> action)
        {
            Extensions.ForEachValue(this, action);
        }

        public void ForEachReference(Action<Reference, bool> action)
        {
            Extensions.ForEachReference(this, action);
        }
        
        public void ForEachValueWhere(Func<Value, bool> predicate, Action<Value, bool> action)
        {
            Extensions.ForEachValueWhere(this, predicate, action);
        }

        public void ForEachUnique(Action<Unique, bool> action)
        {
            Extensions.ForEachUnique(this, action);
            
        }
    }
}