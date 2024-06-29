using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    class UserModel
    {
    }
    public partial class Document
    {
        // objectId sẽ là id xác thực của tài khoản
        public string UserName { get => GetString(nameof(UserName)); set => Push(nameof(UserName), value); }
        public string Email { get => GetString(nameof(Email)); set => Push(nameof(Email), value); }
        public string PhoneNumber { get => GetString(nameof(PhoneNumber)); set => Push(nameof(PhoneNumber), value); }
        public string UserID { get => GetString(nameof(UserID)); set => Push(nameof(UserID), value); }
        public string EncodePass { get => GetString(nameof(EncodePass)); set => Push(nameof(EncodePass), value); }
        public string Role { get => GetString(nameof(Role)); set => Push(nameof(Role), value); }
        public DocumentList UserList { get => GetArray<DocumentList>(nameof(UserList)); set => Push(nameof(UserList), value); }

        public List<string> StationManagement { get => GetArray<List<string>>(nameof(StationManagement)); set => Push(nameof(StationManagement), value); }

        //role in format.cs
        //userID in UserModel.cs
        public string RegisDate { get => GetString(nameof(RegisDate)); set => Push(nameof(RegisDate), value); }
        public string WorkingUnit { get => GetString(nameof(WorkingUnit)); set => Push(nameof(WorkingUnit), value); }
        public string Position { get => GetString(nameof(Position)); set => Push(nameof(Position), value); }
        public List<string> Station { get => GetArray<List<string>>(nameof(Station)); set => Push(nameof(Station), value); }
        public string VerificationCode { get => GetString(nameof(VerificationCode)); set => Push(nameof(VerificationCode), value); }


    }
    public partial class DB
    {
        static public BsonData.Collection? User => MainManager.GetCollection(nameof(User));
    }
}
