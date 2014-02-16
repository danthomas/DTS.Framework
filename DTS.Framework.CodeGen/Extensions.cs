using System;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    internal static class Extensions
    {
        public static void ForEachGroup(this IDomainTemplate domainTemplate, Action<Group, bool> action)
        {
            bool first = true;
            foreach (Group group in domainTemplate.Domain.Groups)
            {
                action(group, first);
                first = false;
            }
        }

        public static void ForEachValue(this IEntityTemplate entityTemplate, Action<Value, bool> action)
        {
            bool first = true;
            foreach (Value value in entityTemplate.Entity.Values)
            {
                action(value, first);
                first = false;
            }
        }

        public static void ForEachProperty(this IEntityTemplate entityTemplate, Action<Property, bool> action)
        {
            bool first = true;
            foreach (Property property in entityTemplate.Entity.Properties)
            {
                action(property, first);
                first = false;
            }
        }
    }
}
