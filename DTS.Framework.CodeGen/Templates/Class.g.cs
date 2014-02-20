using System.Text;

namespace DTS.Framework.CodeGen.Templates
{
    public class ClassGen : Class
    {
        public override string Generate()
        {
            StringBuilder stringBuilder = new StringBuilder();


            stringBuilder.Append(@"/*
Generated file do not modify
*/
using System;

namespace ");

            stringBuilder.Append(Entity.Group.Domain.Name);

            stringBuilder.Append(@".Domain.");

            stringBuilder.Append(Entity.Group.Name);

            stringBuilder.Append(@"
{
	public class ");

            stringBuilder.Append(Entity.Name);

            stringBuilder.Append(@"
	{");
    ForEachValue((value, first) =>
    {
        

            stringBuilder.Append(@"		
		public ");

            stringBuilder.Append(value.DataType.CSharpName);

            stringBuilder.Append(@" ");

            stringBuilder.Append(value.Name);

            stringBuilder.Append(@" { get; set; }");
    });
	

            stringBuilder.Append(@"
	}
}");

            return stringBuilder.ToString();
        }
    }
}