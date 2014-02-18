using System.Collections.Generic;

namespace DTS.Framework.DomainDefinition
{
    public class Unique
    {
        public Unique(params Property[] properties)
        {
            Properties = new List<Property>(properties);
        }

        public List<Property> Properties { get; private set; }
    }
}
