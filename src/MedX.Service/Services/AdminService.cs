using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities.Administrators;
using MedX.Service.DTOs.Administrators;
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
        if (dto.Image is not null)
        {
            dto.Image = await this.assetService.UploadAsync(dto.Image);
        }

        var mappedAdmin = this.mapper.Map<Administrator>(dto);

        mappedAdmin.Password = PasswordHash.Encrypt(dto.Password);

        await this.repository.CreateAsync(mappedAdmin);
        await this.repository.SaveChanges();

        return this.mapper.Map<AdminResultDto>(mappedAdmin);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> DeleteAsync(long id)
    {
        var existAdmin = await this.repository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This Admin not found with id: {id}");

        this.repository.Delete(existAdmin);
        await this.repository.SaveChanges();

        return true;
    }
    public async Task<AdminResultDto> UpdateAsync(AdminUpdateDto dto)
    {
        var existAdmin = await this.repository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This Admin not found with id: {dto.Id}");

        if (dto.Image is not null)
        {
            dto.Image = await this.assetService.UploadAsync(dto.Image);
        }

        this.mapper.Map<Administrator>(dto);

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