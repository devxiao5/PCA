using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public int ProjectId { get; set; }
        public int ContractorId { get; set; }
        public string AIANumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string AccountNumber { get; set; }
        public string OrderNumber { get; set; }
        public double TotalAmount { get; set; }
        public int TermInDays { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateOfInvoice { get; set; }
        public string Status { get; set; }

        // Constraint / Relationships
        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractors { get; set; }
    }
}