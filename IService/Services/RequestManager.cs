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
                await Task.Delay(100);
                string userid = e.UserID;
                var repo = new ResponseSender(userid);
                Broker.Instance.Listen($"dane/service/home/{userid}", (doc) => 
                {
                    bool check = JWTcheck(doc, e);
                    if (check) { repo.HomeResponse(); };
                });
                //await Task.Delay(100);
                Broker.Instance.Listen($"dane/service/stationdetail/{userid}", (doc) => 
                {
                    bool check = JWTcheck(doc, e);
                    string time = TimeFormat.getTimeNow();
                    if (check) { repo.StationDetailReponse(doc.StationID, time); };
                });
                //await Task.Delay(100);
                Broker.Instance.Listen($"dane/service/stationlist/{userid}", (doc) => 
                { 
                });
                //await Task.Delay(100);
                Broker.Instance.Listen($"dane/service/userlist/{userid}", (doc) => 
                { 
                });
                Broker.Instance.Listen($"dane/service/stationprofile/{userid}", (doc) => 
                {
                });
                //await Task.Delay(100);
                Broker.Instance.Listen($"dane/service/userprofile/{userid}", (doc) => 
                {
                });
                Broker.Instance.Listen($"dane/service/managerchange/{userid}", (doc) =>
                {
                });
                //await Task.Delay(100);
                Broker.Instance.Listen($"dane/service/stationchange/{userid}", (doc) =>
                {
                });
                Broker.Instance.Listen($"dane/user/logout/{userid}", (doc) => {
                    //Check Token
                    DB.Token.Delete(userid);
                    Broker.Instance.StopListening($"dane/service/home/{userid}", null);
                    Broker.Instance.StopListening($"dane/service/stationdetail/{userid}", null);
                    Broker.Instance.StopListening($"dane/service/stationlist/{userid}", null);
                    Broker.Instance.StopListening($"dane/service/userlist/{userid}", null);
                    Broker.Instance.StopListening($"dane/service/stationprofile/{userid}", null);
                    Broker.Instance.StopListening($"dane/service/userprofile/{userid}", null);
                    Broker.Instance.StopListening($"dane/service/managerchange/{userid}", null);
                    Broker.Instance.StopListening($"dane/service/stationchange/{userid}", null);
                    EventChanged.Instance.OnLogOutEvent(userid);
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
