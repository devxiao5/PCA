using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class ContractGeneralCondition
    {
        [Key]
        public int ContractGeneralConditionId { get; set; }
        public int ContractId { get; set; }
        public string ScopeOfWork { get; set; }
        [DataType(DataType.Date)]
        public DateTime CommencementDate { get; set; }
        public int WorkingDays { get; set; }

        // Constraint / Relationships
        [ForeignKey("ContractId")]
        public virtual Contract Contracts { get; set; }

    }
}