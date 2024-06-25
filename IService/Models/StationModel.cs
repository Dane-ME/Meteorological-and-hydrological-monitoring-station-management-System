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
        public List<string> StationTypeList { get => GetArray<List<string>>(nameof(StationTypeList)); set => Push(nameof(StationTypeList), value); }
        public string StationType { get => GetString(nameof(StationType)); set => Push(nameof(StationType), value); }
        public List<string> Manager { get => GetArray<List<string>>(nameof(Manager)); set => Push(nameof(Manager), value); }
        public DocumentList StationList { get => GetArray<DocumentList>(nameof(StationList)); set => Push(nameof(StationList), value); }
        public DocumentList HomeData { get => GetArray<DocumentList>(nameof(HomeData)); set => Push(nameof(HomeData), value); }

        public DocumentList StationData { get => GetArray<DocumentList>(nameof(StationData)); set => Push(nameof(StationData), value); }

    }
    public partial class DB
    {
        static public BsonData.Collection? Station => MainManager.GetCollection(nameof(Station));

    }
}
