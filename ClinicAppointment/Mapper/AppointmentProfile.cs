using AutoMapper;
using ClinicAppointment.Commands;
using ClinicAppointment.Domain.Dtos;
using ClinicAppointment.Domain.Entities;

namespace ClinicAppointment.Mapper
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<AddNewAppointment, Appointment>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => new TimeOnly(TimeOnly.FromDateTime(src.Date).Hour, TimeOnly.FromDateTime(src.Date).Minute)))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Date)))
                .ReverseMap();
        }
    }
}
