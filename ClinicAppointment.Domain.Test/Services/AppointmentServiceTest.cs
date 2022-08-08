using ClinicAppointment.Domain.Entities;
using ClinicAppointment.Domain.Interfaces;
using ClinicAppointment.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClinicAppointment.Domain.Test.Services
{
    public class AppointmentServiceTest
    {
        private readonly Mock<IRepository<Appointment>> _appointmentRepositoryMock = new Mock<IRepository<Appointment>>();
        private readonly AppointmentService _appointmentService;

        public AppointmentServiceTest()
        {
            _appointmentService = new AppointmentService(_appointmentRepositoryMock.Object);
        }

        [Fact]
        public async Task SaveAppintmentAsync_ShouldReturnAppointment()
        {
            // Arrange
            var appointment = new Appointment
            {
                Id = 101,
                PatientId = 1,
                Notes = "Notes 1",
                Date = new DateOnly(2022, 08, 08),
                Time = new TimeOnly(10, 00)
            };
            _appointmentRepositoryMock.Setup(r => r.InsertItemAsync(It.IsAny<Appointment>()))
                                  .ReturnsAsync(appointment);
            // Act
            var result = await _appointmentService.SaveAppointmentAsync(appointment);

            // Assert
            Assert.Equal(appointment.Id, result.Id);
        }
        
        [Fact]
        public void HasOverlapTime_ShouldReturnTrueIfHasOvelappedTimeMoreThan2()
        {
            // Arrange
            var appointment1 = new Appointment
            {
                Id = 101,
                PatientId = 1,
                Notes = "Notes 1",
                Date = new DateOnly(2022, 08, 08),
                Time = new TimeOnly(10, 00)
            };

            var appointment2 = new Appointment
            {
                Id = 102,
                PatientId = 2,
                Notes = "Notes 2",
                Date = new DateOnly(2022, 08, 08),
                Time = new TimeOnly(10, 00)
            };

            var appointements = new List<Appointment> { appointment1, appointment2 };
            _appointmentRepositoryMock.Setup(r => r.Query(
                        It.IsAny<Expression<Func<Appointment, bool>>>(),
                        It.IsAny<Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>>>(),
                        It.IsAny<string>(),
                        It.IsAny<int?>(),
                        It.IsAny<int?>()
                        ))
                .Returns(new Func<Expression<Func<Appointment, bool>>,
                                  Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>>,
                                  string,
                                  int?,
                                  int?,
                                  IQueryable<Appointment>>((filter, orderBy, includeProperties, page, pageSize) => appointements.Where(filter.Compile()).AsQueryable() as IQueryable<Appointment>));

            var appointment3 = new Appointment
            {
                Id = 103,
                PatientId = 3,
                Notes = "Notes 3",
                Date = new DateOnly(2022, 08, 08),
                Time = new TimeOnly(10, 00)
            };
            // Act
            var result = _appointmentService.HasOverlapTime(appointment3);

            // Assert
            Assert.True(result);
        }
    }
}
