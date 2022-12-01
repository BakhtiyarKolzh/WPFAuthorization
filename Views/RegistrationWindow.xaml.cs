using System.Windows;
using System.Windows.Threading;

namespace WPFAuthorization
{
    public partial class RegistrationWindow : Window
    {
        readonly RegistrationVM vm;
        public RegistrationWindow()
        {
            InitializeComponent();
            vm = new RegistrationVM();
            DataContext = vm;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                vm.RegistrationCommandHandler();
                this.Hide();
                this.Close();
            });
        }

    }
}
