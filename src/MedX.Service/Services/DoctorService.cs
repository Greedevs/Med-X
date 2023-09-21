using AutoMapper;
using MedX.Domain.Enitities;
using MedX.Service.Exceptions;
using MedX.Data.IRepositories;
using MedX.Service.Interfaces;
using MedX.Service.DTOs.Doctors;
using MedX.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using MedX.Service.Extensions;

namespace MedX.Service.Services;

public class DoctorService : IDoctorService
{
    private readonly IRepository<Doctor> doctorRepository;
    private readonly IRepository<Room> roomRepository;
    private readonly IMapper mapper;
    public DoctorService(IMapper mapper, IRepository<Doctor> doctorRepository, IRepository<Room> roomRepository)
    {
        this.mapper = mapper;
        this.roomRepository = roomRepository;
        this.doctorRepository = doctorRepository;
    }
    public async Task<DoctorResultDto> AddAsync(DoctorCreationDto dto)
    {
        var existDoctor = await this.doctorRepository.GetAsync(d => d.Phone.Equals(dto.Phone));
        if (existDoctor is not null)
            throw new AlreadyExistException($"This doctor already exist with phone: {dto.Phone}");

        var existRoom = await this.roomRepository.GetAsync(r => r.Id == dto.RoomId)
            ?? throw new NotFoundException($"This room not found with id: {dto.RoomId}");

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

    public async Task<IEnumerable<DoctorResultDto>> SearchByQueryAsync(string query)
    {
        var result = await doctorRepository.GetAll(includes: new[] { "Room" })
            .Where(d => d.Professional.Contains(query) ||
            d.FirstName.Contains(query) || d.LastName.Contains(query) ||
            d.SurName.Contains(query) || d.Phone.Contains(query)).ToListAsync();

        if (result is null)
            return null;

        return mapper.Map<IEnumerable<DoctorResultDto>>(result);
    }

    public async Task<IEnumerable<DoctorResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allDoctors = await this.doctorRepository.GetAll(includes: new[] { "Room" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            allDoctors = allDoctors.Where(user => user.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<DoctorResultDto>>(allDoctors);
    }
}