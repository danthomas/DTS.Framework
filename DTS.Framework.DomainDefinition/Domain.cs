using System.Collections.Generic;

namespace DTS.Framework.DomainDefinition
{
    public class Domain
    {
        public Domain(string  name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public Group AddGroup(string name)
        {
            Group group = new Group(name);

            return group;
        }
    }
}
