using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCA.ViewModels
{
    public class BudgetIndexViewModel
    {
        public string PhaseNumber { get; set; }
        public string PhaseName { get; set; }
        public double PhaseTotal { get; set; }


        public string SubPhaseName { get; set; }
        public double BudgetTotal { get; set; }
        public string BudgetDescription { get; set; }
        public string Status { get; set; }



    }
}