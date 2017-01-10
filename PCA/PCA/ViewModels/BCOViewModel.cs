using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCA.ViewModels
{
    public class BCOViewModel
    {
        public int ContractId { get; set; }
        public int ProjectId { get; set; }

        public int ClientId { get; set; }
        public int ContractorId { get; set; }

        public int? OwnerSigAccountId { get; set; }

        public int? ContractorSigAccountId { get; set; }
    
        public string Type { get; set; }

        public DateTime? OwnerSignDate { get; set; }

        public DateTime? ContractorSignDate { get; set; }
        public double TotalAmount { get; set; }
        public string TotalAmountLiteral { get; set; }

        public DateTime CreateDate { get; set; }


        public int ContractBudgetChangeOrderId { get; set; }
        public int BCOBudgetId { get; set; }



    }
}