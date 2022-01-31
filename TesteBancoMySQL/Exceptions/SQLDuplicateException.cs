using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBancoMySQL
{
    class SQLDuplicateException : Exception
    {
        public SQLDuplicateException(string message) : base(message)
        {
        }
    }
}
