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

namespace ChatForFun_Client.View
{
    /// <summary>
    /// Lógica de interacción para AddFriendWindow.xaml
    /// </summary>
    public partial class AddFriendWindow : Window
    {
        public AddFriendWindow()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }

        private void onClickOk(object sender, System.Windows.RoutedEventArgs e)
        {
            if(!ResponseTextBox.Text.Contains(' '))
                DialogResult = true;
            else
                DialogResult = false;
        }

        private void onClickCancel(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
