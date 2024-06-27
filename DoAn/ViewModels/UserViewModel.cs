using DoAn.Models.AdminModel;
using DoAn.Services;
using MQTT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoAn.ViewModels
{
    public class UserViewModel : ObservableObject
    {
        private ObservableCollection<UserListModel> _stationdetail;
        public ObservableCollection<UserListModel> StationDetail
        {
            get => _stationdetail;
            set
            {
                SetProperty(ref _stationdetail, value);
            }
        }
        private ObservableCollection<int> numbers;
        public ObservableCollection<int> Numbers
        {
            get => numbers;
            set
            {
                SetProperty(ref numbers, value);
            }
        }
        public List<string> Name { get; set; }
        public List<string> ID { get; set; }

        private string _username;
        private string _userid;
        private string _role;
        private string _email;
        private string _workingUnit;
        private string _position;
        private string _regisDate;

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
        public string Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string WorkingUnit
        {
            get => _workingUnit;
            set => SetProperty(ref _workingUnit, value);
        }
        public string Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }
        public string RegisDate
        {
            get => _regisDate; 
            set => SetProperty(ref _regisDate, value);
        }
        public ICommand Reload {  get; set; }
        public ICommand LogOutButonCommand { get; private set; }
        public UserViewModel()
        {
            
            Name = new List<string>();
            ID = new List<string>();
            StationDetail = new ObservableCollection<UserListModel>();
            Numbers = new ObservableCollection<int>();
            SendandListen();
            Reload = new Command( () => SendandListen());
            EventChanged.Instance.UserListChanged += (s, e) =>
            {
                var count = StationDetail.Count;
                for (int i = 1; i <= count; i++)
                {
                    Numbers.Add(i);
                }
                foreach (var i in this.StationDetail)
                {
                    this.Name.Add(i.Name);
                    this.ID.Add(i.ID);
                }
            };
            LogOutButonCommand = new Command(() => 
            {
                Shell.Current.GoToAsync("//LoginView");
                Broker.Instance.Send($"dane/user/logout/{Service.Instance.UserID}", new Document() 
                {
                    Token = Service.Instance.Token,
                });
                Service.Instance.Token = null;
                Service.Instance.LoginState = false;
                Service.Instance.Role = null;
                Service.Instance.UserID = null;
            });
        }
        public async void SendandListen()
        {

            await Task.Delay(500);
            Broker.Instance.Listen($"dane/service/user/{Service.Instance.UserID}", (doc) => 
            {
                if(doc != null)
                {
                    this.UserName = doc.UserName;
                    this.Role = doc.Role;
                    this.Email = doc.Email;
                    this.Position = doc.Position;
                    this.RegisDate = doc.RegisDate;
                    this.WorkingUnit = doc.WorkingUnit;
                    UserID = $"{Service.Instance.UserID}";
                    DocumentList list = doc.StationList;
                    if (list != null)
                    {
                        ObservableCollection<UserListModel> list2 = new ObservableCollection<UserListModel>();
                        foreach (Document item in list)
                        {
                            list2.Add(new UserListModel() { Name = item.StationName, ID = item.ObjectId });
                        }
                        StationDetail = list2;
                        EventChanged.Instance.OnUserListChanged();
                    }
                }
            });
            await Task.Delay(500);
            Broker.Instance.Send($"dane/service/user/{Service.Instance.UserID}", new Document() { Token = Service.Instance.Token });
        }
    }
}
