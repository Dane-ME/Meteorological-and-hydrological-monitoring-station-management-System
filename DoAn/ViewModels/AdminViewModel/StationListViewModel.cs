using DoAn.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.ViewModels.AdminViewModel
{
    public class StationListViewModel
    {
        public bool IsLoading {  get; set; }
        public List<StationLabel> NameofStation {  get; set; }
        public List<StationLabel> STT {  get; set; }
        public StationListViewModel() 
        {
            //LoadDataAsync();

            var items = new List<StationLabel>()
            {
                new StationLabel {Name = "Cảng xăng dầu B12" , ID = "8686"},
                new StationLabel {Name = "Cảng Xi Măng Hạ Long", ID = "60001" },
                new StationLabel {Name = "Cảng xăng dầu B12", ID = "8686" },
                new StationLabel {Name = "Cảng Xi Măng Hạ Long", ID = "60001" },
                new StationLabel {Name = "Cảng xăng dầu B12", ID = "8686" },
                new StationLabel {Name = "Cảng Xi Măng Hạ Long", ID = "60001" },
                new StationLabel { Name = "Cảng xăng dầu B12", ID = "8686" },
                new StationLabel { Name = "Cảng Xi Măng Hạ Long", ID = "60001" },
            };
            this.NameofStation = items;
            STT = new List<StationLabel>();
            var c = this.NameofStation.Count;
            for (int i = 0; i < c; i++)
            {
                this.STT.Add(new StationLabel { Num = $"{i + 1}" });
            }
        }
        private async Task LoadDataAsync()
        {
            IsLoading = true;
            var items = new List<StationLabel>
            {
                new StationLabel { Name = "Cảng xăng dầu B12" },
                new StationLabel { Name = "Cảng Xi Măng Hạ Long" },
                new StationLabel { Name = "Cảng xăng dầu B12" },
                new StationLabel { Name = "Cảng Xi Măng Hạ Long" },
                new StationLabel { Name = "Cảng xăng dầu B12" },
                new StationLabel { Name = "Cảng Xi Măng Hạ Long" },
                new StationLabel { Name = "Cảng xăng dầu B12" },
                new StationLabel { Name = "Cảng Xi Măng Hạ Long" }
            };
            NameofStation = items;
            STT = new List<StationLabel>();
            for (int i = 0; i < items.Count; i++)
            {
                STT.Add(new StationLabel { Num = $"{i + 1}" });
            }
            await Task.Delay(2000); // Simulate data loading delay
            IsLoading = false;
        }
    }
    public class StationLabel
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Num { get; set; }
    }
}
