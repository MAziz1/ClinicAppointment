using AutoMapper;
using ClinicAppointment.Commands;
using ClinicAppointment.Domain.Dtos;
using ClinicAppointment.Domain.Entities;
using ClinicAppointment.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentService appointmentService, 
            IPatientService patientService,IMapper mapper)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody]AddNewAppointment newAppointment)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var appointment = _mapper.Map<Appointment>(newAppointment);
            var patient = _mapper.Map<Patient>(newAppointment);

            patient = await _patientService.SavePatientAsync(patient);

            if (!patient.IsValid)
                return BadRequest("Failed to create patient");

            var doctorHasOvelappedTime = _appointmentService.HasOverlapTime(appointment);
            if(doctorHasOvelappedTime)
                return Conflict("Doctor has 2 appointments on this time, please select another time");


            appointment.PatientId = patient.Id;
            appointment = await _appointmentService.SaveAppointmentAsync(appointment);
            if (appointment.IsCreated)
                return StatusCode(StatusCodes.Status201Created);
            else
                return BadRequest("Failed to create appointment");
        }
    }
}
