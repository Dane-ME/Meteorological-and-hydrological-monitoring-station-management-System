using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Services
{
    public class TimeFormat
    {
        public static string getTimeNow()
        {
            DateTime now = DateTime.Now;
            string time = now.ToString("ddMMyyyy");
            return time;
        }
    }
}
