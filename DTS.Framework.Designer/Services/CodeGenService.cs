using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DTS.Framework.CodeGen;
using DTS.Framework.Designer.ViewModels;

namespace DTS.Framework.Designer.Services
{
    public class CodeGenService
    {
        private readonly Generator _generator;

        public CodeGenService(Generator generator)
        {
            _generator = generator;
        }

        public void RefreshTemplateTypes(ObservableCollection<TemplateType> templates)
        {
            templates.Clear();

            foreach (Type type in _generator.GetTypes())
            {
                templates.Add(new TemplateType { Name = type.Name });
            }
        }
    }
}
