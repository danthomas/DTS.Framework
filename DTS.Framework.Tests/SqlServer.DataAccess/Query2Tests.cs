using DTS.SqlServer.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.SqlServer.DataAccess
{
    public partial class QueryTests
    {
        [TestMethod]
        public void Test()
        {
            Query2 query2 = new Query2(_databaseDef);

            query2.AddLine(0, "t");
            
            query2.Lines[0].Text = "ta";

        }
    }
}
