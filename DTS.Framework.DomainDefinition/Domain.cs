using System;
using System.Collections.Generic;

namespace DTS.Framework.DomainDefinition
{
    public class Domain
    {
        public Domain(string name) 
            : this(name, new DomainOptions())
        {   
        }

        public Domain(string name, DomainOptions domainOptions)
        {
            Name = name;
            DomainOptions = domainOptions;
            Groups = new List<Group>();
            DataTypes = new List<DataType>();
        }

        public string Name { get; set; }

        public DomainOptions DomainOptions { get; private set; }

        public List<DataType> DataTypes { get; set; }
        
        public List<Group> Groups { get; set; }

        public Group AddGroup(string name)
        {
            Group group = new Group(this, name);

            Groups.Add(group);

            return group;
        }

        public override string ToString()
        {
            string ret = Name;

            foreach (Group group in Groups)
            {
                ret += String.Format(@"
  {0}", group.Name);

                foreach (Entity entity in group.Entities)
                {
                    ret += String.Format(@"
    {0}", entity.Name);

                    foreach (Property property in entity.Properties)
                    {
                        ret += String.Format(@"
      {0} {1}", property.Name, property.DataType.Name);

                        if (property.ReferencedEntity != null)
                        {
                            ret += String.Format(" {0}", property.ReferencedEntity.Name);
                        }
                        else if (property.Length > 0)
                        {
                            ret += String.Format(" {0}", property.Length);
                        }
                        else if (property.Length > 0)
                        {
                            ret += String.Format(" {0}", property.Length);
                        }
                    }
                }
            }

            return ret;
        }

        public void AddDefaultDataTypes()
        {
            AddDataType(new BooleanDataType("Boolean"));
            AddDataType(new ByteDataType("Byte"));
            AddDataType(new Int16DataType("Int16"));
            AddDataType(new Int32DataType("Int32"));
            AddDataType(new Int64DataType("Int64"));
            AddDataType(new StringDataType("String"));
            AddDataType(new DateDataType("Date"));
            AddDataType(new TimeSpanDataType("TimeSpan"));
            AddDataType(new DecimalDataType("Decimal"));
        }

        private Domain AddDataType(DataType dataType)
        {
            DataTypes.Add(dataType);

            return this;
        }
    }
}
