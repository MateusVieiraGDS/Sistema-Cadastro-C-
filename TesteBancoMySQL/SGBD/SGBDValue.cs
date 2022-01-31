using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBancoMySQL.SGBD
{
    class SGBDValue
    {
        public object value { get;}
        public Type type { get;}

        public SGBDValue()
        {
        }

        public SGBDValue(object value, Type type)
        {
            this.value = value;
            this.type = type;
        }
    }
}
