using System.Data.SqlClient;

namespace WPFAuthorization
{
    internal class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=IASP-158\SQLEXPRESS; 
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
