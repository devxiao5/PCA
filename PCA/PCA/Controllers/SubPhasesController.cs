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
    public class SubPhasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubPhases
        public ActionResult Index()
        {
            return View(db.SubPhases.ToList());
        }

        // GET: SubPhases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubPhase subPhase = db.SubPhases.Find(id);
            if (subPhase == null)
            {
                return HttpNotFound();
            }
            return View(subPhase);
        }

        // GET: SubPhases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubPhases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubPhaseId,Name")] SubPhase subPhase)
        {
            if (ModelState.IsValid)
            {
                db.SubPhases.Add(subPhase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subPhase);
        }

        // GET: SubPhases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubPhase subPhase = db.SubPhases.Find(id);
            if (subPhase == null)
            {
                return HttpNotFound();
            }
            return View(subPhase);
        }

        // POST: SubPhases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubPhaseId,Name")] SubPhase subPhase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subPhase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subPhase);
        }

        // GET: SubPhases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubPhase subPhase = db.SubPhases.Find(id);
            if (subPhase == null)
            {
                return HttpNotFound();
            }
            return View(subPhase);
        }

        // POST: SubPhases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubPhase subPhase = db.SubPhases.Find(id);
            db.SubPhases.Remove(subPhase);
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
