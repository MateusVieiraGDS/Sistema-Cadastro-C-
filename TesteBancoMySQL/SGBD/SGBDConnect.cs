using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace TesteBancoMySQL.SGBD
{
   class SGBDConnect : IDisposable
    {
        private MySqlConnection SGBDCONN;
        private bool disposedValue;

        public ConnectionState State => SGBDCONN.State;

        public SGBDConnect(string connectionString)
        {
            SGBDCONN = new MySqlConnection();
            SGBDCONN.ConnectionString = connectionString;
        }
        public bool OpenConnection()
        {
            try
            {
                SGBDCONN.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public void CloseConnection() {
            SGBDCONN.Close();
        }
        public SGBDResult ExecuteQuery(String queryStr) {
            if (SGBDCONN.State != ConnectionState.Open) return null;

            using (MySqlCommand query = new MySqlCommand(queryStr, SGBDCONN))
            using (MySqlDataReader reader = query.ExecuteReader())
            {
                SGBDResult result = new SGBDResult(reader);

                //Console.WriteLine(reader.GetDataTypeName(0));
                //Console.WriteLine(reader.GetFieldType(0));
                //Console.WriteLine(reader.GetName(0));
                return result;
            }                        
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    SGBDCONN.Close();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
