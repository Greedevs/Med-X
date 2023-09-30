using AutoMapper;
using MedX.Service.Helpers;
using MedX.Data.IRepositories;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using MedX.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using MedX.Service.DTOs.Administrators;
using MedX.Domain.Entities.Administrators;

namespace MedX.Service.Services;

public class AdminService : IAdminService
{
    private readonly IRepository<Administrator> repository;
    private readonly IMapper mapper;
    public AdminService(IMapper mapper, IRepository<Administrator> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async Task<AdminResultDto> AddAsync(AdminCreationDto dto)
    {
        var existAdmin = await this.repository.GetAsync(d => d.Phone.Equals(dto.Phone));
        if (existAdmin is not null)
            throw new AlreadyExistException($"This Admin already exist with number: {dto.Phone}");

        var mappedAdmin = this.mapper.Map<Administrator>(dto);
        mappedAdmin.Password = PasswordHash.Encrypt(dto.Password);

        await this.repository.CreateAsync(mappedAdmin);
        await this.repository.SaveChanges();

        return this.mapper.Map<AdminResultDto>(mappedAdmin);
    }

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

        this.mapper.Map(dto, existAdmin);
        this.repository.Update(existAdmin);
        await this.repository.SaveChanges();

        return this.mapper.Map<AdminResultDto>(existAdmin);
    }
    public async Task<AdminResultDto> GetAsync(long id)
    {
        var existAdmin = await this.repository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This Admin not found with id: {id}");

        return this.mapper.Map<AdminResultDto>(existAdmin);
    }

    public async Task<IEnumerable<AdminResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allAdmins = await this.repository.GetAll()
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
}