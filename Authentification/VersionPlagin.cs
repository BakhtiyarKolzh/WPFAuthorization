using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;

namespace WPFAuthorization
{
    public static class VersionPlagin
    {
        public static string SendVersion(string email)
        {
            string result = null;

            DataBase database = new DataBase();

            Assembly assembly = Assembly.GetExecutingAssembly();

            using (SqlConnection connection = database.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    AssemblyName asemblyName = assembly.GetName();
                    string sql = $"UPDATE Users SET Version = '{asemblyName.Version}' WHERE Email = '{email}'";
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sql, connection);
                        DataTable table = new DataTable();
                        adapter.SelectCommand = command;
                        _ = adapter.Fill(table);
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
