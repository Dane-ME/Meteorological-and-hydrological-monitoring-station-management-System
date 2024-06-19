using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Models
{
    public class StationDetailModel
    {
        public string Time { get; set; }

        public double Values { get; set; }

        public StationDetailModel(string xValue, double yValue)
        {
            Time = xValue;
            Values = yValue;
        }
    }
}
