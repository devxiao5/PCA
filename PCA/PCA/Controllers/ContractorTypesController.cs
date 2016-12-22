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
    public class ContractorTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContractorTypes
        public ActionResult Index()
        {
            return View(db.ContractorType.ToList());
        }

        // GET: ContractorTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorType contractorType = db.ContractorType.Find(id);
            if (contractorType == null)
            {
                return HttpNotFound();
            }
            return View(contractorType);
        }

        // GET: ContractorTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractorTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractorTypeId,Name")] ContractorType contractorType)
        {
            if (ModelState.IsValid)
            {
                db.ContractorType.Add(contractorType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractorType);
        }

        // GET: ContractorTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorType contractorType = db.ContractorType.Find(id);
            if (contractorType == null)
            {
                return HttpNotFound();
            }
            return View(contractorType);
        }

        // POST: ContractorTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractorTypeId,Name")] ContractorType contractorType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractorType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractorType);
        }

        // GET: ContractorTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractorType contractorType = db.ContractorType.Find(id);
            if (contractorType == null)
            {
                return HttpNotFound();
            }
            return View(contractorType);
        }

        // POST: ContractorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractorType contractorType = db.ContractorType.Find(id);
            db.ContractorType.Remove(contractorType);
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
