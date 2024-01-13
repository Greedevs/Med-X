using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Service.DTOs.Employees;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> doctorRepository;
    private readonly IAssetService assetService;
    private readonly IMapper mapper;
    public EmployeeService(IMapper mapper, IRepository<Employee> doctorRepository, IAssetService assetService)
    {
        this.mapper = mapper;
        this.assetService = assetService;
        this.doctorRepository = doctorRepository;
    }

    public async Task<EmployeeResultDto> AddAsync(EmployeeCreationDto dto)
    {
        var existDoctor = await this.doctorRepository.GetAsync(d => d.Phone.Equals(dto.Phone));
        if (existDoctor is not null)
            throw new AlreadyExistException($"This doctor already exist with phone: {dto.Phone}");

        string accountNumber = GenerateAccountNumber();

        if (dto.Image is not null)
        {
            dto.Image = await this.assetService.UploadAsync(dto.Image);
        }

        var mappedEmployee = this.mapper.Map<Employee>(dto);
        await this.doctorRepository.CreateAsync(mappedEmployee);
        await this.doctorRepository.SaveChanges();

        return this.mapper.Map<EmployeeResultDto>(mappedEmployee);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This doctor not found with id: {id}");

        this.doctorRepository.Delete(existDoctor);
        await this.doctorRepository.SaveChanges();

        return true;
    }
    public async Task<EmployeeResultDto> UpdateAsync(EmployeeUpdateDto dto)
    {
        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This doctor not found with id: {dto.Id}");

        if (dto.IsSelectImage)
        {
            dto.Image = await this.assetService.UploadAsync(dto.Image);
        }

        this.mapper.Map(dto, existDoctor);

        this.doctorRepository.Update(existDoctor);
        await this.doctorRepository.SaveChanges();

        return this.mapper.Map<EmployeeResultDto>(existDoctor);
    }
    public async Task<EmployeeResultDto> GetAsync(long id)
    {
        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This doctor not found with id: {id}");

        return this.mapper.Map<EmployeeResultDto>(existDoctor);
    }

    public async Task<IEnumerable<EmployeeResultDto>> GetAllAsync(PaginationParams @params, string search = null)
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

        return this.mapper.Map<IEnumerable<EmployeeResultDto>>(allDoctors);
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