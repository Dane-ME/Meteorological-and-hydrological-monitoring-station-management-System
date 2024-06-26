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
        public string Type { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
        #endregion
        public string userID { get; set; }
        public Document ?profileuser { get; set; }
        public DocumentList homedata { get; set; } 
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
            Document ?stationprofile = DB.Station.Find(stationid);
            Document ?stationData = MethodHandle.CallMethod(stationid, "Find", $"{TimeFormat.getTimeNow()}") as Document;
            Document ?DataNewest = stationData.StationData.Last();
            return new Document()
            {
                StationName = stationprofile.StationName,
                StationAddress = stationprofile.StationAddress,
                StationType = "Meteorological",
                WindSpeed = DataNewest.WindSpeed,
                WindSpeedAt2mHeight = DataNewest.WindSpeedAt2mHeight,
                AverageWindSpeedIn2s = DataNewest.AverageWindSpeedIn2s,
                WindDirection = DataNewest.WindDirection,
                WindDirectionAt2mHeight = DataNewest.WindDirectionAt2mHeight,
                AverageWindDirectionIn2s = DataNewest.AverageWindDirectionIn2s
            };
        }
        public Document getHydroData(string stationid)
        {
            Document? stationprofile = DB.Station.Find(stationid);
            Document? stationData = MethodHandle.CallMethod(stationid, "Find", $"{TimeFormat.getTimeNow()}") as Document;
            Document? DataNewest = stationData.StationData.Last();
            return new Document()
            {
                StationName = stationprofile.StationName,
                StationAddress = stationprofile.StationAddress,
                StationType = "Hydrological",
                SeaLevel = DataNewest.SeaLevel,
                WaveHeight = DataNewest.WaveHeight,
                WaveLength = DataNewest.WaveLength,
                WaveHeightMax = DataNewest.WaveHeightMax,
            };
        }
        #endregion

        #region STATIONDETAIL RESPONSE

        public async void StationDetailReponse(string stationid, string time) 
        {
            Document ?stationProfile = DB.Station.Find(stationid);
            Document ?stationData = MethodHandle.CallMethod(stationid, "Find", time) as Document;
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

    }
}
