using DoAn.Models;
using MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public partial class Document
    {
        public string Status { get => GetString(nameof(Status)); set => Push(nameof(Status), value); }
    }
}
namespace DoAn.Services
{
    public class AuthService
    {
        public bool LoginState;
        private const string AuthStateKey = "AuthState";
        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(2000);

            var authState = Preferences.Default.Get<bool>(AuthStateKey, false);

            return authState;
        }
        public async Task<bool> IsLoginAsync()
        {
            if (this.LoginState) { await Task.Delay(1000); return true; }
            else await Task.Delay(1000); return false;
        }
        public async Task<bool> IsConnectedToNetworkAsync()
        {
            var networkState = Connectivity.NetworkAccess;
            if (networkState == NetworkAccess.Internet) { await Task.Delay(1000); return true; }
            else { await Task.Delay(1000); return false; }
        }
        public bool status {  get; private set; }
        public async Task<bool> IsServerReady()
        {
            Broker.Instance.Connect();
            Document doc = new Document()
            {
                Status = "ready"
            };
            await Task.Delay(500);
            Broker.Instance.Send("dane/system/checking", doc);
            await Task.Delay(1000);
            check();
            if (status) { await Task.Delay(1000); return true; }
            else { await Task.Delay(1000); return false; }
        }
        public void check() {
            Broker.Instance.Listen("dane/system/checking", (doc) =>
            {
                if (doc.Status == "ready") { status = true; }
                else { status = false; }
            });
        }
        public void IsLogined(string account, string password)
        {
            var in4 = new AccountModel();
            if (account == in4.Account && password == in4.PassWord) { this.LoginState = true; }
            else this.LoginState = false;
        }
        public void Logout()
        {
            foreach(Document obj in DB.Token.SelectAll())
            {
                DB.Token.Delete(obj.ObjectId);
            } 
        }
    }
}
