using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Core
{
    class TopicFormat
    {
        public static TopicFormat instance;
        public static TopicFormat Instance 
        {
            get {
                if (instance == null)
                {
                    instance = new TopicFormat();
                }
                return instance;
            }
        }

        public string getTopic(string ControllerName, string ActionName, string ID)
        {
            string Topic = ControllerName +  '/' + ActionName + '/' + ID;
            return Topic;
        }
    }
}
