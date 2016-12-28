using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace PCA.Models
{
    public class DailyReport
    {
        [Key]
        public int DailyReportId { get; set; }
        public int ProjectId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [StringLength(1000)]
        public string Summary { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        // Constraint / Relationships
        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
        public virtual ICollection<WorkItem> WorkItems { get; set; }
    }
}