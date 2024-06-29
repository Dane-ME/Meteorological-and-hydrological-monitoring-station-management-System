using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DoAn.Services;
using DoAn.Views.AdminView;
using IService.Services;
using MQTT;

namespace DoAn.ViewModels.AdminViewModel
{
    public class AddUserViewModel : ObservableObject
    {
        private string _userid;
        private string _username;
        private string _password;
        private string _passwordagain;
        private string _email;
        private string _workingunit;
        private string _position;
        private bool _answer;

        public string UserID { get => _userid; set => SetProperty(ref _userid, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string PasswordAgain { get => _passwordagain; set => SetProperty(ref _passwordagain, value); }
        public string Name { get => _username; set => SetProperty(ref _username, value); }
        public  string Email { get => _email; set => SetProperty(ref _email, value); }
        public string WorkingUnit { get => _workingunit; set => SetProperty(ref _workingunit, value); }
        public string Position { get => _position; set => SetProperty(ref _position, value); }
        public bool Answer
        {
            get => _answer;
            set
            {
                SetProperty(ref _answer, value);
                EventChanged.Instance.OnAnswerChanged();
            } 
        }
        public ICommand RegisCommand { get; set; }
        public AddUserViewModel() 
        {
            
            RegisCommand = new Command(EventChanged.Instance.OnPopupHandleChanged);

            EventChanged.Instance.AnswerChanged += (s, e) =>
            {
                if(Answer) 
                {
                    Document doc = new Document()
                    {
                        Token = Service.Instance.Token,
                        UserID = this.UserID,
                        UserName = this.Name,
                        EncodePass = Password.ToMD5(),
                        Role = "User",
                        Email = this.Email,
                        WorkingUnit = this.WorkingUnit,
                        Position = this.Position,
                        RegisDate = TimeFormat.getTimeNow()
                    };
                    Broker.Instance.Send($"dane/user/regis/{Service.Instance.UserID}", doc);
                }
            };
        }
        public void IsUserIDFormatCorrect()
        {
            Func<bool, int> convert = (input) =>
            {
                if (input) { return 1; }
                else return 0;
            };
            Func<string, bool> checkAcc = (input) =>
            {
                if (string.IsNullOrEmpty(this.UserID) || this.UserID.Length < 6) return false;
                else return true;
            };
            //int res = (convert(checkAcc(this.UserID)) << 1) | convert(!string.IsNullOrEmpty(this.Password));
            //switch (res)
            //{
            //    case 3: Run(); break;
            //    case 2: IsValidAcc = false; IsValidPass = true; break;
            //    case 1: IsValidAcc = true; IsValidPass = false; break;
            //    case 0: IsValidAcc = true; IsValidPass = true; break;
            //    default: break;
            //}
        }

    }
}
