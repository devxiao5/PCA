using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class ContractorTypeUnion
    {
        [Key]
        public int ContractorTypeUnionId { get; set; }
        public int ContractorId { get; set; }
        public int ContractorTypeId { get; set; }

        [ForeignKey("ContractorId")]
        public virtual Contractor Contractors { get; set; }
        [ForeignKey("ContractorTypeId")]
        public virtual ContractorType ContractorTypes { get; set; }

    }
}