using System.Text;

namespace DTS.Framework.CodeGen.Templates
{
    public class SchemasGen : Schemas
    {
        public override string Generate()
        {
            StringBuilder stringBuilder = new StringBuilder();


            stringBuilder.Append(@"/*
Generated file do not modify
*/
");
ForEachGroup((group, first) =>
{
if (!first)
{



            stringBuilder.Append(@"
go
");
}


            stringBuilder.Append(@"create schema [");

            stringBuilder.Append(group.Name);

            stringBuilder.Append(@"]");
});


            return stringBuilder.ToString();
        }
    }
}