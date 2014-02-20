namespace DTS.Framework.CodeGen
{
    public interface ITemplate
    {
        string TransformText();
        string RelativeFilePath { get; }
    }
}
