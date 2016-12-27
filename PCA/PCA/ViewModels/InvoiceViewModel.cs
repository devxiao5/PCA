using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCA.ViewModels
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }
        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public double InvoiceTotal { get; set; }
        public string InvoiceDate { get; set; }

        public InvoiceViewModel(int invid, int contid, string cname, double total, string date)
        {
            this.InvoiceId = invid;
            this.ContractorId = contid;
            this.ContractorName = cname;
            this.InvoiceTotal = total;
            this.InvoiceDate = date;
        }
    }
}