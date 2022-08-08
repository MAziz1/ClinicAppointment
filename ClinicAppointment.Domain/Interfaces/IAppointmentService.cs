using ClinicAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Domain.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment> SaveAppointmentAsync(Appointment appointment);
        bool HasOverlapTime(Appointment appointment);
        List<Appointment> GetAppointments();
    }
}
