using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;
using PCA.ViewModels;

namespace PCA.Controllers
{
    public class DailyReportController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: DailyReport
        public ActionResult Index()
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            // -----------

            //List<BudgetPhaseViewModel> viewModel = new List<BudgetPhaseViewModel>();

            int currentProjectNumber = ViewBag.CurrentProjectNumber;

            List<DailyReport> dailyReports = new List<DailyReport>(from dailyReport in db.DailyReport
                                                                   where dailyReport.ProjectId == currentProjectNumber
                                                                   select dailyReport);
            return View(dailyReports);
        }

        public ActionResult Detail(int id)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            var currentReport = db.DailyReport.Find(id);

            ViewBag.currentReportDate = currentReport.Date;
            ViewBag.currentReportSummary = currentReport.Summary;
            ViewBag.currentReportId = currentReport.DailyReportId;

            List<WorkItem> workList = new List<WorkItem>(from work in db.WorkItems
                                                                   where work.DailyReportId == id
                                                                   select work);

            int currentProjectNumber = ViewBag.CurrentProjectNumber;

            return View(workList);
        }

        // GET: EXAMPLEProjects/Create
        public ActionResult Create()
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            return View();
        }

        // POST: EXAMPLEProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DailyReportId,Date,Summary,ProjectId")] DailyReport dailyReport)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            if (ModelState.IsValid)
            {
                db.DailyReport.Add(dailyReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            return View(dailyReport);
        }

        // GET: Budget6/CreateWork
        public ActionResult CreateWork(int id)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name");
            var model = new WorkItem()
            {
                DailyReportId = id
            };
            return View(model);
        }

        // POST: EXAMPLEProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWork([Bind(Include = "WorkItemId,Summary,Performance,ContractorId,DailyReportId,MenWorked,HoursWorked")] WorkItem work)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            if (ModelState.IsValid)
            {
                db.WorkItems.Add(work);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name");
            return View(work);
        }
    }
}


