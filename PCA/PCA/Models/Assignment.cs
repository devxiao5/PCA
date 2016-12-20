using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class Assignment
    {
        [Key]
        [Required]
        public int AssignmentId { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public int PositionId { get; set; }
        [Required]
        public int ProjectId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Accounts { get; set; }
        [ForeignKey("PositionId")]
        public virtual Position Positions { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }

    }
}