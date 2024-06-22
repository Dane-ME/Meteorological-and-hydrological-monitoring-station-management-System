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
        public ICommand LogOutButonCommand { get; private set; }
        public UserViewModel()
        {
            LogOutButonCommand = new Command(() => 
            {
                Shell.Current.GoToAsync("//LoginView");
            });
        }
    }
}
