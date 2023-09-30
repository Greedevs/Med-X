using AutoMapper;
using MedX.Domain.Entities;
using MedX.Data.IRepositories;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using MedX.Service.DTOs.Doctors;
using MedX.Domain.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class DoctorService : IDoctorService
{
    private readonly IRepository<Doctor> doctorRepository;
    private readonly IMapper mapper;
    public DoctorService(IMapper mapper, IRepository<Doctor> doctorRepository)
    {
        this.mapper = mapper;
        this.doctorRepository = doctorRepository;
    }
    public async Task<DoctorResultDto> AddAsync(DoctorCreationDto dto)
    {
        var existDoctor = await this.doctorRepository.GetAsync(d => d.Phone.Equals(dto.Phone));
        if (existDoctor is not null)
            throw new AlreadyExistException($"This doctor already exist with phone: {dto.Phone}");

        var mappedDoctor = this.mapper.Map<Doctor>(dto);

        await this.doctorRepository.CreateAsync(mappedDoctor);
        await this.doctorRepository.SaveChanges();

        return this.mapper.Map<DoctorResultDto>(mappedDoctor);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This doctor not found with id: {id}");

        this.doctorRepository.Delete(existDoctor);
        await this.doctorRepository.SaveChanges();

        return true;
    }
    public async Task<DoctorResultDto> UpdateAsync(DoctorUpdateDto dto)
    {
        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This doctor not found with id: {dto.Id}");

        this.mapper.Map(dto, existDoctor);

        this.doctorRepository.Update(existDoctor);
        await this.doctorRepository.SaveChanges();

        return this.mapper.Map<DoctorResultDto>(existDoctor);
    }
    public async Task<DoctorResultDto> GetAsync(long id)
    {
        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This doctor not found with id: {id}");

        return this.mapper.Map<DoctorResultDto>(existDoctor);
    }

    public async Task<IEnumerable<DoctorResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allDoctors = await this.doctorRepository.GetAll()
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            allDoctors = allDoctors.Where(d => d.Professional.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Patronymic.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<DoctorResultDto>>(allDoctors);
    }
}