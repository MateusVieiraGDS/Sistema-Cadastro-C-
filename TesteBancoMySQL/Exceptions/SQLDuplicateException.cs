using System;

namespace TesteBancoMySQL
{
    class SQLDuplicateException : Exception
    {
        public SQLDuplicateException(string message) : base(message)
        {
        }
    }
}
