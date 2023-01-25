using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace WPFAuthorization
{
    public static class VersionPlagin
    {
        public static string SendVersion(string email)
        {
            string result = null;

            DataBase database = new DataBase();

            using (SqlConnection connection = database.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    string sql = $"UPDATE Users SET Version = '1.0.0.2' WHERE Email = '{email}'";
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sql, connection);
                        DataTable table = new DataTable();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                    }
                    catch (Exception ex)
                    {
                        Debug.Print("Error Version: " + ex.Message);
                    }
                }
            }
            return result;
        }
    }
}
