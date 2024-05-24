using DoAn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoAn.ViewModels
{
    public class UserViewModel
    {
        private readonly AuthService _authService;
        public ICommand LogOutButonCommand { get; private set; }
        public UserViewModel(AuthService authService)
        {
            _authService = authService;
            LogOutButonCommand = new Command(() => 
            {
                _authService.Logout();
                Shell.Current.GoToAsync("//LoginView");
            });
        }
    }
}
