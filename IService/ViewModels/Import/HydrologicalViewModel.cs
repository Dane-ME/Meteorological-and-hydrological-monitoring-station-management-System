using IService.Core;
using System.Reflection;
using MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IService.Services;

namespace IService.ViewModels.Import
{
    public class HydrologicalViewModel
    {
        public string Code { get; set; }
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
            SendCommand = new RelayCommand(execute =>
            {
                Document dt = new Document()
                {
                    ObjectId = "17062024",
                };
                string code = this.Code;
                
                object t = MethodHandle.CallMethod("6868", "Find", "17062024");
            });
            SaveCommand = new RelayCommand( execute => {
                object obj = MethodHandle.CallMethod($"{this.Code}", "Find", "14062024");
                DocumentList doclist = new DocumentList();

                Document docs = new Document() 
                {
                    ObjectId = "14062024",
                    StationData = 
                };
                if (obj is null)
                {
                    MethodHandle.CallMethod($"{this.Code}", "Insert", docs);
                }
                else
                {
                    object doc = MethodHandle.CallMethod($"{this.Code}", "Find", "14062024");
                    int loc = TimeMap(this.TimeBinding);

                   //string t = doc.StationData.ToString();

                    if (loc != -1) {

                        var d = new Document()
                        {
                            Time = this.TimeBinding,
                            SeaLevel = this.SeaLevelBinding,
                            WaveHeight = this.WaveHeightBinding,
                            WaveLength = this.WaveLengthBinding,
                            WaveHeightMax = this.WaveHeightMaxBinding,
                        };
                        DocumentList dl = doc.StationData;
                        dl.Add(new Document()
                        {
                            Time = this.TimeBinding,
                            SeaLevel = this.SeaLevelBinding,
                            WaveHeight = this.WaveHeightBinding,
                            WaveLength = this.WaveLengthBinding,
                            WaveHeightMax = this.WaveHeightMaxBinding
                        });
                        doc.StationData = dl;
                        DB.Station.Update(doc);
                    }
                }
            ;});
        }

        public DocumentList ParaData()
        {
            DocumentList data = new DocumentList();
            int loc = TimeMap(this.TimeBinding);
            if ( loc != -1) { data.Add(setParamData()); }
            else throw new FormatException("Time is invalid.");
            return data;
        }
        public Document setParamData()
        {
            return new Document()
            {
                ObjectId = this.TimeBinding,
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
