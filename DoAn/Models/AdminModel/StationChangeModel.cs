using DoAn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Models.AdminModel
{
    public class StationChangeModel : ObservableObject
    {
        private string _stationname;
        private string _stationid;
        private bool _isvalid;

        public string StationName
        {
            get => _stationname;
            set => SetProperty(ref _stationname, value);
        }
        public string StationID
        {
            get => _stationid;
            set => SetProperty(ref _stationid, value);
        }
        public bool IsValid
        {
            get => _isvalid;
            set => SetProperty(ref _isvalid, value);
        }
        public StationChangeModel( string stationname, string stationid, bool isvalid )
        {
            StationName = stationname;
            StationID = stationid;
            IsValid = isvalid;
        }
    }
}
