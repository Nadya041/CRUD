using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Web.Models
{
    public class LogInModel
    {
        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Username")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        public string RedirectUrl { get; set; }
    }
}
