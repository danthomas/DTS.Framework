namespace DTS.Framework.CodeGen
{
    public class BuildResult
    {
        public string TemplateFilePath { get; set; }

        public string TemplateText { get; set; }

        public string Code { get; set; }

        public string GenFilePath { get; set; }
        
        public string FullTypeName { get; set; }
    }
}