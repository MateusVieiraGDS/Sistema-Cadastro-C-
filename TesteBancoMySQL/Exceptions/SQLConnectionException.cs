using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBancoMySQL.Exceptions
{
    class SQLConnectionException: Exception
    {
        public SQLConnectionException(string message) : base(message) { }
    }
}
