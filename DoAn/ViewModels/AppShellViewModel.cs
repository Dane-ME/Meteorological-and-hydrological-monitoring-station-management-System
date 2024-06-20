using DoAn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.ViewModels
{
    public class AppShellViewModel : ObservableObject
    {
        private bool _isuser;
        public bool IsUser
        {
            get { return _isuser; }
            set { _isuser = value; OnPropertyChanged(); }
        }
        public AppShellViewModel() 
        {
            EventChanged.Instance.RoleChanged += (s, e) =>
            {
                if(Service.Instance.Role == "User") { IsUser = false; }
                else if(Service.Instance.Role == "Admin") { IsUser = true; }
                else { Shell.Current.GoToAsync("//LoginView"); }
            };
            
        }
    }
}
