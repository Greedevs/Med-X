using AutoMapper;
using MedX.Domain.Entities;
using MedX.Data.IRepositories;
using MedX.Service.DTOs.Rooms;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using MedX.Service.DTOs.Assets;
using MedX.Domain.Configurations;
using MedX.Domain.Entities.Assets;
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

        var mappedRoom = new Room
        {
            Number = dto.Number,
            Quantity = dto.Quantity,
            Busy = dto.Busy
        };

        if (dto.Image is not null)
        {
            var uploadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image });
            var createdImage = new Asset
            {
                FileName = uploadedImage.FileName,
                FilePath = uploadedImage.FilePath,
            };

            mappedRoom.ImageId = uploadedImage.Id;
            mappedRoom.Image = createdImage;
        }

        await this.repository.CreateAsync(mappedRoom);
        await this.repository.SaveChanges();

        return this.mapper.Map<RoomResultDto>(mappedRoom);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existRoom = await this.repository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This Room not found with id: {id}");

        this.repository.Delete(existRoom);
        await this.assetService.RemoveAsync(existRoom.Image);
        await this.repository.SaveChanges();

        return true;
    }

    public async Task<RoomResultDto> UpdateAsync(RoomUpdateDto dto)
    {
        var existRoom = await this.repository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This Room not found with id: {dto.Id}");

        var uploadedImage = new Asset();
        if (dto.Image is not null)
        {
            uploadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image });
            await this.assetService.RemoveAsync(existRoom.Image);
        }

        existRoom.Number = dto.Number;
        existRoom.Quantity = dto.Quantity;
        existRoom.Busy = dto.Busy;

        if (uploadedImage.Id > 0)
        {
            if (existRoom.Image == null)
            {
                existRoom.Image = new Asset();
            }
            existRoom.ImageId = uploadedImage.Id;
            existRoom.Image.FileName = uploadedImage.FileName;
            existRoom.Image.FilePath = uploadedImage.FilePath;
        }

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