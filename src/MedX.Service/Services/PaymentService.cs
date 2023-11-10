using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Service.DTOs.Payments;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using MedX.Service.DTOs.CashDesks;

namespace MedX.Service.Services;

public class PaymentService : IPaymentService
{
    private readonly IMapper mapper;
    private readonly IRepository<Payment> repository;
    private readonly IRepository<Patient> patientRepository;
    private readonly ICashDeskService cashDeskService;

    public PaymentService(IMapper mapper, IRepository<Payment> repository,
                          IRepository<Patient> patientRepository, 
                          ICashDeskService cashDeskService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.cashDeskService = cashDeskService;
        this.patientRepository = patientRepository;
    }

    public async Task<PaymentResultDto> AddAsync(PaymentCreationDto dto)
    {
        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
           ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        Payment payment = this.mapper.Map<Payment>(dto);
        payment.Patient = existPatient;

        await this.repository.CreateAsync(payment);
        await this.repository.SaveChanges();

        var cashDesk = await cashDeskService.GetLastCashDeskAsync();
        if (cashDesk == null)
            cashDesk = new CashDesk();
        
        cashDesk.Balance += dto.Amount;
        cashDesk.PaymentId = payment.Id;
        cashDesk.Description = dto.Description;
        var createCash = mapper.Map<CashDeskCreationDto>(cashDesk);

        await cashDeskService.AddAsync(createCash);

        existPatient.Balance += dto.Amount;
        this.patientRepository.Update(existPatient);
        await this.patientRepository.SaveChanges();

        return this.mapper.Map<PaymentResultDto>(payment);
    }

    public async Task<PaymentResultDto> UpdateAsync(PaymentUpdateDto dto)
    {
        Payment payment = await this.repository.GetAsync(p => p.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This id is not found {dto.Id}");

        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
           ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        decimal oldAmount = payment.Amount;
        Payment mappedPayment = this.mapper.Map(dto, payment);
        payment.Patient = existPatient;
        this.repository.Update(mappedPayment);
        await this.repository.SaveChanges();

        if (oldAmount != dto.Amount)
        {
            var cashDesk = await cashDeskService.GetLastCashDeskAsync();
            if (cashDesk == null)
                cashDesk = new CashDesk();

            decimal amountDifference = dto.Amount - oldAmount;
            cashDesk.Balance += amountDifference;
            var cashDeskDto = new CashDeskCreationDto
            {
                Amount = amountDifference,
                Description = dto.Description,
                IsIncome = amountDifference > 0
            };
            await cashDeskService.AddAsync(cashDeskDto);
        }

        return this.mapper.Map<PaymentResultDto>(mappedPayment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        Payment payment = await this.repository.GetAsync(p => p.Id.Equals(id))
            ?? throw new NotFoundException($"This id is not found {id}");

        this.repository.Delete(payment);
        await this.repository.SaveChanges();
        await this.cashDeskService.DeleteByPaymentIdAsync(id);

        return true;
    }

    public async Task<PaymentResultDto> GetAsync(long id)
    {
        Payment payment = await this.repository.GetAsync(p => p.Id.Equals(id), includes: new[] { "Patient" })
            ?? throw new NotFoundException($"This id is not found {id}");

        return this.mapper.Map<PaymentResultDto>(payment);
    }

    public async Task<IEnumerable<PaymentResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var payments = await this.repository.GetAll(includes: new[] { "Patient" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            payments = payments.Where(d => d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
            || d.Amount.ToString().Equals(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<PaymentResultDto>>(payments);
    }

    public async Task<IEnumerable<PaymentResultDto>> GetAllByPatientIdAsync(long patientId, string search = null)
    {
        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(patientId))
          ?? throw new NotFoundException($"This Patient not found with id: {patientId}");

        var payments = await this.repository.GetAll(p => p.PatientId == patientId).ToListAsync();

        if (search is not null)
        {
            payments = payments.Where(d => d.Amount.ToString().Equals(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return this.mapper.Map<IEnumerable<PaymentResultDto>>(payments);
    }
}
