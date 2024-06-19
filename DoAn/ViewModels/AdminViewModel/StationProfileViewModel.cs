using DoAn.Models.AdminModel;
using DoAn.Services;
using MQTT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.ViewModels.AdminViewModel
{
    public class StationProfileViewModel : ObservableObject
    {
        public string _idd;
        public string ID 
        {
            get => _idd; 
            set
            {
                if(_idd != value)
                {
                    if(value != null)
                    {
                        EventChanged.Instance.OnStationIDChanged();
                        _idd = value;
                    }
                }
            } 
        }
        #region INIT ObservableCollection
        public ObservableCollection<StationProfileModel> _manager = new ObservableCollection<StationProfileModel>();
        public ObservableCollection<StationProfileModel> manager
        {
            get => _manager;
            set => SetProperty(ref _manager, value);
        }

        public ObservableCollection<StationProfileModel> _type = new ObservableCollection<StationProfileModel>();
        public ObservableCollection<StationProfileModel> type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public List<string> Type { get; set; }
        public List<string> Manager { get; set; }

        public string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public string _adr;
        public string Address
        {
            get => _adr;
            set => SetProperty(ref _adr, value);
        }
        #endregion

        public StationProfileViewModel() 
        {
            Type = new List<string>();
            Manager = new List<string>();

            EventChanged.Instance.StationIDChanged += (s, e) =>
            {
                if(manager.Count == 0 && type.Count == 0)
                {
                    SendRequest();
                    ListenResponse();
                }
                else
                {
                    SendRequest();
                }
            };
            EventChanged.Instance.StationProfileChanged += (s, e) => 
            {
                Id = ID;
                foreach (var i in type)
                {
                    this.Type.Add(i.Type);
                }
            };

        }

        public void SendRequest() 
        {
            Broker.Instance.Send($"dane/service/stationprofile/hhdangev02", new Document() { ObjectId = $"{ID}", Token = "00000" });
        }
        public void ListenResponse() 
        {
            Broker.Instance.Listen($"dane/service/stationprofile/hhdangev02", (doc) =>
            {
                if (doc != null)
                {
                    ObservableCollection<StationProfileModel> list = new ObservableCollection<StationProfileModel>();
                    ObservableCollection<StationProfileModel> list2 = new ObservableCollection<StationProfileModel>();
                    this.Name = doc.StationName;
                    this.Address = doc.StationAddress;
                    foreach (var i in doc.StationTypeList)
                    {
                        list.Add(new StationProfileModel() { Type = i });
                    }
                    foreach (var i in doc.Manager)
                    {
                        list2.Add(new StationProfileModel() { Manager = i });
                    }
                    type = list;
                    manager = list2;
                    EventChanged.Instance.OnStationProfileChanged();
                    //EventChanged.Instance.OnStationListChanged();
                }
            });
        }

    }
}
