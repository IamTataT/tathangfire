using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tathangfire.Controllers;
using Zeekpipo.Core.Model;
using Zeekpipo.Core.Repository;
using Zeekpipo.CrowdFundingDomain.Model;
using Zeekpipo.CrowdFundingDomain.Repository;

namespace tathangfire.Tasks
{
    public class SendEmailWhenProjectFailedTask
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IUserRepository _userRepo;
        public SendEmailWhenProjectFailedTask(IUserRepository userRepo, IProjectRepository projectRepo)
        {
            _userRepo = userRepo;
            _projectRepo = projectRepo;
        }

        public void Send(long userId, string projectId)
        {
            var user = _userRepo.GetUserById(new UserIdentity(userId.ToString()));
            var email = user.Email;
            var project = _projectRepo.FindById(new ProjectIdentity(projectId));
            //TODO: mark project as sended

            new MailController().NotifyProjectFailed(email, project).Deliver();
        }
    }
}