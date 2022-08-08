using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string? Notes { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public bool IsCreated => Id > 0;
    }
}
