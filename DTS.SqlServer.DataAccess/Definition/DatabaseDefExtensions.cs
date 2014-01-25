namespace DTS.SqlServer.DataAccess.Definition
{
    public static class DatabaseDefExtensions
    {
        public static Query CreateQuery(this DatabaseDef thisDatabaseDef)
        {
            return new Query(thisDatabaseDef);
        }
    }
}