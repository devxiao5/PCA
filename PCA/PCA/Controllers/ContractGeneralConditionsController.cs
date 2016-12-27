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
    public class ContractGeneralConditionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContractGeneralConditions
        public ActionResult Index()
        {
            var contractGeneralConditions = db.ContractGeneralConditions.Include(c => c.Contracts);
            return View(contractGeneralConditions.ToList());
        }

        // GET: ContractGeneralConditions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractGeneralCondition contractGeneralCondition = db.ContractGeneralConditions.Find(id);
            if (contractGeneralCondition == null)
            {
                return HttpNotFound();
            }
            return View(contractGeneralCondition);
        }

        // GET: ContractGeneralConditions/Create
        public ActionResult Create()
        {
            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type");
            return View();
        }

        // POST: ContractGeneralConditions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractGeneralConditionId,ContractId,ScopeOfWork,CommencementDate,WorkingDays")] ContractGeneralCondition contractGeneralCondition)
        {
            if (ModelState.IsValid)
            {
                db.ContractGeneralConditions.Add(contractGeneralCondition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type", contractGeneralCondition.ContractId);
            return View(contractGeneralCondition);
        }

        // GET: ContractGeneralConditions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractGeneralCondition contractGeneralCondition = db.ContractGeneralConditions.Find(id);
            if (contractGeneralCondition == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type", contractGeneralCondition.ContractId);
            return View(contractGeneralCondition);
        }

        // POST: ContractGeneralConditions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractGeneralConditionId,ContractId,ScopeOfWork,CommencementDate,WorkingDays")] ContractGeneralCondition contractGeneralCondition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractGeneralCondition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type", contractGeneralCondition.ContractId);
            return View(contractGeneralCondition);
        }

        // GET: ContractGeneralConditions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractGeneralCondition contractGeneralCondition = db.ContractGeneralConditions.Find(id);
            if (contractGeneralCondition == null)
            {
                return HttpNotFound();
            }
            return View(contractGeneralCondition);
        }

        // POST: ContractGeneralConditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractGeneralCondition contractGeneralCondition = db.ContractGeneralConditions.Find(id);
            db.ContractGeneralConditions.Remove(contractGeneralCondition);
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
