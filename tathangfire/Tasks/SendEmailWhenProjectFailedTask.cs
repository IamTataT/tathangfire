using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tathangfire.Controllers;
using Zeekpipo.Core.Model;
using Zeekpipo.Core.Repository;
using Zeekpipo.CrowdFundingDomain.Exception;
using Zeekpipo.CrowdFundingDomain.Model;
using Zeekpipo.CrowdFundingDomain.Repository;

namespace tathangfire.Tasks
{
    public class SendEmailWhenProjectFailedTask
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IUserRepository _userRepo;
        private IPledgeRepository _pledgeRepo;
        private ISGExtendedProjectRepository _sgProjectRepo;
        public SendEmailWhenProjectFailedTask(IUserRepository userRepo, IProjectRepository projectRepo, IPledgeRepository pledgeRepo, ISGExtendedProjectRepository sgProjectRepo)
        {
            _userRepo = userRepo;
            _projectRepo = projectRepo;
            _pledgeRepo = pledgeRepo;
            _sgProjectRepo = sgProjectRepo;

        }

        public void Send(string pledgerid, string pledgeid)
        {
            var pledgeitem = _pledgeRepo.FindById(new PledgeItemIdentity(pledgeid));
            var projid = pledgeitem.ProjectIdentity;
            var rewardid = pledgeitem.RewardIdentity;
            var project = _projectRepo.FindById(new ProjectIdentity(projid.ToString()));
            var reward = project.GetReward(new RewardIdentity(rewardid.ToString()));
            var user = _userRepo.GetUserById(new UserIdentity(pledgerid.ToString()));
            var email = user.Email;
            var projectURL = "";
            try
            {
                projectURL = _sgProjectRepo.GetCustomUrlFromProjectIdentity(projid);
                projectURL = "http://crowdrive.com/view/" + projectURL;
                
            }
            catch (ProjectNotFoundException e)
            {
                projectURL = "http://crowdrive.com/project/detail/" + projid.ToString();
            }
            try
            {
                var model = new DataToSendEmailProjectFail()
                {
                    DisplayName = user.DisplayName,
                    ProjectTitle = project.Title,
                    ProjectUrl = projectURL,
                    PledgedRewardName = reward.RewardTitle,
                    PledgedAmount = pledgeitem.Price
                };
                new MailController().NotifyProjectFailed(email, model).Deliver();
            }
            catch
            {
            }
        }
    }
    public class DataToSendEmailProjectFail
    {
        public string DisplayName { get; set; }
        public string ProjectTitle { get; set;  }
        public string ProjectUrl { get; set; }
        public string PledgedRewardName { get; set; }
        public long PledgedAmount { get; set; }
    }
}