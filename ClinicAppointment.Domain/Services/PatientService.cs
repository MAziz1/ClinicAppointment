using ClinicAppointment.Domain.Entities;
using ClinicAppointment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Domain.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _repository;

        public PatientService(IRepository<Patient> repository)
        {
            _repository = repository;
        }
        public async Task<Patient> SavePatientAsync(Patient patient)
        {
            return await _repository.InsertItemAsync(patient);
        }
    }
}
