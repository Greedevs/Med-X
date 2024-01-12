using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Service.DTOs.Rooms;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class RoomService : IRoomService
{
    private readonly IRepository<Room> repository;
    private readonly IAssetService assetService;
    private readonly IMapper mapper;
    public RoomService(IMapper mapper, IRepository<Room> repository, IAssetService assetService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.assetService = assetService;
    }
    public async Task<RoomResultDto> AddAsync(RoomCreationDto dto)
    {
        var existRoom = await this.repository.GetAsync(d => d.Number.Equals(dto.Number));
        if (existRoom is not null)
            throw new AlreadyExistException($"This Room already exist with number: {dto.Number}");

        if (dto.Image is not null)
        {
            dto.Image = await this.assetService.UploadAsync(dto.Image);
        }

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

        if (dto.Image is not null)
        {
            dto.Image = await this.assetService.UploadAsync(dto.Image);
        }

        this.mapper.Map<Room>(dto);

        this.repository.Update(existRoom);
        await this.repository.SaveChanges();

        return this.mapper.Map<RoomResultDto>(existRoom);
    }

    public async Task<RoomResultDto> GetAsync(long id)
    {
        var existRoom = await this.repository.GetAsync(r => r.Id == id, includes: new[] { "Image", "Patients" })
            ?? throw new NotFoundException($"This Room not found with id: {id}");

        return this.mapper.Map<RoomResultDto>(existRoom);
    }

    public async Task<IEnumerable<RoomResultDto>> GetAllAsync(PaginationParams @params, int? search = null)
    {
        var allRooms = await this.repository.GetAll(includes: new[] { "Image", "Patients" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search != null)
        {
            allRooms = allRooms.Where(d => d.Number.Equals(search) ||
            d.Quantity.Equals(search) || d.Busy.Equals(search)).ToList();
        }

        return this.mapper.Map<IEnumerable<RoomResultDto>>(allRooms);
    }
}