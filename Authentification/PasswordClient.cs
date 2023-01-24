using System;
using System.Data.SqlClient;
using System.Diagnostics;


namespace WPFAuthorization
{
    public static class PasswordClient
    {
        public static string GetPasswordFromDataBase(string email)
        {
            string result = string.Empty;
            DataBase database = new DataBase();
            using (SqlConnection connection = database.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    string sql = $"SELECT Password FROM Users WHERE Email = '{email}'";
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sql, connection);
                        object item = command.ExecuteScalar();
                        if (item is string password)
                        {
                            Debug.Print(password);
                            result = password;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Print("Error: " + ex.Message);
                    }
                }
            }
            return result;
        }
    }
}


































