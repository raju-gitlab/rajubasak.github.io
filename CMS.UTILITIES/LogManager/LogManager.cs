using ApplicationTools.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.UTILITIES.LogManager
{
    public class LogManager
    {
        public static void ErrorEntry(Exception ex)
        {
            string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
            string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
            string query = QueryConfig.BookQuerySettings[""].ToString();
            using(SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand(query,con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@", ex.Source);
                    cmd.Parameters.AddWithValue("@", ex.StackTrace);
                    cmd.Parameters.AddWithValue("@", ex.Message);
                    int i = cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
