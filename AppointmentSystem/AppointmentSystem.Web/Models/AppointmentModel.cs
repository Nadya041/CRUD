using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppointmentSystem.Web.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date and Time")]
        public DateTime StartDateTime { get; set; }
       
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date and Time")]
        public DateTime EndDateTime { get; set; }

        public int UserId { get; set; } 
        
        public User User { get; set; }
        
        public List<Activity> Activities { get; set; }

        public List<int> ChosenActivities { get; set; }
    }
}
