using BsonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    class Time
    {
        public string getCurrentTime() 
        {
            DateTime currentTime = DateTime.Now;
            return currentTime.ToString();
        }
        public string calEndTime()
        {
            DateTime oldTime = DateTime.Parse(getCurrentTime());
            DateTime endTime = oldTime.AddDays(2);
            return endTime.ToString();
        }
    }
    class File : Time
    {

        static File instance;   
        static public File Instance {
            get { 
                if (instance == null)
                {
                    instance = new File();
                }
                return instance;
            }
        }
    }

}

