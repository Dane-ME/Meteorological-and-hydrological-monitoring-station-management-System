using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

using System.Text;
using System.Threading.Tasks;
using DoAn.Models;
using DoAn.Services;

namespace DoAn.ViewModels
{
    public class StationDetailViewModel : ObservableObject
    {
        public string _stationId;
        public string StationId 
        { 
            get => _stationId; 
            set => SetProperty(ref _stationId,value); 
        }
        public ObservableCollection<StationDetailModel> Data { get; set; }
        public StationDetailViewModel() { }

        public StationDetailViewModel(string station) {
            StationId = station;
            Data = new ObservableCollection<StationDetailModel>(){
                new StationDetailModel("08:20", 2.1),
                new StationDetailModel("08:30", 1.5),
                new StationDetailModel("08:40", 1.7),
                new StationDetailModel("08:50", 1.7),
                new StationDetailModel("09:00", 2.4),
                new StationDetailModel("09:00", 2.2)
            };
        }
        public async Task SendandListen()
        {
            await Task.Delay(1000);
            MQTT.Broker.Instance.Send($"dane/service/stationdetail/hhdangev02", new Document() { Token = "00000", StationID = $"{this.StationId}" });
            MQTT.Broker.Instance.Listen($"dane/service/stationdetail/hhdangev02", HandleReceivedData);

        }
        private void HandleReceivedData(Document doc)
        {
            if (doc != null && doc.HomeData != null)
            {
                
            }
        }
    }
}
