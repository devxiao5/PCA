using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;
using PCA.ViewModels;

namespace PCA.Controllers
{
    public class ReportController : Controller
    {
        // Database instance object
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Report
        public ActionResult Index()
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            // -----------

            return View();
        }

        public ActionResult BudgetDetail()
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            // -----------

            // Creates collection of viewmodel
            List<BudgetDetailReportViewModel> viewModel = new List<BudgetDetailReportViewModel>();

            // Declarations
            int currentProjectNumber = ViewBag.CurrentProjectNumber;
            double budgetSummaryTotal = 0;
            double runningTotal = 0;
            int runningPhaseId;
            int runningBudgetId;
            int? runningPhaseTotalNumber = 0;
            string runningBudgetDescription;
            string runningPhaseName;
            string runningPhaseNumber;
            double runningBudgetTotal;
            List<Phase> projectPhases = new List<Phase>();  // Phases current project has
            List<double> phaseTotal = new List<double>();

            // Pulls information from database
            List<Phase> phases = new List<Phase>(db.Phases);  // All phases from database
            /*List<Budget> budgets = new List<Budget>(from budget in db.Budgets
                                                    join p in db.Phases on budget.PhaseId equals p.PhaseId
                                                    orderby p.Number
                                                    where budget.ProjectId == currentProjectNumber
                                                    select new
                                                    {
                                                        budget.Description
                                                    }).ToList();*/

            var budgets = (from budget in db.Budgets
                           join p in db.Phases on budget.PhaseId equals p.PhaseId
                           orderby p.Number
                           where budget.ProjectId == currentProjectNumber
                           select new
                           {
                               p.PhaseId,
                               p.Number,
                               p.Name,
                               budget.Description,
                               budget.TotalCost,
                           }).ToList();

            foreach (var budget in budgets)
            {
                runningPhaseId = budget.PhaseId;
                runningPhaseName = budget.Name;
                runningPhaseNumber = budget.Number;
                runningBudgetDescription = budget.Description;
                runningBudgetTotal = budget.TotalCost;
                viewModel.Add(new BudgetDetailReportViewModel(runningPhaseId, runningPhaseName, runningPhaseNumber, runningBudgetDescription, runningBudgetTotal));

                if (!projectPhases.Exists(p => p.PhaseId == runningPhaseId))
                {
                    var runningPhase = new Phase() { Name = runningPhaseName, Number = runningPhaseNumber, PhaseId = runningPhaseId };
                    projectPhases.Add(runningPhase);
                }

                if (runningPhaseTotalNumber == 0)
                {
                    runningTotal += budget.TotalCost;
                    runningPhaseTotalNumber = budget.PhaseId;
                }
                else if (runningPhaseTotalNumber == budget.PhaseId)
                {
                    runningTotal += budget.TotalCost;
                }
                else
                {
                    phaseTotal.Add(runningTotal);
                    runningTotal = budget.TotalCost;
                }

            }

            ViewBag.projectPhases = projectPhases;
            ViewBag.phaseTotal = phaseTotal;


            /* Calculates total budget for each phase
            foreach (var phase in phases)
            {
                runningPhaseId = phase.PhaseId;
                runningPhaseName = phase.Name;
                runningPhaseNumber = phase.Number;

                

                foreach (var budget in budgets)
                {
                        runningTotal += budget.TotalCost;
                        budgetSummaryTotal += budget.TotalCost;
                        runningBudgetId = budget.BudgetId;
                        budgetList.Add(new Budget() { BudgetId = runningBudgetId, Description = budget.Description, TotalCost = budget.TotalCost });
                   
                }
                viewModel.Add(new BudgetDetailReportViewModel(runningPhaseId, runningPhaseName, runningPhaseNumber, budgetList, runningTotal));
                runningTotal = 0;
            }*/
            return View(viewModel);
        }

        public string GetTotal(int phaseId, int currentProject)
        {
            double total = 0;

            var budgets = (from budget in db.Budgets
                           where budget.ProjectId == currentProject
                           where budget.PhaseId == phaseId
                           select new
                           {
                               budget.TotalCost,
                           }).ToList();

            foreach (var item in budgets)
            {
                total += item.TotalCost;
            }

            string totalString = string.Format("{0:C}", total);

            return totalString;
        }
    }
}