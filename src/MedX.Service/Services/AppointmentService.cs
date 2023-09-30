using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Domain.Entities.Appointments;
using MedX.Service.DTOs.Appointments;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IRepository<Doctor> doctorRepository;
    private readonly IRepository<Patient> patientRepository;
    private readonly IRepository<Appointment> appointmentRepository;
    private readonly IMapper mapper;
    public AppointmentService(IMapper mapper,
        IRepository<Doctor> doctorRepository,
        IRepository<Patient> patientRepository,
        IRepository<Appointment> appointmentRepository)
    {
        this.mapper = mapper;
        this.doctorRepository = doctorRepository;
        this.patientRepository = patientRepository;
        this.appointmentRepository = appointmentRepository;
    }
    public async Task<AppointmentResultDto> AddAsync(AppointmentCreationDto dto)
    {
        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
            ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == dto.DoctorId)
            ?? throw new NotFoundException($"This Doctor not found with id: {dto.DoctorId}");


        var mappedAppointment = this.mapper.Map<Appointment>(dto);
        mappedAppointment.Doctor = existDoctor;
        mappedAppointment.Patient = existPatient;

        await this.appointmentRepository.CreateAsync(mappedAppointment);
        await this.appointmentRepository.SaveChanges();

        return this.mapper.Map<AppointmentResultDto>(mappedAppointment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAppointment = await this.appointmentRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This Appointment not found with id: {id}");

        this.appointmentRepository.Delete(existAppointment);
        await this.appointmentRepository.SaveChanges();

        return true;
    }

    public async Task<AppointmentResultDto> UpdateAsync(AppointmentUpdateDto dto)
    {
        var existAppointment = await this.appointmentRepository.GetAsync(r => r.Id == dto.Id, includes: new[] { "Doctor", "Patient" })
            ?? throw new NotFoundException($"This Appointment not found with id: {dto.Id}");

        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
            ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == dto.DoctorId)
            ?? throw new NotFoundException($"This Doctor not found with id: {dto.DoctorId}");

        this.mapper.Map(dto, existAppointment);
        existAppointment.Doctor = existDoctor;
        existAppointment.Patient = existPatient;

        this.appointmentRepository.Update(existAppointment);
        await this.appointmentRepository.SaveChanges();

        return this.mapper.Map<AppointmentResultDto>(existAppointment);
    }

    public async Task<AppointmentResultDto> GetAsync(long id)
    {
        var existAppointment = await this.appointmentRepository.GetAsync(r => r.Id == id, includes: new[] { "Doctor", "Patient" })
            ?? throw new NotFoundException($"This Appointment not found with id: {id}");

        return this.mapper.Map<AppointmentResultDto>(existAppointment);
    }

    public async Task<IEnumerable<AppointmentResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allAppointments = await this.appointmentRepository.GetAll(includes: new[] { "Doctor", "Patient" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            allAppointments = allAppointments
                .Where(d => d.Doctor.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Doctor.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Doctor.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<AppointmentResultDto>>(allAppointments);
    }
}