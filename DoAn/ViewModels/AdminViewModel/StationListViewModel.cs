using DoAn.Models.AdminModel;
using DoAn.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MQTT;

namespace DoAn.ViewModels.AdminViewModel
{
    public class StationListViewModel : ObservableObject
    {
        
        public bool IsLoading {  get; set; }

        public event Action<StationListModel> OnNavigateToStationDetail;

        /// <summary>
        /// //////////
        /// </summary>
        private ObservableCollection<StationListModel> _nameofStation;
        public ObservableCollection<StationListModel> NameofStation 
        {
            get => _nameofStation;
            set
            {
                SetProperty(ref _nameofStation, value);
            }
        }
        /// <summary>
        /// /////////
        /// </summary>
        private ObservableCollection<int> numbers;

        public ObservableCollection<int> Numbers
        {
            get => numbers;
            set
            {
                numbers = value;
                OnPropertyChanged();
            }
        }
        public List<string> Name { get; set; }
        public List<string> ID { get; set; }
        public ICommand OpenDetailCommand { get; private set ; }
        public StationListViewModel() 
        {
            int count = 0;
            EventChanged.Instance.StationList += (s, e) =>
            {
                if (count == 0)
                {
                    SendAndListen();
                    count = 1;
                }
                else
                {
                    Broker.Instance.Send($"dane/service/stationlist/{Service.Instance.UserID}", new Document() { Token = $"{Service.Instance.Token}" });
                }
            };

            Name = new List<string>();
            ID = new List<string>();
            NameofStation = new ObservableCollection<StationListModel>();
            Numbers = new ObservableCollection<int>();

            OpenDetailCommand = new Command<StationListModel>( (e) =>
            {
                OnNavigateToStationDetail?.Invoke(e);
            });

            EventChanged.Instance.StationListChanged += (s, e) =>
            {
                var count = NameofStation.Count;
                for (int i = 1; i <= count; i++)
                {
                    Numbers.Add(i);
                }
                foreach (var i in this.NameofStation)
                {
                    this.Name.Add(i.Name);
                    this.ID.Add(i.ID);
                }
            };
        }
        public void SendAndListen()
        {
            Broker.Instance.Send($"dane/service/stationlist/{Service.Instance.UserID}", new Document() { Token = $"{Service.Instance.Token}" });
            Broker.Instance.Listen($"dane/service/stationlist/{Service.Instance.UserID}", HandleReceivedData);

        }
        private void HandleReceivedData(Document doc)
        {
            if (doc != null)
            {
                DocumentList list = doc.StationList;
                if(list != null)
                {
                    ObservableCollection<StationListModel> list2 = new ObservableCollection<StationListModel>();
                    foreach (Document item in list)
                    {
                        list2.Add(new StationListModel() { Name = item.StationName, ID = item.ObjectId });
                    }
                    NameofStation = list2;
                    EventChanged.Instance.OnStationListChanged();
                }
            }
        }
        
    }
}
