using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace Demo.DBConnection
{
    public partial class GetConnection
    {
        public static SqlConnection getConnection()
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = ConfigurationManager.ConnectionStrings["DemoDBConnectionString"].ConnectionString;
            return cnn;
        }
    }
}
