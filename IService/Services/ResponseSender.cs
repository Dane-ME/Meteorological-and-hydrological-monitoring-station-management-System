using IService.Core;
using IService.Services;
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
        public string ?Type { get; set; }
        public string ?Status { get; set; }
        public string ?Token { get; set; }
        #endregion
        public string ?userID { get; set; }
        public Document ?profileuser { get; set; }
        public DocumentList ?homedata { get; set; } 
        public ResponseSender(string userid) 
        { 
            userID = userid;
            homedata = new DocumentList();
            if (DB.User.Find(userID) != null)
            {
                profileuser = DB.User.Find(userID);
            }
        }
        public ResponseSender(string type, string status) { this.Type = type; this.Status = status; }
        public ResponseSender(string type, string status, string token) { this.Type = type; this.Status = status; this.Token = token; }
       
        #region LOGIN RESPONSE

        public Document CreateResponse(string objectid)
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

        #endregion

        #region FORGOT PASSWORD RESPONSE

        public void ForgotPasswordResponse()
        {
            string email = DB.User.Find(this.userID).Email;
            if(email != null)
            {
                new MailService(email, this.userID, "Verification").SendMessage();
            }
        }
        public void CreateNewPasswordsResponse()
        {
            string email = DB.User.Find(this.userID).Email;
            if( email != null)
            {
                var mailService = new MailService(email, this.userID, "newPassword");
                mailService.SendMessage();
            }
        }

        #endregion

        #region ADD USER RESPONSE

        public void AddUserResponse(Document doc)
        {
            
        }

        #endregion

        #region HOME REQUEST
        public async void HomeResponse()
        {      
            this.homedata = getHomeData();
            await Task.Delay(100);
            Broker.Instance.Send($"dane/service/home/{this.userID}", new Document() { HomeData = this.homedata });
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
        public DocumentList getHomeData()
        {
            DocumentList hd = new DocumentList();
            foreach (var item in getStationManagement())
            {
                Document ?station = DB.Station.Find(item);
                if(station != null)
                {
                    List<string> type = station.StationTypeList;
                    Func<string, int> convert = (input) => {
                        if (type.Contains($"{input}")) { return 1; }
                        else return 0;
                    };
                    int act = (convert("Hydrological") << 2) | (convert("Meteorological") << 1) | convert("RainFall");
                    switch (act)
                    {
                        case 7: hd.Add(getHydroData(item)); hd.Add(getMeteoData(item)); break; //111
                        case 6: hd.Add(getHydroData(item)); hd.Add(getMeteoData(item)); break; //110
                        case 5: hd.Add(getHydroData(item)); break; //101
                        case 4: hd.Add(getHydroData(item)); break; //100
                        case 3: hd.Add(getMeteoData(item)); break; //011
                        case 2: hd.Add(getMeteoData(item)); break; //010
                        case 1: Console.WriteLine("Ehe"); break; //001
                        case 0: Console.WriteLine("Ehe"); break; //000

                    }
                }
            }
            return hd;
        }
        public Document getMeteoData(string stationid)
        {
            Document repo = new Document();
            Document ?stationprofile = DB.Station.Find(stationid);
            Document ?stationData = MethodHandle.CallMethod(stationid, "Find", $"28062024") as Document;
            if(stationData != null)
            {
                Document? DataNewest = stationData.StationData.Last();
                repo.StationName = stationprofile.StationName;
                repo.StationAddress = stationprofile.StationAddress;
                repo.StationType = "Meteorological";
                repo.WindSpeed = DataNewest.WindSpeed;
                repo.WindSpeedAt2mHeight = DataNewest.WindSpeedAt2mHeight;
                repo.TimeOfOccurrenceOffxfx2m = DataNewest.TimeOfOccurrenceOffxfx2m;
                repo.AverageWindSpeedIn2s = DataNewest.AverageWindSpeedIn2s;
                repo.WindDirection = DataNewest.WindDirection;
                repo.WindDirectionAt2mHeight = DataNewest.WindDirectionAt2mHeight;
                repo.AverageWindDirectionIn2s = DataNewest.AverageWindDirectionIn2s;
                repo.TimeOfOccurrenceOffxfx2s = DataNewest.TimeOfOccurrenceOffxfx2s;
            }
            return repo;
        }
        public Document getHydroData(string stationid)
        {
            Document repo = new Document();
            Document? stationprofile = DB.Station.Find(stationid);
            Document? stationData = MethodHandle.CallMethod(stationid, "Find", $"28062024") as Document;
            if( stationData != null ) 
            {
                Document? DataNewest = stationData.StationData.Last();
                repo.StationName = stationprofile.StationName;
                repo.StationAddress = stationprofile.StationAddress;
                repo.StationType = "Hydrological";
                repo.SeaLevel = DataNewest.SeaLevel;
                repo.WaveHeight = DataNewest.WaveHeight;
                repo.WaveLength = DataNewest.WaveLength;
                repo.WaveHeightMax = DataNewest.WaveHeightMax;
            }
            
            return repo;
        }
        #endregion

        #region STATIONDETAIL RESPONSE

        public async void StationDetailReponse(string stationid, string time) 
        {
            Document ?stationProfile = DB.Station.Find(stationid);
            Document ?stationData = MethodHandle.CallMethod(stationid, "Find", "28062024") as Document;
            Document response = new Document()
            {
                StationName = stationProfile.StationName,
                StationID = stationProfile.StationID,
                StationAddress = stationProfile.StationAddress,
                StationTypeList = stationProfile.StationTypeList,
                Time = time,
                StationData = stationData.StationData
            };
            await Task.Delay(100);
            Broker.Instance.Send($"dane/service/stationdetail/{this.userID}", response);
        }

        #endregion

        #region USER RESPONSE

        public void UserResponse()
        {
            Document ?doc = DB.User.Find(this.userID);
            List<string> stationmanagement = doc.StationManagement;
            DocumentList dl = new DocumentList();
            
            foreach(var item in stationmanagement)
            {
                foreach (var item2 in StationList())
                {
                    if(item2.ObjectId == item)
                    {
                        dl.Add(item2);
                    }
                }
            }

            Document repo = new Document()
            {
                ObjectId = doc.ObjectId,
                UserName = doc.UserName,
                Role = doc.Role,
                Email = doc.Email,
                WorkingUnit = doc.WorkingUnit,
                Position = doc.Position,
                RegisDate = doc.RegisDate,
                StationList = dl
            };
            Broker.Instance.Send($"dane/service/user/{this.userID}", repo);
        }

        #endregion

        #region USERLIST RESPONSE

        public void UserListReponse()
        {
            DocumentList repo = UserList(); 
            Broker.Instance.Send($"dane/service/userlist/{this.userID}", new Document() { UserList = repo });
        }

        public DocumentList UserList()
        {
            DocumentList repo = new DocumentList();
            DocumentList? doclist = DB.User.SelectAll();
            foreach (var item in doclist)
            {
                repo.Add(new Document()
                {
                    ObjectId = item.ObjectId,
                    UserName = item.UserName
                });
            }
            return repo;
        }

        #endregion

        #region USERPROFILE RESPONSE

        public void UserProfileResponse(string userid)
        {
            Document ?doc = DB.User.Find(userid);
            Document repo = new Document();
            if(doc != null)
            {
                repo.UserName = doc.UserName;
                repo.Role = doc.Role;
                repo.Email = doc.Email;
                repo.WorkingUnit = doc.WorkingUnit;
                repo.Position = doc.Position;
                repo.RegisDate = doc.RegisDate;
                repo.StationManagement = doc.StationManagement;
            }
            Broker.Instance.Send($"dane/service/userprofile/{this.userID}", repo);
        }

        #endregion

        #region MANAGERCHANGE RESPONSE

        public void ManagerChangeResponse(string stationid)
        {
            DocumentList userlist = UserList();
            Document ?manager = DB.Station.Find(stationid);

            Document repo = new Document() 
            {
                UserList = userlist,
                Manager = manager.Manager
            };
            Broker.Instance.Send($"dane/service/managerchange/{this.userID}", repo );
        }

        #endregion

        #region STATIONLIST RESPONSE

        public void StationListResponse()
        {
            DocumentList repo = StationList();
            Broker.Instance.Send($"dane/service/stationlist/{this.userID}", new Document() { StationList = repo });
        }

        public DocumentList StationList()
        {
            DocumentList repo = new DocumentList();
            DocumentList? doclist = DB.Station.SelectAll();
            foreach (var item in doclist)
            {
                repo.Add(new Document()
                {
                    ObjectId = item.ObjectId,
                    StationName = item.StationName
                });
            }
            return repo;
        }

        #endregion

        #region STATIONPROFILE RESPONSE

        public void StationProfileResponse(string stationid)
        {
            Document? doc = DB.Station.Find(stationid);
            Document repo = new Document();
            if (doc != null)
            {
                repo.StationName = doc.StationName;
                repo.StationAddress = doc.StationAddress;
                repo.StationTypeList = doc.StationTypeList;
                repo.Manager = doc.Manager;
            }
            Broker.Instance.Send($"dane/service/stationprofile/{this.userID}", repo);
        }

        #endregion

        #region STATIONCHANGE RESPONSE

        public void StationChangeResponse(string userid)
        {
            DocumentList stationlist = StationList();
            Document? stationManagement = DB.User.Find(userid);

            Document repo = new Document()
            {
                StationList = stationlist,
                StationManagement = stationManagement.StationManagement
            };
            Broker.Instance.Send($"dane/service/stationchange/{this.userID}", repo);
        }

        #endregion

        #region CHANGE PASSWORD RESPONSE

        public void ChangePasswordResponse()
        {

        }

        #endregion

    }
}
namespace System
{
    public partial class Document
    {
        public string ContentResponse { get => GetString(nameof(ContentResponse)); set => Push(nameof(ContentResponse), value); }
    }
}
