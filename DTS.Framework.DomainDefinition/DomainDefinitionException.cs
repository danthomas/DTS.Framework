using DTS.Utilities;

namespace DTS.Framework.DomainDefinition
{
    public class DomainDefinitionException : ExceptionBase<DomainDefinitionExceptionType>
    {
        public DomainDefinitionException(DomainDefinitionExceptionType domainDefinitionExceptionType, string format, params  object[] args)
            : base(null, domainDefinitionExceptionType, format, args)
        { }
    }
}
