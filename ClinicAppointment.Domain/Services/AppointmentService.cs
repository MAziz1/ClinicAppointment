using ClinicAppointment.Domain.Entities;
using ClinicAppointment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Domain.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment> _repository;

        public AppointmentService(IRepository<Appointment> repository)
        {
            _repository = repository;
        }
        public List<Appointment> GetAppointments()
        {
            return _repository.GetAll().ToList();
        }

        public bool HasOverlapTime(Appointment appointment)
        {
            return _repository.Query(item => item.Date ==  appointment.Date && item.Time == appointment.Time)
                .Count()  == 2;
        }

        public async Task<Appointment> SaveAppointmentAsync(Appointment appointment)
        {
            return await _repository.InsertItemAsync(appointment);

        }
    }
}
