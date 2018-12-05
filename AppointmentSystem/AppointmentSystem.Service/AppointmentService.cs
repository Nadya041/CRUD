using AppointmentSystem.Data.Entities;
using AppointmentSystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Service
{
    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        public AppointmentService(IAppointmentRepository appointmentRepository)
            :base(appointmentRepository)
        {

        }
    }
}
