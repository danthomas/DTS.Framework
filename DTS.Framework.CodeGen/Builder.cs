using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    public class Builder
    {
        public List<BuildResult> Build()
        {
            List<BuildResult> buildResults = new List<BuildResult>();

            foreach (Type type in GetType()
                .Assembly
                .GetTypes()
                .Where(item => item.IsClass && item.IsAbstract && item.GetInterfaces().Contains(typeof(ITemplate))))
            {
                string templateFilePath = FindTemplateFilePath(type);

                if (templateFilePath != "")
                {
                    buildResults.Add(Build(type, templateFilePath));
                }
            }

            return buildResults;
        }

        public BuildResult Build(Type type, string templateFilePath)
        {
            BuildResult buildResult = new BuildResult { TemplateFilePath = templateFilePath };

            buildResult.TemplateText = File.ReadAllText(buildResult.TemplateFilePath);

            buildResult.GenFilePath = buildResult.TemplateFilePath.Replace(".txt", ".g.cs");

            buildResult.FullTypeName = String.Format(@"DTS.Framework.CodeGen.Templates.{0}Gen", type.Name);

            string[] parts = Regex.Split(buildResult.TemplateText, @"(<#|#>)");

            bool isCode = false;
            string code = String.Format(@"using System.Text;

namespace DTS.Framework.CodeGen.Templates
{{
    public class {0}Gen : {0}
    {{
        public override string Generate()
        {{
            StringBuilder stringBuilder = new StringBuilder();
", type.Name);

            foreach (string part in parts)
            {
                if (part == "<#")
                {
                    isCode = true;
                }
                else if (part == "#>")
                {
                    isCode = false;
                }
                else if (isCode)
                {
                    if (part.StartsWith("="))
                    {
                        code += @"

            stringBuilder.Append(" + part.Substring(1).Trim() + ");";
                    }
                    else
                    {
                        code += part;
                    }
                }
                else
                {
                    if (part != "")
                    {
                        string text = part.Replace(@"""", @"""""");

                        code += @"

            stringBuilder.Append(@""" + text + @""");";
                    }
                }
            }

            code += @"

            return stringBuilder.ToString();
        }
    }
}";

            buildResult.Code = code;

            return buildResult;
        }

        private string FindTemplateFilePath(Type type)
        {
            string templateFilePath = "";

            string directoryPath = Path.GetDirectoryName((new Uri(type.Assembly.CodeBase)).AbsolutePath);

            directoryPath = directoryPath.Replace("\\bin\\Debug", "");

            directoryPath = Directory.GetParent(directoryPath).FullName;

            List<string> filePaths = Directory.GetFiles(directoryPath, "*.txt", SearchOption.AllDirectories).Where(item => item.EndsWith(String.Format("\\{0}.txt", type.Name))).ToList();

            if (filePaths.Count == 1)
            {
                templateFilePath = filePaths[0];
            }

            return templateFilePath;
        }
    }
}
