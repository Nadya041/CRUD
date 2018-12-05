using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Web.Models
{
    public class UserModel
    {        
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Name{ get;set;}

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }    
        
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", 
            ErrorMessage = "Password must contain at least one uppercase letter and at least one digit")]
        public string PasswordHash { get; set; }

        public string OldPasswordHash { get; set; }

        public string Phone { get; set; }
    }
}
