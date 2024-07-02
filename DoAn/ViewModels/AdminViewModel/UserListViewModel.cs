using DoAn.Models.AdminModel;
using DoAn.Services;
using DoAn.Views.AdminView;
using MQTT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoAn.ViewModels.AdminViewModel
{
    public class UserListViewModel : ObservableObject
    {
        public event Action<UserListModel> OnNavigateToUserDetail;
        public event Action<UserListModel> OnNavigateToAddUser;

        private ObservableCollection<UserListModel> _userdetail;
        public ObservableCollection<UserListModel> UserDetail
        {
            get => _userdetail;
            set
            {
                SetProperty(ref _userdetail, value);
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
        public ICommand OpenDetailCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand RemoveUserCommand { get; private set; }

        public UserListViewModel() 
        {
            int count = 0;
            EventChanged.Instance.UserList += (s, e) =>
            {
                if (count == 0)
                {
                    SendandListen();
                    count = 1;
                }
                else
                {
                    Numbers = new ObservableCollection<int>();
                    MQTT.Broker.Instance.Send($"dane/service/userlist/{Service.Instance.UserID}", new Document() { Token = $"{Service.Instance.Token}" });
                }
                
            };
            
            Name = new List<string>();
            ID = new List<string>();
            UserDetail = new ObservableCollection<UserListModel>();
            Numbers = new ObservableCollection<int>();


            EventChanged.Instance.UserListChanged += (s, e) =>
            {
                var count = UserDetail.Count;
                for (int i = 1; i <= count; i++)
                {
                    Numbers.Add(i);
                }
                foreach (var i in this.UserDetail)
                {
                    this.Name.Add(i.Name);
                    this.ID.Add(i.ID);
                }
            };
            AddUserCommand = new Command(() =>
            {
                Shell.Current.GoToAsync("AddUserView");
            });
            OpenDetailCommand = new Command<UserListModel>((e) =>
            {
                OnNavigateToUserDetail?.Invoke(e);
            });
            RemoveUserCommand = new Command<UserListModel>(async (e) =>
            {
                Broker.Instance.Send($"dane/user/removeuser/{Service.Instance.UserID}", new Document()
                {
                    Token = Service.Instance.Token,
                    UserID = e.ID
                });
                await Task.Delay(100);
                MQTT.Broker.Instance.Send($"dane/service/userlist/{Service.Instance.UserID}", new Document() { Token = $"{Service.Instance.Token}" });

            });

        }
        private async void SendandListen()
        {
            // Task.Delay(500);
            MQTT.Broker.Instance.Listen($"dane/service/userlist/{Service.Instance.UserID}", (doc) =>
            {
                if (doc != null)
                {
                    DocumentList list = doc.UserList;
                    if (list != null)
                    {
                        ObservableCollection<UserListModel> list2 = new ObservableCollection<UserListModel>();
                        foreach (Document item in list)
                        {
                            list2.Add(new UserListModel() { Name = item.UserName, ID = item.ObjectId });
                        }
                        UserDetail = list2;
                        EventChanged.Instance.OnUserListChanged();
                    }
                }
            });
            await Task.Delay(50);
            MQTT.Broker.Instance.Send($"dane/service/userlist/{Service.Instance.UserID}", new Document() { Token = $"{Service.Instance.Token}" });
            
        }
    }
}
