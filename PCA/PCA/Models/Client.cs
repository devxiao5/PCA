﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCA.Models
{
    public class Client
    {
        [Key]
        [Required]
        public int ClientId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Client Parent { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public int StateId { get; set; }

        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string ClientSinceDate { get; set; }

        [Required]
        [StringLength(15)]
        public string Status { get; set; }

        [ForeignKey("StateId")]
        public virtual State States { get; set; }
    }
}