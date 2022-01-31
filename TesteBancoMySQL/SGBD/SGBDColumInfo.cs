using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBancoMySQL.SGBD
{
    class SGBDColumInfo
    {
        public string ColumName { get; }
        public Type ColumType { get; }

        public SGBDColumInfo(string columName, Type columType)
        {
            ColumName = columName;
            ColumType = columType;
        }
    }
}
