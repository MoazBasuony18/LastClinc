using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LastClinc.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }
    }
}
