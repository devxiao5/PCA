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
    public class BudgetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Budgets
        public ActionResult Index()
        {
            var budgets = db.Budgets.Include(b => b.Phases).Include(b => b.Projects).Include(b => b.SubPhases);
            return View(budgets.ToList());
        }

        // GET: Budgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // GET: Budgets/Create
        public ActionResult Create()
        {
            ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Number");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            ViewBag.SubPhaseId = new SelectList(db.SubPhases, "SubPhaseId", "Name");
            return View();
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetId,ProjectId,PhaseId,SubPhaseId,Type,Description,Quantity,Unit,UnitCost,TotalCost,Status")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                db.Budgets.Add(budget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Number", budget.PhaseId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", budget.ProjectId);
            ViewBag.SubPhaseId = new SelectList(db.SubPhases, "SubPhaseId", "Name", budget.SubPhaseId);
            return View(budget);
        }

        // GET: Budgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Number", budget.PhaseId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", budget.ProjectId);
            ViewBag.SubPhaseId = new SelectList(db.SubPhases, "SubPhaseId", "Name", budget.SubPhaseId);
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetId,ProjectId,PhaseId,SubPhaseId,Type,Description,Quantity,Unit,UnitCost,TotalCost,Status")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Number", budget.PhaseId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", budget.ProjectId);
            ViewBag.SubPhaseId = new SelectList(db.SubPhases, "SubPhaseId", "Name", budget.SubPhaseId);
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Budget budget = db.Budgets.Find(id);
            db.Budgets.Remove(budget);
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
