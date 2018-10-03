using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatForFun_Client.Interface;
using ChatForFun_Client.Interactor;
using System.Threading;

namespace ChatForFun_Client.Presenter
{
    class ChatPresenter : IChatPresenter, IChatDataListener
    {
        private IChatView view;
        private IChatInteractor interactor;

        public ChatPresenter(IChatView view)
        {
            this.view = view;
            interactor = new ChatInteractor(this);
        }

        public void addFriend(string friend)
        {
            interactor.addFriend(friend);
        }

        public void deleteAccount()
        {
            interactor.deleteAccount();
        }

        public void deleteFriend(string friend)
        {
            interactor.deleteFriend(friend);
        }

        public void logout()
        {
            interactor.logout();
        }

        public void onFriendAddingFail()
        {
            view.showFriendAddingFail();
        }

        public void onFriendAddingSuccess(string friend)
        {
            view.showFriendAdding(friend);
        }

        public void onFriendDeletingFail()
        {
            view.showFriendDeletingFail();
        }

        public void onFriendDeletingSuccess(string name)
        {
            view.showFriendDeleting(name);
        }

        public void onLogout()
        {
            view.goToLogin();
        }

        public void onMessageFail()
        {
            view.showMessageSendingFail();
        }

        public void onMessageSuccess(string friend)
        {
            view.showMessage(friend);
        }

        public void onReceiveFriends(List<string> friends, List<string> statusFriends)
        {
            view.showFriends(friends, statusFriends);
        }

        public void onReceiveMessage(string friend, string message, string date)
        {
            view.showMessage(friend, message, date);
        }

        public void onReceiveStatus(string friend, string status)
        {
            if (status.Equals("I"))
                view.showFriendOnlineStatus(friend);
            else if (status.Equals("O"))
                view.showFriendOfflineStatus(friend);
        }

        public void receiveCommand()
        {
            Thread thread = new Thread(interactor.receiveCommand);
            thread.Start();
        }

        public void sendMessage(string friend, string message)
        {
            interactor.sendMessage(friend, message);
        }
    }
}
