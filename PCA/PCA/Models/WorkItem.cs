using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class WorkItem
    {
        [Key]
        public int WorkItemId { get; set; }
        public int DailyReportId { get; set; }
        public int ContractorId { get; set; }

        public string Summary { get; set; }
        public int Performance { get; set; }
        public int MenWorked { get; set; }
        public double HoursWorked { get; set; }

        // Constraint / Relationships
        [ForeignKey("DailyReportId")]
        public virtual DailyReport DailyReports { get; set; }
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractors { get; set; }
    }
}