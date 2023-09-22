using MedX.Domain.Configurations;
using MedX.Service.DTOs.Payments;
using MedX.Service.Interfaces;

namespace MedX.Service.Services;

public class PaymentService : IPaymentService
{
    public Task<PaymentResultDto> AddAsync(PaymentCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PaymentResultDto>> GetAllAsync(PaginationParams @params, decimal search = 0)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentResultDto> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentResultDto> UpdateAsync(PaymentUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
