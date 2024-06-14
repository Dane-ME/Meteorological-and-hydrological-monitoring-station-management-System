using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTT;

namespace IService.Services
{
    public class RequestManager
    {
        public RequestManager() { }
        public void IsItStoredandSendResponse(Document doc)
        {
            if (doc.Type == "id")
            {
                var token = new TokenModel();
                if (IsIDStored(doc.UserID))
                {
                    Document data = DB.User.Find(doc.UserID);
                    var con = new ResponseSender("id", "1", token.CreateToken(doc, data));
                    con.SendResponse($"dane/login/{doc.UserID}", doc.UserID);
                }
                else
                {
                    var con = new ResponseSender("id", "0");
                    //con.SendResponse($"usercontroller/login/{doc.ObjectId}", doc.ObjectId);
                    Broker.Instance.Send($"dane/login/{doc.UserID}", con.CreateResponse($"{doc.UserID}"));
                }
            }
            else
            {
                var con = new ResponseSender("noknown", "0");
                con.SendResponse($"dane/login/{doc.UserID}", doc.UserID);
            };
        }
        public bool IsTokenStored(string objectId)
        {
            if (DB.Token.Find(objectId) != null) { return true; }
            else { return false; }
        }
        public bool IsIDStored(string objectId)
        {
            if (DB.User.Find(objectId) != null) { return true; }
            else { return false; }
        }
        public void LoginByTokenFail(Document doc)
        {
            var con = new ResponseSender("token", "0");
            con.SendResponse($"usercontroller/login/{doc.Token}", $"{doc.Token}");
        }
    }

}
