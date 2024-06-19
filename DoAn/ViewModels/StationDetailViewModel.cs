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
        public Document DataResponse { get; set; }
        public ObservableCollection<StationDetailModel> Data { get; set; }
        private ObservableCollection<StationDetailModel> _dataGrid;
        public ObservableCollection<StationDetailModel> DataGrid
        {
            get { return _dataGrid; }
            set { this._dataGrid = value; OnPropertyChanged(); }
        }
        public event Action ResponseHandle;
        protected virtual void OnResponseHandleEvent()
        {
            ResponseHandle?.Invoke();
        }
        public StationDetailViewModel() { }
        public StationDetailViewModel(string station) 
        {
            SendandListen();

            StationId = station;
            ResponseHandle += OnResponseHandle;

            DataGrid = new ObservableCollection<StationDetailModel>();

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
            if (doc != null)
            {
                this.DataResponse = doc;
                OnResponseHandleEvent();
            }
        }
        private void OnResponseHandle()
        {
            List<string> dl = DataResponse.StationTypeList;
            if(dl.Contains("Hydrological")) 
            {
                Console.WriteLine("ok");
            }
            else if(dl.Contains("Meteorological")) 
            {
                DataGrid = MeteoData(DataResponse);
            }
            else if (dl.Contains("RainFall"))
            {
                Console.WriteLine("ok");
            }
            else { }
        } 
        private ObservableCollection<StationDetailModel> MeteoData(Document doc)
        {
            ObservableCollection<StationDetailModel> detailModels = new ObservableCollection<StationDetailModel>();
            foreach(var item in doc.StationData)
            {
                detailModels.Add(
                    new StationDetailModel(
                    item.ObjectId,
                    item.WindSpeed,
                    item.WindDirection,
                    item.WindSpeedAt2mHeight,
                    item.WindDirectionAt2mHeight,
                    item.TimeOfOccurrenceOffxfx2m,
                    item.AverageWindSpeedIn2s,
                    item.AverageWindDirectionIn2s,
                    item.TimeOfOccurrenceOffxfx2s)
                    );
            }
            return detailModels;
        }
    }
}
