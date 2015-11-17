using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;

namespace TermCN50Lib
{
    public static partial class CntFCN50
    {
        public static SqlCeConnection TOpen(string pathDb, string password)
        {
            SqlCeConnection conn = null;
            string connectionString = String.Format("Data Source={0};Password ={1};Persist Security Info=True", pathDb, password);
            conn = new SqlCeConnection(connectionString);
            conn.Open();
            return conn;
        }

        public static void TClose(SqlCeConnection conn)
        {
            conn.Close();
        }
    }
}
