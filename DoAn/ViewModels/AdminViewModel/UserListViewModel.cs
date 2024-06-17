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

        private ObservableCollection<UserListModel> _nameofStation;
        public ObservableCollection<UserListModel> NameofStation
        {
            get => _nameofStation;
            set
            {
                SetProperty(ref _nameofStation, value);
            }
        }
        public List<string> Name { get; set; }
        public List<string> ID { get; set; }
        public List<string> Num { get; set; }
        public ICommand OpenDetailCommand { get; private set; }
        public UserListViewModel() 
        {
            Name = new List<string>();
            ID = new List<string>();
            Num = new List<string>();
            NameofStation = new ObservableCollection<UserListModel>();

            EventChanged.Instance.UserListChanged += (s, e) =>
            {
                var count = NameofStation.Count;
                foreach (var i in this.NameofStation)
                {
                    this.Name.Add(i.Name);
                    this.ID.Add(i.ID);
                }
            };

        }

        public void SendandListen()
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
                    NameofStation = list2;
                    EventChanged.Instance.OnUserListChanged();
                }
            });
        }
    }
}
