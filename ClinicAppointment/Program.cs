using ClinicAppointment.Domain.Interfaces;
using ClinicAppointment.Domain.Services;
using ClinicAppointment.Infrastructure;
using ClinicAppointment.Infrastructure.NPGSRepository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<AppointmentDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));

// Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(NPGSRepository<>));
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();


void MigrateDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        using (var appContext = scope.ServiceProvider.GetRequiredService<AppointmentDbContext>())
        {
            appContext.Database.Migrate();
        }
    }
}