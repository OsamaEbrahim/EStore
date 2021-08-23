using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Services
{
    public class EmailsSender
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = await Email
              .From("noreply@Estore.com")
              .To(to)
              .Subject(subject)
              .Body(body)
              .SendAsync();
        }
    }
}
