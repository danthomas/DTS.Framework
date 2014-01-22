using System;
using DTS.Utilities;

namespace DTS.SqlServer.DataAccess
{
    public class DataAccessException : ExceptionBase<DataAccessException.ExceptionType>
    {
        public enum ExceptionType
        {
            AliasAlreadySpecified
        }

        public DataAccessException(ExceptionType identifier, string format, params object[] args) 
            : base(null, identifier, format, args)
        {
        }
        
        public DataAccessException(Exception innerException, ExceptionType identifier, string format, params object[] args)
            : base(innerException, identifier, format, args)
        {
        }
    }
}
