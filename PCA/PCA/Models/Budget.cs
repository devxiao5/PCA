using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        public int ProjectId { get; set; }

        public int PhaseId { get; set; }

        public int SubPhaseId { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public double Quantity { get; set; }

        [StringLength(10)]
        public string Unit { get; set; }

        public double UnitCost { get; set; }

        public double TotalCost { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        // Constraints / Relationships
        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
        [ForeignKey("PhaseId")]
        public virtual Phase Phases { get; set; }
        [ForeignKey("SubPhaseId")]
        public virtual SubPhase SubPhases { get; set; }
        
    }
}