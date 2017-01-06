using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCA.ViewModels
{
    public class DashboardViewModel
    {
        public int DailyReportPending { get; set; }
        public int DailyReportApproved { get; set; }

        public int BudgetPending { get; set; }
        public int BudgetApproved { get; set; }


    }
}