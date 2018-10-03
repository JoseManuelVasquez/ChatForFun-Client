using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatForFun_Client.Interface;
using ChatForFun_Client.Interactor;

namespace ChatForFun_Client.Presenter
{
    class SignUpPresenter: ISignUpPresenter, ISignUpDataListener
    {
        private ISignUpView view;
        private ISignUpInteractor interactor;

        public SignUpPresenter(ISignUpView view)
        {
            this.view = view;
            interactor = new SignUpInteractor(this);
        }

        public void registerUser(string user, string password)
        {
            interactor.registerUser(user, password);
        }

        public void onRegisterFail()
        {
            view.showSignUpFail();
        }

        public void onRegisterSuccess()
        {
            view.goToLogin();
        }
    }
}
