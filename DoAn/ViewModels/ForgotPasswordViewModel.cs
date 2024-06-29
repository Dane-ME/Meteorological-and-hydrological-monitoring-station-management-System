using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DoAn.Services;
using MQTT;

namespace DoAn.ViewModels
{
    public class ForgotPasswordViewModel : ObservableObject
    {
        private string _userid;
        private string _verificationCode;
        private bool _isNotice1 = false;
        private bool _isNotice2 = false;

        public string UserID
        {
            get => _userid;
            set => SetProperty(ref _userid, value);
        }
        public string VerificationCode
        {
            get => _verificationCode;
            set => SetProperty(ref _verificationCode, value);
        }
        public bool Notice1
        {
            get => _isNotice1;
            set => SetProperty(ref _isNotice1, value);
        }
        public bool Notice2
        {
            get => _isNotice2; 
            set => SetProperty(ref _isNotice2, value);
        }
        public ICommand VerificationCommand { get; private set; }
        public ICommand CreatePasswordCommand { get; private set; }

        public ForgotPasswordViewModel() 
        {
            VerificationCommand = new Command( () =>
            {
                Notice1 = true;
                Broker.Instance.Send("dane/usercontroller/forgotpassword", new Document() { UserID = $"{this.UserID}" });
            });
            CreatePasswordCommand = new Command(() =>
            {
                Notice1 = false;
                Notice2 = true;
                Broker.Instance.Send($"dane/user/createnewpassword/{this.UserID}", new Document() { VerificationCode = $"{this.VerificationCode}" });
            });
        }
        public void SendRequest()
        {
            Broker.Instance.Send($"dane/user/createnewpassword/{this.UserID}", new Document() { VerificationCode = $"{this.VerificationCode}" });
        }
        
    }
}
