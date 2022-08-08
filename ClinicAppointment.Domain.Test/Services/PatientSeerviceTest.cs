using ClinicAppointment.Domain.Entities;
using ClinicAppointment.Domain.Interfaces;
using ClinicAppointment.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClinicAppointment.Domain.Test.Services
{
    public class PatientServiceTest
    {
        private readonly Mock<IRepository<Patient>> _patientRepositoryMock = new Mock<IRepository<Patient>>();
        private readonly PatientService _patientService;

        public PatientServiceTest()
        {
            _patientService = new PatientService(_patientRepositoryMock.Object);
        }

        [Fact]
        public async Task SavePatientAsync_ShouldReturnPatient()
        {
            // Arrange
            var patient = new Patient
            {
                Id = 101,
                Name = "Patient 1",
                Phone = "01000000000"
            };
            _patientRepositoryMock.Setup(r => r.InsertItemAsync(It.IsAny<Patient>()))
                                  .ReturnsAsync(patient);
            // Act
            var result = await _patientService.SavePatientAsync(patient);

            // Assert
            Assert.Equal(patient.Id, result.Id);
        }
    }
}
