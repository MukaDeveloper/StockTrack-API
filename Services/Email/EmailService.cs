using System.Net;
using System.Net.Mail;
using StockTrack_API.Models;

namespace StockTrack_API.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string receiver, string subject, string body)
        {
            try
            {
                Email email =
                new()
                {
                    Sender = $"{_configuration.GetSection("Email:Sender").Value!}",
                    SenderPassword = $"{_configuration.GetSection("Email:Password").Value!}",
                    Receiver = receiver,
                    Subject = subject,
                    Message = body,
                    PrimaryDomain = "smtp.gmail.com",
                    PrimaryPort = 587,
                };

                string toEmail = email.Receiver;

                MailMessage mailMessage =
                    new() { From = new MailAddress(email.Sender, "StockTrack") };

                mailMessage.To.Add(new MailAddress(toEmail));

                if (!string.IsNullOrEmpty(email.ReceiverCopy))
                    mailMessage.CC.Add(new MailAddress(email.ReceiverCopy));

                mailMessage.Subject = email.Subject;
                mailMessage.Body = email.Message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;

                using (SmtpClient smtp = new(email.PrimaryDomain, email.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(email.Sender, email.SenderPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
