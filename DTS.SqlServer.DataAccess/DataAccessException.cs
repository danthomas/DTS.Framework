using System;
using DTS.Utilities;

namespace DTS.SqlServer.DataAccess
{
    public class DataAccessException : ExceptionBase<DataAccessExceptionType>
    {
        public DataAccessException(DataAccessExceptionType identifier, string format, params object[] args) 
            : base(null, identifier, format, args)
        {
        }
        
        public DataAccessException(Exception innerException, DataAccessExceptionType identifier, string format, params object[] args)
            : base(innerException, identifier, format, args)
        {
        }
    }
}
