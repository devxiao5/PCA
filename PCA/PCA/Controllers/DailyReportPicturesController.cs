using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PCA.Models;

namespace PCA.Controllers
{
    public class DailyReportPicturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DailyReportPictures
        public ActionResult Index()
        {
            var dailyReportPicture = db.DailyReportPicture.Include(d => d.DailyReports);
            return View(dailyReportPicture.ToList());
        }

        // GET: DailyReportPictures/Details/5
        public ActionResult Details(int? id)
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

        // GET: DailyReportPictures/Create
        public ActionResult Create()
        {
            ViewBag.DailyReportId = new SelectList(db.DailyReport, "DailyReportId", "Summary");
            return View();
        }

        // POST: DailyReportPictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DailyReportPictureId,DailyReportId,Description,Date")] DailyReportPicture dailyReportPicture)
        {
            if (ModelState.IsValid)
            {
                db.DailyReportPicture.Add(dailyReportPicture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DailyReportId = new SelectList(db.DailyReport, "DailyReportId", "Summary", dailyReportPicture.DailyReportId);
            return View(dailyReportPicture);
        }

        // GET: DailyReportPictures/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "DailyReportPictureId,DailyReportId,Description,Date")] DailyReportPicture dailyReportPicture)
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
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyReportPicture dailyReportPicture = db.DailyReportPicture.Find(id);
            db.DailyReportPicture.Remove(dailyReportPicture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
