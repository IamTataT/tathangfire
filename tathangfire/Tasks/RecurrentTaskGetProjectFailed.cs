using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zeekpipo.Core.Model;
using Zeekpipo.Core.Repository;
using Zeekpipo.CrowdFundingDomain.Model;
using Zeekpipo.CrowdFundingDomain.Repository;
using Zeekpipo.CrowdFundingDomain.Service;
using Zeekpipo.Data;

namespace tathangfire.Tasks
{
    public class RecurrentTaskGetProjectFailed
    {
        private readonly IProjectRepository _projectRepo;
        private ISGExtendedProjectRepository _sgProjectRepo;
        private ProjectService _projectService;
        private IPledgeRepository _pledgeRepo;
        private IUserRepository _userRepo;
        private IRepository<Zeekpipo.Model.DbModel.Project> _dbProjectRepo;
        
        public RecurrentTaskGetProjectFailed(IProjectRepository projectRepo, ISGExtendedProjectRepository sgProjectRepo, ProjectService projectService, IPledgeRepository pledgeRepo, IUserRepository userRepo, IRepository<Zeekpipo.Model.DbModel.Project> rawDbProjectRepo)
        {
            _projectRepo = projectRepo;
            _sgProjectRepo = sgProjectRepo;
            _projectService = projectService;
            _pledgeRepo = pledgeRepo;
            _userRepo = userRepo;
            _dbProjectRepo = rawDbProjectRepo;
        }

        public void GetProject(){
            IList<Project> failedProjectList = _sgProjectRepo.AllEndedProjectsAndNotSendEmail().Where(project => _projectService.IsProjectFailed(project)).ToList();
            foreach (var currentProject in failedProjectList)
            {
                var dbProject = _dbProjectRepo.Get(currentProject.Id.ToString());
                //if (dbProject.IsFailProject)
                //{
                //    continue;
                //}
                IEnumerable<UserIdentity> pledgerIds = _pledgeRepo.FindAllPledgerIdentityByProjectId(currentProject.Id);
                foreach (var userId in pledgerIds)
                {
                    var user = _userRepo.GetUserById(userId);
                    var email = user.Email;
                    //send email to user
                    //BackgroundJob.Enqueue(() => new MailController().NotifyProjectFailed(email, currentProject).Deliver());
                    BackgroundJob.Enqueue<SendEmailWhenProjectFailedTask>(x => x.Send(userId.ToLong(), currentProject.Id.ToString()));
                }
                //mark this project as sended
                dbProject.IsFailProject = true;
                _dbProjectRepo.Update(dbProject);
                _dbProjectRepo.Save();
            }
        }

        public void InitializeJobs()
        {
            RecurringJob.AddOrUpdate("SendFailedProject", () => GetProject(), Cron.Minutely);
        }
    }
}