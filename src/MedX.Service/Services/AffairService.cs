using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities.Services;
using MedX.Service.DTOs.Services;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class AffairService : IAffairService
{
    private readonly IRepository<Affair> affairRepository;
    private readonly IMapper mapper;
    public AffairService(IMapper mapper, IRepository<Affair> affairRepository)
    {
        this.mapper = mapper;
        this.affairRepository = affairRepository;
    }
    public async Task<AffairResultDto> AddAsync(AffairCreationDto dto)
    {
        var mappedAffair = this.mapper.Map<Affair>(dto);

        await this.affairRepository.CreateAsync(mappedAffair);
        await this.affairRepository.SaveChanges();

        return this.mapper.Map<AffairResultDto>(mappedAffair);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAffair = await this.affairRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This Affair not found with id: {id}");

        this.affairRepository.Delete(existAffair);
        await this.affairRepository.SaveChanges();

        return true;
    }
    public async Task<AffairResultDto> UpdateAsync(AffairUpdateDto dto)
    {
        var existAffair = await this.affairRepository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This Affair not found with id: {dto.Id}");

        this.mapper.Map(dto, existAffair);

        this.affairRepository.Update(existAffair);
        await this.affairRepository.SaveChanges();

        return this.mapper.Map<AffairResultDto>(existAffair);
    }
    public async Task<AffairResultDto> GetAsync(long id)
    {
        var existAffair = await this.affairRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This Affair not found with id: {id}");

        return this.mapper.Map<AffairResultDto>(existAffair);
    }

    public async Task<IEnumerable<AffairResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allAffairs = await this.affairRepository.GetAll()
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            search = search.Replace("+", "%2B");
            allAffairs = allAffairs.Where(d => d.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Price.ToString().Equals(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<AffairResultDto>>(allAffairs);
    }
}