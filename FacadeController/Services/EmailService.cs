using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace AudioPlayer.Services
{
    public class EmailService
    {
        private readonly string smtpHost = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string smtpUser = "abbasirahimeen@gmail.com";
        private readonly string smtpPass = "abc";
        private readonly string smtpFrom = "abbasirahimeen@gmail.com";

        public void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(smtpFrom),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                mail.To.Add(toEmail);

                using (SmtpClient smtpServer = new SmtpClient(smtpHost, smtpPort))
                {
                    smtpServer.EnableSsl = true;
                    smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpServer.UseDefaultCredentials = false;
                    smtpServer.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    smtpServer.TargetName = "STARTTLS/smtp.gmail.com";

                    smtpServer.Send(mail);
                }

                Console.WriteLine($"Email sent to: {toEmail}");
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP Error: {smtpEx.StatusCode} - {smtpEx.Message}");
                if (smtpEx.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {smtpEx.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }
    }
}
