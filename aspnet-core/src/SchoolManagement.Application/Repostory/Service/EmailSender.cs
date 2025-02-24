using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Abp.Net.Mail;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Email;
using SchoolManagement.Repostory.Interface;

namespace SchoolManagement.Repostory.Service
{
    public class EmailSender : IEmailSend
    {
        private readonly IConfiguration configuration;
        public EmailSender(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public async Task<bool> EmailSendAsync(string email, string Subject, string message)
        {
           try
           {
                GetEmailSetting getEmailSettings = new GetEmailSetting()
                {
                    SecretKey = configuration.GetValue<string>("AppSettings:SecretKey"),
                    From = configuration.GetValue<string>("AppSettings:EmailSettings:From"),
                    SmtpServer = configuration.GetValue<string>("AppSettings:EmailSettings:SmtServer"),
                    Port = configuration.GetValue<int>("AppSettings:EmailSettings:Port"),
                    EnableSSL = configuration.GetValue<bool>("AppSettings:EmailSettings:EnableSSL"),
                };

                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(getEmailSettings.From),
                    Subject = Subject,
                    Body = message
                };

                mailMessage.To.Add(email);
                SmtpClient smtpClient = new SmtpClient(getEmailSettings.SmtpServer)
                {
                    Port = getEmailSettings.Port,
                    Credentials = new NetworkCredential(getEmailSettings.From, getEmailSettings.SecretKey),
                    EnableSsl = getEmailSettings.EnableSSL
                };

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
