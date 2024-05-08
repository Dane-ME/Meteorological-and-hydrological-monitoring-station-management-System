using DoAn.Models;
using DoAn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoAn.ViewModels
{
    class HomeViewModel
    {
        public string StationLocal {  get; set; }
        public string StationName { get; set; }
        public int StationId { get; set; }
        public ICommand StaionLocalCommand { get; private set; }
        public HomeViewModel() 
        {
            GetData();
        }
        public void GetData()
        {
            var SIM = new StationInformationModel();
            this.StationLocal =  SIM.StationLocal;
            this.StationName = SIM.StationName;
            this.StationId = SIM.StationId;
        }

    }
}
