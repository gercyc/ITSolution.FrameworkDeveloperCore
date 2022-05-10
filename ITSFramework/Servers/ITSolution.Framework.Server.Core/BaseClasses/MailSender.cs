using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITSolution.Framework.Core.Server.BaseClasses
{
    public class MailSender : IEmailSender
    {
        //TODO:
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return new Task(() => Task.Delay(10));
        }
    }
}
