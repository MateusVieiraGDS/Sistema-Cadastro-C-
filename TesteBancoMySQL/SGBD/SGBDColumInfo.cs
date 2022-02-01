using System;

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
