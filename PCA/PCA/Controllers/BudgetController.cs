/* Title: Budget Contoller
 * Description: Manages budget information for selected project
 * Location: /Budget/
*/

// Using
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using PCA.Models;
using PCA.ViewModels;

namespace PCA.Controllers
{
    public class BudgetController : Controller
    {
        // Database instance object
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Budget/
        // Displays list of budget items and budget information 
        public ActionResult Index()
        {
            // Dependencies v0.1
            int currentProjectNumber = Convert.ToInt32(Session["DailyReportProject"]);
            string currentStatus = Session["DailyReportStatus"].ToString();
            int currentUserId = Convert.ToInt32(Session["UserId"]);

            // Declarations
            double runningPhaseTotal = 0; // Total of each budget per phase
            double pendingTotal = 0; // Total of pending budgets
            double reviewedTotal = 0; // Total of reviewed budgets
            double approvedTotal = 0; // Total of approved budgets
            List<BudgetIndexPhaseViewModel> phaseModel = new List<BudgetIndexPhaseViewModel>(); // Create list of view models for phases
            List<BudgetIndexViewModel> indexViewModel = new List<BudgetIndexViewModel>(); // Create list of view model for view



            // Pull budget information to fill view model 
            var viewModelInformation = from bud in db.Budgets
                                       join phase in db.Phases on bud.PhaseId equals phase.PhaseId
                                       join subphase in db.SubPhases on bud.SubPhaseId equals subphase.SubPhaseId
                                       where bud.ProjectId == currentProjectNumber
                                       select new
                                       {
                                           phase.Number,
                                           phase.PhaseId,
                                           phase.Name,
                                           bud.SubPhaseId,
                                           bud.BudgetId,
                                           subphase.SubPhaseName,
                                           bud.TotalCost,
                                           bud.Description,
                                           bud.Status
                                       };

            // Get all phases that the current project uses
            var phaseList = (from phase in viewModelInformation
                            select new
                            {
                                phase.Number
                            }).Distinct();

            
            // Calculate total costs per each phase and workflow phase
            foreach (var phase in phaseList)
            {
                runningPhaseTotal = 0;

                foreach (var bud in viewModelInformation)
                {
                    if (phase.Number == bud.Number)
                    {
                        runningPhaseTotal += bud.TotalCost;
                        switch (bud.Status)
                        {
                            case "Pending":
                                pendingTotal += bud.TotalCost;
                                break;
                            case "Reviewed":
                                reviewedTotal += bud.TotalCost;
                                break;
                            case "Approved":
                                approvedTotal += bud.TotalCost;
                                break;                        }
                    }
                }

                // ViewBag information - pass inforation to view
                ViewBag.budgetTotal = pendingTotal + reviewedTotal + approvedTotal;
                ViewBag.pendingTotal = pendingTotal;
                ViewBag.reviewedTotal = reviewedTotal;
                ViewBag.approvedTotal = approvedTotal;

                // Creates viewmodel per each phase
                BudgetIndexPhaseViewModel p = new BudgetIndexPhaseViewModel();

                // Fill view model information
                p.PhaseNumber = phase.Number;
                p.PhaseTotal = runningPhaseTotal;

                // Add current viewmodel to list of viewmodels
                phaseModel.Add(p);
            }

            // Fill information to view model
            foreach (var model in viewModelInformation)
            {
                BudgetIndexViewModel vm = new BudgetIndexViewModel();
                vm.PhaseNumber = model.Number;
                vm.PhaseName = model.Name;
                vm.PhaseTotal = 0;
                vm.SubPhaseName = model.SubPhaseName;
                vm.BudgetTotal = model.TotalCost;
                vm.BudgetDescription = model.Description;
                vm.Status = model.Status;
                vm.BudgetId = model.BudgetId;
                vm.PhaseTotal = phaseModel.Where(p => p.PhaseNumber == model.Number).Select(p => p.PhaseTotal).First();
                indexViewModel.Add(vm);
            }

            // Pass viewmodel to view
            return View(indexViewModel);
        }

        // REFERENCE METHOD
        public ActionResult IndexOG()
        {
            // Navbar Dependencies
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));

            // Creates collection of viewmodel
            List<BudgetPhaseViewModelOG> viewModel = new List<BudgetPhaseViewModelOG>();

            // Declarations
            int currentProjectNumber = ViewBag.CurrentProjectNumber;
            double budgetSummaryTotal = 0;
            double runningTotal = 0;
            int runningPhaseId;
            string runningPhaseName;
            string runningPhaseNumber;
            double runningSiteTotal = 0;
            double runningEquipmentTotal = 0;
            double runningBuildingTotal = 0;
            double runningMiscTotal = 0;

            // Pulls information from database
            List<Phase> phases = new List<Phase>(db.Phases);
            List<Budget> budgets = new List<Budget>(from budget in db.Budgets
                                                    where budget.ProjectId == currentProjectNumber
                                                    select budget);

            // Calculates total budget for each phase
            foreach (var phase in phases)
            {
                runningPhaseId = phase.PhaseId;
                runningPhaseName = phase.Name;
                runningPhaseNumber = phase.Number;

                foreach (var budget in budgets)
                {
                    if (phase.PhaseId == budget.PhaseId)
                    {
                        runningTotal += budget.TotalCost;
                        budgetSummaryTotal += budget.TotalCost;
                    }
                    switch (budget.Type)
                    {
                        case "Site":
                            runningSiteTotal += budget.TotalCost;
                            break;
                        case "Equipment":
                            runningEquipmentTotal += budget.TotalCost;
                            break;
                        case "Building":
                            runningBuildingTotal += budget.TotalCost;
                            break;
                        case "Misc":
                            runningMiscTotal += budget.TotalCost;
                            break;
                    }
                }
                viewModel.Add(new BudgetPhaseViewModelOG(runningPhaseId, runningPhaseName, runningPhaseNumber, runningTotal));
                runningTotal = 0;
            }

            // Budget Summary - Toal Budgeted
            ViewBag.BudgetSummaryTotal = budgetSummaryTotal;
            ViewBag.SiteTotal = runningSiteTotal;
            ViewBag.EquipmentTotal = runningEquipmentTotal;
            ViewBag.BuildingTotal = runningBuildingTotal;
            ViewBag.MiscTotal = runningMiscTotal;


            return View(viewModel);
        }


        // GET: /Budget/Create
        // Creates new budget item. Can be used from within the index controller or within phase detail
        public ActionResult Create()
        {
            // Navbar Dependencies
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));

            // Gets lists of options for dropdown boxes
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Number");
            ViewBag.SubPhaseId = new SelectList(db.SubPhases, "SubPhaseId", "SubPhaseName");
            ViewBag.Type = new SelectList(Enum.GetValues(typeof(DepreciationType)).Cast<DepreciationType>().Select(t => new SelectListItem
            {
                Text = t.ToString(),
                Value = t.ToString()
            }).ToList(), "Value", "Text");

            Budget budget = new Budget();
            budget.Status = "Pending";

            return View(budget);
        }


        /* Title: Budget Create POST
         * Description: Posts submitted information from the budget create controller
         * Location: /Budget/Create
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetId,Description,ProjectId,PhaseId,SubPhaseId," +
                                                   "Type,Quantity,Unit,Cost,TotalCost,Status")] Budget budget)
        {
            // Current project / Navbar dependencies
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));

            // Validate input then add to database
            if (ModelState.IsValid)
            {
                db.Budgets.Add(budget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Gets lists of options for dropdown boxes
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Number");
            ViewBag.SubPhaseId = new SelectList(db.SubPhases, "SubPhaseId", "SubPhaseName");
            ViewBag.Type = new SelectList(Enum.GetValues(typeof(DepreciationType)).Cast<DepreciationType>().Select(t => new SelectListItem
            {
                Text = t.ToString(),
                Value = t.ToString()
            }).ToList(), "Value", "Text");
            return View(budget);
        }


        /* Title: Budget Edit
         * Description: Loads current budget item information for user to edit
         * Location: /Budget/Edit/1
        */
        public ActionResult Edit(int? id)
        {
            // Access current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = currentList.ElementAt(1);

            // Verify id given by user
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Pulls budget item from database
            Budget budget = db.Budgets.Find(id);

            // Checks it budget item exists
            if (budget == null)
            {
                return HttpNotFound();
            }

            // Gets lists of options for dropdown boxes
            ViewBag.BudgetId = new SelectList(db.Budgets, "BudgetId", "Description", budget.BudgetId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", budget.ProjectId);
            return View(budget);
        }


        /* Title: Budget Edit POST
         * Description: POSTS updated information to database
         * Location: /Budget/Edit/1
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetId,Description,ProjectId,PhaseId,Quantity,Unit,Cost,TotalCost,Status")] Budget budget)
        {
            // Access current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = currentList.ElementAt(1);

            // Gets current phase to return user to correct phase page
            int returnPhase = budget.PhaseId;

            // Checks if edited information is valid & saves to database
            if (ModelState.IsValid)
            {
                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Phase", "Budget", new { id = returnPhase });
            }
            return View();
        }


        /* Title: Budget Phase
         * Description: Displays detailed budget information about a specific phase
         * Location: /Budget/Phase/15
        */
        public ActionResult Phase(int id)
        {
            // Access current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = currentList.ElementAt(1);
            int currentProjectNumber = int.Parse(ViewBag.CurrentProjectNumber);

            // Pulls information from database
            var budgets = new List<Budget>(db.Budgets.Where(i => i.ProjectId == currentProjectNumber));
            var budgetss = from budget in db.Budgets
                           where budget.ProjectId == currentProjectNumber
                           where budget.PhaseId == id
                           select budget;

            // Checks if objects exists
            if (budgets.Count == 0)
            {
                return HttpNotFound();
            }

            // ViewBag.PhaseId = new SelectList(db.Phases, "PhaseId", "Name", budget.);
            // ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", budget.ProjectId);

            var currentPhase = from phase in db.Phases
                               where phase.PhaseId == id
                               select phase;

            foreach (var item in currentPhase)
            {
                var currentPhaseNumber = item.Number;
                var currentPhaseName = item.Name;
                ViewBag.currentPhase = currentPhaseNumber + " - " + currentPhaseName;
            }

            ViewBag.Budgets = budgetss;
            return View(budgets);
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

        public ActionResult Workflow(int? id)
        {
            //Get current project
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
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // GET: Budgets/WorkflowForward/5
        public ActionResult WorkflowForward(int? id)
        {
            //Get current project
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
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }

            switch (budget.Status)
            {
                case "Pending":
                    budget.Status = "Reviewed";
                    db.SaveChanges();
                    break;
                case "Reviewed":
                    budget.Status = "Approved";
                    db.SaveChanges();
                    break;

            }

            return RedirectToAction("Index");
        }

        // GET: Budget/Select
        public ActionResult Select()
        {
            ViewBag.Status = new List<SelectListItem>()
            {
                new SelectListItem() { Text="Pending", Value="Pending" },
                new SelectListItem() { Text="Approved", Value="Approved" },
            };
            List<SelectListItem> projectList = new SelectList(db.Projects, "ProjectId", "Name").ToList();
            projectList.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            ViewBag.ProjectId = projectList;

            return View();
        }



    }
}