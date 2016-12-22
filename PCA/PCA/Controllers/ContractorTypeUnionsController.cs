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
    public class ContractorTypeUnionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContractorTypeUnions
        public ActionResult Index()
        {
            var contractorTypeUnions = db.ContractorTypeUnions.Include(c => c.Contractors).Include(c => c.ContractorTypes);
            return View(contractorTypeUnions.ToList());
        }

        // GET: ContractorTypeUnions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorTypeUnion contractorTypeUnion = db.ContractorTypeUnions.Find(id);
            if (contractorTypeUnion == null)
            {
                return HttpNotFound();
            }
            return View(contractorTypeUnion);
        }

        // GET: ContractorTypeUnions/Create
        public ActionResult Create()
        {
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name");
            ViewBag.ContractorTypeId = new SelectList(db.ContractorType, "ContractorTypeId", "Name");
            return View();
        }

        // POST: ContractorTypeUnions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractorTypeUnionId,ContractorId,ContractorTypeId")] ContractorTypeUnion contractorTypeUnion)
        {
            if (ModelState.IsValid)
            {
                db.ContractorTypeUnions.Add(contractorTypeUnion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name", contractorTypeUnion.ContractorId);
            ViewBag.ContractorTypeId = new SelectList(db.ContractorType, "ContractorTypeId", "Name", contractorTypeUnion.ContractorTypeId);
            return View(contractorTypeUnion);
        }

        // GET: ContractorTypeUnions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorTypeUnion contractorTypeUnion = db.ContractorTypeUnions.Find(id);
            if (contractorTypeUnion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name", contractorTypeUnion.ContractorId);
            ViewBag.ContractorTypeId = new SelectList(db.ContractorType, "ContractorTypeId", "Name", contractorTypeUnion.ContractorTypeId);
            return View(contractorTypeUnion);
        }

        // POST: ContractorTypeUnions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractorTypeUnionId,ContractorId,ContractorTypeId")] ContractorTypeUnion contractorTypeUnion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractorTypeUnion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name", contractorTypeUnion.ContractorId);
            ViewBag.ContractorTypeId = new SelectList(db.ContractorType, "ContractorTypeId", "Name", contractorTypeUnion.ContractorTypeId);
            return View(contractorTypeUnion);
        }

        // GET: ContractorTypeUnions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorTypeUnion contractorTypeUnion = db.ContractorTypeUnions.Find(id);
            if (contractorTypeUnion == null)
            {
                return HttpNotFound();
            }
            return View(contractorTypeUnion);
        }

        // POST: ContractorTypeUnions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractorTypeUnion contractorTypeUnion = db.ContractorTypeUnions.Find(id);
            db.ContractorTypeUnions.Remove(contractorTypeUnion);
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
