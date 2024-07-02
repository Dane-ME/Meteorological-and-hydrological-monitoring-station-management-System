using DoAn.Models.AdminModel;
using DoAn.Services;
using Microsoft.Maui.Controls;
using MQTT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        public ICommand SaveCommand { get; private set; }
        public ManagerChangeViewModel(string ID)
        {
            DataGrid = new ObservableCollection<ManagerChangeModel>();
            StationID = ID;
            ResponseHandle += OnResponseHandle;
            SendandListen();
            SaveCommand = new Command( () =>
            {
                if(DataGrid != null)
                {
                    Document doc = Changed();
                    doc.StationID = StationID;
                    doc.Token = Service.Instance.Token;
                    Broker.Instance.Send($"dane/decentralization/managerchange/{Service.Instance.UserID}", doc);
                }
            });

        }
        private Document Changed()
        {
            List<string> listchanged = [];
            List<string> listnotchanged = DataResponse.Manager;
            foreach (var item in DataGrid)
            {
                if (item.IsValid == true)
                {
                    listchanged.Add(item.UserID);
                }
            }
            List<string> add = listchanged.Except(listnotchanged).ToList();
            List<string> remove = listnotchanged.Except(listchanged).ToList();

            return new Document() { Add = add, Remove = remove };
        }
        public async void SendandListen()
        {
            Broker.Instance.Listen($"dane/service/managerchange/{Service.Instance.UserID}", HandleReceivedData);
            await Task.Delay(50);
            Broker.Instance.Send($"dane/service/managerchange/{Service.Instance.UserID}", new Document() { Token = $"{Service.Instance.Token}", StationID = this.StationID });
        }
        private void HandleReceivedData(Document doc)
        {
            if (doc != null)
            {
                if(doc.UserList != null)
                {
                    this.DataResponse = doc;
                    OnResponseHandleEvent();
                }
            }
        }
        private void OnResponseHandle()
        {
            ObservableCollection<ManagerChangeModel> refdoc = [];
            DocumentList userlist = this.DataResponse.UserList;
            List<string> list = new List<string>();
            List<string> manager = this.DataResponse.Manager;

            if (userlist != null)
            {
                foreach (Document doc in userlist)
                {
                    if (doc != null)
                    {
                        list.Add(doc.ObjectId);
                    }
                }
                List<string> common = new List<string>();
                if (manager != null)
                {
                    common = list.Except(manager).ToList();
                    foreach (string element in manager)
                    {
                        refdoc.Add(new ManagerChangeModel(Find(element, userlist).UserName, element, true));
                    }
                }
                else common = list;
                foreach (string i in common)
                {
                    refdoc.Add(new ManagerChangeModel(Find(i, userlist).UserName, i, false));
                }
            }
            DataGrid = refdoc;

        }
        private Document Find(string id, DocumentList dl)
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
