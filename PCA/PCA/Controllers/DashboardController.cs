/* 
 * Name: Dashboard Controller
 * Description: Controlls Project homepages and project selection
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;
using PCA.ViewModels;
using System.Data.Entity;

namespace PCA.Controllers
{
    public class DashboardController : Controller
    {
        // Database context
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard/Index/1
        public ActionResult Index()
        {
            // Connects to System Controller
            var systemController = DependencyResolver.Current.GetService<SystemController>();

            // Session variable context
            HttpContext context = System.Web.HttpContext.Current;

            // Sets commonly used sessions variables and viewbag objects
            var currentList = systemController.Get();
            @ViewBag.CurrentUserId = currentList.ElementAt(0);
            @ViewBag.CurrentUserName = currentList.ElementAt(1);

            // Queries
            var viewModelInformation = from report in db.DailyReport
                                       select new
                                       {
                                           report.ProjectId,
                                           report.DailyReportId,
                                           report.Status
                                       };

            var projects = (from p in db.Projects
                            select p);

            // Count for workflow
            DashboardViewModel vm = new DashboardViewModel();
            int drp = 0;
            int drr = 0;
            int dra = 0;

            foreach (var r in viewModelInformation)
            {
                switch (r.Status)
                {
                    case "Pending":
                        drp += 1;
                        break;

                    case "Reviewed":
                        drr += 1;
                        break;

                    case "Approved":
                        dra += 1;
                        break;
                }

                vm.DailyReportPending = drp;
                vm.DailyReportReviewed = drr;
                vm.DailyReportApproved = dra;
            }
            
            ViewBag.ProjectList = projects;

            return View(vm);
        }


        // GET: Dashboard/Select
        public ActionResult Select()
        {
            ViewBag.CurrentUserId = Session["UserId"];

            // Get all current projects
            var projects = db.Projects.ToList();
            List<ProjectSelectViewModel> list = new List<ProjectSelectViewModel>();

            foreach (var project in projects)
            {
                ProjectSelectViewModel vm = new ProjectSelectViewModel();
                List<File> files = new List<File>();
                vm.ProjectId = project.ProjectId;
                vm.ClientId = project.ClientId;
                vm.ProjectName = project.Name;

                /*
                var picture = new List<DailyReportPicture>(from pic in db.DailyReportPicture
                                                                                join report in db.DailyReport on pic.DailyReportId equals report.DailyReportId
                                                                                where report.ProjectId == project.ProjectId
                                                                                orderby pic.Timestamp descending
                                                                                select pic).Take(3);

                foreach (var p in picture)
                {
                    DailyReportPicture pic = db.DailyReportPicture.Include(s => s.Files).SingleOrDefault(s => s.DailyReportPictureId == p.DailyReportPictureId);

                    foreach (var image in pic.Files)
                    {
                        files.Add(image);
                    }
                }
                vm.Files = files;*/
                list.Add(vm);
            }

            return View(list);

        }

        public List<DailyReport> DailyReport(string status)
        {
            // Queries
            List<DailyReport> reports = new List<DailyReport>(from report in db.DailyReport
                                                              where report.Status == status
                                                                select report);

            return reports;
        }


    }
}