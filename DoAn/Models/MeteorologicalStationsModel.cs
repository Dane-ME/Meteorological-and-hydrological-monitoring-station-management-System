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
        public string StationAddress { get => GetString(nameof(StationAddress)); set => Push(nameof(StationAddress), value); }
        public string StationType { get => GetString(nameof(StationType)); set => Push(nameof(StationType), value); }
        public List<Document> StationData { get => GetArray<List<Document>>(nameof(StationData)); set => Push(nameof(StationData), value); }

    }
    public partial class DB
    {
        static public BsonData.Collection? Station => Main.GetCollection(nameof(Station));

    }
}
