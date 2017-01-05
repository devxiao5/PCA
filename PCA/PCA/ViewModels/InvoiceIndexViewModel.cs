using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCA.ViewModels
{
    public class InvoiceIndexViewModel
    {
        public int InvoiceId { get; set; }
        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public double InvoiceTotal { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
    }
}