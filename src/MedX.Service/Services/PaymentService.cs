using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Service.DTOs.Patients;
using MedX.Service.DTOs.Payments;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;

namespace MedX.Service.Services;

public class PaymentService : IPaymentService 
{
    private readonly IMapper mapper;
    private readonly IRepository<Payment> repository;

    public PaymentService(IMapper mapper, IRepository<Payment> repository, IRepository<Appointment> appointmentRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<PaymentResultDto> AddAsync(PaymentCreationDto dto)
    {
        Payment payment = this.mapper.Map<Payment>(dto);
        await this.repository.CreateAsync(payment);
        await this.repository.SaveChanges();

        return this.mapper.Map<PaymentResultDto>(payment);
    }

    public async Task<PaymentResultDto> UpdateAsync(PaymentUpdateDto dto)
    {
        Payment payment = this.repository.GetAll().FirstOrDefault(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This id is not found {dto.Id}");

        Payment mappedPayment = this.mapper.Map(dto, payment);
        this.repository.Update(mappedPayment);
        await this.repository.SaveChanges();

        return this.mapper.Map<PaymentResultDto>(mappedPayment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        Payment payment = this.repository.GetAll().FirstOrDefault(x => x.Id.Equals(id))
            ?? throw new NotFoundException($"This id is not found {id}");

        this.repository.Delete(payment);
        await this.repository.SaveChanges();
        return true;
    }

    public async Task<PaymentResultDto> GetAsync(long id)
    {
        Payment payment = this.repository.GetAll().FirstOrDefault(x => x.Id.Equals(id))
            ?? throw new NotFoundException($"This id is not found {id}");

        return this.mapper.Map<PaymentResultDto>(payment);
    }

    public async Task<IEnumerable<PaymentResultDto>> GetAllAsync(PaginationParams @params)
    {
        var payments = this.repository.GetAll()
            .ToPaginate(@params)
            .ToList();

        return this.mapper.Map<IEnumerable<PaymentResultDto>>(payments);
    }

    public async Task<IEnumerable<PaymentResultDto>> SearchByQuery(decimal query)
    {
        var results = this.repository.GetAll()
        .Where(d => d.Amount==query).ToList();

        if (results.Count()>0)
            return this.mapper.Map<IEnumerable<PaymentResultDto>>(results);
        return null;
    }
}
