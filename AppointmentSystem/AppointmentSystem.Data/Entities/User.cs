using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppointmentSystem.Data.Entities
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public override int Id { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Name { get; set; }

        public string Phone { get; set; }

        public ICollection<Appointment> Appointments { get; set; }        
    }
}
