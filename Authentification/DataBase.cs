using System.Data.SqlClient;

namespace WPFAuthorization
{
    internal class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=192.168.103.45, 1433; 
                                                            Initial Catalog=AuthorizationDB; User Id = Administrator; Password = 123qweQWE");

        public void OpenConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }

    }
}
