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
            string date = currentReport.Date.ToString("yyyy-MM-dd");

            ViewBag.currentReportDate = currentReport.Date.ToString("yyyy-MM-dd");
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
                return RedirectToAction("Detail", new { id = work.DailyReportId });
            }

            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name");
            return View(work);
        }

        // GET: DailyReport/Delete/5
        public ActionResult Delete(int? id)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

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
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

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
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport dailyReport = db.DailyReport.Find(id);
            if (dailyReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", dailyReport.ProjectId);
            return View(dailyReport);
        }

        // POST: DailyReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DailyReportId,ProjectId,Date,Summary,Status")] DailyReport dailyReport)
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
                db.Entry(dailyReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", dailyReport.ProjectId);
            return View(dailyReport);
        }

        // GET: DailyReport/CreatePicture
        public ActionResult CreatePicture(int id)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            ViewBag.CurrentDailyReportId = id;
            ViewBag.DailyReportId = new SelectList(db.DailyReport, "DailyReportId", "Date");
            return View();
        }

        // POST: DailyReport/CreatePicture
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePicture([Bind(Include = "DailyReportPictureId,DailyReportId,Description")] DailyReportPicture dailyReportPicture, HttpPostedFileBase upload)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------
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
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            // -----------

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
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
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
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
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
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name", workItem.ContractorId);
            ViewBag.DailyReportId = new SelectList(db.DailyReport, "DailyReportId", "Summary", workItem.DailyReportId);
            return View(workItem);
        }

        // POST: DailyReport/WorkEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkEdit([Bind(Include = "WorkItemId,DailyReportId,ContractorId,Summary,Performance,MenWorked,HoursWorked")] WorkItem workItem)
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
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
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

        // POST: DailyReport/WorkDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkDelete(int id)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

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
    }
}


