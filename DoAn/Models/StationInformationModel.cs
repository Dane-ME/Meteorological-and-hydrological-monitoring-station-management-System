using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Models
{
    public partial class StationInformationModel
    {
        public string StationName { get; set; }
        public int StationId { get; set; }
        public string StationLocal { get; set; }
        public StationInformationModel() 
        {
            this.StationName = "CẢNG XĂNG DẦU B12";
            this.StationId = 6868;
            this.StationLocal = "Bãi Cháy, TP Hạ Long, Quảng Ninh";
        }


    }
}
