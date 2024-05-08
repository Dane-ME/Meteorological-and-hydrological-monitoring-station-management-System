using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.File
{
    class CacheData
    {
        public static CacheData instance;
        public static CacheData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CacheData();
                }
                return instance;
            }
        }

    }
}
