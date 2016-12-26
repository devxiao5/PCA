using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class Contractor
    {
        [Key]
        public int ContractorId { get; set; }
        public int ContactAccountId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        public int StateId { get; set; }

        [StringLength(20)]
        public string Zip { get; set; }

        public double Retainage { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Website { get; set; }

        public bool IsUnion { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        [ForeignKey("ContactAccountId")]
        public virtual Account Accounts { get; set; }
        [ForeignKey("StateId")]
        public virtual State States { get; set; }

    }
}