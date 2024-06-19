using MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.ViewModels
{
    public class ListViewModel
    {
        public ListViewModel() 
        {
            Broker.Instance.Send("dane/service/home/hhdangev02", new Document() { Token = "TEST"});
        }
    }
}
