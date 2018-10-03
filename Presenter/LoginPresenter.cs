using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatForFun_Client.Interface;
using ChatForFun_Client.Interactor;

namespace ChatForFun_Client.Presenter
{
    class LoginPresenter : ILoginPresenter, ILoginOnDataListener
    {
        private ILoginView view;
        private ILoginInteractor interactor;

        public LoginPresenter(ILoginView view)
        {
            this.view = view;
            interactor = new LoginInteractor(this);
        }

        public void authenticateUser(string user, string password)
        {
            interactor.authenticateUser(user, password);
        }

        public void onLoginFail()
        {
            view.showLoginFail();
        }

        public void onLoginSuccess()
        {
            view.goToChat();
        }
    }
}
