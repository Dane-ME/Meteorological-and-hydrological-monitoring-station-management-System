using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public partial class Document
    {
        public string ControllerName { get => GetString(nameof(ControllerName)); set => Push(nameof(ControllerName), value); }
        public string ActionName { get => GetString(nameof(ActionName)); set => Push(nameof(ActionName), value); }
    }
    public partial class DB
    {
        static public BsonData.Collection? Topic => Main.GetCollection(nameof(Topic));

    }
}
