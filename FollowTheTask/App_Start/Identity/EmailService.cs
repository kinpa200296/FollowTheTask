﻿using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace FollowTheTask.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var from = new MailAddress(ConfigurationManager.AppSettings["EmailAdress"], "Follow The Task");
            var to = new MailAddress(message.Destination);
            var m = new MailMessage(from, to) {Subject = message.Subject, Body = message.Body, IsBodyHtml = true};
            var smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpHost"], 587)
            {
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAdress"],
                    ConfigurationManager.AppSettings["EmailPassword"]),
                EnableSsl = true
            };
            return smtpClient.SendMailAsync(m);
        }
    }
}