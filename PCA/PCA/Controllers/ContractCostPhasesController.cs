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
    public class ContractCostPhasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContractCostPhases
        public ActionResult Index()
        {
            var contractCostPhases = db.ContractCostPhases.Include(c => c.Contracts).Include(c => c.Phases).Include(c => c.SubPhases);
            return View(contractCostPhases.ToList());
        }

        // GET: ContractCostPhases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractCostPhase contractCostPhase = db.ContractCostPhases.Find(id);
            if (contractCostPhase == null)
            {
                return HttpNotFound();
            }
            return View(contractCostPhase);
        }

        // GET: ContractCostPhases/Create
        public ActionResult Create()
        {
            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type");
            ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Number");
            ViewBag.SubPhaseId = new SelectList(db.SubPhases, "SubPhaseId", "Name");
            return View();
        }

        // POST: ContractCostPhases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractCostPhaseId,ContractId,PhaseId,SubPhaseId,Amount,Description,DepreceatonPhase")] ContractCostPhase contractCostPhase)
        {
            if (ModelState.IsValid)
            {
                db.ContractCostPhases.Add(contractCostPhase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type", contractCostPhase.ContractId);
            ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Number", contractCostPhase.PhaseId);
            ViewBag.SubPhaseId = new SelectList(db.SubPhases, "SubPhaseId", "Name", contractCostPhase.SubPhaseId);
            return View(contractCostPhase);
        }

        // GET: ContractCostPhases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractCostPhase contractCostPhase = db.ContractCostPhases.Find(id);
            if (contractCostPhase == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type", contractCostPhase.ContractId);
            ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Number", contractCostPhase.PhaseId);
            ViewBag.SubPhaseId = new SelectList(db.SubPhases, "SubPhaseId", "Name", contractCostPhase.SubPhaseId);
            return View(contractCostPhase);
        }

        // POST: ContractCostPhases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractCostPhaseId,ContractId,PhaseId,SubPhaseId,Amount,Description,DepreceatonPhase")] ContractCostPhase contractCostPhase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractCostPhase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type", contractCostPhase.ContractId);
            ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Number", contractCostPhase.PhaseId);
            ViewBag.SubPhaseId = new SelectList(db.SubPhases, "SubPhaseId", "Name", contractCostPhase.SubPhaseId);
            return View(contractCostPhase);
        }

        // GET: ContractCostPhases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractCostPhase contractCostPhase = db.ContractCostPhases.Find(id);
            if (contractCostPhase == null)
            {
                return HttpNotFound();
            }
            return View(contractCostPhase);
        }

        // POST: ContractCostPhases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractCostPhase contractCostPhase = db.ContractCostPhases.Find(id);
            db.ContractCostPhases.Remove(contractCostPhase);
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
