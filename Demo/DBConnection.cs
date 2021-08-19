using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Demo
{
    public partial class DBConnection
    {
        public SqlConnection DB_Connection()
        {
            string Connection_String = null;
            SqlConnection cnn;
            Connection_String = ""; //Connection string to database
            cnn = new SqlConnection(Connection_String);
            return cnn;
        }
    }
}
