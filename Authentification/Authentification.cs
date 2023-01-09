using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace WPFAuthorization
{
    internal class Authentification
    {
        private static readonly string connectionString = Properties.Settings.Default.DataConnectionString;

        public static bool IsActivate { get; internal set; } = Properties.Settings.Default.IsActivated;
        private static string Password { get; set; } = Properties.Settings.Default.Password;
        private static string Email { get; set; } = Properties.Settings.Default.Email;


        public static bool ValidateActivation()
        {
            Properties.Settings.Default.Upgrade();
            if (!IsActivate)
            {
                AuthorizationWindow wnd = new AuthorizationWindow();
                wnd.Show();
            }
            return IsActivate;
        }


        public static bool ValidateActivationDataBase(bool isactivatedfield, string loginfield, string passwordfield)
        {
            string queryExpression = $"Select id, IsActivated, Email, Password from Users where IsActivated='{isactivatedfield}' and Email= '{loginfield}' and Password='{passwordfield}'";
            DataBase database = new DataBase();
            using (SqlConnection connection = database.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(queryExpression, connection);
                        DataTable table = new DataTable();

                        adapter.SelectCommand = command;
                        adapter.Fill(table);

                        IsActivate = false;
                        Email = String.Empty;
                        Password = String.Empty;
                        StringBuilder builder = new StringBuilder();
                        foreach (DataRow row in table.Rows)
                        {
                            object[] items = row.ItemArray;
                            if (items.Length == 4)
                            {
                                IsActivate = row.Field<bool>("IsActivated");
                                Password = row.Field<string>("Password");
                                Email = row.Field<string>("Email");

                                builder.AppendLine($"IsActivated: " + IsActivate);
                                builder.AppendLine($"Password: " + Password);
                                builder.AppendLine($"Email: " + Email);

                                Debug.Print(builder.ToString());
                            }
                            builder.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Print("Error: {0}" + ex.ToString());
                    }
                    finally
                    {
                        Properties.Settings.Default.Save();
                    }
                }

                return IsActivate;
            }
        }


        public static object GetActivationData(string queryExpression)
        {
            object resultTask = null;

            string connectionString = "";
            //string connectionString = Properties.Settings.Default.DataConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(queryExpression, connection))
            {
                SqlCommand command = new SqlCommand(queryExpression, connection);

                // Part 3: Use DataAdapter to fill DataTable.
                DataTable tableUsers = new DataTable();
                adapter.Fill(tableUsers);

                // Part 4: render data onto the screen.
                // dataGridView.DataSource = tableUsers;
            }

            return resultTask;
        }


        public static void RegistrationInDataBase(string firstName, string lastName, string email, string serialNumber)
        {
            string queryString = $"insert into Users (FirstName, LastName, Email, SerialNumber) " +
                $"Values ('{firstName}', '{lastName}','{email}', '{serialNumber}')";

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Data filled out incorrectly");
                return;
            }

            DataBase database = new DataBase();
            using (SqlConnection conection = database.GetConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, conection);
                    database.OpenConnection();
                    conection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Data sent successfully. Please expect a message with an attached password " +
                            "within 24 hours from Erkebulan@mail.ru");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error.Data not accepted. Contact support.");
                }
            }
        }


    }
}