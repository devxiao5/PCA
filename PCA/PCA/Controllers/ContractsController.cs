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
    public class ContractsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contracts
        public ActionResult Index()
        {
            var contracts = db.Contracts.Include(c => c.Clients).Include(c => c.ContractorAccount).Include(c => c.OwnerAccount).Include(c => c.Projects);
            return View(contracts.ToList());
        }

        // GET: Contracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: Contracts/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name");
            ViewBag.ContractorSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName");
            ViewBag.OwnerSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractId,ProjectId,ClientId,OwnerSigAccountId,ContractorSigAccountId,Type,OwnerSignDate,ContractorSignDate,TotalAmount,TotalAmountLiteral")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", contract.ClientId);
            ViewBag.ContractorSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName", contract.ContractorSigAccountId);
            ViewBag.OwnerSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName", contract.OwnerSigAccountId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", contract.ProjectId);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", contract.ClientId);
            ViewBag.ContractorSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName", contract.ContractorSigAccountId);
            ViewBag.OwnerSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName", contract.OwnerSigAccountId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", contract.ProjectId);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractId,ProjectId,ClientId,OwnerSigAccountId,ContractorSigAccountId,Type,OwnerSignDate,ContractorSignDate,TotalAmount,TotalAmountLiteral")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", contract.ClientId);
            ViewBag.ContractorSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName", contract.ContractorSigAccountId);
            ViewBag.OwnerSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName", contract.OwnerSigAccountId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", contract.ProjectId);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
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
