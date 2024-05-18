using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Services
{
    public class Service
    {
        public static Service instance;
        public static Service Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Service();
                }
                return instance;
            }
        }
        public bool LoginState { get; set; }
        public async Task<bool> IsLoginAsync()
        {
            if (this.LoginState) { await Task.Delay(1000); return true; }
            else await Task.Delay(1000); return false;
        }
    }
}
