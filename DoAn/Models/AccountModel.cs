using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Models
{
    class AccountModel
    {
        public string Account {  get; set; }
        public string PassWord { get; set; }

        public AccountModel() 
        {
            this.Account = "hhdangev02";
            this.PassWord = "admin";
        }   

    }
}
