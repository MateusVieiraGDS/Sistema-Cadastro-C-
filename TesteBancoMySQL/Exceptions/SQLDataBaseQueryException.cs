using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBancoMySQL.Exceptions
{
    class SQLDataBaseQueryException : Exception
    {
        public SQLDataBaseQueryException(string message) : base(message)
        {
        }
    }
}
