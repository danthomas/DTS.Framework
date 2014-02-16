using System;
using System.IO;
using System.Linq;
using DTS.Framework.DomainDefinition;
using DTS.Utilities;

namespace DTS.Framework.CodeGen
{
    public class Generator
    {
        public void Generate(string directoryPath, Domain domain)
        {
            Type iTemplateType = typeof(ITemplate);
            Type iEntityTemplateType = typeof(IEntityTemplate);
            Type iDomainTemplateType = typeof(IDomainTemplate);

            foreach (Type type in GetType().Assembly.GetTypes()
                .Where(item => item.GetInterfaces().Contains(iTemplateType)))
            {
                if (type.GetInterfaces().Contains(iDomainTemplateType))
                {
                    IDomainTemplate domainTemplate = (IDomainTemplate)Activator.CreateInstance(type);
                    domainTemplate.Domain = domain;

                    WriteText(directoryPath, domainTemplate);
                }
                else if (type.GetInterfaces().Contains(iEntityTemplateType))
                {
                    foreach (Entity entity in domain.Groups.SelectMany(item => item.Entities))
                    {
                        IEntityTemplate entityTemplate = (IEntityTemplate)Activator.CreateInstance(type);
                        entityTemplate.Entity = entity;

                        WriteText(directoryPath, entityTemplate);
                    }
                }
            }
        }

        private static void WriteText(string directoryPath, ITemplate template)
        {
            string text = template.TransformText();
            string filePath = Path.Combine(directoryPath, template.RelativeFilePath);
            filePath.CreateDirectories();
            File.WriteAllText(filePath, text);
        }
    }
}
