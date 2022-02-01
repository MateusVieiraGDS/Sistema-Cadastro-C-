using System;

namespace TesteBancoMySQL.Exceptions
{
    class SQLConnectionException: Exception
    {
        public SQLConnectionException(string message) : base(message) { }
    }
}
