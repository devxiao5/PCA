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
        public string Name { get; set; }
    }
}