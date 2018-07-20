﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Egharpay.Business.EmailServiceReference;
using Egharpay.Business.Interfaces;
using System.Threading.Tasks;
using iTextSharp.text;

namespace Egharpay.Business.Services
{
    public class EmailBusinessService : IEmailBusinessService
    {
        public async Task<bool> SendEmail(EmailData data)
        {
            UseOverrideEmailDataIfSet(data);
            try
            {
                var msg = CreateMessage(data);
                await SendEmail(msg);
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        public void SendEmail(EmailData data, List<Guid> docGuids)
        {
            UseOverrideEmailDataIfSet(data);
            try
            {
                var msg = CreateMessage(data);
                SendEmail(msg);
            }
            catch (Exception Ex)
            {
                // ignored
            }
        }

        public async Task<bool> SendEmail(EmailData data, Dictionary<string, byte[]> attachments)
        {
            try
            {
                var msg = CreateMessage(data);
                foreach (var attachment in attachments)
                {
                    msg.Attachments.Add(new Attachment(new MemoryStream(attachment.Value), attachment.Key));
                }
                await SendEmail(msg);
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        private MailMessage CreateMessage(EmailData data)
        {
            data.FromAddress = ConfigurationManager.AppSettings["FromAddress"];
            var mail = new MailMessage
            {
                From = new MailAddress(data.FromAddress),
                IsBodyHtml = data.IsHtml,
                Body = data.Body,
                Subject = data.Subject
            };
            mail.To.Add(string.Join(",", data.ToAddressList));
            if (data.CCAddressList != null && data.CCAddressList.Any())
                mail.CC.Add(string.Join(",", data.CCAddressList));
            if (data.BCCAddressList != null && data.BCCAddressList.Any())
                mail.Bcc.Add(string.Join(",", data.BCCAddressList));
            return mail;
        }

        private async Task SendEmail(MailMessage msg)
        {
            var host = ConfigurationManager.AppSettings["SMTPHost"];
            var port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
            using (var client = new SmtpClient(host, port))
            {
                var id = ConfigurationManager.AppSettings["SMTPLoginId"];
                var pwd = ConfigurationManager.AppSettings["SMTPLoginPwd"];
                client.Credentials = new NetworkCredential(id, pwd);
                client.EnableSsl = true;
                try
                {
                  await client.SendMailAsync(msg);
                }
                catch (Exception Ex)
                {
                    Console.Write(Ex);
                }
                
            }
        }

        private void UseOverrideEmailDataIfSet(EmailData data)
        {
            var overrideEmailAddresses = ConfigurationManager.AppSettings["OverrideEmailAddresses"];
            if (!string.IsNullOrEmpty(overrideEmailAddresses))
            {
                data.ToAddressList.Clear();
                data.ToAddressList.AddRange(new List<string> { overrideEmailAddresses });
                if (data.CCAddressList != null)
                    data.CCAddressList.Clear();
                if (data.BCCAddressList != null)
                    data.BCCAddressList.Clear();
            }
        }
    }
}
