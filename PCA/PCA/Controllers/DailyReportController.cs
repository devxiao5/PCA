using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;
using PCA.ViewModels;
using System.Net;
using System.Data.Entity;

namespace PCA.Controllers
{
    public class DailyReportController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();



        public ActionResult WorkflowRedirect(string project, string status)
        {
            Session["DailyReportProject"] = project;
            Session["DailyReportStatus"] = status;

            return RedirectToAction("Index");

        }

        // GET: DailyReport
        public ActionResult Index()
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            // -----------

            //List<BudgetPhaseViewModel> viewModel = new List<BudgetPhaseViewModel>();

            // Dependencies v0.1
            int currentProjectNumber = Convert.ToInt32(Session["DailyReportProject"]);
            string currentStatus = Session["DailyReportStatus"].ToString();
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentStatus = currentStatus;
            ViewBag.currentProject = currentProjectNumber;


            // ----- Auth -----
            if (systemController.Auth(new List<int> { 1, 2 }, currentUserId) == false)
            {
                return RedirectToAction("Index", "Home");
            }

            // ------------------

            List<DailyReportViewModel> viewModel = new List<DailyReportViewModel>();

            if (currentProjectNumber == 0)
            {
                var vm = from dailyReport in db.DailyReport
                            join project in db.Projects on dailyReport.ProjectId equals project.ProjectId
                            where dailyReport.Status == currentStatus
                            select new
                            {
                                dailyReport.DailyReportId,
                                dailyReport.ProjectId,
                                project.Name,
                                dailyReport.Date,
                                dailyReport.Summary,
                                dailyReport.Status
                            };

                foreach (var m in vm)
                {
                    DailyReportViewModel d = new DailyReportViewModel(m.DailyReportId, m.ProjectId, m.Name, m.Date, m.Summary, m.Status, m.Date.ToString("yyyy-MM-dd"));
                    viewModel.Add(d);
                }

             }
            else
            {
                var vm = from dailyReport in db.DailyReport
                         join project in db.Projects on dailyReport.ProjectId equals project.ProjectId
                         where dailyReport.Status == currentStatus &&
                               dailyReport.ProjectId == currentProjectNumber
                         select new
                         {
                             dailyReport.DailyReportId,
                             dailyReport.ProjectId,
                             project.Name,
                             dailyReport.Date,
                             dailyReport.Summary,
                             dailyReport.Status
                         };

                foreach (var m in vm)
                {
                    DailyReportViewModel d = new DailyReportViewModel(m.DailyReportId, m.ProjectId, m.Name, m.Date, m.Summary, m.Status, m.Date.ToString("yyyy-MM-dd"));
                    viewModel.Add(d);
                }

            }

            /*
            var viewModelInformation = from work in db.WorkItems
                                       join report in db.DailyReport on work.DailyReportId equals report.DailyReportId
                                       where report.ProjectId == currentProjectNumber
                                       select new { report.DailyReportId, report.ProjectId, report.Date, report.Summary,
                                       report.Status, work.WorkItemId, work.ContractorId, work.Performance, work.MenWorked,
                                       work.HoursWorked };

            List<DailyReportViewModel> reportModel = new List<DailyReportViewModel>();
            

            foreach (var report in dailyReports)
            {
                double totalHourRunningTotal = 0;
                DailyReportViewModel vm = new DailyReportViewModel();
                vm.DailyReportId = report.DailyReportId;
                vm.ProjectId = report.ProjectId;
                vm.Date = report.Date;
                vm.Summary = report.Summary;
                vm.Status = report.Status;
                var lw = new List<WorkItem>();
                foreach (var item in viewModelInformation)
                {
                    if (item.DailyReportId == vm.DailyReportId)
                    {
                        WorkItem w = new WorkItem();
                        w.WorkItemId = item.WorkItemId;
                        w.DailyReportId = item.DailyReportId;
                        w.ContractorId = item.ContractorId;
                        w.Performance = item.Performance;
                        w.MenWorked = item.MenWorked;
                        w.HoursWorked = item.HoursWorked;
                        double workHoursTotal = item.MenWorked * item.HoursWorked;
                        totalHourRunningTotal += workHoursTotal;
                        lw.Add(w);
                    }
                }

                vm.TotalHours = totalHourRunningTotal;
                vm.DateString = vm.Date.ToString("yyyy-MM-dd");
                vm.WorkItems = lw;
                reportModel.Add(vm);
            }

            List<string> DailyReportWeekAnal = new List<string>();
            */
            //Analytics 

            //Week report (hours)

            /*DateTime currentDate = DateTime.Now;
            for (int i=0; i>=-7; i--)
            {
                DateTime d = DateTime.Now.AddDays(i);
                string sd = d.ToString("yyyy-MM-dd");
                DailyReportWeekAnal.Add(sd);
            }

            ViewBag.DailyReportAnalWeekDay1 = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            ViewBag.DailyReportAnalWeekDay2 = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
            ViewBag.DailyReportAnalWeekDay3 = DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd");
            ViewBag.DailyReportAnalWeekDay4 = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
            ViewBag.DailyReportAnalWeekDay5 = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
            ViewBag.DailyReportAnalWeekDay6 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            ViewBag.DailyReportAnalWeekDay7 = DateTime.Now.ToString("yyyy-MM-dd");*/


            return View(viewModel);
        }

        public ActionResult Detail(int id)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            var currentReport = db.DailyReport.Find(id);
            string date = currentReport.Date.ToString("yyyy-MM-dd");

            ViewBag.currentReportDate = currentReport.Date.ToString("yyyy-MM-dd");
            ViewBag.currentReportSummary = currentReport.Summary;
            ViewBag.currentReportId = currentReport.DailyReportId;

            List<WorkItem> workList = new List<WorkItem>(from work in db.WorkItems
                                                         where work.DailyReportId == id
                                                         select work);

            return View(workList);
        }

        // GET: EXAMPLEProjects/Create
        public ActionResult Create()
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            return View();
        }

        // POST: EXAMPLEProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DailyReportId,Date,Summary,IsDelay,DelayType,DelayDescription,ProjectId")] DailyReport dailyReport)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            if (ModelState.IsValid)
            {
                dailyReport.Status = "Pending";
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
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

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
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            if (ModelState.IsValid)
            {
                db.WorkItems.Add(work);
                db.SaveChanges();
                return RedirectToAction("Detail", new { id = work.DailyReportId });
            }

            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name");
            ViewBag.Performance = new SelectList(Enum.GetValues(typeof(WorkPerformance)).Cast<WorkPerformance>().Select(t => new SelectListItem
            {
                Text = t.ToString(),
                Value = t.ToString()
            }).ToList(), "Value", "Text");
            return View(work);
        }

        // GET: DailyReport/Delete/5
        public ActionResult Delete(int? id)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport dailyReport = db.DailyReport.Find(id);
            if (dailyReport == null)
            {
                return HttpNotFound();
            }
            return View(dailyReport);
        }

        // POST: DailyReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            DailyReport dailyReport = db.DailyReport.Find(id);

            // Remove workitems attatched to daily report
            db.WorkItems.RemoveRange(dailyReport.WorkItems);
            List<DailyReportPicture> reportpictures = new List<DailyReportPicture>(from report in db.DailyReportPicture
                                                                                   where report.DailyReportId == id
                                                                                   select report);

            

            foreach (var picture in reportpictures)
            {
                db.DailyReportPicture.Remove(picture);
                var file = db.Files.Where(f => f.DailyReportPictureId == picture.DailyReportPictureId).Single();
                db.Files.Remove(file);
            }
            db.DailyReport.Remove(dailyReport);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: DailyReport/Edit/5
        public ActionResult Edit(int? id)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport dailyReport = db.DailyReport.Find(id);
            if (dailyReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects.Where(p => p.ProjectId == currentProjectNum), "ProjectId", "Name");

            return View(dailyReport);
        }

        // POST: DailyReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DailyReportId,ProjectId,Date,Summary,Status")] DailyReport dailyReport)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;


            if (ModelState.IsValid)
            {
                db.Entry(dailyReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects.Where(p => p.ProjectId == currentProjectNum), "ProjectId", "Name");
            return View(dailyReport);
        }

        // GET: DailyReport/CreatePicture
        public ActionResult CreatePicture(int id)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            ViewBag.CurrentDailyReportId = id;
            ViewBag.DailyReportId = new SelectList(db.DailyReport.Where(p => p.DailyReportId == id), "DailyReportId", "Date");
            return View();
        }

        // POST: DailyReport/CreatePicture
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePicture([Bind(Include = "DailyReportPictureId,DailyReportId,Description")] DailyReportPicture dailyReportPicture, HttpPostedFileBase upload)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            int currentReportId = dailyReportPicture.DailyReportId;
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var picture = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Picture,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        picture.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    dailyReportPicture.Files = new List<File> { picture };
                }
                db.DailyReportPicture.Add(dailyReportPicture);
                db.SaveChanges();
                return RedirectToAction("PictureIndex", new { id = currentReportId });
            }

            ViewBag.DailyReportId = new SelectList(db.DailyReport, "DailyReportId", "Summary", dailyReportPicture.DailyReportId);
            return View(dailyReportPicture);
        }

        public ActionResult PictureIndex(int? id)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            //List<BudgetPhaseViewModel> viewModel = new List<BudgetPhaseViewModel>();
            ViewBag.CurrentDailyReportId = id;
            int currentProjectNumber = ViewBag.CurrentProjectNumber;

            List<DailyReportPicture> pictures = new List<DailyReportPicture>(from pic in db.DailyReportPicture
                                                                             where pic.DailyReportId == id
                                                                             select pic);
            var currenreport = from r in db.DailyReport
                               where r.DailyReportId == id
                               select r;

            foreach (var rep in currenreport)
            {
                ViewBag.currentReportDate = rep.Date.ToString();
                ViewBag.currentReportSummary = rep.Summary;
            }

            return View(pictures);
        }

        public ActionResult WorkDetail(int? id)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;
            //---------------------------

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkItem workItem = db.WorkItems.Find(id);
            if (workItem == null)
            {
                return HttpNotFound();
            }
            return View(workItem);


        }

        // GET: DailyReport/WorkEdit/5
        public ActionResult WorkEdit(int? id)
        {
            // Session
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkItem workItem = db.WorkItems.Find(id);
            if (workItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name", workItem.ContractorId);
            ViewBag.DailyReportId = new SelectList(db.DailyReport, "DailyReportId", "Summary", workItem.DailyReportId);
            return View(workItem);
        }

        // POST: DailyReport/WorkEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkEdit([Bind(Include = "WorkItemId,DailyReportId,ContractorId,Summary,Performance,MenWorked,HoursWorked")] WorkItem workItem)
        {
            // Session
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            if (ModelState.IsValid)
            {
                db.Entry(workItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Detail", new { id = workItem.DailyReportId });
            }
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name", workItem.ContractorId);
            ViewBag.DailyReportId = new SelectList(db.DailyReport, "DailyReportId", "Summary", workItem.DailyReportId);
            return View(workItem);
        }

        // GET: DailyReport/WorkDelete/5
        public ActionResult WorkDelete(int? id)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkItem workItem = db.WorkItems.Find(id);
            if (workItem == null)
            {
                return HttpNotFound();
            }
            return View(workItem);
        }

        // POST: DailyReport/WorkDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkDelete(int id)
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            WorkItem workItem = db.WorkItems.Find(id);
            int? drid = workItem.DailyReportId;
            db.WorkItems.Remove(workItem);
            db.SaveChanges();
            return RedirectToAction("Detail", new { id = drid });
        }

        public ActionResult ViewAttachment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReportPicture picture = db.DailyReportPicture.Include(s => s.Files).SingleOrDefault(s => s.DailyReportPictureId == id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // GET: DailyReport/PictureEdit/5
        public ActionResult PictureEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReportPicture dailyReportPicture = db.DailyReportPicture.Find(id);
            if (dailyReportPicture == null)
            {
                return HttpNotFound();
            }
            ViewBag.DailyReportId = new SelectList(db.DailyReport, "DailyReportId", "Summary", dailyReportPicture.DailyReportId);
            return View(dailyReportPicture);
        }

        // POST: DailyReportPictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PictureEdit([Bind(Include = "DailyReportPictureId,DailyReportId,Description")] DailyReportPicture dailyReportPicture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailyReportPicture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DailyReportId = new SelectList(db.DailyReport, "DailyReportId", "Summary", dailyReportPicture.DailyReportId);
            return View(dailyReportPicture);
        }

        // GET: DailyReportPictures/Delete/5
        public ActionResult PictureDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReportPicture dailyReportPicture = db.DailyReportPicture.Find(id);
            if (dailyReportPicture == null)
            {
                return HttpNotFound();
            }
            return View(dailyReportPicture);
        }

        // POST: DailyReportPictures/Delete/5
        [HttpPost, ActionName("PictureDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PictureDeleteConfirmed(int id)
        {
            DailyReportPicture dailyReportPicture = db.DailyReportPicture.Find(id);
            db.DailyReportPicture.Remove(dailyReportPicture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Budgets/Details/5
        public ActionResult WorkFlow(int? id)
        {
            //Get current project
            // -----------

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport report = db.DailyReport.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Budgets/Details/5
        public ActionResult WorkFlowForward(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport report = db.DailyReport.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }

            var currentStatus = report.Status;
            if (currentStatus == "Pending")
            {
                report.Status = "Approved";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult WorkFlowReturn(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport report = db.DailyReport.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }

            var currentStatus = report.Status;
            if (currentStatus == "Approved")
            {
                report.Status = "Pending";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: DailyReport/Select
        public ActionResult Select()
        {
            ViewBag.Status = new List<SelectListItem>()
            {
                new SelectListItem() { Text="Pending", Value="Pending" },
                new SelectListItem() { Text="Approved", Value="Approved" },
            };
            List<SelectListItem> projectList = new SelectList(db.Projects, "ProjectId", "Name").ToList();
            projectList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            ViewBag.ProjectId = projectList;

            return View();
        }

        // POST: DailyReport/Select
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Select([Bind(Include = "DailyReportId,ProjectId,Status")] DailyReportSelectViewModel dailyReportSelectViewModel)
        {
            if (ModelState.IsValid)
            {
                Session["DailyReportProject"] = dailyReportSelectViewModel.ProjectId;
                Session["DailyReportStatus"] = dailyReportSelectViewModel.Status;

                return RedirectToAction("Index");

            }

            return View(dailyReportSelectViewModel);
        }


    }
}


