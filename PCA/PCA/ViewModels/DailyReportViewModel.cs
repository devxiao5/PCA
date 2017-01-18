using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCA.Models;

namespace PCA.ViewModels
{
    public class DailyReportViewModel
    {
        public int DailyReportId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        // public double TotalHours { get; set; }
        public string DateString { get; set; }
        // public IList<WorkItem> WorkItems { get; set; }

        public DailyReportViewModel(int drid, int pid, string name, DateTime date, string summary, string status, string ds)
        {
            this.DailyReportId = drid;
            this.ProjectId = pid;
            this.Name = name;
            this.Date = date;
            this.Summary = summary;
            this.Status = status;
            this.DateString = ds;
        }
    }
}