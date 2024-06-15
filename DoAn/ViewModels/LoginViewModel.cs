using DoAn.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DoAn.Services;
using DoAn.Views;
using MQTT;
using System;

public class LoginViewModel : ObservableObject
{
    private bool _isValidAcc = false;
    private bool _isValidPass = false;
    private string _account;
    private string _password;
    public string Token { get; set; }
    private readonly UserModel _userModel;
    public bool IsValidAcc
    {
        get => _isValidAcc;
        set => SetProperty(ref _isValidAcc, value);
    }

    public bool IsValidPass
    {
        get => _isValidPass;
        set => SetProperty(ref _isValidPass, value);
    }

    public string Account
    {
        get => _account;
        set => SetProperty(ref _account, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public ICommand LoginButtonCommand { get; private set; }
    public LoginViewModel(UserModel userModel)
    {
        _userModel = userModel;
        LoginButtonCommand = new Command(() =>
        {
            if (!string.IsNullOrEmpty(this.Password) && !string.IsNullOrEmpty(this.Account))
            {
                this.Password = this.Password.ToMD5();
                _userModel.Account = this.Account;
                _userModel.Password = this.Password;
                _userModel.LoginRequest();
                Shell.Current.GoToAsync("//CheckingLoginView");
                this.Token = _userModel.token;
            }
            else
            {
                if (string.IsNullOrEmpty(this.Password))
                {
                    IsValidPass = true;
                }
                else
                {
                    IsValidPass = false;
                }

                if (string.IsNullOrEmpty(this.Account) || this.Account.Length < 6)
                {
                    IsValidAcc = true;
                }
                else
                {
                    IsValidAcc = false;
                }
            }
        });
    }
}
