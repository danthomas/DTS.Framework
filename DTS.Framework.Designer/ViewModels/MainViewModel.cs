using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DTS.Framework.CodeGen;
using DTS.Framework.Designer.Services;

namespace DTS.Framework.Designer.ViewModels
{
    public class MainViewModel
    {
        private readonly CodeGenService _codeGenService;
        private readonly DomainDefinitionService _domainDefinitionService;

        public MainViewModel(CodeGenService codeGenService, DomainDefinitionService domainDefinitionService)
        {
            _codeGenService = codeGenService;
            _domainDefinitionService = domainDefinitionService;

            Templates = new ObservableCollection<TemplateType>();
            Entities = new ObservableCollection<Entity>();
            Properties = new ObservableCollection<Property>();

            Refresh();
        }

        private void Refresh()
        {
            _codeGenService.RefreshTemplateTypes(Templates);
            _domainDefinitionService.RefreshEntities(Entities);
        }

        public ObservableCollection<TemplateType> Templates { get; private set; }

        public ObservableCollection<Entity> Entities { get; private set; }

        public ObservableCollection<Property> Properties { get; private set; }

        public Generator Generator { get; private set; }
    }

    public class Property
    {
        public string Name { get; set; }
    }
}
