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
    private readonly IRepository<CashDesk> cashDeskRepository;
    private readonly IRepository<Payment> paymentRepository;
    private readonly IMapper mapper;
    public CashDeskService(IMapper mapper,
        IRepository<CashDesk> cashDeskRepository,
        IRepository<Payment> paymentRepository)
    {
        this.mapper = mapper;
        this.paymentRepository = paymentRepository;
        this.cashDeskRepository = cashDeskRepository;
    }
    public async Task<CashDeskResultDto> AddAsync(CashDeskCreationDto dto)
    {
        var payment = await paymentRepository.GetAsync(p => p.Id.Equals(dto.PaymentId))
            ?? throw new NotFoundException("This payment is not found!");

        var cashDesk = this.mapper.Map<CashDesk>(dto);

        await this.cashDeskRepository.CreateAsync(cashDesk);
        await this.cashDeskRepository.SaveChanges();

        return this.mapper.Map<CashDeskResultDto>(cashDesk);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existCashDesk = await this.cashDeskRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This CashDesk not found with id: {id}");

        this.cashDeskRepository.Delete(existCashDesk);
        await this.cashDeskRepository.SaveChanges();

        return true;
    }

    public async Task<CashDeskResultDto> UpdateAsync(CashDeskUpdateDto dto)
    {
        var existCashDesk = await this.cashDeskRepository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This CashDesk not found with id: {dto.Id}");

        this.mapper.Map(dto, existCashDesk);

        this.cashDeskRepository.Update(existCashDesk);
        await this.cashDeskRepository.SaveChanges();

        return this.mapper.Map<CashDeskResultDto>(existCashDesk);
    }

    public async Task<CashDeskResultDto> GetAsync(long id)
    {
        var existCashDesk = await this.cashDeskRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This CashDesk not found with id: {id}");

        return this.mapper.Map<CashDeskResultDto>(existCashDesk);
    }

    public async Task<IEnumerable<CashDeskResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allCashDesks = await this.cashDeskRepository.GetAll()
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            search = search.Replace("+", "%2B");
            allCashDesks = allCashDesks.Where(d => d.AccountNumber.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Description.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<CashDeskResultDto>>(allCashDesks);
    }

    public async Task<CashDesk> GetLastCashDeskAsync()
    {
        var result = await cashDeskRepository.GetAll()
            .OrderByDescending(c => c.Id)
            .FirstOrDefaultAsync();

        return result;
    }
}