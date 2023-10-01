using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Domain.Entities.Services;
using MedX.Service.DTOs.ServiceItems;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class AffairItemService : IAffairItemService
{
    private readonly IMapper mapper;
    private readonly IRepository<Affair> affairRepository;
    private readonly IRepository<Patient> patientRepository;
    private readonly IRepository<AffairItem> affairItemRepository;
    public AffairItemService(IMapper mapper, 
        IRepository<Affair> affairRepository,
        IRepository<Patient> patientRepository,
        IRepository<AffairItem> affairItemRepository)
    {
        this.mapper = mapper;
        this.affairRepository = affairRepository;
        this.patientRepository = patientRepository;
        this.affairItemRepository = affairItemRepository;
    }
    public async Task<AffairItemResultDto> AddAsync(AffairItemCreationDto dto)
    {
        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
            ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        var existAffair = await this.affairRepository.GetAsync(d => d.Id.Equals(dto.AffairId))
            ?? throw new NotFoundException($"This Patient not found with id: {dto.AffairId}");

        var mappedAffairItem = this.mapper.Map<AffairItem>(dto);
        mappedAffairItem.Patient = existPatient;
        mappedAffairItem.Affair = existAffair;

        await this.affairItemRepository.CreateAsync(mappedAffairItem);
        await this.affairItemRepository.SaveChanges();

        return this.mapper.Map<AffairItemResultDto>(mappedAffairItem);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAffairItem = await this.affairItemRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This AffairItem not found with id: {id}");

        this.affairItemRepository.Delete(existAffairItem);
        await this.affairItemRepository.SaveChanges();

        return true;
    }

    public async Task<AffairItemResultDto> UpdateAsync(AffairItemUpdateDto dto)
    {
        var existAffairItem = await this.affairItemRepository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This AffairItem not found with id: {dto.Id}");

        this.mapper.Map(dto, existAffairItem);

        this.affairItemRepository.Update(existAffairItem);
        await this.affairItemRepository.SaveChanges();

        return this.mapper.Map<AffairItemResultDto>(existAffairItem);
    }

    public async Task<AffairItemResultDto> GetAsync(long id)
    {
        var existAffairItem = await this.affairItemRepository.GetAsync(r => r.Id == id, includes: new[] { "Patient", "Affair" })
            ?? throw new NotFoundException($"This AffairItem not found with id: {id}");

        return this.mapper.Map<AffairItemResultDto>(existAffairItem);
    }

    public async Task<IEnumerable<AffairItemResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allAffairItems = await this.affairItemRepository.GetAll(includes: new[] {"Patient","Affair"})
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            allAffairItems = allAffairItems.Where(d => d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Affair.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<AffairItemResultDto>>(allAffairItems);
    }

    public async Task<IEnumerable<AffairItemResultDto>> GetAllByAffairIdAsync(long affairId, PaginationParams @params, string search = null)
    {
        var existAffair = await this.affairRepository.GetAsync(a => a.Id == affairId)
            ?? throw new NotFoundException($"This service is not found with id: {affairId}");

        var affairs = await this.affairItemRepository.GetAll(a => a.AffairId == affairId, includes: new[] { "Patient", "Affair" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            affairs = affairs.Where(d => d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Affair.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<AffairItemResultDto>>(affairs);
    }

    public async Task<IEnumerable<AffairItemResultDto>> GetAllByPatientIdAsync(long patientId, PaginationParams @params, string search = null)
    {
        var existPatient = await this.patientRepository.GetAsync(a => a.Id == patientId)
            ?? throw new NotFoundException($"This service is not found with id: {patientId}");

        var patients = await this.affairItemRepository.GetAll(a => a.PatientId == patientId, includes: new[] { "Patient", "Affair" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            patients = patients.Where(d => d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Affair.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<AffairItemResultDto>>(patients);
    }
}