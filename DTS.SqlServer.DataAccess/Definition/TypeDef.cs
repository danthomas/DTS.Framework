namespace DTS.SqlServer.DataAccess.Definition
{
    public class TypeDef
    {
        public TypeDef(int typeId, string name)
        {
            TypeId = typeId;
            Name = name;
        }

        public int TypeId { get; set; }

        public string Name { get; set; }
    }
}