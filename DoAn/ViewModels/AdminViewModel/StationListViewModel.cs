using DoAn.Models;
using DoAn.Models.AdminModel;
using DoAn.Services;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MQTT;

namespace DoAn.ViewModels.AdminViewModel
{
    public class StationListViewModel : ObservableObject
    {
        
        public bool IsLoading {  get; set; }

        public event Action OnNavigateToStationDetail;
        /// <summary>
        /// //////////
        /// </summary>
        private ObservableCollection<StationListModel> _nameofStation;
        public ObservableCollection<StationListModel> NameofStation 
        {
            get => _nameofStation;
            set
            {
                EventChanged.Instance.OnChanged();
                SetProperty(ref _nameofStation, value);
            }
        }
        private ObservableCollection<NumOder> _numOder;
        public ObservableCollection<NumOder> NumOrder
        {
            get => _numOder;
            set => SetProperty(ref _numOder, value);
        }
        /// <summary>
        /// /////////
        /// </summary>
        public List<string> Name { get; set; }
        public List<string> ID { get; set; }
        public List<string> Num { get; set; }
        public ICommand OpenDetailCommand { get; private set ; }
        public StationListViewModel() 
        {
            SendandListen();
            Name = new List<string>();
            ID = new List<string>();
            Num = new List<string>();

            foreach(var i in this.NameofStation)
            {
                this.Name.Add(i.Name);
                this.ID.Add(i.ID);
            }
            OpenDetailCommand = new Command<StationListModel>( (e) =>
            {
                OnNavigateToStationDetail?.Invoke();
            });
            EventChanged.Instance.DataChanged += (s, e) =>
            {
                var count = NameofStation.Count;
                NumOrder.Add(new NumOder() { Num = $"{count}" });
                foreach (var i in this.NumOrder)
                {
                    this.Num.Add(i.Num);
                }
            };

            
        }
        public void SendandListen()
        {
            Broker.Instance.Send("dane/service/stationlist/hhdangev02", new Document() { Token = "00000" });
            Broker.Instance.Listen("dane/service/stationlist/hhdangev02", (doc) =>
            {
                if (doc != null)
                {
                    DocumentList list = doc.StationList;
                    foreach (Document item in list)
                    {
                        NameofStation.Add(new StationListModel() { Name = item.StationName, ID = item.ObjectId });
                    }
                }
            });
        }
    }
}
