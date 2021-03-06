﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;
using System.Net;
using PCA.ViewModels;

namespace PCA.Controllers
{
    public class ContractController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contract
        public ActionResult Index()
        {
            // Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            // -----------

            // Declarations
            int currentProjectNumber = ViewBag.CurrentProjectNumber;

            List<Contract> contracts = new List<Contract>(from contract in db.Contracts
                                                       where contract.ProjectId == currentProjectNumber
                                                       select contract);

            return View(contracts);
        }

        public ActionResult GeneralCondition(int? id)
        {

            return View();

        }

        public ActionResult MaterialPurchaseOrder()
        {
            return View();

        }

        // GET: Contract/Create
        public ActionResult Create()
        {
            // Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            // -----------

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name");
            ViewBag.ContractorSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName");
            ViewBag.OwnerSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name");
            return View();
        }

        // POST: Contract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractId,ProjectId,ClientId,ContractorId,Type,TotalAmount,TotalAmountLiteral")] Contract contract)
        {
            // Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            // -----------

            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
                TempData["ContractId"] = contract.ContractId;
                if (contract.Type == "GeneralCondition")
                {
                    return RedirectToAction("CreateGC");
                }
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", contract.ClientId);
            ViewBag.ContractorSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName", contract.ContractorSigAccountId);
            ViewBag.OwnerSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName", contract.OwnerSigAccountId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", contract.ProjectId);
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name");
            return View(contract);
        }

        // GET: ContractGeneralConditions/Create
        public ActionResult CreateGC()
        {
            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "Type", TempData["ContractId"]);
            return View();
        }

        // POST: ContractGeneralConditions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGC([Bind(Include = "ContractGeneralConditionId,ContractId,ScopeOfWork,CommencementDate,WorkingDays")] ContractGeneralCondition contractGeneralCondition)
        {
            if (ModelState.IsValid)
            {
                db.ContractGeneralConditions.Add(contractGeneralCondition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractId = new SelectList(db.Contracts, "ContractId", "ContractId", contractGeneralCondition.ContractId);
            return View(contractGeneralCondition);
        }

        // GET: Contract/Details/5
        public ActionResult Details(int? id)
        {
            // Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            // -----------

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

        public ActionResult CreateBCO(int id)
        {
            // Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            int currentProject = int.Parse(currentList.ElementAt(1));
            ViewBag.CurrentProjectNumber = currentProject;
            // -----------

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name");
            ViewBag.ContractorSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName");
            ViewBag.OwnerSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects.Where(p => p.ProjectId == currentProject), "ProjectId", "Name");
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name");

            var budget = db.Budgets.Find(id);

            ViewBag.ContractCurrentTotal = budget.TotalCost;

            BCOViewModel bco = new BCOViewModel();

            bco.CreateDate = DateTime.Now;
            bco.ContractId = currentProject;
            bco.OwnerSigAccountId = null;
            bco.ContractorSigAccountId = null;
            bco.Type = "BCO";
            bco.BCOBudgetId = id;

            return View(bco);
        }

        // POST: Contract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBCO([Bind(Include = "ContractId,ProjectId,ClientId,ContractorId,OwnerSigAccountId,ContractorSigAccountId,Type,OwnerSignDate,ContractorSignDate,TotalAmount,TotalAmountLiteral,CreateDate, ContractBudgetChangeOrderId, BCOBudgetId")] BCOViewModel contract)
        {
            // Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            // -----------


            Contract c = new Contract();
            c.ContractId = contract.ContractId;
            c.ProjectId = contract.ProjectId;
            c.ClientId = contract.ClientId;
            c.ContractorId = contract.ContractorId;
            c.OwnerSigAccountId = contract.OwnerSigAccountId;
            c.ContractorSigAccountId = contract.ContractorSigAccountId;
            c.Type = contract.Type;
            c.OwnerSignDate = contract.OwnerSignDate;
            c.ContractorSignDate = contract.ContractorSignDate;
            c.TotalAmount = contract.TotalAmount;
            c.TotalAmountLiteral = contract.TotalAmountLiteral;
            c.CreateDate = contract.CreateDate;

            ContractBudgetChangeOrder bco = new ContractBudgetChangeOrder();
            bco.BudgetId = contract.BCOBudgetId;
            bco.ContractId = contract.ContractId;

            if (ModelState.IsValid)
            {
                db.Contracts.Add(c);
                db.ContractBudgetChangeOrders.Add(bco);
                db.SaveChanges();
                return RedirectToAction("Index","Budget");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", contract.ClientId);
            ViewBag.ContractorSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName", contract.ContractorSigAccountId);
            ViewBag.OwnerSigAccountId = new SelectList(db.Accounts, "AccountId", "FirstName", contract.OwnerSigAccountId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", contract.ProjectId);
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name");
            return View(contract);
        }
    }
}