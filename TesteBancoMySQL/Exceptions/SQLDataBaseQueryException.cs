using System;

namespace TesteBancoMySQL.Exceptions
{
    class SQLDataBaseQueryException : Exception
    {
        public SQLDataBaseQueryException(string message) : base(message)
        {
        }
    }
}
