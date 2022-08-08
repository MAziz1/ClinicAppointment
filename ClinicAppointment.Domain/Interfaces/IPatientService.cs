using ClinicAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Domain.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> SavePatientAsync(Patient patient);
    }
}
