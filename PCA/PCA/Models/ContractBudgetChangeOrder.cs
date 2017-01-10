using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class ContractBudgetChangeOrder
    {
        [Key]
        public int ContractBudgetChangeOrderId { get; set; }

        public int ContractId { get; set; }
        public int BudgetId { get; set; }

        [ForeignKey("ContractId")]
        public virtual Contract Contracts { get; set; }

        [ForeignKey("BudgetId")]
        public virtual Budget Budgets { get; set; }

    }
}