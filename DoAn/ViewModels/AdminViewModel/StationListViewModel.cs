using DoAn.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.ViewModels.AdminViewModel
{
    public class StationListViewModel : StationLabel
    {
        public bool IsLoading {  get; set; }
        public List<StationLabel> NameofStation {  get; set; }
        public List<StationLabel> STT {  get; set; }
        public StationListViewModel() 
        {
            var items = new List<StationLabel>()
            {
                new StationLabel("Cảng xăng dầu B12", "6868"),
                new StationLabel("Cảng Xi Măng Hạ Long", "60001"),
            };
            this.NameofStation = items;
            STT = new List<StationLabel>();
            var c = this.NameofStation.Count;
            for (int i = 0; i < c; i++)
            {
                this.STT.Add(new StationLabel { Num = $"{i + 1}" });
            }
        }
    }
    public class StationLabel
    {
        public StationLabel() { }
        public StationLabel(string name, string id) 
        {
            Name = name;
            ID = id;
        }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Num { get; set; }
    }
}
