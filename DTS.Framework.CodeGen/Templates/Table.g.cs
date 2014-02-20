using System.Text;

namespace DTS.Framework.CodeGen.Templates
{
    public class TableGen : Table
    {
        public override string Generate()
        {
            StringBuilder stringBuilder = new StringBuilder();


            stringBuilder.Append(@"create table [");

            stringBuilder.Append(Entity.Group.Name);

            stringBuilder.Append(@"].[");

            stringBuilder.Append(Entity.Name);

            stringBuilder.Append(@"]
(
");
    ForEachProperty((property, first) =>
    {
		

            stringBuilder.Append(first ? "" : @"
, ");

            stringBuilder.Append(@"[");

            stringBuilder.Append(property.Name);

            stringBuilder.Append(@"] ");

            stringBuilder.Append(property.SqlServerType);

            stringBuilder.Append(@" ");

            stringBuilder.Append(property.IsNullable ? "" : "not ");

            stringBuilder.Append(@"null");

            stringBuilder.Append(property.IsAuto ? " identity (1, 1)" : "");

            stringBuilder.Append(property.Default == null ? "" : " default " + property.DefaultSqlServerString);
    });
	

            stringBuilder.Append(@"
, constraint [PrimaryKey_");

            stringBuilder.Append(Entity.Name);

            stringBuilder.Append(@"] primary key ([");

            stringBuilder.Append(Entity.IdentityValue.Name);

            stringBuilder.Append(@"])");
    ForEachReference((reference, first) =>
    {


            stringBuilder.Append(@"
, constraint [ForeignKey_");

            stringBuilder.Append(Entity.Name);

            stringBuilder.Append(@"_");

            stringBuilder.Append(reference.Name);

            stringBuilder.Append(@"_");

            stringBuilder.Append(reference.ReferencedEntity.Name);

            stringBuilder.Append(@"] foreign key ([");

            stringBuilder.Append(reference.Name);

            stringBuilder.Append(@"]) references [");

            stringBuilder.Append(reference.ReferencedEntity.Group.Name);

            stringBuilder.Append(@"].[");

            stringBuilder.Append(reference.ReferencedEntity.Name);

            stringBuilder.Append(@"]([");

            stringBuilder.Append(reference.ReferencedEntity.IdentityValue.Name);

            stringBuilder.Append(@"])");        
    });

    ForEachUnique((unique, first) =>
    {
        

            stringBuilder.Append(@"
, constraint [Unique_");

            stringBuilder.Append(Entity.Name); unique.ForEachUniqueProperty((property, firstProperty) => { 

            stringBuilder.Append(@"_");

            stringBuilder.Append(property.Name); }); 

            stringBuilder.Append(@"] unique ("); unique.ForEachUniqueProperty((property, firstProperty) => { 

            stringBuilder.Append(firstProperty ? "" : ", ");

            stringBuilder.Append(@"[");

            stringBuilder.Append(property.Name);

            stringBuilder.Append(@"]"); }); 

            stringBuilder.Append(@")");
    });

    ForEachValueWhere(item => item.MinLength > 0, (value, first) =>
    {
        

            stringBuilder.Append(@"
, constraint [Check_");

            stringBuilder.Append(Entity.Name);

            stringBuilder.Append(@"_");

            stringBuilder.Append(value.Name);

            stringBuilder.Append(@"_MinLength] check (len([");

            stringBuilder.Append(value.Name);

            stringBuilder.Append(@"]) > ");

            stringBuilder.Append(value.MinLength);

            stringBuilder.Append(@")");
    });


            stringBuilder.Append(@"

)");

            return stringBuilder.ToString();
        }
    }
}