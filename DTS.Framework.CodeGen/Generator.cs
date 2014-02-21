using System;
using System.Collections.Generic;
using System.Linq;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    public class Generator
    {
        public List<GenerateResult> Generate(Domain domain)
        {
            List<GenerateResult> genFiles = new List<GenerateResult>();

            foreach (Type type in GetTypes())
            {
                genFiles.AddRange(Generate(domain, type));
            }

            return genFiles;
        }

        public IEnumerable<Type> GetTypes()
        {
            return GetType().Assembly.GetTypes().Where(item => item.IsClass
                                                               && !item.IsAbstract &&
                                                               item.GetInterfaces().Contains(typeof (ITemplate)));
        }

        public List<GenerateResult> Generate(Domain domain, Type type)
        {
            List<GenerateResult> genFiles = new List<GenerateResult>();

            ITemplate template = (ITemplate)Activator.CreateInstance(type);

            if (template is DomainTemplate)
            {
                DomainTemplate domainTemplate = (DomainTemplate)template;
                domainTemplate.Domain = domain;
                genFiles.Add(new GenerateResult(template.RelativeFilePath, template.Generate()));
            }
            else if (template is EntityTemplate)
            {
                foreach (Entity entity in domain.Groups.SelectMany(item => item.Entities))
                {
                    EntityTemplate entityTemplate = (EntityTemplate)template;
                    entityTemplate.Entity = entity;
                    genFiles.Add(new GenerateResult(template.RelativeFilePath, template.Generate()));
                }
            }

            return genFiles;
        }
    }
}
