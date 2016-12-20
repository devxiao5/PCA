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
        public string Type { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double UnitCost { get; set; }
        public double TotalCost { get; set; }
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