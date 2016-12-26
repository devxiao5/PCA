using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class ContractExhibit
    {
        [Key]
        public int ContractExhibitId { get; set; }
        public int ContractId { get; set; }
        public int Number { get; set; }
        public int SubNumber { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [ForeignKey("ContractId")]
        public virtual Contract Contracts { get; set; }
    }
}