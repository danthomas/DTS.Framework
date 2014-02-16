using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    interface ITemplate
    {
        string TransformText();
        string RelativeFilePath { get; }
    }

    interface IEntityTemplate : ITemplate
    {
        Entity Entity { get; set; }
    }
}
