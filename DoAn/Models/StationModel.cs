using DoAn.Services;
using MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Models
{
    public partial class StationModel
    {
        public bool isLoaded { get; set; }
        public string Token { get; set; }
        public string Account { get; set; }
        private readonly AuthService _authService;
        public StationModel(AuthService authService)
        {
            _authService = authService;
        }
        public async Task<bool> IsLoadedData()
        {
            if (this.isLoaded) { await Task.Delay(1000); return true; }
            else await Task.Delay(1000); return false;
        }
        public void SendRequest(string content)
        {
            Broker.Instance.Send($"dane/service/{_authService.Account}", new Document()
            {
                Token = _authService.Token,
                Content = content
            });
            Broker.Instance.Listen($"dane/service/home/hhdangev02", (doc) =>
            {
                if (doc != null)
                {
                    this.isLoaded = true;
                    this.Name = doc.StationName;
                    this.Local = doc.StationAddress;
                    this.Type = doc.StationType;
                    if (this.Type == "Hydrological")
                    {
                        this.data = doc.StationData;
                    }
                }
            });
            
        }

        public void getData()
        {
            Broker.Instance.Listen($"dane/service/home/hhdangev02", (doc) =>
            {
                if(doc != null) { 
                    _authService.isLoaded = true;
                    this.Name = doc.StationName;
                    this.Local = doc.StationAddress;
                    this.Type = doc.StationType;
                    if (this.Type == "Hydrological")
                    {
                        this.data = doc.StationData;
                    }
                }
            });
        }
    }

    public class RecordList : DocumentList
    {
        public event Action<Document> OnAdded;
        public void Receive(Document doc)
        {
            Add(doc);
            OnAdded?.Invoke(doc);
        }
    }
    public partial class StationModel
    {
        public string time { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Local { get; set; }
        public string Type { get; set; }
        public RecordList data { get; set; }
        public string seaLevel { get; set; }
        public string waveHeight { get; set; }
        public string waveLength { get; set; }
        public string waveHeightMax { get; set; }
        public string seaWaterTemperature { get; set; }
        public string seaSaltLevel { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string windSpeedAt2mHeight { get; set; }
        public string windDirectionAt2mHeight { get; set; }
        public string timeOfOccurrenceOffxfx2m { get; set; }
        public string averageWindSpeedIn2s { get; set; }
        public string averageWindDirectionIn2s { get; set; }
        public string timeOfOccurrenceOffxfx2s { get; set; }
        public string batteryIndex { get; set; }
        public string waterLevel { get; set; }
        public string rainfallIn10min { get; set; }
        public string rainfallIn24hour { get; set; }
        public string distantViewOfTheSea { get; set; }
        public string airTemp { get; set; }
        public string airTempMax { get; set; }
        public string airTempMin { get; set; }
        public string airHumidity { get; set; }
        public string airHumidityMin { get; set; }
        public string installationDate { get; set; }
        public string seaLevelAirPressureMax { get; set; }
        public string stationLevelAirPressure { get; set; }
        public string seaLevelAirPressureMin { get; set; }
        public string radiationIntensity { get; set; }

    }
}
