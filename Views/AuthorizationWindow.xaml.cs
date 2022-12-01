using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;


namespace WPFAuthorization
{

    public partial class AuthorizationWindow : Window
    {
        private static bool ValidateResult { get; set; }

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void SignIN_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                bool isactivatedfield = true;
                string loginfield = LoginField.Text;
                string passwordfield = PasswordField.Password;

                ValidateResult = Authentification.ValidateActivationDataBase(isactivatedfield, loginfield, passwordfield);

                if (ValidateResult)
                {
                    this.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password error. Try again");
                }

            });
        }


        private void Registrationlink_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var window = new RegistrationWindow();
            window.Show();
            this.Close();



        }
    }
}
