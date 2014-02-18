using DTS.Framework.DomainDefinition;

namespace DTS.Framework.Tests.Common
{
    public class CodeDataType : StringDataType
    {
        public CodeDataType()
            : base("Code", 5, 3)
        {
        }
    }
}