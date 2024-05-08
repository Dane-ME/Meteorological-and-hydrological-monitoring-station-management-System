using DoAn.Core;
using DoAn.Models;
using DoAn.Views;
using MQTT;
using System;
using System.Windows.Input;

namespace DoAn.ViewModels
{
    public class LoginViewModels
    {
        public string Account { get;  set; }
        public string Password { get; set; }

        public ICommand LoginButtonCommand { get; private set; }

        public LoginViewModels()
        {

            //System.File.Instance.saveToken(con);
            if (System.File.Instance.getToken() != null)
            {
                string tokenid = (string)System.File.Instance.getToken();
                var tokenmqtt = new TokenMQTT(tokenid);
                tokenmqtt.Send(tokenmqtt.CreateTokenContent());
                // gui token toi server 
                // neu token con han, mo giao dien chinh
                OnNavigatedTo();
            }
            var in4 = new AccountModel();

            LoginButtonCommand = new Command(() =>
            {
                if (this.Account == in4.Account && this.Password == in4.PassWord)
                {
                    // chuyen tk mk sang ma md5 gui ve server
                    // lay ve token va luu lai
                    // mo app
                    Shell.Current.GoToAsync("//HomeView");
                }
                //neu khong dung tk mk, hien thong bao o day
            });
        }
        public async void OnNavigatedTo()
        {
            // Đợi một khoảng thời gian ngắn để đảm bảo LoginView đã load hoàn toàn
            await Task.Delay(500);

            // Điều hướng đến LoadingView
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync($"//HomeView");
            }
        }

    }
}
