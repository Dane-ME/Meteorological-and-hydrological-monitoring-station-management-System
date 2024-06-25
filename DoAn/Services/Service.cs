using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Services
{
    public class Service : ObservableObject
    {
        private bool _loginState;
        private string? _token;
        private string? _userid;
        private string? _role;

        public static Service? instance;
        public static Service Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Service();
                }
                return instance;
            }
        }
        public bool LoginState { 
            get => _loginState; 
            set => SetProperty(ref _loginState, value); 
        }
        public string Token { 
            get => _token; 
            set => SetProperty(ref _token, value); 
        }
        public string UserID
        {
            get => _userid;
            set => SetProperty(ref _userid, value);
        }
        public string Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }
        public async Task<bool> IsLoginAsync()
        {
            if (this.LoginState) { await Task.Delay(500); return true; }
            else await Task.Delay(500); return false;
        }
    }
}
