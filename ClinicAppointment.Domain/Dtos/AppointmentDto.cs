using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Domain.Dtos
{
    public record AppointmentDto(string Notes, DateTime Date);
}
