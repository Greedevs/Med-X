using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Service.DTOs.CashDesks;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class CashDeskService : ICashDeskService
{
    private readonly IRepository<CashDesk> CashDeskRepository;
    private readonly IMapper mapper;
    public CashDeskService(IMapper mapper, IRepository<CashDesk> CashDeskRepository)
    {
        this.mapper = mapper;
        this.CashDeskRepository = CashDeskRepository;
    }
    public async Task<CashDeskResultDto> AddAsync(CashDeskCreationDto dto)
    {
        var mappedCashDesk = this.mapper.Map<CashDesk>(dto);

        await this.CashDeskRepository.CreateAsync(mappedCashDesk);
        await this.CashDeskRepository.SaveChanges();

        return this.mapper.Map<CashDeskResultDto>(mappedCashDesk);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existCashDesk = await this.CashDeskRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This CashDesk not found with id: {id}");

        this.CashDeskRepository.Delete(existCashDesk);
        await this.CashDeskRepository.SaveChanges();

        return true;
    }
    public async Task<CashDeskResultDto> UpdateAsync(CashDeskUpdateDto dto)
    {
        var existCashDesk = await this.CashDeskRepository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This CashDesk not found with id: {dto.Id}");

        this.mapper.Map(dto, existCashDesk);

        this.CashDeskRepository.Update(existCashDesk);
        await this.CashDeskRepository.SaveChanges();

        return this.mapper.Map<CashDeskResultDto>(existCashDesk);
    }
    public async Task<CashDeskResultDto> GetAsync(long id)
    {
        var existCashDesk = await this.CashDeskRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This CashDesk not found with id: {id}");

        return this.mapper.Map<CashDeskResultDto>(existCashDesk);
    }

    public async Task<IEnumerable<CashDeskResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allCashDesks = await this.CashDeskRepository.GetAll()
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            allCashDesks = allCashDesks.Where(d => d.AccountNumber.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Description.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<CashDeskResultDto>>(allCashDesks);
    }
}