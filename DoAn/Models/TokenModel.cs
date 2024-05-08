using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{

    public partial class Document
    {
        //public string? TokenID { get => ObjectId; set => ObjectId = (string?)value; }
        // ObjectID là Token,
        public string DeviceInfo { get => GetString(nameof(DeviceInfo)); set => Push(nameof(DeviceInfo), value); }
        public string Time { get => GetString(nameof(Time)); set => Push(nameof(Time), value); }

    }

    public partial class DB
    {
        static public BsonData.Collection? Token => Main.GetCollection(nameof(Token));
    }
}
