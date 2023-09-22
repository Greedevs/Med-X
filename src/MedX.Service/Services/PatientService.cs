using AutoMapper;
using MedX.Domain.Enitities;
using MedX.Service.Interfaces;
using MedX.Data.IRepositories;
using MedX.Service.DTOs.Patients;
using MedX.Domain.Configurations;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.DTOs.Doctors;

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
        Patient patient=this.repository.GetAll().FirstOrDefault(u=>u.Phone.Equals(dto.Phone));
        if (patient is not null)
            throw new AlreadyExistException($"This patient is already exist with phone {dto.Phone}");

        patient = this.repository.GetAll().FirstOrDefault(u => u.Pinfl.Equals(dto.Pinfl));
        if (patient is not null)
            throw new AlreadyExistException($"This patient is already exist with Pinfl {dto.Pinfl}");

        Patient mappedPatient =mapper.Map<Patient>(dto);
        await this.repository.CreateAsync(mappedPatient);
        await this.repository.SaveChanges();

        PatientResultDto result=mapper.Map<PatientResultDto>(mappedPatient);
        return result;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        Patient patient = this.repository.GetAll().FirstOrDefault(p => p.Id.Equals(id));
        if(patient is not null && patient.IsDeleted==true)
            throw new NotFoundException($"This patient is not found {id}");

        this.repository.Delete(patient);
        await this.repository.SaveChanges();
        return true;
    }

    public async Task<IEnumerable<PatientResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var patients=this.repository.GetAll(includes: new[] { "Treatments", "Transactions", "Appointments" })
            .ToPaginate(@params)
            .ToList();

        if (search is not null)
        {
            patients = patients.Where(user => user.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<PatientResultDto>>(patients);

    }

    public async Task<PatientResultDto> GetAsync(long id)
    {
        Patient existPatient = this.repository.GetAll(includes: new[] {"Treatments","Transactions","Appointments"}).FirstOrDefault(p=>p.Id.Equals(id))
            ?? throw new AlreadyExistException($"This patient is not found {id}");
        
        PatientResultDto result=this.mapper.Map<PatientResultDto>(existPatient);
        return result;
    }

    public async Task<IEnumerable<PatientResultDto>> SearchByQuery(string query)
    {
        var result = this.repository.GetAll(includes: new[] { "Treatments", "Transactions", "Appointments" })
        .Where(d => d.Pinfl.Contains(query) ||
        d.FirstName.Contains(query) || d.LastName.Contains(query) ||
        d.SurName.Contains(query) || d.Phone.Contains(query) || d.Address.Contains(query)).ToList();

        if (result is not null)
            return this.mapper.Map<IEnumerable<PatientResultDto>>(result);
        return null;
    }

    public async Task<PatientResultDto> UpdateAsync(PatientUpdateDto dto)
    {
        Patient patient = this.repository.GetAll().FirstOrDefault(p => p.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This patient is not found {dto.Id}");

        this.mapper.Map(dto, patient);
        patient.UpdatedAt = DateTime.UtcNow;
        this.repository.Update(patient);
        await this.repository.SaveChanges();

        PatientResultDto result = this.mapper.Map<PatientResultDto>(patient);
        return result;
    }
}
