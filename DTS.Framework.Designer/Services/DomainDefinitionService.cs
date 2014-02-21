using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.Designer.Services
{
    public class CodeDataType : StringDataType
    {
        public CodeDataType()
            : base("Code", 5, 3)
        {
        }
    }

    public class DomainDefinitionService
    {
        public DomainDefinitionService()
        {
            Domain = new Domain("DTS.Manager", new DomainOptions
            {
                DefaultId = DefaultId.Auto,
                DefaultIdType = typeof(Int16)
            })
            .AddDefaultDataTypes()
            .AddDataType(new CodeDataType());

            Group security = Domain.AddGroup("Security");
            Group companies = Domain.AddGroup("Companies");
            Group projects = Domain.AddGroup("Projects");
            Group scheduling = Domain.AddGroup("Scheduling");

            Entity company = companies.AddEntity("Company")
                .Value("Code", "Code", isUnique: true)
                .Value<bool>("IsInternal")
                .Value<string>("Name", 30)
                .Value<bool>("IsActive", @default: true)
                .Value<bool>("IsDeleted");

            Entity user = security.AddEntity("User")
                .Value<string>("Username", 20, 6, isUnique: true)
                .Value<string>("Email", 200, 6, isUnique: true)
                .Value<string>("FirstName", 20)
                .Value<string>("MiddleName", 20)
                .Value<string>("LastName", 30)
                .Value<string>("PreferredName", 20)
                .Value<bool>("IsActive", @default: true)
                .Value<bool>("IsDeleted")
                .Reference(company);

            Entity role = security.AddEntity("Role")
                .Value<string>("Name", 20, 4, isUnique: true)
                .Value<bool>("IsExternal")
                .Value<bool>("IsDeleted");

            Entity userRole = security.AddEntity("UserRole")
                .Reference(user)
                .Reference(role);

            Entity project = projects.AddEntity("Project")
                .Value("Code", "Code", isUnique: true)
                .Value<string>("Name")
                .Value<bool>("IsActive", @default: true)
                .Value<bool>("IsDeleted")
                .Reference(company);

            Entity calendar = scheduling.AddEntity("Calendar")
                .Value<string>("Name")
                .Value<bool>("IsActive", @default: true)
                .Value<bool>("IsDeleted");

            Entity entryType = scheduling.AddEntity<short>("EntryType")
                .Value("Code", "Code", isUnique: true)
                .Value<string>("Name", 20);

            Entity entry = scheduling.AddEntity<int>("Entry")
                .Reference(calendar)
                .Reference(user, true)
                .Value<DateTime>("DateTime")
                .Reference(entryType)
                .Value<short>("Duration");

            foreach (Entity entity in new[] { company, user, role, project, calendar, entry })
            {
                entity.Value<DateTime>("CreatedDateTime", @default: DefaultValue.Now)
                    .Value<DateTime>("CreatedUser", @default: DefaultValue.CurrentUser)
                    .Value<DateTime>("UpdatedDateTime", nullable: true)
                    .Value<DateTime>("UpdatedUser", nullable: true);
            }
        }

        public Domain Domain { get; set; }

        public void RefreshEntities(ObservableCollection<ViewModels.Entity> entities)
        {
            entities.Clear();

            foreach (Entity entity in Domain.Groups.SelectMany(item => item.Entities))
            {
                entities.Add(new ViewModels.Entity
                {
                    Name = entity.Name,
                    GroupName = entity.Group.Name
                });
            }
        }
    }
}
