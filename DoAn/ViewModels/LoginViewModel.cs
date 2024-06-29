using DoAn.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DoAn.Services;
using DoAn.Views;
using MQTT;
using System;
using Microsoft.Maui;

public class LoginViewModel : ObservableObject
{
    private bool _isValidAcc = false;
    private bool _isValidPass = false;
    private bool _ischeckBox = false;
    private bool _isvisiblePass = true;
    private string _account = "hhdangev02";
    private string _password = "1";
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
    public bool IsVisiblePass
    {
        get => _isvisiblePass;
        set => SetProperty(ref _isvisiblePass, value);
    }
    public bool IsCheckBox
    {
        get => _ischeckBox;
        set
        {
            _ischeckBox = value;
            IsVisiblePass = !_ischeckBox;  // Khi checkbox được chọn, IsVisiblePass là false và ngược lại
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsVisiblePass));  // Thông báo thay đổi cho IsVisiblePass
        }
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
    public ICommand ForgotPasswordCommand { get; private set; }
    public LoginViewModel(UserModel userModel)
    {
        _userModel = userModel;
        LoginButtonCommand = new Command(async () =>
        {

            Func<bool, int> convert = (input) => 
            {
                if (input) { return 1; }
                else return 0;
            };
            Func<string, bool> checkAcc = (input) =>
            {
                if (string.IsNullOrEmpty(this.Account) || this.Account.Length < 6) return false;
                else return true;
            };          
           
            Action Run = () =>
            { 
                Listen();
                Send();
                Shell.Current.GoToAsync("//CheckingLoginView");
            };
            int res = (convert(checkAcc(this.Account)) << 1) | convert(!string.IsNullOrEmpty(this.Password));
            switch (res)
            {
                case 3: Run();  break;
                case 2: IsValidAcc = false; IsValidPass = true; break;
                case 1: IsValidAcc = true; IsValidPass = false; break;
                case 0: IsValidAcc = true; IsValidPass = true; break;
                default: break;
            }
        });
        ForgotPasswordCommand = new Command(() =>
        {
            Shell.Current.GoToAsync("ForgotPasswordView");
        });
    }
    public void Send()
    {
        Broker.Instance.Send("dane/usercontroller/login", new Document()
        {
            Type = "id",
            UserID = this.Account,
            EncodePass = this.Password.ToMD5(),
        });
        
    }
    public void Listen()
    {
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
                    Service.Instance.Token = token; 
                    Service.Instance.UserID = this.Account;
                    EventChanged.Instance.OnRoleChanged();
                }
                else { Service.Instance.LoginState = false; }
            }
        });
    }
}
