using MedX.Domain.Configurations;
using MedX.Service.DTOs.Payments;

namespace MedX.Service.Interfaces;

public interface IPaymentService
{
    Task<PaymentResultDto> AddAsync(PaymentCreationDto dto);
    Task<PaymentResultDto> UpdateAsync(PaymentUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<PaymentResultDto> GetAsync(long id);
    Task<IEnumerable<PaymentResultDto>> GetAllAsync(PaginationParams @params, decimal search = 0);
}
