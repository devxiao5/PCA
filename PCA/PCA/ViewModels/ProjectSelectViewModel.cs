using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCA.Models;

namespace PCA.ViewModels
{
    public class ProjectSelectViewModel
    {
        public int ProjectId { get; set; }

        public int ClientId { get; set; }

        public string ProjectName { get; set; }

        public virtual ICollection<File> Files { get; set; }


    }
}