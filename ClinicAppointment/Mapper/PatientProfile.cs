using AutoMapper;
using ClinicAppointment.Commands;
using ClinicAppointment.Domain.Dtos;
using ClinicAppointment.Domain.Entities;

namespace ClinicAppointment.Mapper
{
    public class PatientProfile:Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, AddNewAppointment>().ReverseMap();
        }
    }
}
