using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCA.Models;

namespace PCA.ViewModels
{
    public class BudgetDetailReportViewModel
    {
        /*public List<Phase> Phases { get; set; }
        public List<Budget> Budgets { get; set; }
        public List<double> TotalCost { get; set; }*/


        public int PhaseId { get; set; }
        public string PhaseName { get; set; }
        public string PhaseNumber { get; set; }
        public string BudgetDescription { get; set; }
        public double TotalCost { get; set; }

        public BudgetDetailReportViewModel(int pid, string pname, string pnum, string des, double tc)
        {
            this.PhaseId = pid;
            this.PhaseName = pname;
            this.PhaseNumber = pnum;
            this.BudgetDescription = des;
            this.TotalCost = tc;
        }

    }
}
