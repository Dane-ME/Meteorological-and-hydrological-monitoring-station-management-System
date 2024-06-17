using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Models.AdminModel
{
    public class StationProfileModel
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string Manager { get; set; }
    }
    public class StationProfileSupportModel
    {
        public string Type { get; set; }
        public string Manager { get; set; } 
    }
}
namespace System
{
    public partial class Document
    {
        public List<string> Manager { get => GetArray<List<string>>(nameof(Manager)); set => Push(nameof(Manager), value); }
    }
}
