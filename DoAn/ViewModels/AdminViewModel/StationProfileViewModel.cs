﻿using DoAn.Models.AdminModel;
using DoAn.Services;
using DoAn.Views.AdminView;
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
    public class StationProfileViewModel : ObservableObject
    {
        public string IDpara { get; set; }
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
        private View _editmanagerView;
        public View editManagerView 
        { 
            get => _editmanagerView; 
            set
            {
                _editmanagerView = value;
                OnPropertyChanged();
            } 
        }
        public ICommand EditCommand {  get; set; }
        private EventHandler _stationIDChangedHandler;
        public StationProfileViewModel(string stationid) 
        {
            this.IDpara = stationid;
            Type = new List<string>();
            Manager = new List<string>();
            List<string> list = Broker.Instance.topicCallbacks.Keys.ToList();
            if (!list.Contains($"dane/service/stationprofile/{Service.Instance.UserID}"))
            {
                Broker.Instance.StopListening($"dane/service/stationprofile/{Service.Instance.UserID}", null);
            }
            ListenResponse();
            SendRequest();
            EditCommand = new Command(() =>
            {
                editManagerView = new ManagerChangeView(IDpara);
            });
            

            EventChanged.Instance.StationProfileChanged += (s, e) => 
            {
                foreach (var i in type)
                {
                    this.Type.Add(i.Type);
                }
            };

        }
        
        public void SendRequest() 
        {
            Broker.Instance.Send($"dane/service/stationprofile/{Service.Instance.UserID}", new Document() { StationID = $"{IDpara}", Token = $"{Service.Instance.Token}" });
        }
        public void ListenResponse() 
        {
            Broker.Instance.Listen($"dane/service/stationprofile/{Service.Instance.UserID}", (doc) =>
            {
                if (doc != null)
                {
                    if(doc.StationName != null)
                    {
                        ObservableCollection<StationProfileModel> list = new ObservableCollection<StationProfileModel>();
                        ObservableCollection<StationProfileModel> list2 = new ObservableCollection<StationProfileModel>();
                        this.Name = doc.StationName;
                        this.Address = doc.StationAddress;
                        if (doc.StationTypeList != null && doc.Manager != null)
                        {
                            foreach (var i in doc.StationTypeList)
                            {
                                list.Add(new StationProfileModel() { Type = i });
                            }
                            foreach (var i in doc.Manager)
                            {
                                list2.Add(new StationProfileModel() { Manager = i });
                            }
                        }
                        type = list;
                        manager = list2;
                        Id = IDpara;
                    }
                }
            });
        }

    }
}
