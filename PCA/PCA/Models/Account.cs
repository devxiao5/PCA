  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PCA.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Required]
        public string LastName { get; set; }

        [StringLength(255)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(255)]
        [Required]
        public string Username { get; set; }

        [StringLength(255)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(255)]
        [Required]
        [Compare("Password", ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [StringLength(255)]
        [Required]
        public string Type { get; set; }

        [Required]
        public bool CanLogin { get; set; }


    }
}