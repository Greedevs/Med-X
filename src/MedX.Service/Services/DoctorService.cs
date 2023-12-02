using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Domain.Entities.Assets;
using MedX.Service.DTOs.Assets;
using MedX.Service.DTOs.Doctors;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class DoctorService : IDoctorService
{
    private readonly IRepository<Doctor> doctorRepository;
    private readonly IAssetService assetService;
    private readonly IMapper mapper;
    public DoctorService(IMapper mapper, IRepository<Doctor> doctorRepository, IAssetService assetService)
    {
        this.mapper = mapper;
        this.assetService = assetService;
        this.doctorRepository = doctorRepository;
    }
    public async Task<DoctorResultDto> AddAsync(DoctorCreationDto dto)
    {
        var existDoctor = await this.doctorRepository.GetAsync(d => d.Phone.Equals(dto.Phone));
        if (existDoctor is not null)
            throw new AlreadyExistException($"This doctor already exist with phone: {dto.Phone}");

        string accountNumber = GenerateAccountNumber();
        var mappedDoctor = new Doctor
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Patronymic = dto.Patronymic,
            Email = dto.Email,
            Phone = dto.Phone,
            Professional = dto.Professional,
            Balance = dto.Balance,
            AccountNumber = accountNumber,
        };

        if (dto.Image is not null)
        {
            var uploadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image });
            var createdImage = new Asset
            {
                FileName = uploadedImage.FileName,
                FilePath = uploadedImage.FilePath,
            };

            mappedDoctor.ImageId = uploadedImage.Id;
            mappedDoctor.Image = createdImage;
        }

        await this.doctorRepository.CreateAsync(mappedDoctor);
        await this.doctorRepository.SaveChanges();

        return this.mapper.Map<DoctorResultDto>(mappedDoctor);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This doctor not found with id: {id}");

        this.doctorRepository.Delete(existDoctor);
        await this.assetService.RemoveAsync(existDoctor.Image);
        await this.doctorRepository.SaveChanges();

        return true;
    }
    public async Task<DoctorResultDto> UpdateAsync(DoctorUpdateDto dto)
    {
        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This doctor not found with id: {dto.Id}");

        var uploadedImage = new Asset();
        if (dto.Image is not null)
        {
            uploadedImage = await this.assetService.UploadAsync(new AssetCreationDto { FormFile = dto.Image });
            await this.assetService.RemoveAsync(existDoctor.Image);
        }

        existDoctor.FirstName = dto.FirstName;
        existDoctor.LastName = dto.LastName;
        existDoctor.Patronymic = dto.Patronymic;
        existDoctor.Email = dto.Email;
        existDoctor.Phone = dto.Phone;
        existDoctor.Professional = dto.Professional;
        existDoctor.Balance = dto.Balance;

        if (uploadedImage.Id > 0)
        {
            if (existDoctor.Image == null)
            {
                existDoctor.Image = new Asset();
            }
            existDoctor.ImageId = uploadedImage.Id;
            existDoctor.Image.FileName = uploadedImage.FileName;
            existDoctor.Image.FilePath = uploadedImage.FilePath;
        }

        this.doctorRepository.Update(existDoctor);
        await this.doctorRepository.SaveChanges();

        return this.mapper.Map<DoctorResultDto>(existDoctor);
    }
    public async Task<DoctorResultDto> GetAsync(long id)
    {
        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == id, includes: new[] { "Image" })
            ?? throw new NotFoundException($"This doctor not found with id: {id}");

        return this.mapper.Map<DoctorResultDto>(existDoctor);
    }

    public async Task<IEnumerable<DoctorResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allDoctors = await this.doctorRepository.GetAll(includes: new[] { "Image" })
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

    private string GenerateAccountNumber()
    {
        Random random = new Random();
        int accountNumberLength = 9;
        string accountNumber = random.Next((int)Math.Pow(10,
        accountNumberLength - 1), (int)Math.Pow(10, accountNumberLength)).ToString();

        return accountNumber;
    }
}