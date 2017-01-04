using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace PCA.Models
{
    public class WorkItem
    {
        [Key]
        public int WorkItemId { get; set; }

        public int? DailyReportId { get; set; }

        public int? ContractorId { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000)]
        public string Summary { get; set; }

        public WorkPerformance Performance { get; set; }

        [Display(Name = "Men Worked")]
        public int MenWorked { get; set; }

        [Display(Name = "Hours Worked")]
        public double HoursWorked { get; set; }

        public DateTime Timestamp { get; set; }

        // Constraint / Relationships
        [ForeignKey("DailyReportId")]
        public virtual DailyReport DailyReports { get; set; }

        [Display(Name = "Contractor")]
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractors { get; set; }

        public WorkItem()
        {
            Timestamp = DateTime.Now;
        }

    }
}