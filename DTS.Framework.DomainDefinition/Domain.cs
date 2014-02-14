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
      {0}", property.Name);
                        if (property is Reference)
                        {
                            ret += String.Format(" {0}", ((Reference)property).ReferencedEntity.Name);
                        }
                        else if (property is Value)
                        {
                            Value value = (Value)property;

                            ret += String.Format(@" {0}", value.DataType.Name);

                            if (value.Length > 0)
                            {
                                ret += String.Format(" {0}", value.Length);
                            }
                            else if (value.Length > 0)
                            {
                                ret += String.Format(" {0}", value.Length);
                            }
                        }
                    }
                }
            }

            return ret;
        }

        public Domain AddDefaultDataTypes()
        {
            AddDataType(new BooleanDataType());
            AddDataType(new ByteDataType());
            AddDataType(new Int16DataType());
            AddDataType(new Int32DataType());
            AddDataType(new Int64DataType());
            AddDataType(new StringDataType());
            AddDataType(new DateDataType());
            AddDataType(new DateTimeDataType());
            AddDataType(new TimeSpanDataType());
            AddDataType(new DecimalDataType());

            return this;
        }

        private Domain AddDataType(DataType dataType)
        {
            DataTypes.Add(dataType);

            return this;
        }
    }
}
