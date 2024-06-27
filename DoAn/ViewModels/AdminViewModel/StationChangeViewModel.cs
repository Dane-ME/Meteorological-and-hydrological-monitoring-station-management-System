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
        public async void SendandListen()
        {
            //await Task.Delay(500);
            MQTT.Broker.Instance.Listen($"dane/service/stationchange/{Service.Instance.UserID}", HandleReceivedData);
            await Task.Delay(50);
            MQTT.Broker.Instance.Send($"dane/service/stationchange/{Service.Instance.UserID}", new Document() { Token = $"{Service.Instance.Token}", UserID = this.UserID });


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
            ObservableCollection<StationChangeModel> refdoc = [];
            DocumentList stationlist = this.DataResponse.StationList;
            List<string> list = new List<string>();
            List<string> ?sattionmanagement = this.DataResponse.StationManagement;

            if (stationlist != null && sattionmanagement != null)
            {
                foreach (Document doc in stationlist)
                {
                    if (doc != null)
                    {
                        list.Add(doc.ObjectId);
                    }
                }
                List<string> common = list.Except(sattionmanagement).ToList();
                foreach (string element in sattionmanagement)
                {
                    refdoc.Add(new StationChangeModel(Find(element, stationlist).StationName, element, true));
                }
                foreach (string i in common)
                {
                    refdoc.Add(new StationChangeModel(Find(i, stationlist).StationName, i, false));
                }
                this.DataGrid = refdoc;
            }
        }
        public Document Find(string id, DocumentList dl)
        {
            Document res = [];
            foreach (Document doc in dl)
            {
                if (doc.ObjectId == id)
                {
                    res = doc; break;
                }
            }
            return res;
        }
    }
}
