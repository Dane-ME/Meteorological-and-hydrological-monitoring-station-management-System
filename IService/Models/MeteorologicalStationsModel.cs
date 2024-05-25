using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace System
{
    public partial class Document
    {
        public string StationName { get => GetString(nameof(StationName)); set => Push(nameof(StationName), value); }
        public string StaionCode { get => GetString(nameof(StaionCode)); set => Push(nameof(StaionCode), value); }
        public string StationAddress { get => GetString(nameof(StationAddress)); set => Push(nameof(StationAddress), value); }
        public string SationType { get => GetString(nameof(SationType)); set => Push(nameof(SationType), value); }

    }
    public partial class DB
    {
        static public BsonData.Collection? Station => Main.GetCollection(nameof(Station));

    }
}
