using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCA.Models
{
    public class DailyReportPicture
    {
        [Key]
        public int DailyReportPictureId { get; set; }

        public int DailyReportId { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public virtual ICollection<File> Files { get; set; }

        // Constraint / Relationships
        [ForeignKey("DailyReportId")]
        public virtual DailyReport DailyReports { get; set; }

    }
}