using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    interface ITemplate
    {
        string TransformText();
        string RelativeFilePath { get; }
    }

    interface IDomainTemplate :  ITemplate
    {
        Domain Domain { get; set; }
    }

    interface IEntityTemplate : ITemplate
    {
        Entity Entity { get; set; }
    }
}
