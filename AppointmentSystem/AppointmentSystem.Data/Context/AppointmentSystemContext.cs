using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystem.Data.Context
{
    public class AppointmentSystemContext : IdentityDbContext<User, IdentityRole<int>, int>
    {       
        public AppointmentSystemContext(DbContextOptions<AppointmentSystemContext> options)
            : base(options)
        {
            
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Activity> Activities { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)            
        {
            base.OnModelCreating(modelBuilder);
            
            //one-to-many
            modelBuilder.Entity<User>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.User)
                .HasForeignKey(p => p.UserId);

            //many-to-many
            modelBuilder.Entity<AppointmentActivity>()
                .HasKey(aa => new { aa.AppointmentId, aa.ActivityId });

            modelBuilder.Entity<AppointmentActivity>()
                .HasOne(aa => aa.Appointment)
                .WithMany(a => a.Activities)
                .HasForeignKey(aa => aa.AppointmentId);

            modelBuilder.Entity<AppointmentActivity>()
                .HasOne(aa => aa.Activity)
                .WithMany(c => c.AppointmentActivities)
                .HasForeignKey(bc => bc.ActivityId);           
        }
    }
}
