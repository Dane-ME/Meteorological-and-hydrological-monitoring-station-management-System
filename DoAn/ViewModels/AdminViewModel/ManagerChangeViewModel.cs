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
    public class ManagerChangeViewModel : ObservableObject
    {
        public string StationID { get; set; }
        public Document DataResponse { get; set; }
        public event Action ResponseHandle;
        private ObservableCollection<ManagerChangeModel> _datagrid;
        public ObservableCollection<ManagerChangeModel> DataGrid
        { get => _datagrid; set => SetProperty(ref _datagrid, value); }
        protected virtual void OnResponseHandleEvent()
        {
            ResponseHandle?.Invoke();
        }
        public ManagerChangeViewModel(string ID)
        {
            DataGrid = new ObservableCollection<ManagerChangeModel>();
            StationID = ID;
            ResponseHandle += OnResponseHandle;
            SendandListen();

        }
        public async void SendandListen()
        {
            //await Task.Delay(500);
            MQTT.Broker.Instance.Listen($"dane/service/managerchange/{Service.Instance.UserID}", HandleReceivedData);
            await Task.Delay(50);
            MQTT.Broker.Instance.Send($"dane/service/managerchange/{Service.Instance.UserID}", new Document() { Token = $"{Service.Instance.Token}", StationID = this.StationID });

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
            ObservableCollection<ManagerChangeModel> refdoc = [];
            DocumentList userlist = this.DataResponse.UserList;
            List<string> list = new List<string>();
            List<string> manager = this.DataResponse.Manager;

            if (userlist != null && manager != null)
            {
                foreach (Document doc in userlist)
                {
                    if (doc != null)
                    {
                        list.Add(doc.ObjectId);
                    }
                }
                List<string> common = list.Except(manager).ToList();
                foreach (string element in manager)
                {
                    refdoc.Add(new ManagerChangeModel(Find(element, userlist).UserName, element, true));
                }
                foreach (string i in common)
                {
                    refdoc.Add(new ManagerChangeModel(Find(i, userlist).UserName, i, false));
                }
            }
            DataGrid = refdoc;

        }
        public Document Find(string id, DocumentList dl)
        {
            Document res = [];
            foreach(Document doc in dl)
            {
                if(doc.ObjectId == id)
                {
                    res = doc; break;
                }
            }
            return res;
        }
    }
}
