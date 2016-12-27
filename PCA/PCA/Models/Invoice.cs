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
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        [Display(Name = "Contractor")]
        public int ContractorId { get; set; }
        [Display(Name = "AIA #")]
        public string AIANumber { get; set; }
        [Display(Name = "Invoice #")]
        public string InvoiceNumber { get; set; }
        [Display(Name = "Account #")]
        public string AccountNumber { get; set; }
        [Display(Name = "Order #")]
        public string OrderNumber { get; set; }
        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }
        [Display(Name = "Term (Days)")]
        public int TermInDays { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Received")]
        public DateTime DateReceived { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Invoice")]
        public DateTime DateOfInvoice { get; set; }
        public string Status { get; set; }
        [Display(Name = "Image")]
        public virtual ICollection<File> Files { get; set; }

        // Constraint / Relationships
        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractors { get; set; }
    }
}