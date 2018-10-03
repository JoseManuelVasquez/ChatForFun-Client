using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatForFun_Client.Interface;
using ChatForFun_Client.Presenter;

namespace ChatForFun_Client.View
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Page, ILoginView
    {
        /* Presenter Login */
        private ILoginPresenter presenter;

        public Login()
        {
            InitializeComponent();

            /* Presenter */
            presenter = new LoginPresenter(this);
        }

        public void goToChat()
        {
            ChatWindow chatWindow = new ChatWindow(tbUser.Text);
            Window.GetWindow(this).Close();
            chatWindow.Show();
        }

        public void showLoginFail()
        {
            MessageBox.Show("User does not exist.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void goToSignUp(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SignUp());
        }

        private void onClickLogin(object sender, RoutedEventArgs e)
        {
            presenter.authenticateUser(tbUser.Text, tbPassword.Password);
        }
    }
}
