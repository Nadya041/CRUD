using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Data.Context;
using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppointmentSystem.Data.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        private AppointmentSystemContext Dbcontext;
      
        public AppointmentRepository(AppointmentSystemContext dbContext)
            : base(dbContext)
        {
            Dbcontext = dbContext;
        }

        public override void Insert(Appointment appointment)
        {
            base.Insert(appointment);            
        }

        public override Appointment GetById(int id)
        {
            List<Appointment> appointments = new List<Appointment>();
            var context = Dbcontext;

            var entity = context.Appointments
                .Include(s => s.Activities)
                .Include(u=>u.User)
                .Where(p=> p.Id == id)
                .ToList();

            appointments = entity;
           
            var appointment = appointments.FirstOrDefault();

            return appointment;
        }

        public override void Edit(Appointment item)
        {
            Appointment appointment = GetById(item.Id);                 
            appointment.Activities.Clear();

            appointment.Activities = item.Activities;
            appointment.EndDateTime = item.EndDateTime;
            appointment.Id = item.Id;
            appointment.StartDateTime = item.StartDateTime;
            appointment.UserId = item.UserId;

            DbSet.Update(appointment);
            Context.SaveChanges();
        }
    }
}
