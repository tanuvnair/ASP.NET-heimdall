using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ASP.NET_heimdall
{
    public static class DatabaseHelper
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Laptop"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}