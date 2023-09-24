using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Service.DTOs.Patients;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class PatientService : IPatientService
{
    private readonly IMapper mapper;
    private readonly IRepository<Patient> repository;
    public PatientService(IMapper mapper, IRepository<Patient> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async Task<PatientResultDto> AddAsync(PatientCreationDto dto)
    {
        Patient patient = await this.repository.GetAsync(u => u.Phone.Equals(dto.Phone));
        if (patient is not null)
            throw new AlreadyExistException($"This patient is already exist with phone {dto.Phone}");

        patient = this.repository.GetAll().FirstOrDefault(u => u.Pinfl.Equals(dto.Pinfl));
        if (patient is not null)
            throw new AlreadyExistException($"This patient is already exist with Pinfl {dto.Pinfl}");

        Patient mappedPatient = mapper.Map<Patient>(dto);
        await this.repository.CreateAsync(mappedPatient);
        await this.repository.SaveChanges();

        PatientResultDto result = mapper.Map<PatientResultDto>(mappedPatient);
        return result;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        Patient patient = await this.repository.GetAsync(p => p.Id.Equals(id));
        if (patient is not null)
            throw new NotFoundException($"This patient is not found {id}");

        this.repository.Delete(patient);
        await this.repository.SaveChanges();
        return true;
    }


    public async Task<PatientResultDto> GetAsync(long id)
    {
        Patient existPatient = await this.repository.GetAsync(p => p.Id == id)
            ?? throw new NotFoundException($"This patient is not found {id}");

        PatientResultDto result = this.mapper.Map<PatientResultDto>(existPatient);
        return result;
    }

    public async Task<PatientResultDto> UpdateAsync(PatientUpdateDto dto)
    {
        Patient existPatient = await this.repository.GetAsync(p => p.Id == dto.Id)
            ?? throw new NotFoundException($"This patient is not found {dto.Id}");

        this.mapper.Map(dto, existPatient);
        this.repository.Update(existPatient);
        await this.repository.SaveChanges();

        PatientResultDto result = this.mapper.Map<PatientResultDto>(existPatient);
        return result;
    }

    public async Task<IEnumerable<PatientResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var patients = await this.repository.GetAll(includes: new[] { "Treatments", "Transactions", "Appointments" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            patients = patients.Where(d => d.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.SurName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Address.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<PatientResultDto>>(patients);
    }
}
