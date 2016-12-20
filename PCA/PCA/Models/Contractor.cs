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

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string Zip { get; set; }
        public double Retainage { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public bool IsUnion { get; set; }
        public string Notes { get; set; }

        [ForeignKey("ContactAccountId")]
        public virtual Account Accounts { get; set; }
        [ForeignKey("StateId")]
        public virtual State States { get; set; }

    }
}