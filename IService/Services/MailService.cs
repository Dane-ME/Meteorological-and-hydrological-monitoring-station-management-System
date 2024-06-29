using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace IService.Services
{
    public class MailService
    {
        private string ?Mailto {  get; set; }
        private string ?UserId { get; set; }
        private string ?Request { get; set; }
        public MailService() { }
        public MailService(string mailto) { 
            this.Mailto = mailto;
        }
        public MailService( string mailto, string userid) 
        {
            this.Mailto = mailto;
            this.UserId = userid;
        }
        public MailService(string mailto, string userid, string request) 
        {
            this.Mailto = mailto;
            this.UserId = userid;
            this.Request = request;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Sử dụng biểu thức chính quy để kiểm tra định dạng email
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }
        public static string GenerateVerificationCode(int length = 8)
        {
            // Tạo một GUID mới
            Guid guid = Guid.NewGuid();

            // Chuyển GUID thành chuỗi và loại bỏ dấu gạch ngang
            string guidString = guid.ToString("N");

            // Lấy số ký tự đầu tiên theo độ dài mong muốn
            return guidString[..Math.Min(length, guidString.Length)];
        }
        public static SmtpClient InitSTMPClient()
        {
            using var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 10000;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("hhdangev02@gmail.com", "xius zeai buzb ikou");
            return smtpClient;
        }
        public string CreateAndSaveVerificationCode(string userid) {
            string verification = GenerateVerificationCode();
            DB.Verification.Insert(new Document() { ObjectId = userid, VerificationCode = verification });
            return verification;
        }
        public static string CreateAndSaveNewPassword(string userid)
        {
            string password = GenerateVerificationCode(12);
            Document ?doc = DB.User.Find(userid);
            if (doc != null)
            {
                Document clone = doc;
                clone.EncodePass = password.ToMD5();
                DB.User.Update(userid, clone);
            }
            return password;
        }
        public MailMessage MessageContent(string request, string userid)
        {
            
            if(request == "Verification")
            {
                string verification = CreateAndSaveVerificationCode(userid);
                return new MailMessage
                {
                    From = new MailAddress("hhdangev02@gmail.com", "Trung tâm quản lý trạm quan trắc"),
                    Subject = "[Verification] Mã xác nhận",
                    Body = $"<p>Vui lòng sử dụng mã OTP dưới đây để xác thực :</p><h2>{verification}</h2><p>Mã xác thực sẽ có hiệu lực trong vòng 5 phút</p>",
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };
            }
            else
            {
                string newpassword = CreateAndSaveNewPassword(userid);
                return new MailMessage
                {
                    From = new MailAddress("hhdangev02@gmail.com", "Trung tâm quản lý trạm quan trắc"),
                    Subject = "[Verification] Mã xác nhận",
                    Body = $"<p>Mật khẩu mới:</p><h2>{newpassword}</h2><p>Đừng quên đổi mật khẩu để tài khoản được an toàn</p>",
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };
            } 
        }
        public MailMessage ?MessageContent()
        {

            if (this.Request == "Verification")
            {
                if(this.UserId != null)
                {
                    string verification = CreateAndSaveVerificationCode(this.UserId);
                    return new MailMessage
                    {
                        From = new MailAddress("hhdangev02@gmail.com", "Trung tâm quản lý trạm quan trắc"),
                        Subject = "[Verification] Mã xác nhận",
                        Body = $"<p>Vui lòng sử dụng mã OTP dưới đây để xác thực :</p><h2>{verification}</h2><p>Mã xác thực sẽ có hiệu lực trong vòng 5 phút</p>",
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8
                    };
                }
                return null;
            }
            else
            {
                if(this.UserId != null)
                {
                    string newpassword = CreateAndSaveNewPassword(this.UserId);
                    return new MailMessage
                    {
                        From = new MailAddress("hhdangev02@gmail.com", "Trung tâm quản lý trạm quan trắc"),
                        Subject = "[Verification] Mã xác nhận",
                        Body = $"<p>Mật khẩu mới:</p><h2>{newpassword}</h2><p>Đừng quên đổi mật khẩu để tài khoản được an toàn</p>",
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8
                    };
                }
                return null;
                
            }
        }
        public static void SendMessage(MailMessage mailMessage, string addressto)
        {
            var smtpClient = InitSTMPClient();
            mailMessage.To.Add(addressto);
            smtpClient.Send(mailMessage);
        }
        public void SendMessage()
        {
            using var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 10000;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("hhdangev02@gmail.com", "xius zeai buzb ikou");
            var mailMessage = MessageContent();
            mailMessage.To.Add(this.Mailto);
            smtpClient.Send(mailMessage);
        }
    }
}
