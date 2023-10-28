using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities.Administrators;
using MedX.Domain.Entities.Assets;
using MedX.Service.DTOs.Administrators;
using MedX.Service.DTOs.Assets;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Helpers;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MedX.Service.Services;

public class AdminService : IAdminService
{
    private readonly IRepository<Administrator> repository;
    private readonly IAssetService assetService;
    private readonly IMapper mapper;
    public AdminService(IMapper mapper, IRepository<Administrator> repository, IAssetService assetService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.assetService = assetService;
    }
    public async Task<AdminResultDto> AddAsync(AdminCreationDto dto)
    {
        var existAdmin = await this.repository.GetAsync(d => d.Phone.Equals(dto.Phone));
        if (existAdmin is not null)
            throw new AlreadyExistException($"This Admin already exist with number: {dto.Phone}");

        string accountNumber = GenerateAccountNumber();
        var mappedAdmin = new Administrator
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Phone = dto.Phone,
            Email = dto.Email,
            Role = dto.Role,
            AccountNumber = accountNumber
        };
        mappedAdmin.Password = PasswordHash.Encrypt(dto.Password);

        if (dto.Image is not null)
        {
            var uploadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image });
            var createdImage = new Asset
            {
                FileName = uploadedImage.FileName,
                FilePath = uploadedImage.FilePath,
            };

            mappedAdmin.ImageId = uploadedImage.Id;
            mappedAdmin.Image = createdImage;
        }

        await this.repository.CreateAsync(mappedAdmin);
        await this.repository.SaveChanges();

        return this.mapper.Map<AdminResultDto>(mappedAdmin);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAdmin = await this.repository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This Admin not found with id: {id}");

        this.repository.Delete(existAdmin);
        await this.assetService.RemoveAsync(existAdmin.Image);
        await this.repository.SaveChanges();

        return true;
    }
    public async Task<AdminResultDto> UpdateAsync(AdminUpdateDto dto)
    {
        var existAdmin = await this.repository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This Admin not found with id: {dto.Id}");

        var uploadedImage = new Asset();
        if (dto.Image is not null)
        {
            uploadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image });
            await this.assetService.RemoveAsync(existAdmin.Image);
        }

        existAdmin.FirstName = dto.FirstName;
        existAdmin.LastName = dto.LastName;
        existAdmin.Phone = dto.Phone;
        existAdmin.Email = dto.Email;
        existAdmin.Role = dto.Role;

        if (uploadedImage.Id > 0)
        {
            if (existAdmin.Image == null)
            {
                existAdmin.Image = new Asset();
            }
            existAdmin.ImageId = uploadedImage.Id;
            existAdmin.Image.FileName = uploadedImage.FileName;
            existAdmin.Image.FilePath = uploadedImage.FilePath;
        }

        this.repository.Update(existAdmin);
        await this.repository.SaveChanges();

        return this.mapper.Map<AdminResultDto>(existAdmin);
    }

    public async Task<AdminResultDto> GetAsync(long id)
    {
        var existAdmin = await this.repository.GetAsync(r => r.Id == id, includes: new[] { "Image" })
            ?? throw new NotFoundException($"This Admin not found with id: {id}");

        return this.mapper.Map<AdminResultDto>(existAdmin);
    }

    public async Task<IEnumerable<AdminResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allAdmins = await this.repository.GetAll(includes: new[] { "Image" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search != null)
        {
            allAdmins = allAdmins.Where(d => d.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<AdminResultDto>>(allAdmins);
    }

    private string GenerateAccountNumber()
    {
        Random random = new Random();
        int accountNumberLength = 9;
        string accountNumber = random.Next((int)Math.Pow(10,
        accountNumberLength - 1), (int)Math.Pow(10, accountNumberLength)).ToString();

        return accountNumber;
    }
}