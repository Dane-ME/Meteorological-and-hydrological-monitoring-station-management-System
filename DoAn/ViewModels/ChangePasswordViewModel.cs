using DoAn.Services;
using MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoAn.ViewModels
{
    public class ChangePasswordViewModel : ObservableObject
    {
        private string _oldPassword;
        private string _newPassword;
        private string _rememberMe;
        private string _warning;

        public string RememberMe { get => _rememberMe; set => SetProperty(ref _rememberMe, value); }
        public string OldPassword { get => _oldPassword; set => SetProperty(ref _oldPassword, value); }
        public string NewPassword { get => _newPassword; set => SetProperty(ref _newPassword, value); }
        public string? Warning { get => _warning; set => SetProperty(ref _warning, value); }  

        public ICommand ChangePasswordCommand { get; private set; }
        public ChangePasswordViewModel() 
        {
            ChangePasswordCommand = new Command(() => {
                int isnull = convert(OldPassword != null) << 2 | convert(NewPassword != null) << 1 | convert(RememberMe != null); ;    
                switch (isnull)
                {
                    case 7: {
                            bool check = IsDuplicate();
                            if(check) { Send(); }
                            break; } //111
                    case 6: Warning = "Vui lòng nhập lại mật khẩu mới"; break; //110
                    case 5: Warning = "Mật khẩu mới chưa nhập"; break; //101
                    case 4: Warning = "Mật khẩu mới chưa nhập"; break; //100
                    case 3: Warning = "Chưa nhập mật khẩu cũ"; break; //011
                    case 2: Warning = "Chưa nhập mật khẩu cũ"; break; //010
                    case 1: Warning = "Chưa nhập mật khẩu cũ"; break; //001
                    case 0: Warning = "Form không được để trống"; break; //000
                    default: break;
                }
            });
        }
        public bool IsDuplicate()
        {
            int isduplicate = convert(OldPassword == NewPassword) << 1 | convert(NewPassword != RememberMe) << 0;
            switch (isduplicate)
            {
                case 3: Warning = "Mật khẩu mới không được trùng mật khẩu cũ"; return false; //11
                case 2: Warning = "Mật khẩu mới không được trùng mật khẩu cũ"; return false; //10
                case 1: Warning = "Mật khẩu mới nhập lại phải trùng với mật khẩu mới"; return false; //01
                case 0: Warning = null; return true;
                default: return false;
            }
        }
        public int convert(bool input)
        {
            if (input) { return 1; }
            else return 0;
        }
        public void Send()
        {
            Broker.Instance.Send($"dane/service/changepassword/{Service.Instance.UserID}", new Document
            {
                Token = Service.Instance.Token,
                OldPass = this.OldPassword.ToMD5(),
                EncodePass = this.NewPassword.ToMD5(),
            });
        }
        public void Listen()
        {

        }
    }
}
