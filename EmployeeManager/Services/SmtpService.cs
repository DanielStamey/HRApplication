using System;
using System.Net;
using System.Net.Mail;

namespace EmployeeManager
{
    public static class SmtpService
    {
        private static string smtpAddress = "smtp.gmail.com";
        private static int portNumber = 587;
        private static bool enableSSL = true;
        private static string emailFromAddress = "fordevelopmentprojects@gmail.com"; 
        private static string password = "!L0v3D3v3l0pm3nt";
        private static string emailToAddress = "fordevelopmentprojects@gmail.com";  
        private static string subject = "System Changes";

        public static void SendNotification(string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFromAddress);
                    mail.To.Add(emailToAddress);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateCopyMessage(): {0}", ex.ToString());
            }
        }
    }
}