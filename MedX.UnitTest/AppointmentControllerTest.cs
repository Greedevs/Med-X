using MedX.Data.Contexts;
using MedX.Data.IRepositories;
using MedX.Domain.Entities.Appointments;
using MedX.Domain.Entities;
using AutoMapper;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using MedX.Service.Mappers;
using MedX.Data.Repositories;
using MedX.Service.Services;
using MedX.Service.DTOs.Appointments;
using MedX.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using MedX.WebApi.Models;

namespace MedX.UnitTest;

public class AppointmentControllerTest : IDisposable
{
    private readonly IMapper mapper;
    private readonly AppDbContext appDbContext;
    private readonly IRepository<Doctor> doctorRepository;
    private readonly IRepository<Patient> patientRepository;
    private readonly IRepository<Appointment> appointmentRepository;
    private readonly IAppointmentService appointmentService;

    public AppointmentControllerTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
        appDbContext = new AppDbContext(options);
        mapper = new MapperConfiguration(c => c.AddProfile<MappingProfile>()).CreateMapper();

        doctorRepository = new Repository<Doctor>(appDbContext);
        patientRepository = new Repository<Patient>(appDbContext);
        appointmentRepository = new Repository<Appointment>(appDbContext);

        appointmentService = new AppointmentService(mapper, doctorRepository, patientRepository, appointmentRepository);
    }

    [Fact]
    public async Task CheckControllerTest()
    {
        //Arrange
        var controller = new AppointmentsController(appointmentService);
        var doctor = new Doctor
        {
            FirstName = "test",
            LastName = "test",
            Phone = "2222222222222222",
            Email = "test",
            RoomNumber = 1,
            Patronymic = "sdsdsf",
        };
        await doctorRepository.CreateAsync(doctor);
        await doctorRepository.SaveChanges();

        var patient = new Patient
        {
            FirstName = "test",
            LastName = "test",
            Phone = "33333333333",
            Patronymic = "32432",
            Balance = 1,
            DateOfBirth = DateTime.Now,
        };

        await patientRepository.CreateAsync(patient);
        await patientRepository.SaveChanges();

        var appointmentDto = new AppointmentCreationDto
        {
            DoctorId = doctor.Id,
            PatientId = patient.Id,
        };

        //Act
        var result = await controller.PostAsync(appointmentDto) as OkObjectResult;
        var response = result.Value as Response;
        var appointmentRes = response.Data as AppointmentResultDto;

        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, response.StatusCode);
        Assert.Equal("OK", response.Message);
        Assert.NotNull(appointmentRes);
        Assert.Equal(appointmentRes.Doctor.Id, doctor.Id);
        Assert.Equal(appointmentRes.Patient.Id, patient.Id);
        Dispose();
    }

    public void Dispose()
    {
        appDbContext.Dispose();
    }
}
