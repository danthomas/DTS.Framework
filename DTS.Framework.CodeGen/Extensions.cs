using System;
using System.Linq;
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

        public static void ForEachValueWhere(this IEntityTemplate entityTemplate, Func<Value, bool> predicate, Action<Value, bool> action)
        {
            bool first = true;
            foreach (Value value in entityTemplate.Entity.Values.Where(predicate))
            {
                action(value, first);
                first = false;
            }
        }

        public static void ForEachReference(this IEntityTemplate entityTemplate, Action<Reference, bool> action)
        {
            bool first = true;
            foreach (Reference reference in entityTemplate.Entity.References)
            {
                action(reference, first);
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

        public static void ForEachUnique(this IEntityTemplate entityTemplate, Action<Unique, bool> action)
        {
            bool first = true;
            foreach (Unique unique in entityTemplate.Entity.Uniques)
            {
                action(unique, first);
                first = false;
            }
        }

        public static void ForEachUniqueProperty(this Unique unique, Action<Property, bool> action)
        {
            bool first = true;
            foreach (Property property in unique.Properties)
            {
                action(property, first);
                first = false;
            }
        }
    }
}
