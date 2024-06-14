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
        public string Type { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
        public ResponseSender(string type) { this.Type = type; }
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
            Broker.Instance.Send(topic, CreateResponse(objectid));
            Broker.Instance.Listen($"dane/service/{objectid}", doc => 
            {
                Document docs = DB.Station.Find("6868");
                Broker.Instance.Send($"dane/service/home/{objectid}", docs);
            });
        }

    }
}
