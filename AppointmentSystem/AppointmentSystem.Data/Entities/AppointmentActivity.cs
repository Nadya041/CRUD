using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Data.Entities
{
    public class AppointmentActivity
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
