using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBancoMySQL.SGBD
{
    class SGBDRow
    {
        private Dictionary<string, SGBDValue> row_data = new Dictionary<string, SGBDValue>();
        public int ColumsCount => row_data.Count();

        public SGBDRow()
        {

        }
        public KeyValuePair<string, SGBDValue>[] getValues() {
            return row_data.ToArray();
        }
        public void setValue(string columName, SGBDValue value)
        {
            row_data[columName] = value;
        }
        public SGBDValue getValue(string columName)
        {
            return row_data[columName];
        }
    }
}
