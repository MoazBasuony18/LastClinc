using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LastClinc.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password and the confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
