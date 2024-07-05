using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.Services
{
    public class JWTAuth : System.Format
    {
        public string JWT { get; set; }
        public JWTAuth(string jwt) { this.JWT = jwt; }
        public Document getHeader()
        {
            string header =  JWT.Split(".")[0];
            return Base64urlDecode(header);
        }
        public Document getPayload()
        {
            string payload = JWT.Split(".")[1];
            return Base64urlDecode(payload);
        }
        public string getSig()
        {
            return JWT.Split(".")[2];
        }
        public bool IsJWTValid(string userid)
        {
            if(DB.Token.Find(userid) is not null)
            {
                string ?secretket = DB.Token.Find(userid).SecretKey;
                if (getHeader().Alg == "HS256")
                {
                    string data = Base64urlEncode(getHeader()) + Base64urlEncode(getPayload());
                    string sigcheck = EncodeSHA256(data, secretket);
                    if (sigcheck == getSig()) // jwt toàn vẹn
                    {
                        if(getPayload().UserID == userid) { return true; }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

    }
}
