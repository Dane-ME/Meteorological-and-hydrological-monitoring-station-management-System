using DoAn.Models.AdminModel;
using DoAn.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.ViewModels.AdminViewModel
{
    public class StationChangeViewModel : ObservableObject
    {
        public string UserID { get; set; }
        public Document DataResponse { get; set; }
        public event Action ResponseHandle;
        private ObservableCollection<StationChangeModel> _datagrid;
        public ObservableCollection<StationChangeModel> DataGrid
        { get => _datagrid; set => SetProperty(ref _datagrid, value); }
        protected virtual void OnResponseHandleEvent()
        {
            ResponseHandle?.Invoke();
        }
        public StationChangeViewModel(string userid)
        {
            DataGrid = new ObservableCollection<StationChangeModel>();
            UserID = userid;
            ResponseHandle += OnResponseHandle;
            SendandListen();
        }
        public async Task SendandListen()
        {
            await Task.Delay(1000);
            MQTT.Broker.Instance.Send($"dane/service/stationchange/hhdangev02", new Document() { Token = "00000", StationID = $"{this.UserID}" });
            MQTT.Broker.Instance.Listen($"dane/service/stationchange/hhdangev02", HandleReceivedData);
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
            ObservableCollection<StationChangeModel> refdoc = new ObservableCollection<StationChangeModel>();
            DocumentList userlist = this.DataResponse.UserList;
            List<string> manager = this.DataResponse.Manager;
            if (userlist != null && manager != null)
            {
                foreach (var item in userlist)
                {
                    foreach (var item2 in manager)
                    {
                        if (item.ObjectId == item2)
                        {
                            refdoc.Add(new StationChangeModel(item.StationName, item.ObjectId, true));
                        }
                        else refdoc.Add(new StationChangeModel(item.StationName, item.ObjectId, false));
                    }
                }
                this.DataGrid = refdoc;
            }
        }
    }
}
