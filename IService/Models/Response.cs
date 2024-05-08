using MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public partial class Document
    {
        public string Status { get => GetString(nameof(Status)); set => Push(nameof(Status), value); }
    } 
    public class Response
    {
        public string Type { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
        public Response(string type) { this.Type = type; }
        public Response(string type, string status) { this.Type = type; this.Status = status; }
        public Response(string type, string status, string token) { this.Type = type; this.Status = status; this.Token = token; }  
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
        }

    }
}
