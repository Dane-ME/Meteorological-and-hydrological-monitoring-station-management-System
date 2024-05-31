using DoAn.Models;
using DoAn.Services;
using DoAn.Views;
using MQTT;
using System;
using System.Windows.Input;

namespace DoAn.ViewModels
{
    public class LoginViewModel
    {
        public string Account { get;  set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public ICommand LoginButtonCommand { get; private set; }
        private readonly UserModel _userModel;
        public LoginViewModel(UserModel userModel)
        {
            _userModel = userModel;
            LoginButtonCommand = new Command(() =>
            {
                if (this.Password != null && this.Account != null)
                {
                    this.Password = this.Password.ToMD5();
                    _userModel.Account = this.Account;
                    _userModel.Password = this.Password;
                }
                _userModel.LoginRequest();
                //Shell.Current.GoToAsync("//LoadingView");
                Shell.Current.GoToAsync("//CheckingLoginView");
                this.Token = _userModel.token;
                //neu khong dung tk mk, hien thong bao o day
            });

            
        }

    }
}
