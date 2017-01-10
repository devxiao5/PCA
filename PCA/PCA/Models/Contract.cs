using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace PCA.Models
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }
        public int ProjectId { get; set; }

        public int ClientId { get; set; }
        public int ContractorId { get; set; }

        public int? OwnerSigAccountId { get; set; }

        public int? ContractorSigAccountId { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        public DateTime? OwnerSignDate { get; set; }

        public DateTime? ContractorSignDate { get; set; }
        public double TotalAmount { get; set; }
        public string TotalAmountLiteral { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Clients { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
        [ForeignKey("OwnerSigAccountId")]
        public virtual Account OwnerAccount { get; set; }
        [ForeignKey("ContractorSigAccountId")]
        public virtual Account ContractorAccount { get; set; }
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractors { get; set; }
    }
}