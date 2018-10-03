using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatForFun_Client.Interface
{
    interface ILoginOnDataListener
    {
        void onLoginSuccess();
        void onLoginFail();
    }

    interface ILoginView
    {
        void goToChat();
        void showLoginFail();
    }

    interface ILoginPresenter
    {
        void authenticateUser(string user, string password);
    }

    interface ILoginInteractor
    {
        void authenticateUser(string user, string password);
    }
}
