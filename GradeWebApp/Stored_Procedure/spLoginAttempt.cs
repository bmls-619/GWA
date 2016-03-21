using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GradeWebApp.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace GradeWebApp.Stored_Procedure
{
    public class spLoginAttempt
    {
        SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        public void LoginAttempt(string username, string password)
        {
            _connection.Open();

            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Connection = _connection;
            sqlcmd.CommandText = "spLoginAttempts";
            sqlcmd.Parameters.Clear();
            sqlcmd.Parameters.AddWithValue("@Username", username);
            sqlcmd.Parameters.AddWithValue("@Password", password);
            sqlcmd.ExecuteNonQuery();

            _connection.Close();
        }
    }
}