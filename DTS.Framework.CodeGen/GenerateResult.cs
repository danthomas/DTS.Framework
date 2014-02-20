namespace DTS.Framework.CodeGen
{
    public class GenerateResult
    {
        public GenerateResult(string relativeFilePath, string text)
        {
            RelativeFilePath = relativeFilePath;
            Text = text;
        }

        public string RelativeFilePath { get; set; }

        public string Text { get; set; }
    }
}