using System.Net.Mail;
using System.Net;

namespace Service.UI.EmailSender
{
    public class EmailSend
    {
        public static bool SendEmail(string ToEmail, string Subject, string Body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("myproject302@gmail.com", "ShopOcean");
                mail.To.Add(ToEmail);
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                try
                {
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("myproject302@gmail.com", "tqcxgsoulqobupps");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        return true;
                    }
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
        }
    }
