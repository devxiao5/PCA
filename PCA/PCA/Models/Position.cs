using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class Position
    {
        [Key]
        [Required]
        public int PositionId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}