using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppointmentSystem.Data.Entities
{
    public class Activity : IBaseEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required.")]
        public double Duration { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<AppointmentActivity> AppointmentActivities { get; set; }
    }
}
