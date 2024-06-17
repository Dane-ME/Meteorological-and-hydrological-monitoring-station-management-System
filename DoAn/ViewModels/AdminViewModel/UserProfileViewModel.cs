using System;
using System.Collections.Generic;
using System.Linq;
using MQTT;
using System.Threading.Tasks;
using DoAn.Models.AdminModel;
using System.Collections.ObjectModel;
using DoAn.Services;

namespace DoAn.ViewModels.AdminViewModel
{
    public class UserProfileViewModel : ObservableObject
    {
        public string _idd;
        public string ID
        {
            get => _idd;
            set
            {
                if (_idd != value)
                {
                    if (value != null)
                    {
                        EventChanged.Instance.OnUserIDChanged();
                        _idd = value;
                    }
                }
            }
        }

        public ObservableCollection<UserProfileModel> _station = new ObservableCollection<UserProfileModel>();
        public ObservableCollection<UserProfileModel> station
        {
            get => _station;
            set => SetProperty(ref _station, value);
        }
        public List<string> Station { get; set; }

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
        public string _role;
        public string Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }
        public string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string _workingUnit;
        public string WorkingUnit
        {
            get => _workingUnit;
            set => SetProperty(ref _workingUnit, value);
        }
        public string _regisDate;
        public string RegisDate
        {
            get => _regisDate;
            set => SetProperty(ref _regisDate, value);
        }
        public string _position;
        public string Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        public UserProfileViewModel() 
        {
            Station = new List<string>();
            EventChanged.Instance.UserIDChanged += (s, e) =>
            {
                if (station.Count == 0)
                {
                    SendRequest();
                    ListenResponse();
                }
                else
                {
                    SendRequest();
                }
            };
            EventChanged.Instance.UserProfileChanged += (s, e) => 
            {
                Id = ID;

            };
        }
        public void SendRequest()
        {
            Broker.Instance.Send($"dane/service/userprofile/hhdangev02", new Document() { ObjectId = $"{ID}", Token = "00000" });
        }
        public void ListenResponse()
        {
            Broker.Instance.Listen($"dane/service/userprofile/hhdangev02", (doc) =>
            {
                if (doc != null)
                {
                    ObservableCollection<UserProfileModel> list = new ObservableCollection<UserProfileModel>();
                    this.Name = doc.UserName;
                    this.Role = doc.role;
                    this.Email = doc.Email;
                    this.WorkingUnit = doc.WorkingUnit;
                    this.Position = doc.Position;
                    this.RegisDate = doc.RegisDate;
                    foreach (var i in doc.Station)
                    {
                        list.Add(new UserProfileModel() { Station = i });
                    }
                    station = list;
                    EventChanged.Instance.OnUserProfileChanged();
                    //EventChanged.Instance.OnStationListChanged();
                }
            });
        }
    }

}
