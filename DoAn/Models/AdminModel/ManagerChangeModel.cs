using DoAn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Models.AdminModel
{
    public class ManagerChangeModel : ObservableObject
    {
        private string _username;
        private string _userid;
        private bool _isvalid;

        public string UserName
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
        public string UserID
        {
            get => _userid;
            set => SetProperty(ref _userid, value);
        }
        public bool IsValid
        {
            get => _isvalid;
            set => SetProperty(ref _isvalid, value);
        }
        public ManagerChangeModel(string username, string userid, bool isvalid) 
        {
            this.UserName = username;
            this.UserID = userid;
            this.IsValid = isvalid;
        }
    }
}
