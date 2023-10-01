using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Entities;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using MedX.Domain.Configurations;
using MedX.Service.DTOs.Payments;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class PaymentService : IPaymentService
{
    private readonly IMapper mapper;
    private readonly IRepository<Payment> repository;
    private readonly IRepository<Patient> patientRepository;

    public PaymentService(IMapper mapper, IRepository<Payment> repository,
                          IRepository<Patient> patientRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
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

        return this.mapper.Map<PaymentResultDto>(payment);
    }

    public async Task<PaymentResultDto> UpdateAsync(PaymentUpdateDto dto)
    {
        Payment payment = await this.repository.GetAsync(p => p.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This id is not found {dto.Id}");

        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
           ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        Payment mappedPayment = this.mapper.Map(dto, payment);
        payment.Patient = existPatient;
        this.repository.Update(mappedPayment);
        await this.repository.SaveChanges();

        return this.mapper.Map<PaymentResultDto>(mappedPayment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        Payment payment = await this.repository.GetAsync(p => p.Id.Equals(id))
            ?? throw new NotFoundException($"This id is not found {id}");

        this.repository.Delete(payment);
        await this.repository.SaveChanges();
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
