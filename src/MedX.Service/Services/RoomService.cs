using AutoMapper;
using MedX.Domain.Enitities;
using MedX.Data.IRepositories;
using MedX.Service.DTOs.Rooms;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using MedX.Domain.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class RoomService : IRoomService
{
    private readonly IRepository<Room> repository;
    private readonly IMapper mapper;
    public RoomService(IMapper mapper, IRepository<Room> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async Task<RoomResultDto> AddAsync(RoomCreationDto dto)
    {
        var existRoom = await this.repository.GetAsync(d => d.RoomNumber.Equals(dto.RoomNumber));
        if (existRoom is not null)
            throw new AlreadyExistException($"This Room already exist with number: {dto.RoomNumber}");

        var mappedRoom = this.mapper.Map<Room>(dto);
        await this.repository.CreateAsync(mappedRoom);
        await this.repository.SaveChanges();

        return this.mapper.Map<RoomResultDto>(mappedRoom);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existRoom = await this.repository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This Room not found with id: {id}");

        this.repository.Delete(existRoom);
        await this.repository.SaveChanges();

        return true;
    }
    public async Task<RoomResultDto> UpdateAsync(RoomUpdateDto dto)
    {
        var existRoom = await this.repository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This Room not found with id: {dto.Id}");

        this.mapper.Map(dto, existRoom);
        this.repository.Update(existRoom);
        await this.repository.SaveChanges();

        return this.mapper.Map<RoomResultDto>(existRoom);
    }
    public async Task<RoomResultDto> GetAsync(long id)
    {
        var existRoom = await this.repository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This Room not found with id: {id}");

        return this.mapper.Map<RoomResultDto>(existRoom);
    }

    public async Task<IEnumerable<RoomResultDto>> SearchByQueryAsync(int query)
    {
        var result = await repository.GetAll(includes: new[] { "Patient" })
            .Where(d => d.RoomNumber.Equals(query) ||
            d.Quantity.Equals(query)).ToListAsync();

        if (result is null)
            return null;

        return mapper.Map<IEnumerable<RoomResultDto>>(result);
    }

    public async Task<IEnumerable<RoomResultDto>> GetAllAsync(PaginationParams @params)
    {
        var allRooms = await this.repository.GetAll(includes: new[] {"Patients"})
            .ToPaginate(@params)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<RoomResultDto>>(allRooms);
    }
}