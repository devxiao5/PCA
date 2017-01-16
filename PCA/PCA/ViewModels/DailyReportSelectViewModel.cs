using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.ViewModels
{
    public class DailyReportSelectViewModel
    {
        [Key]
        public int DailyReportId { get; set; }
        public int ProjectId { get; set; }
        public string Status { get; set; }


    }
}