using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.ViewModels
{
    public class LoadingViewModel
    {
        private readonly StationModel _stationModel;
        public LoadingViewModel(StationModel stationModel)
        {
            _stationModel = stationModel;
            _stationModel.SendRequest("home");
        }
    }
}
