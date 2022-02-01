using System.Collections.Generic;
using System.Linq;

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
            //if (row_data.ContainsKey(columName) == false) return;
            row_data[columName] = value;
        }
        public SGBDValue getValue(string columName)
        {
            if (row_data.ContainsKey(columName) == false) return null;
            return row_data[columName];
        }
    }
}
