using System;

namespace DTS.Framework.DomainDefinition
{
    public abstract class Property
    {
        protected Property(Entity entity, string name, bool isNullable, bool isAuto, object @default)
        {
            Entity = entity;
            Name = name;
            IsNullable = isNullable;
            IsAuto = isAuto;
            Default = @default;
        }

        public Entity Entity { get; private set; }

        public string Name { get; private set; }

        public abstract IDataType DataType { get; }

        public bool IsNullable { get; set; }

        public bool IsAuto { get; set; }

        public object Default { get; set; }

        public abstract string SqlServerType { get; }

        public string DefaultSqlServerString
        {
            get
            {
                string ret = "";

                if (Default != null)
                {
                    if (Default.Equals(DefaultValue.Now) || Default.Equals(DefaultValue.Today))
                    {
                        ret = "getutcdate()";
                    }
                    else if (Default.Equals(DefaultValue.CurrentUser))
                    {
                        ret = "suser_sname()";
                    } 
                    else if (DataType.SqlServerName == "bit" && Default is bool)
                    {
                        bool @default = (bool)Default;

                        ret = @default ? "1" : "0";
                    }
                    else
                    {
                        throw new DomainDefinitionException(DomainDefinitionExceptionType.DefaultNotRecognised, "Default {0} not recognised", Default);
                    }
                }

                return ret;
            }
        }
    }
}