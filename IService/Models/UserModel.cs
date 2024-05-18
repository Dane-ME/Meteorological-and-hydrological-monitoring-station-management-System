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
        public string Name { get => GetString(nameof(Name)); set => Push(nameof(Name), value); }
        public string Email { get => GetString(nameof(Email)); set => Push(nameof(Email), value); }
        public string PhoneNumber { get => GetString(nameof(PhoneNumber)); set => Push(nameof(PhoneNumber), value); }
    }
    public partial class DB
    {
        static public BsonData.Collection? User => Main.GetCollection(nameof(User));
    }
}
