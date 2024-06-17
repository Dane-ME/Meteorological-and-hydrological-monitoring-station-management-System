using DoAn.Models.AdminModel;
using DoAn.Services;
using DoAn.Views.AdminView;
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
            SendandListen();
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

            AddUserCommand = new Command((e) =>
            {

            });
            OpenDetailCommand = new Command<UserListModel>((e) =>
            {
                OnNavigateToUserDetail?.Invoke(e);
            });
            RemoveUserCommand = new Command((e) =>
            {

            });

        }

        private void SendandListen()
        {
            MQTT.Broker.Instance.Send("dane/service/userlist/hhdangev02", new Document() { Token = "00000" });
            MQTT.Broker.Instance.Listen("dane/service/userlist/hhdangev02", (doc) =>
            {
                if (doc != null)
                {
                    DocumentList list = doc.StationList;
                    ObservableCollection<UserListModel> list2 = new ObservableCollection<UserListModel>();
                    foreach (Document item in list)
                    {
                        list2.Add(new UserListModel() { Name = item.StationName, ID = item.ObjectId });
                    }
                    UserDetail = list2;
                    EventChanged.Instance.OnUserListChanged();
                }
            });
        }
    }
}
