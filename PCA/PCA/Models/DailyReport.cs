using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class DailyReport
    {
        [Key]
        public int DailyReportId { get; set; }
        public int ProjectId { get; set; }
        public DateTime Date { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }

        // Constraint / Relationships
        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
    }
}