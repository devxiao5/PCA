using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PCA.Models
{
    public class State
    {
        [Required]
        [Key]
        public int StateId { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [Required]
        [StringLength(2)]
        public string Initials { get; set; }
    }
}