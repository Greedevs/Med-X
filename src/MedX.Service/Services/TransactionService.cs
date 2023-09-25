    using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Service.DTOs.Transactions;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class TransactionService : ITransactionService
{
    private readonly IRepository<Payment> paymentRepository;
    private readonly IRepository<Doctor> doctorRepository;
    private readonly IRepository<Patient> patientRepository;
    private readonly IRepository<Transaction> transactionRepository;
    private readonly IMapper mapper;
    public TransactionService(IMapper mapper,
        IRepository<Doctor> doctorRepository,
        IRepository<Patient> patientRepository,
        IRepository<Transaction> transactionRepository,
        IRepository<Payment> paymentRepository)
    {
        this.mapper = mapper;
        this.paymentRepository = paymentRepository;
        this.doctorRepository = doctorRepository;
        this.patientRepository = patientRepository;
        this.transactionRepository = transactionRepository;
        this.paymentRepository = paymentRepository;
    }
    public async Task<TransactionResultDto> AddAsync(TransactionCreationDto dto)
    {
        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
            ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == dto.DoctorId)
            ?? throw new NotFoundException($"This Doctor not found with id: {dto.DoctorId}");

        var existPayment = await this.paymentRepository.GetAsync(r => r.Id == dto.PaymentId)
            ?? throw new NotFoundException($"This payment not found with id: {dto.PaymentId}");

        var mappedTransaction = this.mapper.Map<Transaction>(dto);
        mappedTransaction.Doctor = existDoctor;
        mappedTransaction.Payment = existPayment;
        mappedTransaction.Patient = existPatient;

        await this.transactionRepository.CreateAsync(mappedTransaction);
        await this.transactionRepository.SaveChanges();

        return this.mapper.Map<TransactionResultDto>(mappedTransaction);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existTransaction = await this.transactionRepository.GetAsync(r => r.Id == id)
            ?? throw new NotFoundException($"This Transaction not found with id: {id}");

        this.transactionRepository.Delete(existTransaction);
        await this.transactionRepository.SaveChanges();

        return true;
    }
    public async Task<TransactionResultDto> UpdateAsync(TransactionUpdateDto dto)
    {
        var existTransaction = await this.transactionRepository.GetAsync(r => r.Id == dto.Id)
            ?? throw new NotFoundException($"This Transaction not found with id: {dto.Id}");

        this.mapper.Map(dto, existTransaction);
        this.transactionRepository.Update(existTransaction);
        await this.transactionRepository.SaveChanges();

        return this.mapper.Map<TransactionResultDto>(existTransaction);
    }
    public async Task<TransactionResultDto> GetAsync(long id)
    {
        var existTransaction = await this.transactionRepository.GetAsync(r => r.Id == id, includes: new[] { "Doctor", "Patient", "Room" })
            ?? throw new NotFoundException($"This Transaction not found with id: {id}");

        return this.mapper.Map<TransactionResultDto>(existTransaction);
    }

    public async Task<IEnumerable<TransactionResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allTransactions = await this.transactionRepository.GetAll(includes: new[] { "Doctor", "Patient", "Payment" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            allTransactions = allTransactions
                .Where(d => d.Doctor.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Doctor.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Doctor.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Payment.Amount.Equals(search)).ToList();
        }

        return this.mapper.Map<IEnumerable<TransactionResultDto>>(allTransactions);
    }
}
