using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCA.Models;

namespace PCA.ViewModels
{
    public class BudgetPhaseViewModel
    {
        /*public List<Phase> Phases { get; set; }
        public List<Budget> Budgets { get; set; }
        public List<double> TotalCost { get; set; }*/


        public int PhaseId { get; set; }
        public string PhaseName { get; set; }
        public string PhaseNumber { get; set; }
        public double TotalCost { get; set; }

        public BudgetPhaseViewModel(int pid, string pname, string pnum, double tc)
        {
            this.PhaseId = pid;
            this.PhaseName = pname;
            this.PhaseNumber = pnum;
            this.TotalCost = tc;
        }

    }
}