using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatForFun_Client.Interface
{
    interface ISignUpDataListener
    {
        void onRegisterSuccess();
        void onRegisterFail();
    }

    interface ISignUpView
    {
        void goToLogin();
        void showSignUpFail();
    }

    interface ISignUpPresenter
    {
        void registerUser(string user, string password);
    }

    interface ISignUpInteractor
    {
        void registerUser(string user, string password);
    }
}
