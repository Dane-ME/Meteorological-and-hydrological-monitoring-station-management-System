using IService.Core;
using MQTT;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public partial class Document
    {
        public string Status { get => GetString(nameof(Status)); set => Push(nameof(Status), value); }
    } 
    public class ResponseSender
    {
        #region Token
        public string Type { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
        #endregion
        public string userID { get; set; }
        private Document ?profileuser { get; set; }
        public ResponseSender(string userid) 
        { 
            userID = userid; 
            if(DB.User.Find(userID) != null)
            {
                profileuser = DB.User.Find(userID);
            }
        }
        public ResponseSender(string type, string status) { this.Type = type; this.Status = status; }
        public ResponseSender(string type, string status, string token) { this.Type = type; this.Status = status; this.Token = token; }  
        public Document CreateResponse( string objectid ) 
        {
            Document content = new Document() 
            {
                Type = this.Type,
                Status = this.Status,
                Token = this.Token
            };
            return content;
        }
        public void SendResponse(string topic, string objectid)
        {
            Task.Delay(500);
            Broker.Instance.Send(topic, CreateResponse(objectid));
            EventChanged.Instance.OnListenActivedEvent(objectid);
        }
        public void HomeResponse()
        {             
            DocumentList homedata = new DocumentList();
            
        }
        public List<string> getStationManagement()
        {
            List<string> stationmanagement = new List<string>();
            if (profileuser != null)
            {
                stationmanagement = profileuser.StationManagement;
            }
            return stationmanagement;
        }
        public Document getDataStation()
        {
            Func<string, int> convert = (input) => {
                if (dl.Contains($"{input}")) { return 1; }
                else return 0;
            };
            int act = (convert("Hydrological") << 2) | (convert("Meteorological") << 1) | convert("RainFall");
            switch (act)
            {
                case 7: break;  // 111
                case 6: break; //110
                case 5: break; //101
                case 4: break; //100
                case 3: break; //011
                case 2: break; //010
                case 1: Console.WriteLine(); break; //001
                case 0: Console.WriteLine("Ehe"); break; //000

            }
            foreach (var item in getStationManagement())
            {
                Document ?station = DB.Station.Find(item);
                if(station != null)
                {
                    List<string> type = station.StationTypeList;

                }
            }
        }

    }
}
