namespace DTS.SqlServer.DataAccess.Definition
{
    public class TypeDef
    {
        private string _name;

        public TypeDef(int typeId, string name)
        {
            TypeId = typeId;
            Name = name;
        }

        public int TypeId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}