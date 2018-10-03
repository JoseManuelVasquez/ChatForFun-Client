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
using System.Windows.Shapes;
using ChatForFun_Client.Interface;
using ChatForFun_Client.Presenter;

namespace ChatForFun_Client.View
{
    /// <summary>
    /// Lógica de interacción para ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window, IChatView
    {
        /* Presenter Login */
        private IChatPresenter presenter;

        /* User */
        String user;

        public ChatWindow(String user)
        {
            InitializeComponent();

            /* Presenter */
            presenter = new ChatPresenter(this);
            presenter.receiveCommand();

            tbGreeting.Text = "Hi " + user + "!";
            this.user = user;
        }

        private StackPanel createMessage(string username, string message, string date)
        {
            StackPanel messageSP = new StackPanel();
            messageSP.Orientation = Orientation.Horizontal;
            messageSP.Height = 20;
            messageSP.Margin = new Thickness(0, 0, 0, 10);
            messageSP.HorizontalAlignment = HorizontalAlignment.Stretch;

            /* Username TextBlock */
            TextBlock tbName = new TextBlock();
            tbName.Text = "[" + date + "] " + username + ": ";
            tbName.FontFamily = new FontFamily("Segoi UI");
            tbName.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
            tbName.TextWrapping = TextWrapping.Wrap;
            messageSP.Children.Add(tbName);

            /* Message TextBlock */
            TextBlock tbMsg = new TextBlock();
            tbMsg.Text = message;
            tbMsg.FontFamily = new FontFamily("Segoi UI");
            tbMsg.Foreground = new SolidColorBrush(Colors.White);
            tbMsg.TextWrapping = TextWrapping.Wrap;
            messageSP.Children.Add(tbMsg);

            return messageSP;
        }

        private void createFriendTab(string friendName)
        {
            /* Header */
            TabItem tabItem = new TabItem();

            StackPanel itemPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            TextBlock tbItem = new TextBlock() { Text = friendName };
            Image imageItem = new Image()
            {
                Name = friendName + "Img",
                Source = new BitmapImage(new Uri("/ChatForFun-Client;Component/resources/close.png", UriKind.Relative)),
                Width = 15,
                Height = 13,
                Margin = new Thickness(3, 2, 0, 0)
            };
            RegisterName(imageItem.Name, imageItem);
            imageItem.MouseEnter += onMouseEnterClose;
            imageItem.MouseLeave += onMouseLeaveClose;
            imageItem.MouseDown += onMouseDownClose;
            itemPanel.Children.Add(tbItem);
            itemPanel.Children.Add(imageItem);

            tabItem.Header = itemPanel;
            tabItem.Name = friendName + "Tab";
            RegisterName(tabItem.Name, tabItem);

            /* Our content in tabItem */
            Grid gridItem = new Grid();

            /* Row definition */
            RowDefinition gridRow1 = new RowDefinition();
            RowDefinition gridRow2 = new RowDefinition();
            gridRow1.Height = new GridLength(1, GridUnitType.Star);
            gridRow2.Height = new GridLength(50, GridUnitType.Pixel);
            gridItem.RowDefinitions.Add(gridRow1);
            gridItem.RowDefinitions.Add(gridRow2);

            /* Message Area */
            ScrollViewer scrollMessage = new ScrollViewer() { VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
            StackPanel panelMessage = new StackPanel() { Margin = new Thickness(15, 10, 15, 10), Name = friendName + "Message" };
            RegisterName(panelMessage.Name, panelMessage);
            scrollMessage.Content = panelMessage;
            Grid.SetRow(scrollMessage, 0);
            gridItem.Children.Add(scrollMessage);

            /* Writing Message Area */
            TextBox textBox = new TextBox();
            textBox.TextWrapping = TextWrapping.Wrap;
            textBox.Text = "Send a message";
            textBox.Padding = new Thickness(10, 10, 10, 10);
            textBox.Margin = new Thickness(15, 0, 15, 10);
            textBox.Name = friendName;
            RegisterName(textBox.Name, textBox);
            textBox.GotFocus += onFocusMsg;
            textBox.LostFocus += onLostFocusMsg;
            textBox.KeyDown += onKeyDownEnterMsg;
            Grid.SetRow(textBox, 1);
            gridItem.Children.Add(textBox);

            tabItem.Content = gridItem;
            messageTabControl.Items.Add(tabItem);
            messageTabControl.SelectedValue = tabItem;
        }

        private void addFriendView(string name)
        {
            Grid friendGrid = new Grid();
            friendGrid.Name = name + "Grid";
            RegisterName(friendGrid.Name, friendGrid);
            friendGrid.Height = 30;
            friendGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            friendGrid.MouseEnter += onMouseEnterFriendArea;
            friendGrid.MouseLeave += onMouseLeaveFriendArea;

            /* Columns definition */
            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();
            ColumnDefinition gridCol3 = new ColumnDefinition();
            ColumnDefinition gridCol4 = new ColumnDefinition();
            friendGrid.ColumnDefinitions.Add(gridCol1);
            friendGrid.ColumnDefinitions.Add(gridCol2);
            friendGrid.ColumnDefinitions.Add(gridCol3);
            friendGrid.ColumnDefinitions.Add(gridCol4);

            /* Friend's image */
            Image ratFriend = new Image();
            ratFriend.Source = new BitmapImage(new Uri("/ChatForFun-Client;Component/resources/rat.png", UriKind.Relative));
            ratFriend.Width = 20;
            ratFriend.Height = 20;
            Grid.SetColumn(ratFriend, 0);
            friendGrid.Children.Add(ratFriend);

            /* Friend's name */
            TextBlock tbFriend = new TextBlock();
            TextBlock tbToolTip = new TextBlock() { Text = name };
            tbFriend.Text = name;
            tbFriend.Name = name + "Friend";
            RegisterName(tbFriend.Name, tbFriend);
            tbFriend.Cursor = Cursors.Hand;
            tbFriend.MouseUp += new MouseButtonEventHandler(onClickFriend);
            tbFriend.Margin = new Thickness(0, 6, 0, 0);
            tbFriend.Foreground = new SolidColorBrush(Colors.White);
            tbFriend.ToolTip = tbToolTip;
            Grid.SetColumn(tbFriend, 1);
            friendGrid.Children.Add(tbFriend);

            /* Delete friend image */
            Image deleteFriend = new Image();
            tbToolTip = new TextBlock();
            tbToolTip.Text = "Delete Friend";
            deleteFriend.Name = name + "Delete";
            RegisterName(deleteFriend.Name, deleteFriend);
            deleteFriend.MouseUp += new MouseButtonEventHandler(onClickDelete);
            deleteFriend.MouseEnter += new MouseEventHandler(onMouseEnterDelete);
            deleteFriend.MouseLeave += new MouseEventHandler(onMouseLeaveDelete);
            deleteFriend.Cursor = Cursors.Hand;
            deleteFriend.ToolTip = tbToolTip;
            deleteFriend.Source = new BitmapImage(new Uri("/ChatForFun-Client;Component/resources/delete_friend.png", UriKind.Relative));
            deleteFriend.Width = 20;
            deleteFriend.Height = 20;
            Grid.SetColumn(deleteFriend, 2);
            friendGrid.Children.Add(deleteFriend);

            /* Status of friend */
            Image onlineFriend = new Image();
            onlineFriend.Name = name + "Status";
            RegisterName(onlineFriend.Name, onlineFriend);
            onlineFriend.Source = new BitmapImage(new Uri("/ChatForFun-Client;Component/resources/online_friend.png", UriKind.Relative));
            onlineFriend.Width = 10;
            onlineFriend.Height = 10;
            Grid.SetColumn(onlineFriend, 3);
            friendGrid.Children.Add(onlineFriend);

            spFriend.Children.Add(friendGrid);
        }

        private void onMouseLeaveFriendArea(object sender, MouseEventArgs e)
        {
            ((Grid)sender).Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x1A, 0x1A));
        }

        private void onMouseEnterFriendArea(object sender, MouseEventArgs e)
        {
            ((Grid)sender).Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x2F, 0x2F, 0x37));
        }

        private void onFocusMsg(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }

        private void onLostFocusMsg(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Length == 0)
                ((TextBox)sender).Text = "Send a message";
        }

        private void onKeyDownEnterMsg(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && ((TextBox)sender).Text.Length > 0)
            {
                presenter.sendMessage(((TextBox)sender).Name, ((TextBox)sender).Text);
            }
        }

        private void onMouseEnterDelete(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("/ChatForFun-Client;Component/resources/trash.png", UriKind.Relative));
        }

        private void onMouseLeaveDelete(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("/ChatForFun-Client;Component/resources/delete_friend.png", UriKind.Relative));
        }

        private void onClickFriend(object sender, MouseButtonEventArgs e)
        {
            string name = ((TextBlock)sender).Name;
            object obj = messageTabControl.FindName(name.Substring(0, name.Length - 6) + "Img");
            if (obj == null)
                createFriendTab(((TextBlock)sender).Text);
        }

        private void onClickDelete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string name = ((Image)sender).Name;
                name = name.Substring(0, (name.Length - 6));
                presenter.deleteFriend(name);
            }
        }

        private void onClickAddFriend(object sender, RoutedEventArgs e)
        {
            AddFriendWindow addFriend = new AddFriendWindow();
            if (addFriend.ShowDialog() == true)
            {
                if (addFriend.ResponseText.Equals(user))
                {
                    MessageBox.Show("You can't add yourself.", "Error adding friend", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                object obj = messageTabControl.FindName(addFriend.ResponseText + "Friend");
                if (obj == null)
                    presenter.addFriend(addFriend.ResponseText);
            }
            else
                MessageBox.Show("User must not contain space characters.", "Error adding friend", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void onMouseEnterClose(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("/ChatForFun-Client;Component/resources/close_expanded.png", UriKind.Relative));
        }

        private void onMouseLeaveClose(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("/ChatForFun-Client;Component/resources/close.png", UriKind.Relative));
        }

        private void onMouseDownClose(object sender, MouseButtonEventArgs e)
        {
            string name = ((Image)sender).Name;
            name = name.Substring(0, (name.Length - 3));
            UnregisterName(((Image)sender).Name);

            object obj = messageTabControl.FindName(name + "Message");
            UnregisterName(((StackPanel)obj).Name);

            obj = messageTabControl.FindName(name + "Img");
            UnregisterName(((Image)obj).Name);

            obj = messageTabControl.FindName(name);
            UnregisterName(((TextBox)obj).Name);

            obj = messageTabControl.FindName(name + "Tab");
            messageTabControl.Items.Remove(((TabItem)obj));
            UnregisterName(((TabItem)obj).Name);
        }

        public void goToLogin()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                MessageBox.Show("You has been logged out.", "Logged out", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow main = new MainWindow();
                this.Close();
                main.Show();
            });
        }

        public void showMessageSendingFail()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                MessageBox.Show("Error while sending message.", "Message failed", MessageBoxButton.OK, MessageBoxImage.Error);
            });
        }

        public void showMessage(string friend, string message, string date)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                Console.WriteLine("ENVIADO DE: " + friend);
                object obj = messageTabControl.FindName(friend + "Message");
                if (obj == null)
                {
                    createFriendTab(friend);
                }
                obj = messageTabControl.FindName(friend + "Message");
                object obj2 = messageTabControl.FindName(friend);
                ((StackPanel)obj).Children.Add(createMessage(friend, message, date));
                ((TextBox)obj2).Text = "";
            });
        }

        public void showFriendAddingFail()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                MessageBox.Show("User does not exist.", "Add failed", MessageBoxButton.OK, MessageBoxImage.Error);
            });
        }

        public void showFriendAdding(string friend)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                addFriendView(friend);
                createFriendTab(friend);
            });
        }

        public void showFriendDeletingFail()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                MessageBox.Show("Error while deleting friend.", "Delete failed", MessageBoxButton.OK, MessageBoxImage.Error);
            });
        }

        public void showFriendDeleting(string name)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                MessageBox.Show("You have deleted your friend.", "Deleted friend", MessageBoxButton.OK, MessageBoxImage.Information);

                object obj = spFriend.FindName(name + "Friend");
                UnregisterName(((TextBlock)obj).Name);

                obj = spFriend.FindName(name + "Delete");
                UnregisterName(((Image)obj).Name);

                obj = spFriend.FindName(name + "Status");
                UnregisterName(((Image)obj).Name);

                obj = spFriend.FindName(name + "Grid");
                UnregisterName(((Grid)obj).Name);
                spFriend.Children.Remove((Grid)obj);

                obj = messageTabControl.FindName(name + "Img");
                UnregisterName(((Image)obj).Name);

                obj = messageTabControl.FindName(name + "Message");
                UnregisterName(((StackPanel)obj).Name);

                obj = messageTabControl.FindName(name);
                UnregisterName(((TextBox)obj).Name);

                obj = messageTabControl.FindName(name + "Tab");
                UnregisterName(((TabItem)obj).Name);
                messageTabControl.Items.Remove(obj);
            });
        }

        public void showMessage(string friend)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                object obj = messageTabControl.FindName(friend + "Message");
                object obj2 = messageTabControl.FindName(friend);
                ((StackPanel)obj).Children.Add(createMessage("You", ((TextBox)obj2).Text, DateTime.Now.ToString()));
                ((TextBox)obj2).Text = "";
            });
        }

        public void showFriends(List<string> friends, List<string> statusFriends)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                for (int i = 0; i < friends.Count; i++)
                {
                    addFriendView(friends.ElementAt(i));
                    createFriendTab(friends.ElementAt(i));
                    if (statusFriends.ElementAt(i).Equals("O"))
                    {
                        object obj = messageTabControl.FindName(friends.ElementAt(i) + "Status");
                        ((Image)obj).Source = null;
                    }
                }
            });
        }

        public void showFriendOnlineStatus(string friend)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                object obj = messageTabControl.FindName(friend + "Status");
                if (obj != null)
                    ((Image)obj).Source = new BitmapImage(new Uri("/ChatForFun-Client;Component/resources/online_friend.png", UriKind.Relative));
            });
        }

        public void showFriendOfflineStatus(string friend)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                object obj = messageTabControl.FindName(friend + "Status");
                if (obj != null)
                    ((Image)obj).Source = null;
            });
        }

        private void onClickLogout(object sender, RoutedEventArgs e)
        {
            presenter.logout();
        }
    }
}