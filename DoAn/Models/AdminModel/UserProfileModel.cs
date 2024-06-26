using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Models.AdminModel
{
    public class UserProfileModel
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string RegisDate { get; set; }
        public string WorkingUnit { get; set; }
        public string Position { get; set; }
        public string Station { get; set; }
    }
}
namespace System
{
    public partial class Document
    {
        public string ?UserName { get => GetString(nameof(UserName)); set => Push(nameof(UserName), value); }

        //role in format.cs
        //userID in UserModel.cs
        public string ?Email { get => GetString(nameof(Email)); set => Push(nameof(Email), value); }
        public string ?RegisDate { get => GetString(nameof(RegisDate)); set => Push(nameof(RegisDate), value); }
        public string ?WorkingUnit { get => GetString(nameof(WorkingUnit)); set => Push(nameof(WorkingUnit), value); }
        public string ?Position { get => GetString(nameof(Position)); set => Push(nameof(Position), value); }
        public List<string> ?StationManagement { get => GetArray<List<string>>(nameof(StationManagement)); set => Push(nameof(StationManagement), value); }
    }
}
