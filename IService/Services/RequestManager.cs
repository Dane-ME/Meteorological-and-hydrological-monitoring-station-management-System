using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MQTT;

namespace IService.Services
{
    
    public class RequestManager
    {
        public RequestManager() 
        {
            EventChanged.Instance.LogOutEvent += (s, e) =>
            {
                Broker.Instance.StopListening($"dane/user/logout/{e.UserID}", null);
            };
            EventChanged.Instance.ListenActivedEvent += async (s, e) =>
            {
                //await Task.Delay(100);
                Broker.Instance.Listen($"dane/service/home/{e.UserID}", (doc) => 
                {
                    bool check = JWTcheck(doc, e);
                    if (check) { var repo = new ResponseSender(); repo.HomeResponse(e.UserID); };
                });
                //await Task.Delay(100);
                Broker.Instance.Listen($"dane/service/stationdetail/{e.UserID}", (doc) => { });
                //await Task.Delay(100);
                Broker.Instance.Listen($"dane/service/stationlist/{e.UserID}", (doc) => { });
                //await Task.Delay(100);
                Broker.Instance.Listen($"dane/service/userlist/{e.UserID}", (doc) => { });
                Broker.Instance.Listen($"dane/user/logout/{e.UserID}", (doc) => {
                    //Check Token
                    Broker.Instance.StopListening($"dane/service/home/{e.UserID}", null);
                    Broker.Instance.StopListening($"dane/service/stationdetail/{e.UserID}", null);
                    Broker.Instance.StopListening($"dane/service/stationlist/{e.UserID}", null);
                    Broker.Instance.StopListening($"dane/service/userlist/{e.UserID}", null);
                    EventChanged.Instance.OnLogOutEvent(e.UserID);
                });
            };
        }
        public bool JWTcheck(Document doc, ListenActivedEventArgs e)
        {
            string? token = doc.Token;
            if (token is not null)
            {
                var check = new JWTAuth(token);
                bool res = check.IsJWTValid(e.UserID);
                if (res) { return true; }
                else return false;
            }
            else return false;
        }
        public void IsItStoredandSendResponse(Document doc)
        {
            if (doc.Type == "id")
            {
                var token = new TokenModel();
                if (IsIDStored(doc.UserID))
                {
                    var EncodedPass = DB.User.Find(doc.UserID).EncodePass; 
                    if(doc.EncodePass == EncodedPass)
                    {
                        Document? data = DB.User.Find(doc.UserID);
                        var con = new ResponseSender("id", "1", token.CreateToken(doc, data));
                        con.SendResponse($"dane/login/{doc.UserID}", doc.UserID);
                    }
                    else
                    {
                        Broker.Instance.Send($"dane/login/{doc.UserID}", new Document() { Status = "PasswordIsNotCorrect"});
                    }
                }
                else
                {
                    var con = new ResponseSender("id", "UserNotExist");
                    //con.SendResponse($"usercontroller/login/{doc.ObjectId}", doc.ObjectId);
                    Broker.Instance.Send($"dane/login/{doc.UserID}", con.CreateResponse($"{doc.UserID}"));
                }
            }
            else
            {
                //var con = new ResponseSender("noknown", "0");
                //con.SendResponse($"dane/login/{doc.UserID}", doc.UserID);
            };
        }
        public bool IsIDStored(string objectId)
        {
            if (DB.User.Find(objectId) != null) { return true; }
            else { return false; }
        }
    }

}
