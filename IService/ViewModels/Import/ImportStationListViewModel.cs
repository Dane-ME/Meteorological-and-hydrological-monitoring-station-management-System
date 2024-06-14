using IService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IService.ViewModels.Import
{
    public class ImportStationListViewModel
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Local { get; set; }
        public string Type { get; set; }
        public string Manager { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ImportStationListViewModel() 
        {
            List<string> typStation = new List<string>();
            List<string> manager = new List<string>();

            SaveCommand = new RelayCommand(execute => {
                string[] ts = this.Type.Split(",");
                string[] mn = this.Manager.Split(",");

                for (int i = 0; i < ts.Length; i++)
                {
                    typStation.Add(ts[i]);
                }

                for (int i = 0; i < mn.Length; i++)
                {
                    manager.Add(mn[i]);
                }
                DB.Station.Insert(new Document()
                {
                    ObjectId = this.ID,
                    StationName = this.Name,
                    StationAddress = this.Local,
                    StationType = typStation,
                    Manager = manager
                });

            });
        }
    }
}
