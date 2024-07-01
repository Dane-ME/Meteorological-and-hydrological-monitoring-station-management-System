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
            MQTT.Broker.Instance.Listen($"dane/service/stationchange/{Service.Instance.UserID}", HandleReceivedData);
            await Task.Delay(50);
            MQTT.Broker.Instance.Send($"dane/service/stationchange/{Service.Instance.UserID}", new Document() { Token = $"{Service.Instance.Token}", UserID = this.UserID });
        }
        private void HandleReceivedData(Document doc)
        {
            if (doc != null)
            {
                if(doc.StationList != null)
                {
                    this.DataResponse = doc;
                    OnResponseHandleEvent();
                }
            }
        }
        private void OnResponseHandle()
        {
            ObservableCollection<StationChangeModel> refdoc = [];
            DocumentList stationlist = this.DataResponse.StationList;
            List<string> list = new List<string>();
            List<string> ?stationmanagement = this.DataResponse.StationManagement;

            if (stationlist != null)
            {
                foreach (Document doc in stationlist)
                {
                    if (doc != null)
                    {
                        list.Add(doc.ObjectId);
                    }
                }
                List<string> common = new List<string>();
                if (stationmanagement != null)
                {
                    common = list.Except(stationmanagement).ToList();
                    foreach (string element in stationmanagement)
                    {
                        refdoc.Add(new StationChangeModel(Find(element, stationlist).StationName, element, true));
                    }

                }
                else common = list;
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
