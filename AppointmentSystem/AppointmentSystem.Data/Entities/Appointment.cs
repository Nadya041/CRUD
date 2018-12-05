using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppointmentSystem.Data.Entities
{
    public class Appointment : IBaseEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required.")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "Required.")]
        public DateTime EndDateTime { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<AppointmentActivity> Activities { get; set; }
    }
}
