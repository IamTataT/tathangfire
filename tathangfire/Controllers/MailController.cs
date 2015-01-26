using ActionMailer.Net.Standalone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public RazorEmailResult NotifyProjectFailed(string email, Project project)
        {
            To.Add(email);
            Subject = "Notification System: Project failed";
            return Email("NotifyProjectFailedEmail", project);
        }
    }
}