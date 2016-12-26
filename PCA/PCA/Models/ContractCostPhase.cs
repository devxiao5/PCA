using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class ContractCostPhase
    {
        [Key]
        public int ContractCostPhaseId { get; set; }

        public int ContractId { get; set; }

        public int PhaseId { get; set; }

        public int SubPhaseId { get; set; }

        public double Amount { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string DepreciationPhase { get; set; }

        [ForeignKey("ContractId")]
        public virtual Contract Contracts { get; set; }
        [ForeignKey("PhaseId")]
        public virtual Phase Phases { get; set; }
        [ForeignKey("SubPhaseId")]
        public virtual SubPhase SubPhases { get; set; }
    }
}