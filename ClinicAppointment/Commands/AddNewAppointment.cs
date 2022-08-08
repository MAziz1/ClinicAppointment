using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Commands
{
    public class AddNewAppointment
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
