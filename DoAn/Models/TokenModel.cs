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
        public string Token { get => GetString(nameof(Token)); set => Push(nameof(Token), value); }
        public string Type { get => GetString(nameof(Type)); set => Push(nameof(Type), value); }

    }

    public partial class DB
    {
        static public BsonData.Collection? Token => Main.GetCollection(nameof(Token));
    }
}
