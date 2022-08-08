using ClinicAppointment.Domain.Entities;
using ClinicAppointment.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Infrastructure
{
    public class AppointmentDbContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Appointment> Appointment { get; set; }

        public AppointmentDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
        }
    }
}
