using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PCA.Models
{
    public class Phase
    {
        [Key]
        public int PhaseId { get; set; }

        [StringLength(255)]
        public string Number { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
        public bool IsMarkup { get; set; }

    }
}