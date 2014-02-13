using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS.Framework.DomainDefinition
{
    public class Group
    {
        public Group(string name)
        {
            Name = name;
            Entities = new List<Entity>();
        }

        public string Name { get; set; }
        public List<Entity> Entities { get; set; }

        public Entity AddEntity(string name)
        {
            Entity entity = new Entity(name);

            return entity;
        }
    }
}
