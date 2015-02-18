using ActionMailer.Net.Standalone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tathangfire.Tasks;
using Zeekpipo.CrowdFundingDomain.Model;

namespace tathangfire.Controllers
{
    public class MailController : RazorMailerBase
    {
        public MailController()
        {
            From = "no-reply@crowdrive.com";

        }

        public override string ViewPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + @"\EmailTemplates"; }
        }

        public RazorEmailResult NotifyProjectFailed(string email, DataToSendEmailProjectFail model)
        {
            To.Add(email);
            Subject = "ご支援いただいたプロジェクトに関しまして";
            return Email("NotifyProjectFailedEmail", model);
        }
    }
}