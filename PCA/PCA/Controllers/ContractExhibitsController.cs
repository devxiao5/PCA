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
    public class ContractExhibitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContractExhibits
        public ActionResult Index()
        {
            var contractExhibits = db.ContractExhibits.Include(c => c.Contracts);
            return View(contractExhibits.ToList());
        }

        // GET: ContractExhibits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractExhibit contractExhibit = db.ContractExhibits.Find(id);
            if (contractExhibit == null)
            {
                return HttpNotFound();
            }
            return View(contractExhibit);
        }

        // GET: ContractExhibits/Create
        public ActionResult Create()
        {
            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type");
            return View();
        }

        // POST: ContractExhibits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractExhibitId,ContractId,Number,SubNumber,Description")] ContractExhibit contractExhibit)
        {
            if (ModelState.IsValid)
            {
                db.ContractExhibits.Add(contractExhibit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type", contractExhibit.ContractId);
            return View(contractExhibit);
        }

        // GET: ContractExhibits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractExhibit contractExhibit = db.ContractExhibits.Find(id);
            if (contractExhibit == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type", contractExhibit.ContractId);
            return View(contractExhibit);
        }

        // POST: ContractExhibits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractExhibitId,ContractId,Number,SubNumber,Description")] ContractExhibit contractExhibit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractExhibit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type", contractExhibit.ContractId);
            return View(contractExhibit);
        }

        // GET: ContractExhibits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractExhibit contractExhibit = db.ContractExhibits.Find(id);
            if (contractExhibit == null)
            {
                return HttpNotFound();
            }
            return View(contractExhibit);
        }

        // POST: ContractExhibits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractExhibit contractExhibit = db.ContractExhibits.Find(id);
            db.ContractExhibits.Remove(contractExhibit);
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
