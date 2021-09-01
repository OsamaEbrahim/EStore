using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EStore.Services
{
    public class EmailsSender
    {
        public IConfiguration Configuration { get; }
        private string EmailAddress;
        private string EmailPass;

        public EmailsSender(IConfiguration configuration)
        {
            Configuration = configuration;
            EmailAddress = Configuration["Email"];
            EmailPass = Configuration["EmailPass"];

        }
        public void SendEmail(string toAddress, string subject, string body)
        {

            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(EmailAddress, EmailPass)
            };


            try
            {
               email.Send(EmailAddress, toAddress,subject, body);
            }
            catch (SmtpException ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
    
