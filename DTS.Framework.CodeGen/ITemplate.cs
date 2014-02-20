namespace DTS.Framework.CodeGen
{
    public interface ITemplate
    {
        string Generate();

        string RelativeFilePath { get; }
    }
}
