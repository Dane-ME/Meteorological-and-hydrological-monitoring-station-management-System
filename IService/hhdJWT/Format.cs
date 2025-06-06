﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public partial class Document
    {
        public string Typ { get => GetString(nameof(Typ)); set => Push(nameof(Typ), value); }
        public string Alg { get => GetString(nameof(Alg)); set => Push(nameof(Alg), value); }
        public string SecretKey { get => GetString(nameof(SecretKey)); set => Push(nameof(SecretKey), value); }
    }
    public class Format
    {
        public string Header { get; set; }
        public string Payload { get; set; }
        public string Signature { get; set; }

        public string Base64urlEncode(Document doc)
        {
            string text = doc.ToString();
            // Encode a byte array
            byte[] inputBytes = Encoding.UTF8.GetBytes($"{text}");
            string encodedString = Convert.ToBase64String(inputBytes)
                .TrimEnd('=') // Remove any trailing padding characters
                .Replace('+', '-') // Replace '+' with '-'
                .Replace('/', '_'); // Replace '/' with '_'
            //Console.WriteLine(encodedString); // Outputs: SGVsbG8sIFdvcmxkIQ
            return encodedString;
        }

        public Document Base64urlDecode(string base64Url)
        {
            // Thay thế '-' bằng '+' và '_' bằng '/'
            string base64 = base64Url.Replace('-', '+').Replace('_', '/');

            // Thêm các ký tự padding '=' nếu cần
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            // Decode chuỗi base64
            byte[] bytes = Convert.FromBase64String(base64);
            Document decoded = Encoding.UTF8.GetString(bytes);

            return decoded;
        }

        public static string EncodeSHA256(string data, string secretKey)
        {
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secretKey);
            byte[] messageBytes = encoding.GetBytes(data);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        public string CreateSecretKey(string accname, string encodedpass, string time)
        {
            string secretkey = (accname + encodedpass + time).ToMD5();
            return secretkey;
        }
        public string CreateJWT()
        {
            if (this.Signature != null)
            {
                return this.Header + "." + this.Payload + "." + this.Signature;
            }
            else { return ""; }
        }

        public void header(string type, string algorithm)
        {
            Document header = new Document()
            {
                Typ = type,
                Alg = algorithm,
            };
            this.Header = Base64urlEncode(header);
        }
        public void payload(string userid, string role, string exp)
        {
            Document payload = new Document()
            {
                UserID = userid,
                Role = role,
                Exp = exp
            };
            this.Payload = Base64urlEncode(payload);
        }

        public void signature(string accname, string encodedpass, string time)
        {
            if (this.Header != null && this.Payload != null)
            {

                string secretkey = CreateSecretKey(accname, encodedpass, time);
                string data = this.Header + this.Payload;
                this.Signature = EncodeSHA256(data, secretkey);
            }
        }



    }
}
