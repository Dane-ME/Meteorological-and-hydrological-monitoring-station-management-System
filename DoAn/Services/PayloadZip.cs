using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Services
{
    public class PayloadZip
    {
    }
}
namespace System
{
    public partial class Document
    {
        public DocumentList StationList { get => GetArray<DocumentList>(nameof(Content)); set => Push(nameof(Content), value); }

    }
}
