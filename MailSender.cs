using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Zp.Crypto;

namespace Zp
{
    class MailSender : IMailSender
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly string smtpAddress;
        private readonly string smtpPort;
        private readonly string senderEmail;
        private readonly string emailText;
        private readonly IConfiguration cfg;

        public MailSender(string smtpAddress, string smtpPort, string senderEmail, string emailText,
            IConfiguration cfg)
        {
            this.smtpAddress = smtpAddress;
            this.smtpPort = smtpPort;
            this.senderEmail = senderEmail;
            this.emailText = emailText;
            this.cfg = cfg;
        }
        public bool SendEmail(string filePath, string destEmail, string emailSubject)
        {
            bool isErrorOccurs = false;

            MailMessage message = null;
            Attachment data = null;

            try
            {
                message = new MailMessage();

                string login = senderEmail.Substring(0, senderEmail.IndexOf('@'));

                logger.Info("[MAIL-S] " +
                    "smtpAddress: " + smtpAddress
                    + " smtpPort: " + smtpPort
                    + " senderEmail: " + senderEmail
                    + " emailSubject: " + emailSubject
                    + " emailText: " + emailText
                    + " destEmail: " + destEmail);

                message.To.Add(destEmail);
                message.Subject = emailSubject;
                message.From = new MailAddress(senderEmail);
                message.Body = emailText;

                // Create the file attachment for this e-mail message.
                data = new Attachment(filePath, MediaTypeNames.Application.Octet);

                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(filePath);
                disposition.ModificationDate = File.GetLastWriteTime(filePath);
                disposition.ReadDate = File.GetLastAccessTime(filePath);

                // Add the file attachment to this e-mail message.
                message.Attachments.Add(data);

                /*
                // Add delivery notifications headers.
                bool isDeliveryNotificationsEnabled = !string.IsNullOrEmpty(cfg.GetSection("emailSettings")["deliveryNotifications"]) && bool.Parse(cfg.GetSection("emailSettings")["deliveryNotifications"]);

                if(isDeliveryNotificationsEnabled)
                {
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure;

                    message.Headers.Add("Disposition-Notification-To", senderEmail);
                    message.Headers.Add("Return-Receipt-To", senderEmail);
                }
                
                //message.hea("Return-Receipt-To", "\"John Doe\" <johnDoe@blahblahblah.com>");
                //message.AddHeaderField("Disposition-Notification-To", "\"John Doe\" <johnDoe@blahblahblah.com>");
                */

                var smtpClient = new SmtpClient(smtpAddress)
                {
                    Port = int.Parse(smtpPort),
                    Credentials = new NetworkCredential(login,
                    Encryptor.DecryptString(cfg.GetSection("emailSettings")["emailPassword"], cfg.GetSection("emailSettings")["emailPasswordSalt"])),
                    EnableSsl = true,
                };

                logger.Info("[MAIL-S] Send email -> " + smtpClient.Host + " -> " + destEmail);

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                isErrorOccurs = true;
            }
            finally
            {
                if (data != null)
                    data.Dispose();

                if (message != null)
                    message.Dispose();
            }

            return isErrorOccurs;
        }
    }
    internal interface IMailSender
    {
        bool SendEmail(string filePath, string destEmail, string emailSubject);
    }
}