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
        public async Task SendandListen()
        {
            await Task.Delay(1000);
            MQTT.Broker.Instance.Send($"dane/service/managerchange/hhdangev02", new Document() { Token = "00000", StationID = $"{this.StationID}" });
            MQTT.Broker.Instance.Listen($"dane/service/managerchange/hhdangev02", HandleReceivedData);
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
            ObservableCollection<ManagerChangeModel> refdoc = new ObservableCollection<ManagerChangeModel>();
            DocumentList userlist = this.DataResponse.UserList;
            List<string> manager = this.DataResponse.Manager;
            if (userlist != null && manager != null)
            {
                foreach(var item in userlist) 
                {
                    foreach(var item2 in manager)
                    {
                        if(item.ObjectId == item2)
                        {
                            refdoc.Add(new ManagerChangeModel(item.UserName, item.ObjectId, true));
                        }
                        else refdoc.Add(new ManagerChangeModel(item.UserName, item.ObjectId, false));
                    }
                }
                this.DataGrid = refdoc;
            }
        }
    }
}
