using System.Windows;


namespace WPFAuthorization
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        // IN PLUGIN
        void StartProgram()
        {
            MessageBox.Show("PLUGIN RUNNING!!!");
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool val = Authentification.ValidateActivation();
            if (val)
            {
                StartProgram();
            }

        }
    }
}
