using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public partial class Document
    {
        public string SeaLevel { get => GetString(nameof(SeaLevel)); set => Push(nameof(SeaLevel), value); }
        public string WaveHeight { get => GetString(nameof(WaveHeight)); set => Push(nameof(WaveHeight), value); }
        public string WaveLength { get => GetString(nameof(WaveLength)); set => Push(nameof(WaveLength), value); }
        public string WaveHeightMax { get => GetString(nameof(WaveHeightMax)); set => Push(nameof(WaveHeightMax), value); }
        public string SeaWaterTemperature { get => GetString(nameof(SeaWaterTemperature)); set => Push(nameof(SeaWaterTemperature), value); }
        public string SeaSaltLevel { get => GetString(nameof(SeaSaltLevel)); set => Push(nameof(SeaSaltLevel), value); }
        public string WindSpeed { get => GetString(nameof(WindSpeed)); set => Push(nameof(WindSpeed), value); }
        public string WindDirection { get => GetString(nameof(WindDirection)); set => Push(nameof(WindDirection), value); }
        public string WindSpeedAt2mHeight { get => GetString(nameof(WindSpeedAt2mHeight)); set => Push(nameof(WindSpeedAt2mHeight), value); }
        public string WindDirectionAt2mHeight { get => GetString(nameof(WindDirectionAt2mHeight)); set => Push(nameof(WindDirectionAt2mHeight), value); }
        public string TimeOfOccurrenceOffxfx2m { get => GetString(nameof(TimeOfOccurrenceOffxfx2m)); set => Push(nameof(TimeOfOccurrenceOffxfx2m), value); }
        public string AverageWindSpeedIn2s { get => GetString(nameof(AverageWindSpeedIn2s)); set => Push(nameof(AverageWindSpeedIn2s), value); }
        public string AverageWindDirectionIn2s { get => GetString(nameof(AverageWindDirectionIn2s)); set => Push(nameof(AverageWindDirectionIn2s), value); }
        public string TimeOfOccurrenceOffxfx2s { get => GetString(nameof(TimeOfOccurrenceOffxfx2s)); set => Push(nameof(TimeOfOccurrenceOffxfx2s), value); }
        public string BatteryIndex { get => GetString(nameof(BatteryIndex)); set => Push(nameof(BatteryIndex), value); }
        public string WaterLevel { get => GetString(nameof(WaterLevel)); set => Push(nameof(WaterLevel), value); }
        public string RainfallIn10min { get => GetString(nameof(RainfallIn10min)); set => Push(nameof(RainfallIn10min), value); }
        public string RainfallIn24hour { get => GetString(nameof(RainfallIn24hour)); set => Push(nameof(RainfallIn24hour), value); }
        public string DistantViewOfTheSea { get => GetString(nameof(DistantViewOfTheSea)); set => Push(nameof(DistantViewOfTheSea), value); }
        public string AirTemp { get => GetString(nameof(AirTemp)); set => Push(nameof(AirTemp), value); }
        public string AirTempMax { get => GetString(nameof(AirTempMax)); set => Push(nameof(AirTempMax), value); }
        public string AirTempMin { get => GetString(nameof(AirTempMin)); set => Push(nameof(AirTempMin), value); }
        public string AirHumidity { get => GetString(nameof(AirHumidity)); set => Push(nameof(AirHumidity), value); }
        public string AirHumidityMin { get => GetString(nameof(AirHumidityMin)); set => Push(nameof(AirHumidityMin), value); }
        public string InstallationDate { get => GetString(nameof(InstallationDate)); set => Push(nameof(InstallationDate), value); }
        public string SeaLevelAirPressureMax { get => GetString(nameof(SeaLevelAirPressureMax)); set => Push(nameof(SeaLevelAirPressureMax), value); }
        public string StationLevelAirPressure { get => GetString(nameof(StationLevelAirPressure)); set => Push(nameof(StationLevelAirPressure), value); }
        public string SeaLevelAirPressureMin { get => GetString(nameof(SeaLevelAirPressureMin)); set => Push(nameof(SeaLevelAirPressureMin), value); }
        public string RadiationIntensity { get => GetString(nameof(RadiationIntensity)); set => Push(nameof(RadiationIntensity), value); }

    }
}
