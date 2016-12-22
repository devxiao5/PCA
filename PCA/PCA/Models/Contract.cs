using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public int OwnerSigAccountId { get; set; }
        public int ContractorSigAccountId { get; set; }
        public string Type { get; set; }
        public DateTime OwnerSignDate { get; set; }
        public DateTime ContractorSignDate { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Clients { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
        [ForeignKey("OwnerSigAccountId")]
        public virtual Account OwnerAccount { get; set; }
        [ForeignKey("ContractorSigAccountId")]
        public virtual Account ContractorAccount { get; set; }
    }
}