using IService.Core;
using MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IService.ViewModels.Import
{
    public class HydrologicalViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Local { get; set; }
        public string TimeBinding { get; set; }
        public string SeaLevelBinding { get; set; }
        public string WaveHeightBinding { get; set; }
        public string WaveLengthBinding {  get; set; } 
        public string WaveHeightMaxBinding { get; set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SendCommand { get; private set; }
        public HydrologicalViewModel() 
        {
            this.Code = "6868";
            this.Name = "Cảng xăng dầu B12";
            this.Local = "Bãi Cháy, TP Hạ Long, Quảng Ninh";
            Document docs = DB.Station.Find("6868");
            SendCommand = new RelayCommand(execute => 
            {
                Broker.Instance.Send("Dane/Test", docs);
            });
            SaveCommand = new RelayCommand( execute => { 
                if (DB.Station.Find($"{this.Code}") is null)
                {
                    Document doc = new Document() 
                    {
                        ObjectId = this.Code,
                        StationName = this.Name,
                        StationAddress = this.Local,
                        StationType = "Hydrological",
                        StationData = ParaData()
                    };
                    DB.Station.Insert(doc);
                }
                else
                {
                    Document doc = DB.Station.Find($"{this.Code}");
                    int loc = TimeMap(this.TimeBinding);

                    string t = doc.StationData.ToString();

                    if (loc != -1) {
                        doc.StationData.Add(new Document()
                        {
                            Time = this.TimeBinding,
                            SeaLevel = this.SeaLevelBinding,
                            WaveHeight = this.WaveHeightBinding,
                            WaveLength = this.WaveLengthBinding,
                            WaveHeightMax = this.WaveHeightMaxBinding,
                        });  
                    }
                    DB.Station.Update(doc);
                }
            ;});
        }

        public List<Document> ParaData()
        {
            List<Document> data = new List<Document>();
            int loc = TimeMap(this.TimeBinding);
            if ( loc != -1) { data.Add(setParamData()); }
            else throw new FormatException("Time is invalid.");
            return data;
        }
        public Document setParamData()
        {
            return new Document()
            {
                Time = this.TimeBinding,
                SeaLevel = this.SeaLevelBinding,
                WaveHeight = this.WaveHeightBinding,
                WaveLength = this.WaveLengthBinding,
                WaveHeightMax = this.WaveHeightMaxBinding,
            };
        }
        public int TimeMap(string time)
        {
            if (time.Length != 4)
            {
                return -1;
            }
            else
            {
                int input = ConvertStringToInt(time);
                int first = (int)(input / 100);
                int two = (input - (first * 100)) / 10;

                return two + ( 6 * first) ;
            }
        }

        public static int ConvertStringToInt(string input)
        {
            int result;
            if (int.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                throw new FormatException("Input string is not a valid integer.");
            }
        }

    }

}
