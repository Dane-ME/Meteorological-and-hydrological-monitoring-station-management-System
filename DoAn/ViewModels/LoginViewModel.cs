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
                Send();
                if(Service.Instance.LoginState == false)
                {
                    Listen();
                }
                _userModel.Account = this.Account;
                _userModel.Password = this.Password;
                _userModel.LoginRequest();
                Shell.Current.GoToAsync("//CheckingLoginView");
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
    public async Task Send()
    {
        await Task.Delay(500);
        Broker.Instance.Send("dane/usercontroller/login", new Document()
        {
            Type = "id",
            UserID = this.Account,
            Pass = this.Password
        });
        
    }
    public async Task Listen()
    {
        await Task.Delay(1000);
        Broker.Instance.Listen($"dane/login/{this.Account}", (doc) =>
        {
            if (doc.Token != null)
            {
                string token = doc.Token;
                string Payload = token.Split('.')[1];
                var decode = new Format();
                string role = decode.Base64urlDecode(Payload).Role;
                if (role == "Admin" || role == "User")
                {
                    Service.Instance.LoginState = true;
                    Service.Instance.Role = role;
                    EventChanged.Instance.OnRoleChanged();
                }
                else { Service.Instance.LoginState = false; }
            }
        });
    }
}
