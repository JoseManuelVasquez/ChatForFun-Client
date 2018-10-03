using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatForFun_Client.Interface
{
    interface IChatDataListener
    {
        void onLogout();
        void onReceiveFriends(List<string> friends, List<string> statusFriends);
        void onReceiveMessage(string friend, string message, string date);
        void onReceiveStatus(string friend, string status);
        void onMessageSuccess(string friend);
        void onMessageFail();
        void onFriendAddingSuccess(string friend);
        void onFriendAddingFail();
        void onFriendDeletingSuccess(string name);
        void onFriendDeletingFail();
    }

    interface IChatView
    {
        void goToLogin();
        void showMessageSendingFail();
        void showMessage(string friend);
        void showMessage(string friend, string message, string date);
        void showFriendAddingFail();
        void showFriendAdding(string friend);
        void showFriendDeletingFail();
        void showFriendDeleting(string name);
        void showFriends(List<string> friends, List<string> statusFriends);
        void showFriendOnlineStatus(string friend);
        void showFriendOfflineStatus(string friend);
    }

    interface IChatPresenter
    {
        void logout();
        void sendMessage(string friend, string message);
        void addFriend(string friend);
        void deleteFriend(string friend);
        void deleteAccount();
        void receiveCommand();
    }

    interface IChatInteractor
    {
        void logout();
        void sendMessage(string friend, string message);
        void addFriend(string friend);
        void deleteFriend(string friend);
        void deleteAccount();
        void receiveCommand();
    }
}
