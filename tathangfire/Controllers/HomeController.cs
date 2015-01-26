using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tathangfire.Models;
using tathangfire.Tasks;
using Zeekpipo.Core.Model;
using Zeekpipo.Core.Repository;
using Zeekpipo.CrowdFundingDomain.Model;
using Zeekpipo.CrowdFundingDomain.Repository;
using Zeekpipo.CrowdFundingDomain.Service;
using Zeekpipo.Data;

namespace tathangfire.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectRepository _projectRepo;
        private ISGExtendedProjectRepository _sgProjectRepo;
        private ProjectService _projectService;
        private IPledgeRepository _pledgeRepo;
        private IUserRepository _userRepo;
        private RecurrentTaskGetProjectFailed _recurrentTask;

        public HomeController(IProjectRepository projectRepo, ISGExtendedProjectRepository sgProjectRepo, ProjectService projectService, IPledgeRepository pledgeRepo, IUserRepository userRepo, RecurrentTaskGetProjectFailed recurrentTask)
        {
            _projectRepo = projectRepo;
            _sgProjectRepo = sgProjectRepo;
            _projectService = projectService;
            _pledgeRepo = pledgeRepo;
            _userRepo = userRepo;
            _recurrentTask = recurrentTask;
        }
        public ActionResult Index()
        {
            _recurrentTask.InitializeJobs();
           /* IList<Project> failedProjectList = _sgProjectRepo.AllEndedProjects().Where(project => _projectService.IsProjectFailed(project)).ToList();
            foreach (var currentProject in failedProjectList)
            {
                IEnumerable<UserIdentity> pledgerIds = _pledgeRepo.FindAllPledgerIdentityByProjectId(currentProject.Id);
                foreach (var userId in pledgerIds)
                {
                    var user = _userRepo.GetUserById(userId);
                    var email = user.Email;
                    //TODO: send email to user
                    //BackgroundJob.Enqueue(() => new MailController().NotifyProjectFailed(email, currentProject).Deliver());
                    BackgroundJob.Enqueue<SendEmailWhenProjectFailedTask>(x => x.Send(userId.ToLong(), currentProject.Id.ToString()));
                }
            }*/
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}