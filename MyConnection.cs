using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;



namespace Discount
{
    class MyConnection
    {
        public SqlConnection con;
        public MyConnection()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["tam"].ConnectionString);
        }
        public static string type;
        public static int key;
    }
}
