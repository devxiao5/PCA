using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PCA.Models
{
    public class SubPhase
    {
        [Key]
        public int SubPhaseId { get; set; }

        [StringLength(255)]
        public string SubPhaseName { get; set; }
    }
}