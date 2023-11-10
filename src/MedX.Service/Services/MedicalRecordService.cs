using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Domain.Entities.MedicalRecords;
using MedX.Service.DTOs.MedicalRecords;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IRepository<Doctor> doctorRepository;
    private readonly IRepository<Patient> patientRepository;
    private readonly IRepository<MedicalRecord> medicalRecordRepository;
    private readonly IMapper mapper;
    public MedicalRecordService(IMapper mapper,
        IRepository<Doctor> doctorRepository,
        IRepository<Patient> patientRepository,
        IRepository<MedicalRecord> medicalRecordRepository)
    {
        this.mapper = mapper;
        this.doctorRepository = doctorRepository;
        this.patientRepository = patientRepository;
        this.medicalRecordRepository = medicalRecordRepository;
    }
    public async Task<MedicalRecordResultDto> AddAsync(MedicalRecordCreationDto dto)
    {
        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
            ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == dto.DoctorId)
            ?? throw new NotFoundException($"This Doctor not found with id: {dto.DoctorId}");


        var mappedMedicalRecord = this.mapper.Map<MedicalRecord>(dto);
        mappedMedicalRecord.Doctor = existDoctor;
        mappedMedicalRecord.Patient = existPatient;

        await this.medicalRecordRepository.CreateAsync(mappedMedicalRecord);
        await this.medicalRecordRepository.SaveChanges();

        return this.mapper.Map<MedicalRecordResultDto>(mappedMedicalRecord);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existMedicalRecord = await this.medicalRecordRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This MedicalRecord not found with id: {id}");

        this.medicalRecordRepository.Delete(existMedicalRecord);
        await this.medicalRecordRepository.SaveChanges();

        return true;
    }

    public async Task<MedicalRecordResultDto> UpdateAsync(MedicalRecordUpdateDto dto)
    {
        var existMedicalRecord = await this.medicalRecordRepository.GetAsync(r => r.Id == dto.Id, includes: new[] { "Doctor", "Patient" })
            ?? throw new NotFoundException($"This MedicalRecord not found with id: {dto.Id}");

        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
            ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == dto.DoctorId)
            ?? throw new NotFoundException($"This Doctor not found with id: {dto.DoctorId}");

        this.mapper.Map(dto, existMedicalRecord);
        existMedicalRecord.Doctor = existDoctor;
        existMedicalRecord.Patient = existPatient;

        this.medicalRecordRepository.Update(existMedicalRecord);
        await this.medicalRecordRepository.SaveChanges();

        return this.mapper.Map<MedicalRecordResultDto>(existMedicalRecord);
    }

    public async Task<MedicalRecordResultDto> GetAsync(long id)
    {
        var existMedicalRecord = await this.medicalRecordRepository.GetAsync(r => r.Id == id,
            includes: new[] { "Doctor", "Patient" })
            ?? throw new NotFoundException($"This MedicalRecord not found with id: {id}");

        return this.mapper.Map<MedicalRecordResultDto>(existMedicalRecord);
    }

    public async Task<IEnumerable<MedicalRecordResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allMedicalRecords = await this.medicalRecordRepository.GetAll(includes: new[] { "Doctor", "Patient" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            allMedicalRecords = allMedicalRecords
                .Where(d => d.Doctor.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Doctor.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Doctor.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<MedicalRecordResultDto>>(allMedicalRecords);
    }

    public async Task<IEnumerable<MedicalRecordResultDto>> GetAllByPatientIdAsync(long patientId)
    {
        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(patientId))
            ?? throw new NotFoundException($"This Patient not found with id: {patientId}");

        var records = await this.medicalRecordRepository.GetAll(a => a.PatientId == patientId,
            includes: new[] { "Doctor", "Patient" }).ToListAsync();

        return this.mapper.Map<IEnumerable<MedicalRecordResultDto>>(records);
    }

    public async Task<IEnumerable<MedicalRecordResultDto>> GetAllByDoctorIdAsync(long doctorId, PaginationParams @params, string search = null)
    {
        var existDoctor = await this.doctorRepository.GetAsync(d => d.Id.Equals(doctorId))
            ?? throw new NotFoundException($"This Patient not found with id: {doctorId}");

        var records = await this.medicalRecordRepository.GetAll(a => a.DoctorId == doctorId,
             includes: new[] { "Doctor", "Patient" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            records = records
                .Where(d => d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<MedicalRecordResultDto>>(records);
    }
}