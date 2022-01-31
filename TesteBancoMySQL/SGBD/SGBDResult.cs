using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBancoMySQL.SGBD;

namespace TesteBancoMySQL.SGBD  
{
    class SGBDResult : IEnumerable
    {
        private List<SGBDRow> rows = new List<SGBDRow>();
        public SGBDColumInfo[] ColumsInfos { get; }
        public int RowsCount => rows.Count();
        public int ColumsCount => ColumsInfos.Count();

        public SGBDResult(SGBDColumInfo[] columsInfo)
        {
            this.ColumsInfos = columsInfo;
        }

        public SGBDResult(MySqlDataReader dataReader, bool afterClose = true)
        {
            if (dataReader.IsClosed) return;

            ColumsInfos = new SGBDColumInfo[dataReader.FieldCount];
            for (int i = 0; i < ColumsInfos.Length; i++)
                ColumsInfos[i] = new SGBDColumInfo(dataReader.GetName(i), dataReader.GetFieldType(i));

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    SGBDRow row = new SGBDRow();
                    for (int i = 0; i < ColumsInfos.Length; i++)
                    {
                        row.setValue(ColumsInfos[i].ColumName, new SGBDValue(dataReader.GetValue(i), ColumsInfos[i].ColumType));
                    }
                    rows.Add(row);
                }
            }
            if (afterClose)
                dataReader.Close();
        }

        public void addRow(SGBDRow r)
        {
            if (r.ColumsCount != ColumsCount) return;
            for (int ci = 0; ci < ColumsCount; ci++)
            {
                if (r.getValues()[ci].Key != ColumsInfos[ci].ColumName)
                    return;
            }

            rows.Add(r);
        }

        public SGBDRow getRow(int index)
        {
            if (index < rows.Count)
                return rows[index];
            return null;
        }

        public void removeRowAt(int index) {
            if(index < rows.Count)
                rows.RemoveAt(index);
        }

        public IEnumerator<SGBDRow> GetEnumerator()
        {
            return rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) rows).GetEnumerator();
        }
    }
}
