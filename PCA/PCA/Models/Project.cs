using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class Project
    {
        [Key]
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Class { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string Zip { get; set; }
        public int TotalSquareFoot { get; set; }

        // Billing options
        public bool BillingOptionIsOne { get; set; }
        public bool PreFeeIsFlat { get; set; }
        public double PreFeeValue { get; set; }
        public bool ConstFeeIsFlat { get; set; }
        public double ConstFeeValue { get; set; }
        public double MarkupValue { get; set; }
        // End Billing

        public int PaymentDueDay { get; set; }
        public string Notes { get; set; }


        // Constraint / Relationships
        [ForeignKey("ClientId")]
        public virtual Client Clients { get; set; }
        [ForeignKey("StateId")]
        public virtual State States { get; set; }
    }
}