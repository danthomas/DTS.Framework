using System;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    internal static class Extensions
    {
        public static void ForEachValueProperty(this IEntityTemplate entityTemplate, Action<Value, bool> action)
        {
            bool first = true;
            foreach (Value value in entityTemplate.Entity.Values)
            {
                action(value, first);
                first = false;
            }
        }
    }
}
