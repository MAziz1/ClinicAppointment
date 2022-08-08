using ClinicAppointment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Infrastructure.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(prop => prop.Date)
                .IsRequired();

            builder.Property(prop => prop.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(prop => prop.Time)
                .HasDefaultValue(new TimeOnly(0,0));
        }
    }
}
