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

        public static bool IsActivated { get; internal set; } = Properties.Settings.Default.IsActivated;
        private static string Password { get; set; } = Properties.Settings.Default.Password;
        private static string Email { get; set; } = Properties.Settings.Default.Email;
        private static string SerialNumber { get; set; } = Properties.Settings.Default.SerialNumber;

        public static bool ValidateActivation()
        {
            Properties.Settings.Default.Upgrade();
            if (!IsActivated)
            {
                AuthorizationWindow wnd = new AuthorizationWindow();
                wnd.Show();
            }
            return IsActivated;
        }

        public static bool ValidateActivationDataBase(bool isactivatedfield, string loginfield, string passwordfield, string serialNumber)
        {
            string queryExpression = $"Select id, IsActivated, Email, Password, SerialNumber from Users where IsActivated='{isactivatedfield}' and Email= '{loginfield}' and Password='{passwordfield}' and SerialNumber='{serialNumber}'";
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

                        IsActivated = false;
                        Email = String.Empty;
                        Password = String.Empty;
                        SerialNumber = String.Empty;

                        StringBuilder builder = new StringBuilder();
                        foreach (DataRow row in table.Rows)
                        {
                            object[] items = row.ItemArray;
                            if (items.Length == 5)
                            {
                                IsActivated = row.Field<bool>("IsActivated");
                                Password = row.Field<string>("Password");
                                Email = row.Field<string>("Email");
                                SerialNumber = row.Field<string>("SerialNumber");

                                builder.AppendLine($"IsActivated: " + IsActivated);
                                builder.AppendLine($"Password: " + Password);
                                builder.AppendLine($"Email: " + Email);
                                builder.AppendLine($"SerialNumber: " + SerialNumber);

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

                return IsActivated;
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


        public static bool RegistrationInDataBase(string firstName, string lastName, string email, string serialNumber)
        {
            string queryString = $"insert into Users (FirstName, LastName, Email, SerialNumber) " +
                $"Values ('{firstName}', '{lastName}','{email}', '{serialNumber}')";

            bool result = false;
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Data filled out incorrectly");
                return result;
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
                        result = true;
                        MessageBox.Show("Data sent successfully. Please expect a message with an attached password " +
                            "within 24 hours from Erkebulan@mail.ru");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error.Data not accepted. Contact support.");
                }
            }

            return result;
        }


    }
}