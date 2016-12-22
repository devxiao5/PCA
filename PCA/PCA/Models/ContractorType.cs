using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class ContractorType
    {
        [Key]
        public int ContractorTypeId { get; set; }
        public string Name { get; set; }

    }
}