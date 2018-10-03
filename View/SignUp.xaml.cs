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
    /// Lógica de interacción para SignUp.xaml
    /// </summary>
    public partial class SignUp : Page, ISignUpView
    {
        /* Presenter Login */
        private ISignUpPresenter presenter;

        public SignUp()
        {
            InitializeComponent();

            /* Presenter */
            presenter = new SignUpPresenter(this);
        }

        public void goToLogin()
        {
            this.NavigationService.Navigate(new Login());
        }

        public void showSignUpFail()
        {
            MessageBox.Show("User already exists.", "Register failed", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void onClickSignUp(object sender, RoutedEventArgs e)
        {
            presenter.registerUser(tbUser.Text, tbPassword.Password);
        }

        private void goBackToLogin(object sender, RoutedEventArgs e)
        {
            goToLogin();
        }
    }
}
