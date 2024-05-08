using MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Core
{
    class TokenMQTT
    {
        public string id { get; private set; } 
        public string topic { get; set; }
        public TokenMQTT(string id) 
        {
            this.id = id; //Token code
            topic = TopicFormat.Instance.getTopic("usercontroller", "login", id);
        }
        //public string getDeviceInfo() 
        //{
        //    string Model = DeviceInfo.Current.Model;
        //    string Name = DeviceInfo.Current.Name;
        //    return Model + Name;
        //}
        //public Document CreateTokenContent()
        //{
        //    DateTime curTime = DateTime.Now;
        //    Document doc = new Document()
        //    {
        //        ObjectId = this.id,
        //        DeviceInfo = getDeviceInfo(),
        //        Time = curTime.ToString(),
        //    };
        //    return doc;
        //}
        public void Send(Document content)
        {
            Broker.Instance.Send(topic, content);
        }

    }
}
